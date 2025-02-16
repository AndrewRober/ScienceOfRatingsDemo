using RatingAnalysisLib;
using RatingAnalysisLib.AverageAggregators;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// Holds the current dataset of rating data points and their corresponding averaging method.
        /// Whenever this property is updated, the chart panel is invalidated to trigger a redraw.
        /// </summary>
        private List<(List<RatingDataPoint>, AveragingMethod)> _values;

        /// <summary>
        /// The dataset that will be plotted on the visualization panel.
        /// When set, it automatically refreshes the panel.
        /// </summary>
        public List<(List<RatingDataPoint>, AveragingMethod)> Values
        {
            get => _values;
            set
            {
                _values = value;
            }
        }

        private DateTime _mouseDownTime;

        Dictionary<AveragingMethod, Color> methodColors = new Dictionary<AveragingMethod, Color>
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

            chartPanel.MouseDown += chartPanel_MouseDown;
            chartPanel.MouseUp += chartPanel_MouseUp;
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

            // Add weight column
            pointsDgv.Columns.Add("Weight", "Weight");
            pointsDgv.Columns["Weight"].ValueType = typeof(double);
            pointsDgv.Columns["Weight"].Width = 80;
        }

        private void chartPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDownTime = DateTime.Now; // Start tracking time
        }

        private void chartPanel_MouseUp(object sender, MouseEventArgs e)
        {
            TimeSpan clickDuration = DateTime.Now - _mouseDownTime;
            double weight = Math.Max(1, clickDuration.TotalMilliseconds / 200); // Scale weight (min 1)

            AddDataPoint(e.Location, RatingDataGenerator.Clamp(Math.Round(weight, 1), 0, 10));
            chartPanel.Invalidate();
        }

        private void AddDataPoint(Point clickPosition, double weight)
        {
            if (Values == null)
                Values = new List<(List<RatingDataPoint>, AveragingMethod)>();

            Rectangle bounds = chartPanel.ClientRectangle;
            DateTime start = startTimePicker.Value;
            DateTime end = endTimePicker.Value;

            // Convert X position to DateTime
            double totalSeconds = (end - start).TotalSeconds;
            DateTime timestamp = start.AddSeconds(totalSeconds *
                (clickPosition.X - ChartRenderer.AxisPaddingX) / (bounds.Width - ChartRenderer.AxisPaddingX - ChartRenderer.MarginRight));

            // Convert Y position to rating (Y-axis is inverted)
            double rating = 10 -
                ((clickPosition.Y - ChartRenderer.MarginTop) / (double)(bounds.Height - ChartRenderer.AxisPaddingY - ChartRenderer.MarginTop) * 9.0);
            rating = Math.Round(Math.Max(1.0, Math.Min(10.0, rating)), 1); // Clamp to 1-10

            // Create a new rating data point
            var newPoint = new RatingDataPoint(timestamp, rating, weight);

            // Insert into the DataGridView in sorted order
            int insertIndex = pointsDgv.Rows.Cast<DataGridViewRow>()
                                .Where(row => row.Cells["Timestamp"].Value != null)
                                .TakeWhile(row => (DateTime)row.Cells["Timestamp"].Value < timestamp)
                                .Count();

            pointsDgv.Rows.Insert(insertIndex, timestamp, rating, weight);

            // Ensure Values list is also sorted
            if (!Values.Any(v => v.Item2 == AveragingMethod.SimpleAverage))
                Values.Add((new List<RatingDataPoint>(), AveragingMethod.SimpleAverage));

            var simpleAvgList = Values.First(v => v.Item2 == AveragingMethod.SimpleAverage).Item1;
            simpleAvgList.Add(newPoint);
            simpleAvgList.Sort((a, b) => a.Timestamp.CompareTo(b.Timestamp));

            // Redraw chart after adding data
            chartPanel.Invalidate();
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
                var ratings = RatingDataGenerator.GenerateRatings(trend, startTime, interval, dataPoints, SelectedWeightMethod);

                // Display in DataGridView
                DisplayDataPoints(ratings);
                chartPanel.Invalidate();
            }
        }

        private void DisplayDataPoints(List<RatingDataPoint> ratings)
        {
            pointsDgv.Rows.Clear();
            foreach (var point in ratings)
            {
                pointsDgv.Rows.Add(point.Timestamp, point.Rating, point.Weight);
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

        private void visualizationPanel_Paint(object sender, PaintEventArgs e)
        {
            //recalculate result
            Values = CalculateAverages();


            Rectangle bounds = chartPanel.ClientRectangle;

            // Use selected start/end time to scale X-axis properly
            DateTime start = startTimePicker.Value;
            DateTime end = endTimePicker.Value;
            int gridSpacing = 15;

            // Draw base chart components
            ChartRenderer.DrawGrid(e.Graphics, bounds, gridSpacing);
            ChartRenderer.DrawAxis(e.Graphics, bounds);
            ChartRenderer.DrawLabelsAndTicks(e.Graphics, bounds, start, end);

            // Extract computed trend lines (excluding raw actual data)
            List<(List<RatingDataPoint>, AveragingMethod)> computedAverages = Values ?? new List<(List<RatingDataPoint>, AveragingMethod)>();

            // Draw raw actual data points
            if (pointsDgv.Rows.Count > 0)
                ChartRenderer.DrawActualDataPoints(e.Graphics, bounds, start, end,
                pointsDgv.Rows.Cast<DataGridViewRow>()
                .Select(r => new RatingDataPoint((DateTime)r.Cells[0].Value, (double)r.Cells[1].Value, (double)r.Cells[2].Value)).ToList());

            // Draw computed trend lines
            ChartRenderer.DrawTrendLines(
                e.Graphics,
                bounds,
                start,
                end,
                computedAverages,
                methodColors,
                showTrendPoints: true
            );

            // Pass correct legend parameters
            ChartRenderer.DrawLegend(e.Graphics, bounds, methodColors.ToDictionary(k => k.Key.ToString(), v => v.Value));
        }

        private List<(List<RatingDataPoint>, AveragingMethod)> CalculateAverages()
        {
            var dataPoints = pointsDgv.Rows.Cast<DataGridViewRow>()
                .Select(r => new RatingDataPoint((DateTime)r.Cells[0].Value,
                (double)r.Cells[1].Value, (double)r.Cells[2].Value)).ToList();

            if (!dataPoints.Any())
                return null;

            var SimpleAveragePoints = SimpleAverageCalculator.ComputeAverageOverTime(dataPoints);
            var WeightedAveragePoints = WeightedAverageCalculator.ComputeWeightedAverageOverTime(dataPoints);
            var BayesianAveragePoints = BayesianAverageCalculator.ComputeBayesianAverageOverTime(dataPoints, BayesianPrior, BayesianM);
            var MedianScorePoints = MedianScoreCalculator.ComputeMedianScoreOverTime(dataPoints);
            var TruncatedMeanPoints = TruncatedMeanCalculator.ComputeTruncatedMeanOverTime(dataPoints, TrimPercent);
            var ExponentialMovingAveragePoints = ExponentialMovingAverageCalculator.ComputeExponentialMovingAverageOverTime(dataPoints, EmaAlpha);

            return new List<(List<RatingDataPoint>, AveragingMethod)>
            {
                (SimpleAveragePoints, AveragingMethod.SimpleAverage),
                (WeightedAveragePoints, AveragingMethod.WeightedAverage),
                (BayesianAveragePoints, AveragingMethod.BayesianAverage),
                (MedianScorePoints, AveragingMethod.MedianScore),
                (TruncatedMeanPoints, AveragingMethod.TruncatedMean),
                (ExponentialMovingAveragePoints, AveragingMethod.ExponentialMovingAverage)
            };
        }

        private void bayesianPriorNumeric_ValueChanged(object sender, EventArgs e)
        {
            chartPanel.Invalidate();
        }
    }


}
