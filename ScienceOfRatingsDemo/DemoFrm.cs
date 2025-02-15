using RatingAnalysisLib;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ScienceOfRatingsDemo
{
    public partial class DemoFrm : Form
    {
        public DemoFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            startTimePicker.Value = DateTime.Now.AddYears(-1);
            endTimePicker.Value = DateTime.Now;

            InitializeDataGridView();
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

            // Example trend colors
            var trendColors = new Dictionary<RatingTrend, Color>
            {
                { RatingTrend.SteadyGood, Color.Green },
                { RatingTrend.SteadyBad, Color.Red },
                { RatingTrend.GoodWentBad, Color.Orange },
                { RatingTrend.BadImproved, Color.Blue },
                { RatingTrend.ExtremeSwings, Color.Purple },
                { RatingTrend.FakeGood, Color.LightGreen },
                { RatingTrend.FakeBad, Color.LightCoral },
                { RatingTrend.GradualOscillation, Color.DarkCyan },
                { RatingTrend.SuddenDropRecovery, Color.Magenta },
                { RatingTrend.NewBusinessEffect, Color.Gray }
            };

            DateTime start = startTimePicker.Value;
            DateTime end = endTimePicker.Value;
            int gridSpacing = 15;

            ChartRenderer.DrawGrid(e.Graphics, bounds, gridSpacing);
            ChartRenderer.DrawAxis(e.Graphics, bounds);
            ChartRenderer.DrawLabelsAndTicks(e.Graphics, bounds, start, end);
            ChartRenderer.DrawLegend(e.Graphics, bounds, trendColors);

            // Example data (Replace with real rating data)
            List<(List<Point>, RatingTrend)> sampleData = new List<(List<Point>, RatingTrend)>
            {
                (new List<Point> { new Point(50, 200), new Point(100, 150), new Point(150, 100) }, RatingTrend.SteadyGood),
                (new List<Point> { new Point(50, 250), new Point(100, 300), new Point(150, 350) }, RatingTrend.BadImproved),
            };

            ChartRenderer.DrawDataPoints(e.Graphics, bounds, sampleData, trendColors);
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

            int timeSteps = 8; // More ticks for better readability
            float xStep = (bounds.Width - AxisPaddingX - MarginRight) / (float)(timeSteps - 1);

            // X-axis labels and ticks (time)
            for (int i = 0; i < timeSteps; i++)
            {
                DateTime labelTime = start.AddTicks((end - start).Ticks * i / (timeSteps - 1));
                float x = bounds.Left + AxisPaddingX + i * xStep;
                g.DrawLine(AxisPen, x, xAxisY - 5, x, xAxisY + 5);
                g.DrawString(labelTime.ToShortTimeString(), AxisFont, TextBrush, x - 20, xAxisY + 8);
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

        public static void DrawLegend(Graphics g, Rectangle bounds, Dictionary<RatingTrend, Color> trendColors)
        {
            int legendWidth = (int)(138 * 1.15); // Increased by 15%
            int legendHeight = (trendColors.Count + 1) * 20 + 15; // +1 for actual ratings
            int legendX = bounds.Right - legendWidth - 10;
            int legendY = bounds.Top + 10;

            // Background
            g.FillRectangle(Brushes.White, legendX, legendY, legendWidth, legendHeight);
            g.DrawRectangle(AxisPen, legendX, legendY, legendWidth, legendHeight);

            int offsetY = legendY + 5;

            // Add actual ratings in black
            g.FillRectangle(Brushes.Black, legendX + 5, offsetY, 12, 12);
            g.DrawRectangle(Pens.Black, legendX + 5, offsetY, 12, 12);
            g.DrawString("Actual Ratings", LegendFont, TextBrush, legendX + 20, offsetY - 2);
            offsetY += 20;

            // Add calculated trends
            foreach (var entry in trendColors)
            {
                Brush colorBrush = new SolidBrush(entry.Value);
                g.FillRectangle(colorBrush, legendX + 5, offsetY, 12, 12);
                g.DrawRectangle(Pens.Black, legendX + 5, offsetY, 12, 12);
                g.DrawString(entry.Key.ToString(), LegendFont, TextBrush, legendX + 20, offsetY - 2);
                offsetY += 20;
            }
        }

        public static void DrawDataPoints(Graphics g, Rectangle bounds,
            List<(List<Point>, RatingTrend)> dataSeries, Dictionary<RatingTrend, Color> trendColors)
        {
            if (dataSeries.Count == 0)
                return;

            // Apply margins
            Rectangle adjustedBounds = new Rectangle(bounds.Left, bounds.Top + MarginTop,
                                                     bounds.Width - MarginRight, bounds.Height - MarginTop);

            // Draw original ratings in black (ONLY POINTS)
            foreach (var series in dataSeries)
            {
                List<Point> points = series.Item1;
                RatingTrend trend = series.Item2;

                Brush pointBrush = trend == RatingTrend.SteadyGood ? Brushes.Black : new SolidBrush(trendColors[trend]);

                foreach (var point in points)
                {
                    g.FillEllipse(pointBrush, point.X - 3, point.Y - 3, 6, 6);
                }
            }

            // Draw calculated averages as SMOOTH CURVES
            foreach (var series in dataSeries)
            {
                List<Point> points = series.Item1;
                RatingTrend trend = series.Item2;

                if (trend == RatingTrend.SteadyGood)
                    continue; // Don't draw a line for original ratings

                Pen trendPen = new Pen(trendColors[trend], 2);
                DrawSmoothCurve(g, trendPen, points);
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
