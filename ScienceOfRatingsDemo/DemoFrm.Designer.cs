namespace ScienceOfRatingsDemo
{
    partial class DemoFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pointsDgv = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataPointsNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.recencyLambdaNumeric = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.emaAlphaNumeric = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.wilsonConfidenceNumeric = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.bayesianMNumeric = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.trimPercentNumeric = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.bayesianPriorNumeric = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.weightMethodComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pointsDgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointsNumeric)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recencyLambdaNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emaAlphaNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wilsonConfidenceNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bayesianMNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trimPercentNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bayesianPriorNumeric)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pointsDgv
            // 
            this.pointsDgv.AllowUserToAddRows = false;
            this.pointsDgv.AllowUserToDeleteRows = false;
            this.pointsDgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointsDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pointsDgv.Location = new System.Drawing.Point(6, 19);
            this.pointsDgv.Name = "pointsDgv";
            this.pointsDgv.ReadOnly = true;
            this.pointsDgv.Size = new System.Drawing.Size(374, 289);
            this.pointsDgv.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.pointsDgv);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 749);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "date points generator";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.dataPointsNumeric);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.startTimePicker);
            this.groupBox3.Controls.Add(this.endTimePicker);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(6, 314);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(374, 77);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Interval and data points count";
            // 
            // dataPointsNumeric
            // 
            this.dataPointsNumeric.Location = new System.Drawing.Point(306, 33);
            this.dataPointsNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.dataPointsNumeric.Name = "dataPointsNumeric";
            this.dataPointsNumeric.Size = new System.Drawing.Size(62, 20);
            this.dataPointsNumeric.TabIndex = 16;
            this.dataPointsNumeric.Value = new decimal(new int[] {
            42,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Points: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "End";
            // 
            // startTimePicker
            // 
            this.startTimePicker.Location = new System.Drawing.Point(43, 19);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startTimePicker.TabIndex = 11;
            // 
            // endTimePicker
            // 
            this.endTimePicker.Location = new System.Drawing.Point(43, 45);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(200, 20);
            this.endTimePicker.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Start";
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(6, 663);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(374, 39);
            this.button9.TabIndex = 10;
            this.button9.Text = "Sudden Drop & Recovery (Crisis Event) – Big drop in ratings, then slow recovery";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.Location = new System.Drawing.Point(6, 708);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(374, 35);
            this.button10.TabIndex = 9;
            this.button10.Text = "New Business Effect (Starts High, Then Normalizes) – Only early fans rate it high" +
    ", then reality sets in";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(6, 634);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(374, 23);
            this.button8.TabIndex = 8;
            this.button8.Text = "Gradual Oscillation (Wavelike Changes) – Smooth up-and-down rating cycle";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(6, 587);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(374, 41);
            this.button7.TabIndex = 7;
            this.button7.Text = "Fake Bad (Manipulated to Look Worse) – Mostly fake 1s, but real users rate it hig" +
    "her";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(6, 542);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(374, 41);
            this.button6.TabIndex = 6;
            this.button6.Text = "Fake Good (Manipulated to Look Better) – Mostly fake 10s, but real users rate it " +
    "lower";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(6, 513);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(374, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Extreme Swings (Chaotic & Unstable) – Random high and low swings";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(6, 484);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(374, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Bad Improved (Rising Trend) – Starts low, but improves over time";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(6, 455);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(374, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Good Went Bad (Declining Trend) – Starts high, but deteriorates over time";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(6, 426);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(374, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Steady Bad (Consistently Bad) – Natural, low ratings (1-3)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(6, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(374, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Steady Good (Consistently Good) – Natural, high ratings (8-10)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chartPanel);
            this.groupBox2.Location = new System.Drawing.Point(404, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(920, 607);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visualization";
            // 
            // chartPanel
            // 
            this.chartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPanel.Location = new System.Drawing.Point(3, 16);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(914, 588);
            this.chartPanel.TabIndex = 0;
            this.chartPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.visualizationPanel_Paint);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.recencyLambdaNumeric);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.emaAlphaNumeric);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.wilsonConfidenceNumeric);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.bayesianMNumeric);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.trimPercentNumeric);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.bayesianPriorNumeric);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.weightMethodComboBox);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(407, 622);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(356, 139);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fine tunning";
            // 
            // recencyLambdaNumeric
            // 
            this.recencyLambdaNumeric.Location = new System.Drawing.Point(296, 98);
            this.recencyLambdaNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.recencyLambdaNumeric.Name = "recencyLambdaNumeric";
            this.recencyLambdaNumeric.Size = new System.Drawing.Size(49, 20);
            this.recencyLambdaNumeric.TabIndex = 27;
            this.recencyLambdaNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.recencyLambdaNumeric.ValueChanged += new System.EventHandler(this.bayesianPriorNumeric_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(193, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Recency Lambda:";
            // 
            // emaAlphaNumeric
            // 
            this.emaAlphaNumeric.Location = new System.Drawing.Point(132, 98);
            this.emaAlphaNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.emaAlphaNumeric.Name = "emaAlphaNumeric";
            this.emaAlphaNumeric.Size = new System.Drawing.Size(57, 20);
            this.emaAlphaNumeric.TabIndex = 25;
            this.emaAlphaNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.emaAlphaNumeric.ValueChanged += new System.EventHandler(this.bayesianPriorNumeric_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "EMA Alpha:";
            // 
            // wilsonConfidenceNumeric
            // 
            this.wilsonConfidenceNumeric.Location = new System.Drawing.Point(296, 72);
            this.wilsonConfidenceNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.wilsonConfidenceNumeric.Name = "wilsonConfidenceNumeric";
            this.wilsonConfidenceNumeric.Size = new System.Drawing.Size(49, 20);
            this.wilsonConfidenceNumeric.TabIndex = 23;
            this.wilsonConfidenceNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.wilsonConfidenceNumeric.ValueChanged += new System.EventHandler(this.bayesianPriorNumeric_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(193, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Wilson Confidence:";
            // 
            // bayesianMNumeric
            // 
            this.bayesianMNumeric.Location = new System.Drawing.Point(296, 46);
            this.bayesianMNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.bayesianMNumeric.Name = "bayesianMNumeric";
            this.bayesianMNumeric.Size = new System.Drawing.Size(49, 20);
            this.bayesianMNumeric.TabIndex = 21;
            this.bayesianMNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.bayesianMNumeric.ValueChanged += new System.EventHandler(this.bayesianPriorNumeric_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(193, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Bayesian m:";
            // 
            // trimPercentNumeric
            // 
            this.trimPercentNumeric.Location = new System.Drawing.Point(132, 72);
            this.trimPercentNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.trimPercentNumeric.Name = "trimPercentNumeric";
            this.trimPercentNumeric.Size = new System.Drawing.Size(57, 20);
            this.trimPercentNumeric.TabIndex = 19;
            this.trimPercentNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.trimPercentNumeric.ValueChanged += new System.EventHandler(this.bayesianPriorNumeric_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Truncated Mean Trim %";
            // 
            // bayesianPriorNumeric
            // 
            this.bayesianPriorNumeric.Location = new System.Drawing.Point(132, 46);
            this.bayesianPriorNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.bayesianPriorNumeric.Name = "bayesianPriorNumeric";
            this.bayesianPriorNumeric.Size = new System.Drawing.Size(57, 20);
            this.bayesianPriorNumeric.TabIndex = 17;
            this.bayesianPriorNumeric.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.bayesianPriorNumeric.ValueChanged += new System.EventHandler(this.bayesianPriorNumeric_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Bayesian Prior:";
            // 
            // weightMethodComboBox
            // 
            this.weightMethodComboBox.FormattingEnabled = true;
            this.weightMethodComboBox.Location = new System.Drawing.Point(112, 19);
            this.weightMethodComboBox.Name = "weightMethodComboBox";
            this.weightMethodComboBox.Size = new System.Drawing.Size(233, 21);
            this.weightMethodComboBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Weight generation:";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.checkBox10);
            this.groupBox5.Controls.Add(this.checkBox9);
            this.groupBox5.Controls.Add(this.checkBox8);
            this.groupBox5.Controls.Add(this.checkBox7);
            this.groupBox5.Controls.Add(this.checkBox6);
            this.groupBox5.Controls.Add(this.checkBox5);
            this.groupBox5.Controls.Add(this.checkBox4);
            this.groupBox5.Controls.Add(this.checkBox3);
            this.groupBox5.Controls.Add(this.checkBox2);
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Location = new System.Drawing.Point(769, 625);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(451, 136);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Chart display settings";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Simple Average";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(6, 41);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(115, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Weighted Average";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(6, 64);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(112, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Bayesian Average";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(6, 87);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(92, 17);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "Median Score";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(6, 110);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(105, 17);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Text = "Truncated Mean";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Checked = true;
            this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox6.Location = new System.Drawing.Point(123, 19);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(162, 17);
            this.checkBox6.TabIndex = 12;
            this.checkBox6.Text = "Exponential Moving Average";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Checked = true;
            this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox7.Location = new System.Drawing.Point(123, 42);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(127, 17);
            this.checkBox7.TabIndex = 11;
            this.checkBox7.Text = "Wilson Score Interval";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Checked = true;
            this.checkBox8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox8.Location = new System.Drawing.Point(123, 65);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(153, 17);
            this.checkBox8.TabIndex = 10;
            this.checkBox8.Text = "Gini Based Review Spread";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Checked = true;
            this.checkBox9.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox9.Location = new System.Drawing.Point(123, 88);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(147, 17);
            this.checkBox9.TabIndex = 9;
            this.checkBox9.Text = "Recency Adjusted Rating";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Checked = true;
            this.checkBox10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox10.Location = new System.Drawing.Point(123, 111);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(104, 17);
            this.checkBox10.TabIndex = 8;
            this.checkBox10.Text = "Geometric Mean";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // DemoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 773);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DemoFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Science of Ratings Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pointsDgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointsNumeric)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recencyLambdaNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emaAlphaNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wilsonConfidenceNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bayesianMNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trimPercentNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bayesianPriorNumeric)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView pointsDgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.DateTimePicker endTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown dataPointsNumeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel chartPanel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown recencyLambdaNumeric;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown emaAlphaNumeric;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown wilsonConfidenceNumeric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown bayesianMNumeric;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown trimPercentNumeric;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown bayesianPriorNumeric;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox weightMethodComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

