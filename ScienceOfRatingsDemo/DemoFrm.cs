using RatingAnalysisLib;
using RatingAnalysisLib.AverageAggregators;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ScienceOfRatingsDemo
{
    public partial class DemoFrm : Form
    {
        #region average equations params
        public WeightGenerationMethod SelectedWeightMethod { get; set; } = WeightGenerationMethod.Random;

        // Bayesian Average Parameters
        public double BayesianPrior { get; set; } = 7.5;  // Industry-wide default
        public int BayesianM { get; set; } = 10;         // Assumed prior review count

        // Truncated Mean Parameters
        public double TrimPercent { get; set; } = 0.1;  // 10% Trimmed

        // Exponential Moving Average
        public double EmaAlpha { get; set; } = 0.2;  // Weight of recent reviews

        // Wilson Score
        public double WilsonConfidence { get; set; } = 1.96;  // 95% Confidence

        // Recency Adjustment
        public double RecencyLambda { get; set; } = 0.02;  // Decay rate 
        #endregion

        public DemoFrm()
        {
            InitializeComponent();
            InitializeUIControls();
        }

        private void InitializeUIControls()
        {
            // ComboBox for Weight Generation Method
            weightMethodComboBox.DataSource = Enum.GetValues(typeof(WeightGenerationMethod));
            weightMethodComboBox.SelectedIndexChanged += (s, e) =>
            {
                SelectedWeightMethod = (WeightGenerationMethod)weightMethodComboBox.SelectedItem;
            };

            // Bayesian Prior Controls
            bayesianPriorNumeric.Minimum = 1;
            bayesianPriorNumeric.Maximum = 10;
            bayesianPriorNumeric.DecimalPlaces = 1;
            bayesianPriorNumeric.Value = (decimal)BayesianPrior;
            bayesianPriorNumeric.ValueChanged += (s, e) =>
            {
                BayesianPrior = (double)bayesianPriorNumeric.Value;
            };

            bayesianMNumeric.Minimum = 1;
            bayesianMNumeric.Maximum = 100;
            bayesianMNumeric.Value = BayesianM;
            bayesianMNumeric.ValueChanged += (s, e) =>
            {
                BayesianM = (int)bayesianMNumeric.Value;
            };

            // Truncated Mean Controls
            trimPercentNumeric.Minimum = 0;
            trimPercentNumeric.Maximum = 0.5M; // Trims up to 50%
            trimPercentNumeric.Increment = 0.01M;
            trimPercentNumeric.DecimalPlaces = 2;
            trimPercentNumeric.Value = (decimal)TrimPercent;
            trimPercentNumeric.ValueChanged += (s, e) =>
            {
                TrimPercent = (double)trimPercentNumeric.Value;
            };

            // Exponential Moving Average Controls
            emaAlphaNumeric.Minimum = 0.01M;
            emaAlphaNumeric.Maximum = 1.0M;
            emaAlphaNumeric.Increment = 0.01M;
            emaAlphaNumeric.DecimalPlaces = 2;
            emaAlphaNumeric.Value = (decimal)EmaAlpha;
            emaAlphaNumeric.ValueChanged += (s, e) =>
            {
                EmaAlpha = (double)emaAlphaNumeric.Value;
            };

            // Wilson Score Interval Controls
            wilsonConfidenceNumeric.Minimum = 0.5M;
            wilsonConfidenceNumeric.Maximum = 3.0M;
            wilsonConfidenceNumeric.Increment = 0.1M;
            wilsonConfidenceNumeric.DecimalPlaces = 2;
            wilsonConfidenceNumeric.Value = (decimal)WilsonConfidence;
            wilsonConfidenceNumeric.ValueChanged += (s, e) =>
            {
                WilsonConfidence = (double)wilsonConfidenceNumeric.Value;
            };

            // Recency Decay Control
            recencyLambdaNumeric.Minimum = 0.001M;
            recencyLambdaNumeric.Maximum = 0.1M;
            recencyLambdaNumeric.Increment = 0.001M;
            recencyLambdaNumeric.DecimalPlaces = 3;
            recencyLambdaNumeric.Value = (decimal)RecencyLambda;
            recencyLambdaNumeric.ValueChanged += (s, e) =>
            {
                RecencyLambda = (double)recencyLambdaNumeric.Value;
            };
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            startTimePicker.Value = DateTime.Now.AddYears(-1);
            endTimePicker.Value = DateTime.Now;

            InitializeDataGridView();

            this.Resize += (s, ea) => chartPanel.Invalidate(); // Forces redraw on resize
        }

        private void InitializeDataGridView()
        {
            pointsDgv.Columns.Clear(); // Remove any existing columns

            // Add timestamp column
            pointsDgv.Columns.Add("Timestamp", "Timestamp");
            pointsDgv.Columns["Timestamp"].ValueType = typeof(DateTime);
            pointsDgv.Columns["Timestamp"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Fill available space

            // Add rating column (fixed width)
            pointsDgv.Columns.Add("Rating", "Rating");
            pointsDgv.Columns["Rating"].ValueType = typeof(double);
            pointsDgv.Columns["Rating"].Width = 80; // Set fixed width
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                RatingTrend trend = GetTrendFromButton(button);

                // Get user-defined start and end time
                DateTime startTime = startTimePicker.Value;
                DateTime endTime = endTimePicker.Value;

                // Ensure start time is before end time
                if (startTime >= endTime)
                {
                    MessageBox.Show("Start time must be before end time.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the number of data points
                int dataPoints = (int)dataPointsNumeric.Value;

                // Calculate the interval dynamically
                TimeSpan interval = TimeSpan.FromTicks((endTime - startTime).Ticks / (dataPoints - 1));


                // Generate data points
                var ratings = RatingDataGenerator.GenerateRatings(trend, startTime, interval, dataPoints);

                // Display in DataGridView
                DisplayDataPoints(ratings);
            }
        }

        private void DisplayDataPoints(List<RatingDataPoint> ratings)
        {
            pointsDgv.Rows.Clear();
            foreach (var point in ratings)
            {
                pointsDgv.Rows.Add(point.Timestamp, point.Rating);
            }
        }

        private RatingTrend GetTrendFromButton(Button button)
        {
            switch (button.Name)
            {
                case "button1":
                    return RatingTrend.SteadyGood;
                case "button2":
                    return RatingTrend.SteadyBad;
                case "button3":
                    return RatingTrend.GoodWentBad;
                case "button4":
                    return RatingTrend.BadImproved;
                case "button5":
                    return RatingTrend.ExtremeSwings;
                case "button6":
                    return RatingTrend.FakeGood;
                case "button7":
                    return RatingTrend.FakeBad;
                case "button8":
                    return RatingTrend.GradualOscillation;
                case "button9":
                    return RatingTrend.SuddenDropRecovery;
                case "button10":
                    return RatingTrend.NewBusinessEffect;
                default:
                    throw new ArgumentException("Invalid button");
            }
        }

        private void PlotRatings(List<RatingDataPoint> ratings)
        {
            // TO BE IMPLEMENTED (Panel details needed)
        }

        private void visualizationPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle bounds = chartPanel.ClientRectangle;

            // Define averaging methods and their colors
            var methodColors = new Dictionary<AveragingMethod, Color>
            {
                { AveragingMethod.SimpleAverage, Color.Green },
                { AveragingMethod.WeightedAverage, Color.Red },
                { AveragingMethod.BayesianAverage, Color.Orange },
                { AveragingMethod.MedianScore, Color.Blue },
                { AveragingMethod.TruncatedMean, Color.Purple },
                { AveragingMethod.ExponentialMovingAverage, Color.Cyan },
                { AveragingMethod.WilsonScoreInterval, Color.Magenta },
                { AveragingMethod.GiniBasedReviewSpread, Color.Brown },
                { AveragingMethod.RecencyAdjustedRating, Color.DarkBlue },
                { AveragingMethod.GeometricMean, Color.Gray }
            };

            // Use selected start/end time to scale X-axis properly
            DateTime start = startTimePicker.Value;
            DateTime end = endTimePicker.Value;
            int gridSpacing = 15;

            // Draw base chart components
            ChartRenderer.DrawGrid(e.Graphics, bounds, gridSpacing);
            ChartRenderer.DrawAxis(e.Graphics, bounds);
            ChartRenderer.DrawLabelsAndTicks(e.Graphics, bounds, start, end);

            // Example data (Replace with real rating data)
            //List<(List<RatingDataPoint>, AveragingMethod)> sampleData = new List<(List<RatingDataPoint>, AveragingMethod)>
            //{
            //    (new List<RatingDataPoint>
            //    {
            //        new RatingDataPoint(start.AddMonths(0), 8.5, 0),
            //        new RatingDataPoint(start.AddMonths(1), 7.8, 2),
            //        new RatingDataPoint(start.AddMonths(2), 6.9, 4),
            //        new RatingDataPoint(start.AddMonths(3), 2.4, 5),
            //        new RatingDataPoint(start.AddMonths(3), 7.4, 6),
            //        new RatingDataPoint(start.AddMonths(4), 7.4, 8),
            //        new RatingDataPoint(start.AddMonths(5), 7.4, 10),
            //    }, AveragingMethod.SimpleAverage)
            //};

            // Pass correct time scaling parameters to `DrawDataPoints`
            ChartRenderer.DrawDataPoints(e.Graphics, bounds, start, end, null, methodColors);

            // Pass correct legend parameters
            ChartRenderer.DrawLegend(e.Graphics, bounds, methodColors.ToDictionary(k => k.Key.ToString(), v => v.Value));
        }




    }



    public static class ChartRenderer
    {
        private static readonly Font AxisFont = new Font("Arial", 8);
        private static readonly Font LegendFont = new Font("Arial", 9, FontStyle.Bold);
        private static readonly Font AxisTitleFont = new Font("Arial", 10, FontStyle.Bold);
        private static readonly Pen GridPen = new Pen(Color.LightGray, 1);
        private static readonly Pen AxisPen = new Pen(Color.Black, 2);
        private static readonly Brush TextBrush = Brushes.Black;
        private static readonly Pen ArrowPen = new Pen(Color.Black, 3); // For axis arrows

        private const int AxisPaddingX = 30;  // Increased horizontal padding
        private const int AxisPaddingY = 40;  // Increased vertical padding
        private const int MarginRight = 50;  // Space for legend & rightmost labels
        private const int MarginTop = 20;    // Space for topmost labels

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

        public static void DrawDataPoints(Graphics g, Rectangle bounds, DateTime startTime, DateTime endTime,
            List<(List<RatingDataPoint>, AveragingMethod)> dataSeries, Dictionary<AveragingMethod, Color> methodColors)
        {
            if (dataSeries == null || dataSeries.Count == 0)
                return;

            double totalDuration = (endTime - startTime).TotalSeconds; // X-axis range

            // Find min/max weight for normalization
            double minWeight = dataSeries.SelectMany(series => series.Item1).Min(p => p.Weight);
            double maxWeight = dataSeries.SelectMany(series => series.Item1).Max(p => p.Weight);

            // Define min/max sizes for rating points
            double minSize = 4.0;
            double maxSize = 14.0;

            // Define available drawing space for X-axis
            float drawableWidth = bounds.Width - AxisPaddingX - MarginRight;

            // Store calculated trend points separately
            Dictionary<AveragingMethod, List<Point>> trendLines = new Dictionary<AveragingMethod, List<Point>>();

            // Draw actual ratings as BLACK circles (original data)
            foreach (var series in dataSeries)
            {
                List<RatingDataPoint> points = series.Item1;

                foreach (var point in points)
                {
                    // Normalize weight scaling
                    double normalizedWeight = (point.Weight - minWeight) / (maxWeight - minWeight);
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

            // Draw calculated averages as **smooth curves**, clipped to the grid
            foreach (var series in dataSeries)
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
