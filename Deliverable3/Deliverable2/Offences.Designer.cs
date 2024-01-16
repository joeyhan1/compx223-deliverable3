
namespace Deliverable2
{
    partial class Offences
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.listBoxOffences = new System.Windows.Forms.ListBox();
            this.buttonDisplay = new System.Windows.Forms.Button();
            this.buttonAddOffence = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonUpdateOffence = new System.Windows.Forms.Button();
            this.listBoxInfringements = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.buttonInfringement = new System.Windows.Forms.Button();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.comboBoxXandY = new System.Windows.Forms.ComboBox();
            this.buttonSummary = new System.Windows.Forms.Button();
            this.listBoxSummary = new System.Windows.Forms.ListBox();
            this.buttonToggleChart = new System.Windows.Forms.Button();
            this.chartOffences = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBoxChart = new System.Windows.Forms.ComboBox();
            this.labelChart = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartOffences)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxOffences
            // 
            this.listBoxOffences.FormattingEnabled = true;
            this.listBoxOffences.Location = new System.Drawing.Point(4, 11);
            this.listBoxOffences.Name = "listBoxOffences";
            this.listBoxOffences.Size = new System.Drawing.Size(943, 394);
            this.listBoxOffences.TabIndex = 0;
            // 
            // buttonDisplay
            // 
            this.buttonDisplay.Location = new System.Drawing.Point(566, 411);
            this.buttonDisplay.Name = "buttonDisplay";
            this.buttonDisplay.Size = new System.Drawing.Size(75, 43);
            this.buttonDisplay.TabIndex = 1;
            this.buttonDisplay.Text = "Display Offences";
            this.buttonDisplay.UseVisualStyleBackColor = true;
            this.buttonDisplay.Click += new System.EventHandler(this.buttonDisplay_Click);
            // 
            // buttonAddOffence
            // 
            this.buttonAddOffence.Location = new System.Drawing.Point(647, 411);
            this.buttonAddOffence.Name = "buttonAddOffence";
            this.buttonAddOffence.Size = new System.Drawing.Size(75, 43);
            this.buttonAddOffence.TabIndex = 2;
            this.buttonAddOffence.Text = "Add Offences";
            this.buttonAddOffence.UseVisualStyleBackColor = true;
            this.buttonAddOffence.Click += new System.EventHandler(this.buttonAddOffence_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(734, 474);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.ReadOnly = true;
            this.textBoxInput.Size = new System.Drawing.Size(149, 43);
            this.textBoxInput.TabIndex = 3;
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(625, 489);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(59, 13);
            this.labelInput.TabIndex = 4;
            this.labelInput.Text = "User Input:";
            // 
            // buttonEnter
            // 
            this.buttonEnter.Enabled = false;
            this.buttonEnter.Location = new System.Drawing.Point(889, 474);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(58, 43);
            this.buttonEnter.TabIndex = 5;
            this.buttonEnter.Text = "Enter";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonUpdateOffence
            // 
            this.buttonUpdateOffence.Location = new System.Drawing.Point(728, 411);
            this.buttonUpdateOffence.Name = "buttonUpdateOffence";
            this.buttonUpdateOffence.Size = new System.Drawing.Size(75, 43);
            this.buttonUpdateOffence.TabIndex = 6;
            this.buttonUpdateOffence.Text = "Update Offences";
            this.buttonUpdateOffence.UseVisualStyleBackColor = true;
            this.buttonUpdateOffence.Click += new System.EventHandler(this.buttonUpdateOffence_Click);
            // 
            // listBoxInfringements
            // 
            this.listBoxInfringements.FormattingEnabled = true;
            this.listBoxInfringements.Location = new System.Drawing.Point(4, 407);
            this.listBoxInfringements.Name = "listBoxInfringements";
            this.listBoxInfringements.Size = new System.Drawing.Size(330, 69);
            this.listBoxInfringements.TabIndex = 7;
            // 
            // buttonInfringement
            // 
            this.buttonInfringement.Location = new System.Drawing.Point(4, 482);
            this.buttonInfringement.Name = "buttonInfringement";
            this.buttonInfringement.Size = new System.Drawing.Size(136, 45);
            this.buttonInfringement.TabIndex = 8;
            this.buttonInfringement.Text = "Display Infringements";
            this.buttonInfringement.UseVisualStyleBackColor = true;
            this.buttonInfringement.Click += new System.EventHandler(this.buttonInfringement_Click);
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(809, 411);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 43);
            this.buttonFilter.TabIndex = 9;
            this.buttonFilter.Text = "Filter Offences";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // comboBoxXandY
            // 
            this.comboBoxXandY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxXandY.FormattingEnabled = true;
            this.comboBoxXandY.Location = new System.Drawing.Point(734, 486);
            this.comboBoxXandY.Name = "comboBoxXandY";
            this.comboBoxXandY.Size = new System.Drawing.Size(149, 21);
            this.comboBoxXandY.TabIndex = 10;
            this.comboBoxXandY.Visible = false;
            // 
            // buttonSummary
            // 
            this.buttonSummary.Location = new System.Drawing.Point(340, 482);
            this.buttonSummary.Name = "buttonSummary";
            this.buttonSummary.Size = new System.Drawing.Size(136, 43);
            this.buttonSummary.TabIndex = 11;
            this.buttonSummary.Text = "Summary Report";
            this.buttonSummary.UseVisualStyleBackColor = true;
            this.buttonSummary.Click += new System.EventHandler(this.buttonSummary_Click);
            // 
            // listBoxSummary
            // 
            this.listBoxSummary.FormattingEnabled = true;
            this.listBoxSummary.Location = new System.Drawing.Point(340, 407);
            this.listBoxSummary.Name = "listBoxSummary";
            this.listBoxSummary.Size = new System.Drawing.Size(220, 69);
            this.listBoxSummary.TabIndex = 12;
            // 
            // buttonToggleChart
            // 
            this.buttonToggleChart.Location = new System.Drawing.Point(889, 411);
            this.buttonToggleChart.Name = "buttonToggleChart";
            this.buttonToggleChart.Size = new System.Drawing.Size(58, 43);
            this.buttonToggleChart.TabIndex = 13;
            this.buttonToggleChart.Text = "Toggle Chart";
            this.buttonToggleChart.UseVisualStyleBackColor = true;
            this.buttonToggleChart.Click += new System.EventHandler(this.buttonToggleChart_Click);
            // 
            // chartOffences
            // 
            chartArea1.Name = "ChartArea1";
            this.chartOffences.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartOffences.Legends.Add(legend1);
            this.chartOffences.Location = new System.Drawing.Point(4, 57);
            this.chartOffences.Name = "chartOffences";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartOffences.Series.Add(series1);
            this.chartOffences.Size = new System.Drawing.Size(943, 348);
            this.chartOffences.TabIndex = 14;
            this.chartOffences.Text = "chart1";
            this.chartOffences.Visible = false;
            // 
            // comboBoxChart
            // 
            this.comboBoxChart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChart.FormattingEnabled = true;
            this.comboBoxChart.Items.AddRange(new object[] {
            "Age vs Number of Offences",
            "Vehicle Colour vs Num of Offences"});
            this.comboBoxChart.Location = new System.Drawing.Point(407, 33);
            this.comboBoxChart.Name = "comboBoxChart";
            this.comboBoxChart.Size = new System.Drawing.Size(160, 21);
            this.comboBoxChart.TabIndex = 15;
            this.comboBoxChart.Visible = false;
            this.comboBoxChart.SelectedIndexChanged += new System.EventHandler(this.comboBoxChart_SelectedIndexChanged);
            // 
            // labelChart
            // 
            this.labelChart.AutoSize = true;
            this.labelChart.Location = new System.Drawing.Point(323, 36);
            this.labelChart.Name = "labelChart";
            this.labelChart.Size = new System.Drawing.Size(78, 13);
            this.labelChart.TabIndex = 16;
            this.labelChart.Text = "Offence Charts";
            this.labelChart.Visible = false;
            // 
            // Offences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 539);
            this.Controls.Add(this.labelChart);
            this.Controls.Add(this.comboBoxChart);
            this.Controls.Add(this.chartOffences);
            this.Controls.Add(this.buttonToggleChart);
            this.Controls.Add(this.listBoxSummary);
            this.Controls.Add(this.buttonSummary);
            this.Controls.Add(this.comboBoxXandY);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.buttonInfringement);
            this.Controls.Add(this.listBoxInfringements);
            this.Controls.Add(this.buttonUpdateOffence);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.buttonAddOffence);
            this.Controls.Add(this.buttonDisplay);
            this.Controls.Add(this.listBoxOffences);
            this.Name = "Offences";
            this.Text = "Offences";
            ((System.ComponentModel.ISupportInitialize)(this.chartOffences)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxOffences;
        private System.Windows.Forms.Button buttonDisplay;
        private System.Windows.Forms.Button buttonAddOffence;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonUpdateOffence;
        private System.Windows.Forms.ListBox listBoxInfringements;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button buttonInfringement;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.ComboBox comboBoxXandY;
        private System.Windows.Forms.Button buttonSummary;
        private System.Windows.Forms.ListBox listBoxSummary;
        private System.Windows.Forms.Button buttonToggleChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOffences;
        private System.Windows.Forms.ComboBox comboBoxChart;
        private System.Windows.Forms.Label labelChart;
    }
}

