using RatingAnalysisLib;
using RatingAnalysisLib.AverageAggregators;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;



namespace ScienceOfRatingsDemo
{
    /// <summary>
    /// Provides utility methods for rendering various chart components, including
    /// grid lines, axes, labels, legends, and data points.
    /// </summary>
    public static class ChartRenderer
    {
        #region Properties and fields
        private static readonly Font AxisFont = new Font("Arial", 8);
        private static readonly Font LegendFont = new Font("Arial", 9, FontStyle.Bold);
        private static readonly Font AxisTitleFont = new Font("Arial", 10, FontStyle.Bold);
        private static readonly Pen GridPen = new Pen(Color.LightGray, 1);
        private static readonly Pen AxisPen = new Pen(Color.Black, 2);
        private static readonly Brush TextBrush = Brushes.Black;
        private static readonly Pen ArrowPen = new Pen(Color.Black, 3); // For axis arrows

        public const int AxisPaddingX = 30;  // Increased horizontal padding
        public const int AxisPaddingY = 40;  // Increased vertical padding
        public const int MarginRight = 50;  // Space for legend & rightmost labels
        public const int MarginTop = 20;    // Space for topmost labels 
        #endregion

        /// <summary>
        /// Draws a light gray grid within the chart panel to aid visualization.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="bounds">The rectangle area defining the chart panel.</param>
        /// <param name="gridSpacing">The spacing between grid lines.</param>
        public static void DrawGrid(Graphics g, Rectangle bounds, int gridSpacing)
        {
            Rectangle adjustedBounds = new Rectangle(bounds.Left + AxisPaddingX, bounds.Top + MarginTop,
                                                     bounds.Width - AxisPaddingX - MarginRight,
                                                     bounds.Height - AxisPaddingY - MarginTop);

            for (int x = adjustedBounds.Left; x < adjustedBounds.Right; x += gridSpacing)
            {
                g.DrawLine(GridPen, x, adjustedBounds.Top, x, adjustedBounds.Bottom);
            }

            for (int y = adjustedBounds.Top; y < adjustedBounds.Bottom; y += gridSpacing)
            {
                g.DrawLine(GridPen, adjustedBounds.Left, y, adjustedBounds.Right, y);
            }
        }

        /// <summary>
        /// Draws the X and Y axes, including arrows at their ends for directional indication.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="bounds">The rectangle area defining the chart panel.</param>
        public static void DrawAxis(Graphics g, Rectangle bounds)
        {
            int xAxisY = bounds.Bottom - AxisPaddingY;
            int yAxisX = bounds.Left + AxisPaddingX;

            // Draw X-axis (Shifted for better visibility)
            g.DrawLine(AxisPen, bounds.Left + AxisPaddingX, xAxisY, bounds.Right - MarginRight, xAxisY);

            // Draw Y-axis (Shifted for better visibility)
            g.DrawLine(AxisPen, yAxisX, bounds.Top + MarginTop, yAxisX, bounds.Bottom - AxisPaddingY);

            // Draw X-axis arrow
            g.DrawLine(ArrowPen, bounds.Right - MarginRight - 10, xAxisY - 3, bounds.Right - MarginRight, xAxisY);
            g.DrawLine(ArrowPen, bounds.Right - MarginRight - 10, xAxisY + 3, bounds.Right - MarginRight, xAxisY);

            // Draw Y-axis arrow
            g.DrawLine(ArrowPen, yAxisX - 3, bounds.Top + MarginTop + 10, yAxisX, bounds.Top + MarginTop);
            g.DrawLine(ArrowPen, yAxisX + 3, bounds.Top + MarginTop + 10, yAxisX, bounds.Top + MarginTop);
        }

        /// <summary>
        /// Draws labels and tick marks for both the X-axis (time) and Y-axis (rating scores).
        /// The number of X-axis ticks dynamically adjusts based on the chart panel width.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="bounds">The rectangle area defining the chart panel.</param>
        /// <param name="start">The starting DateTime for the X-axis.</param>
        /// <param name="end">The ending DateTime for the X-axis.</param>
        public static void DrawLabelsAndTicks(Graphics g, Rectangle bounds, DateTime start, DateTime end)
        {
            int xAxisY = bounds.Bottom - AxisPaddingY;
            int yAxisX = bounds.Left + AxisPaddingX;

            // Dynamically adjust number of time ticks based on panel width
            int timeSteps = Math.Max(5, bounds.Width / 100); // Ensure at least 5, adjust as needed
            float xStep = (bounds.Width - AxisPaddingX - MarginRight) / (float)(timeSteps - 1);

            // X-axis labels and ticks (time)
            for (int i = 0; i < timeSteps; i++)
            {
                DateTime labelTime = start.AddSeconds((end - start).TotalSeconds * i / (timeSteps - 1));
                float x = bounds.Left + AxisPaddingX + i * xStep;

                g.DrawLine(AxisPen, x, xAxisY - 5, x, xAxisY + 5);
                g.DrawString(labelTime.ToShortDateString(), AxisFont, TextBrush, x - 20, xAxisY + 8);
            }

            int scoreSteps = 11; // Number of Y-axis labels (0-10)
            float yStep = (bounds.Height - AxisPaddingY - MarginTop) / (float)(scoreSteps - 1);

            // Y-axis labels and ticks (scores)
            for (int i = 0; i <= scoreSteps; i++)
            {
                float y = bounds.Bottom - AxisPaddingY - i * yStep;
                g.DrawLine(AxisPen, yAxisX - 5, y, yAxisX + 5, y);
                g.DrawString(i.ToString(), AxisFont, TextBrush, yAxisX - 25, y - 5);
            }

            // Draw axis labels
            g.DrawString("Time", AxisTitleFont, TextBrush, bounds.Right - MarginRight - 30, xAxisY + 20);
            g.DrawString("Score", AxisTitleFont, TextBrush, yAxisX - 30, bounds.Top + MarginTop - 20);
        }

        /// <summary>
        /// Draws a legend box at the top-right corner of the chart to indicate the meaning
        /// of each colored trend line, along with the actual data points.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="bounds">The rectangle area defining the chart panel.</param>
        /// <param name="methodColors">A dictionary mapping method names to their respective colors.</param>
        public static void DrawLegend(Graphics g, Rectangle bounds, Dictionary<string, Color> methodColors)
        {
            // Function to insert spaces before capital letters in PascalCase/camelCase
            string FormatLegendText(string text) =>
                Regex.Replace(text, "(?<!^)([A-Z])", " $1"); // Adds a space before each capital letter except the first one

            // Determine the widest text in the legend dynamically
            int maxTextWidth = methodColors.Keys.Max(text => (int)g.MeasureString(FormatLegendText(text), LegendFont).Width);
            int legendWidth = maxTextWidth + 20; // Ensure padding around text
            int legendHeight = (methodColors.Count + 1) * 20 + 5; // +1 for actual data
            int legendX = bounds.Right - legendWidth - MarginRight; // Apply right margin
            int legendY = bounds.Top + MarginTop; // Apply top margin

            // Background
            g.FillRectangle(Brushes.White, legendX, legendY, legendWidth, legendHeight);
            g.DrawRectangle(AxisPen, legendX, legendY, legendWidth, legendHeight);

            int offsetY = legendY + 5;

            // Add "Actual Data" entry with black dot
            g.FillRectangle(Brushes.Black, legendX + 5, offsetY, 12, 12);
            g.DrawRectangle(Pens.Black, legendX + 5, offsetY, 12, 12);
            g.DrawString("Actual Data", LegendFont, TextBrush, legendX + 20, offsetY - 2);
            offsetY += 20;

            // Add averaging methods
            foreach (var entry in methodColors)
            {
                string formattedText = FormatLegendText(entry.Key); // Convert PascalCase to readable text
                Brush colorBrush = new SolidBrush(entry.Value);
                g.FillRectangle(colorBrush, legendX + 5, offsetY, 12, 12);
                g.DrawRectangle(Pens.Black, legendX + 5, offsetY, 12, 12);
                g.DrawString(formattedText, LegendFont, TextBrush, legendX + 20, offsetY - 2);
                offsetY += 20;
            }
        }

        /// <summary>
        /// Draws actual rating data points as weighted black circles.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="bounds">The rectangle area defining the chart panel.</param>
        /// <param name="startTime">The starting DateTime for X-axis scaling.</param>
        /// <param name="endTime">The ending DateTime for X-axis scaling.</param>
        /// <param name="rawDataPoints">The list of raw rating data points.</param>
        public static void DrawActualDataPoints(Graphics g, Rectangle bounds, DateTime startTime, DateTime endTime,
            List<RatingDataPoint> rawDataPoints)
        {
            if (rawDataPoints == null || rawDataPoints.Count == 0)
                return;

            double totalDuration = (endTime - startTime).TotalSeconds; // X-axis range

            // Find min/max weight for normalization
            double minWeight = rawDataPoints.Min(p => p.Weight);
            double maxWeight = rawDataPoints.Max(p => p.Weight);

            // Define min/max sizes for rating points
            double minSize = 4.0;
            double maxSize = 14.0;

            // If all weights are the same, avoid division by zero
            bool uniformWeight = Math.Abs(maxWeight - minWeight) < 0.0001;

            // Define available drawing space for X-axis
            float drawableWidth = bounds.Width - AxisPaddingX - MarginRight;

            // Draw actual ratings as BLACK circles (original data)
            foreach (var point in rawDataPoints)
            {
                // Normalize weight scaling (handle case when min == max)
                double normalizedWeight = uniformWeight ? 0.5 : (point.Weight - minWeight) / (maxWeight - minWeight);
                double size = minSize + (maxSize - minSize) * normalizedWeight;

                // Convert timestamp to X position (correct scaling)
                float x = bounds.Left + AxisPaddingX +
                          (float)((point.Timestamp - startTime).TotalSeconds / totalDuration * drawableWidth);
                float y = bounds.Bottom - (float)((point.Rating - 1) / 9.0 * bounds.Height); // Scale rating (1-10)

                // Strictly clip within bounds
                x = Math.Max(bounds.Left + AxisPaddingX, Math.Min(bounds.Right - MarginRight, x));
                y = Math.Max(bounds.Top + MarginTop, Math.Min(bounds.Bottom - AxisPaddingY, y));

                g.FillEllipse(Brushes.Black, x - (float)size / 2, y - (float)size / 2, (float)size, (float)size);
            }
        }



        /// <summary>
        /// Draws trend lines representing calculated averages using different averaging methods.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="bounds">The rectangle area defining the chart panel.</param>
        /// <param name="startTime">The starting DateTime for X-axis scaling.</param>
        /// <param name="endTime">The ending DateTime for X-axis scaling.</param>
        /// <param name="computedDataSeries">A list of tuples containing computed rating data points and the corresponding averaging method.</param>
        /// <param name="methodColors">A dictionary mapping averaging methods to their respective colors.</param>
        public static void DrawTrendLines(Graphics g, Rectangle bounds, DateTime startTime, DateTime endTime,
            List<(List<RatingDataPoint>, AveragingMethod)> computedDataSeries, Dictionary<AveragingMethod, Color> methodColors)
        {
            if (computedDataSeries == null || computedDataSeries.Count == 0)
                return;

            double totalDuration = (endTime - startTime).TotalSeconds; // X-axis range
            float drawableWidth = bounds.Width - AxisPaddingX - MarginRight;

            // Store calculated trend points separately
            Dictionary<AveragingMethod, List<Point>> trendLines = new Dictionary<AveragingMethod, List<Point>>();

            foreach (var series in computedDataSeries)
            {
                List<RatingDataPoint> points = series.Item1;
                AveragingMethod method = series.Item2;

                // Get color for the trend line
                Pen methodPen = new Pen(methodColors[method], 2);

                // Convert to screen points and strictly clip to grid
                List<Point> trendPoints = points
                    .Select(p => new Point(
                        (int)Math.Max(bounds.Left + AxisPaddingX,
                                      Math.Min(bounds.Right - MarginRight,
                                               bounds.Left + AxisPaddingX +
                                               (float)((p.Timestamp - startTime).TotalSeconds / totalDuration * drawableWidth))),
                        (int)Math.Max(bounds.Top + MarginTop,
                                      Math.Min(bounds.Bottom - AxisPaddingY,
                                               bounds.Bottom - (float)((p.Rating - 1) / 9.0 * bounds.Height)))
                    )).ToList();

                // Remove first and last points if they go out of bounds
                trendPoints = trendPoints.Where(p => p.X >= bounds.Left + AxisPaddingX && p.X <= bounds.Right - MarginRight).ToList();

                // Store trend points for drawing later
                if (trendPoints.Count > 1) // Avoid drawing invalid points
                    trendLines[method] = trendPoints;
            }

            // Now draw all trend lines (ensuring they don't extend past the grid)
            foreach (var method in trendLines.Keys)
            {
                Pen methodPen = new Pen(methodColors[method], 2);
                DrawSmoothCurve(g, methodPen, trendLines[method]);
            }
        }


        /// <summary>
        /// Draws a smooth interpolated curve connecting the given list of points.
        /// Uses cubic Bézier interpolation to create a smooth transition.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="pen">The pen used to draw the curve.</param>
        /// <param name="points">A list of points to be connected by the curve.</param>
        private static void DrawSmoothCurve(Graphics g, Pen pen, List<Point> points)
        {
            if (points.Count < 3)
            {
                // If not enough points for a curve, fallback to straight lines
                g.DrawLines(pen, points.ToArray());
                return;
            }

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddCurve(points.ToArray(), 0.5f);  // Adjust tension for smoothness
                g.DrawPath(pen, path);
            }
        }
    }


}
