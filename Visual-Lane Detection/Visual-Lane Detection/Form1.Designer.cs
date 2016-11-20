namespace Visual_Lane_Detection
{
    partial class VisualLaneDetection
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea15 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend15 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series43 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series44 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series45 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea16 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend16 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series46 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series47 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series48 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button_Graph = new System.Windows.Forms.Button();
            this.button_Smooth = new System.Windows.Forms.Button();
            this.pictureBox_Original = new System.Windows.Forms.PictureBox();
            this.pictureBox_Grayscale = new System.Windows.Forms.PictureBox();
            this.label_Original = new System.Windows.Forms.Label();
            this.label_Blurred = new System.Windows.Forms.Label();
            this.chart_Channels = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_GrayscaleChannel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBox_Segmentation = new System.Windows.Forms.PictureBox();
            this.label_Segmentation = new System.Windows.Forms.Label();
            this.button_SaveGaussian = new System.Windows.Forms.Button();
            this.button_SaveSegmentation = new System.Windows.Forms.Button();
            this.button_FindLine = new System.Windows.Forms.Button();
            this.pictureBox_CenterLine = new System.Windows.Forms.PictureBox();
            this.label_CenterLine = new System.Windows.Forms.Label();
            this.button_SaveLine = new System.Windows.Forms.Button();
            this.label_Answer = new System.Windows.Forms.Label();
            this.label_Slope = new System.Windows.Forms.Label();
            this.label_Intercept = new System.Windows.Forms.Label();
            this.textBox_Slope = new System.Windows.Forms.TextBox();
            this.textBox_Intercept = new System.Windows.Forms.TextBox();
            this.button_Complete = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_ColorHistogram = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_GaussianMask = new System.Windows.Forms.TextBox();
            this.button_LoadImage = new System.Windows.Forms.Button();
            this.checkBox_GaussianBlur = new System.Windows.Forms.CheckBox();
            this.textBox_GaussianBlur = new System.Windows.Forms.TextBox();
            this.comboBox_Camera = new System.Windows.Forms.ComboBox();
            this.button_Gaussian = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Grayscale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Channels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_GrayscaleChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Segmentation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CenterLine)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Graph
            // 
            this.button_Graph.Enabled = false;
            this.button_Graph.Location = new System.Drawing.Point(0, 0);
            this.button_Graph.Name = "button_Graph";
            this.button_Graph.Size = new System.Drawing.Size(57, 27);
            this.button_Graph.TabIndex = 5;
            this.button_Graph.Text = "Graph";
            this.button_Graph.UseVisualStyleBackColor = true;
            this.button_Graph.Click += new System.EventHandler(this.button_Graph_Click);
            // 
            // button_Smooth
            // 
            this.button_Smooth.Enabled = false;
            this.button_Smooth.Location = new System.Drawing.Point(59, 0);
            this.button_Smooth.Name = "button_Smooth";
            this.button_Smooth.Size = new System.Drawing.Size(57, 27);
            this.button_Smooth.TabIndex = 6;
            this.button_Smooth.Text = "Smooth";
            this.button_Smooth.UseVisualStyleBackColor = true;
            this.button_Smooth.Click += new System.EventHandler(this.button_Smooth_Click);
            // 
            // pictureBox_Original
            // 
            this.pictureBox_Original.Location = new System.Drawing.Point(12, 22);
            this.pictureBox_Original.Name = "pictureBox_Original";
            this.pictureBox_Original.Size = new System.Drawing.Size(297, 219);
            this.pictureBox_Original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Original.TabIndex = 7;
            this.pictureBox_Original.TabStop = false;
            // 
            // pictureBox_Grayscale
            // 
            this.pictureBox_Grayscale.Location = new System.Drawing.Point(12, 277);
            this.pictureBox_Grayscale.Name = "pictureBox_Grayscale";
            this.pictureBox_Grayscale.Size = new System.Drawing.Size(297, 219);
            this.pictureBox_Grayscale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Grayscale.TabIndex = 8;
            this.pictureBox_Grayscale.TabStop = false;
            // 
            // label_Original
            // 
            this.label_Original.AutoSize = true;
            this.label_Original.Location = new System.Drawing.Point(9, 5);
            this.label_Original.Name = "label_Original";
            this.label_Original.Size = new System.Drawing.Size(42, 13);
            this.label_Original.TabIndex = 9;
            this.label_Original.Text = "Original";
            // 
            // label_Blurred
            // 
            this.label_Blurred.AutoSize = true;
            this.label_Blurred.Location = new System.Drawing.Point(9, 259);
            this.label_Blurred.Name = "label_Blurred";
            this.label_Blurred.Size = new System.Drawing.Size(72, 13);
            this.label_Blurred.TabIndex = 10;
            this.label_Blurred.Text = "Gaussian Blur";
            // 
            // chart_Channels
            // 
            this.chart_Channels.BackColor = System.Drawing.Color.Transparent;
            chartArea15.Name = "ChartArea1";
            this.chart_Channels.ChartAreas.Add(chartArea15);
            legend15.Enabled = false;
            legend15.Name = "Legend1";
            this.chart_Channels.Legends.Add(legend15);
            this.chart_Channels.Location = new System.Drawing.Point(-15, 508);
            this.chart_Channels.Margin = new System.Windows.Forms.Padding(0);
            this.chart_Channels.Name = "chart_Channels";
            series43.ChartArea = "ChartArea1";
            series43.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series43.Color = System.Drawing.Color.Blue;
            series43.Legend = "Legend1";
            series43.MarkerColor = System.Drawing.Color.Blue;
            series43.Name = "Blue";
            series43.XValueMember = "Pixel Value";
            series43.YValueMembers = "Pixel Count";
            series44.ChartArea = "ChartArea1";
            series44.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series44.Color = System.Drawing.Color.Green;
            series44.Legend = "Legend1";
            series44.MarkerColor = System.Drawing.Color.Green;
            series44.Name = "Green";
            series45.ChartArea = "ChartArea1";
            series45.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series45.Color = System.Drawing.Color.Red;
            series45.Legend = "Legend1";
            series45.MarkerColor = System.Drawing.Color.Red;
            series45.Name = "Red";
            this.chart_Channels.Series.Add(series43);
            this.chart_Channels.Series.Add(series44);
            this.chart_Channels.Series.Add(series45);
            this.chart_Channels.Size = new System.Drawing.Size(342, 236);
            this.chart_Channels.TabIndex = 13;
            this.chart_Channels.Text = " ";
            // 
            // chart_GrayscaleChannel
            // 
            this.chart_GrayscaleChannel.BackColor = System.Drawing.Color.Transparent;
            chartArea16.Name = "ChartArea1";
            this.chart_GrayscaleChannel.ChartAreas.Add(chartArea16);
            legend16.Enabled = false;
            legend16.Name = "Legend1";
            this.chart_GrayscaleChannel.Legends.Add(legend16);
            this.chart_GrayscaleChannel.Location = new System.Drawing.Point(1022, 508);
            this.chart_GrayscaleChannel.Margin = new System.Windows.Forms.Padding(0);
            this.chart_GrayscaleChannel.Name = "chart_GrayscaleChannel";
            this.chart_GrayscaleChannel.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chart_GrayscaleChannel.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Goldenrod,
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))))};
            series46.ChartArea = "ChartArea1";
            series46.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series46.Legend = "Legend1";
            series46.Name = "Grayscale";
            series47.ChartArea = "ChartArea1";
            series47.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series47.Legend = "Legend1";
            series47.Name = "Maxima";
            series48.ChartArea = "ChartArea1";
            series48.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series48.Legend = "Legend1";
            series48.Name = "Minima";
            this.chart_GrayscaleChannel.Series.Add(series46);
            this.chart_GrayscaleChannel.Series.Add(series47);
            this.chart_GrayscaleChannel.Series.Add(series48);
            this.chart_GrayscaleChannel.Size = new System.Drawing.Size(342, 236);
            this.chart_GrayscaleChannel.TabIndex = 14;
            this.chart_GrayscaleChannel.Text = "Grayscale";
            // 
            // pictureBox_Segmentation
            // 
            this.pictureBox_Segmentation.Location = new System.Drawing.Point(1049, 22);
            this.pictureBox_Segmentation.Name = "pictureBox_Segmentation";
            this.pictureBox_Segmentation.Size = new System.Drawing.Size(297, 219);
            this.pictureBox_Segmentation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Segmentation.TabIndex = 15;
            this.pictureBox_Segmentation.TabStop = false;
            // 
            // label_Segmentation
            // 
            this.label_Segmentation.AutoSize = true;
            this.label_Segmentation.Location = new System.Drawing.Point(1046, 5);
            this.label_Segmentation.Name = "label_Segmentation";
            this.label_Segmentation.Size = new System.Drawing.Size(72, 13);
            this.label_Segmentation.TabIndex = 16;
            this.label_Segmentation.Text = "Segmentation";
            // 
            // button_SaveGaussian
            // 
            this.button_SaveGaussian.Enabled = false;
            this.button_SaveGaussian.Location = new System.Drawing.Point(87, 254);
            this.button_SaveGaussian.Name = "button_SaveGaussian";
            this.button_SaveGaussian.Size = new System.Drawing.Size(44, 21);
            this.button_SaveGaussian.TabIndex = 17;
            this.button_SaveGaussian.Text = "Save";
            this.button_SaveGaussian.UseVisualStyleBackColor = true;
            this.button_SaveGaussian.Click += new System.EventHandler(this.button_SaveGaussian_Click);
            // 
            // button_SaveSegmentation
            // 
            this.button_SaveSegmentation.Enabled = false;
            this.button_SaveSegmentation.Location = new System.Drawing.Point(1124, 1);
            this.button_SaveSegmentation.Name = "button_SaveSegmentation";
            this.button_SaveSegmentation.Size = new System.Drawing.Size(44, 21);
            this.button_SaveSegmentation.TabIndex = 19;
            this.button_SaveSegmentation.Text = "Save";
            this.button_SaveSegmentation.UseVisualStyleBackColor = true;
            this.button_SaveSegmentation.Click += new System.EventHandler(this.button_SaveSegmentation_Click);
            // 
            // button_FindLine
            // 
            this.button_FindLine.Enabled = false;
            this.button_FindLine.Location = new System.Drawing.Point(153, 0);
            this.button_FindLine.Name = "button_FindLine";
            this.button_FindLine.Size = new System.Drawing.Size(62, 27);
            this.button_FindLine.TabIndex = 20;
            this.button_FindLine.Text = "Find Line";
            this.button_FindLine.UseVisualStyleBackColor = true;
            this.button_FindLine.Click += new System.EventHandler(this.button_FindLine_Click);
            // 
            // pictureBox_CenterLine
            // 
            this.pictureBox_CenterLine.Location = new System.Drawing.Point(1049, 277);
            this.pictureBox_CenterLine.Name = "pictureBox_CenterLine";
            this.pictureBox_CenterLine.Size = new System.Drawing.Size(297, 219);
            this.pictureBox_CenterLine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_CenterLine.TabIndex = 21;
            this.pictureBox_CenterLine.TabStop = false;
            // 
            // label_CenterLine
            // 
            this.label_CenterLine.AutoSize = true;
            this.label_CenterLine.Location = new System.Drawing.Point(1046, 259);
            this.label_CenterLine.Name = "label_CenterLine";
            this.label_CenterLine.Size = new System.Drawing.Size(61, 13);
            this.label_CenterLine.TabIndex = 22;
            this.label_CenterLine.Text = "Center Line";
            // 
            // button_SaveLine
            // 
            this.button_SaveLine.Enabled = false;
            this.button_SaveLine.Location = new System.Drawing.Point(1124, 255);
            this.button_SaveLine.Name = "button_SaveLine";
            this.button_SaveLine.Size = new System.Drawing.Size(44, 21);
            this.button_SaveLine.TabIndex = 23;
            this.button_SaveLine.Text = "Save";
            this.button_SaveLine.UseVisualStyleBackColor = true;
            this.button_SaveLine.Click += new System.EventHandler(this.button_SaveLine_Click);
            // 
            // label_Answer
            // 
            this.label_Answer.AutoSize = true;
            this.label_Answer.Location = new System.Drawing.Point(1193, 214);
            this.label_Answer.Name = "label_Answer";
            this.label_Answer.Size = new System.Drawing.Size(0, 13);
            this.label_Answer.TabIndex = 24;
            // 
            // label_Slope
            // 
            this.label_Slope.AutoSize = true;
            this.label_Slope.Location = new System.Drawing.Point(631, 689);
            this.label_Slope.Name = "label_Slope";
            this.label_Slope.Size = new System.Drawing.Size(40, 13);
            this.label_Slope.TabIndex = 25;
            this.label_Slope.Text = "Slope: ";
            // 
            // label_Intercept
            // 
            this.label_Intercept.AutoSize = true;
            this.label_Intercept.Location = new System.Drawing.Point(738, 689);
            this.label_Intercept.Name = "label_Intercept";
            this.label_Intercept.Size = new System.Drawing.Size(62, 13);
            this.label_Intercept.TabIndex = 26;
            this.label_Intercept.Text = "Y-Intercept:";
            // 
            // textBox_Slope
            // 
            this.textBox_Slope.Enabled = false;
            this.textBox_Slope.Location = new System.Drawing.Point(634, 705);
            this.textBox_Slope.Name = "textBox_Slope";
            this.textBox_Slope.Size = new System.Drawing.Size(100, 20);
            this.textBox_Slope.TabIndex = 27;
            // 
            // textBox_Intercept
            // 
            this.textBox_Intercept.Enabled = false;
            this.textBox_Intercept.Location = new System.Drawing.Point(741, 705);
            this.textBox_Intercept.Name = "textBox_Intercept";
            this.textBox_Intercept.Size = new System.Drawing.Size(100, 20);
            this.textBox_Intercept.TabIndex = 28;
            // 
            // button_Complete
            // 
            this.button_Complete.Enabled = false;
            this.button_Complete.Location = new System.Drawing.Point(400, 700);
            this.button_Complete.Name = "button_Complete";
            this.button_Complete.Size = new System.Drawing.Size(213, 29);
            this.button_Complete.TabIndex = 29;
            this.button_Complete.Text = "Complete Necessary Computation";
            this.button_Complete.UseVisualStyleBackColor = true;
            this.button_Complete.Click += new System.EventHandler(this.button_Complete_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(878, 699);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(73, 31);
            this.button_Exit.TabIndex = 32;
            this.button_Exit.Text = "Exit";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox_GaussianMask);
            this.panel2.Controls.Add(this.button_Graph);
            this.panel2.Controls.Add(this.button_Smooth);
            this.panel2.Controls.Add(this.button_FindLine);
            this.panel2.Location = new System.Drawing.Point(790, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(217, 27);
            this.panel2.TabIndex = 33;
            // 
            // label_ColorHistogram
            // 
            this.label_ColorHistogram.AutoSize = true;
            this.label_ColorHistogram.Location = new System.Drawing.Point(47, 508);
            this.label_ColorHistogram.Name = "label_ColorHistogram";
            this.label_ColorHistogram.Size = new System.Drawing.Size(215, 13);
            this.label_ColorHistogram.TabIndex = 34;
            this.label_ColorHistogram.Text = "Histogram: Color Pixel Value VS. Pixel Count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1084, 508);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Histogram: Gray Pixel Value VS. Pixel Count";
            // 
            // textBox_GaussianMask
            // 
            this.textBox_GaussianMask.Enabled = false;
            this.textBox_GaussianMask.Location = new System.Drawing.Point(122, 4);
            this.textBox_GaussianMask.Name = "textBox_GaussianMask";
            this.textBox_GaussianMask.Size = new System.Drawing.Size(25, 20);
            this.textBox_GaussianMask.TabIndex = 36;
            // 
            // button_LoadImage
            // 
            this.button_LoadImage.Location = new System.Drawing.Point(4, 5);
            this.button_LoadImage.Name = "button_LoadImage";
            this.button_LoadImage.Size = new System.Drawing.Size(87, 27);
            this.button_LoadImage.TabIndex = 0;
            this.button_LoadImage.Text = "Load Image";
            this.button_LoadImage.UseVisualStyleBackColor = true;
            this.button_LoadImage.Click += new System.EventHandler(this.button_LoadImage_Click);
            // 
            // checkBox_GaussianBlur
            // 
            this.checkBox_GaussianBlur.AutoSize = true;
            this.checkBox_GaussianBlur.Location = new System.Drawing.Point(227, 11);
            this.checkBox_GaussianBlur.Name = "checkBox_GaussianBlur";
            this.checkBox_GaussianBlur.Size = new System.Drawing.Size(91, 17);
            this.checkBox_GaussianBlur.TabIndex = 1;
            this.checkBox_GaussianBlur.Text = "Gaussian Blur";
            this.checkBox_GaussianBlur.UseVisualStyleBackColor = true;
            this.checkBox_GaussianBlur.CheckedChanged += new System.EventHandler(this.checkBox_GuassianBlur_CheckedChanged);
            // 
            // textBox_GaussianBlur
            // 
            this.textBox_GaussianBlur.Enabled = false;
            this.textBox_GaussianBlur.Location = new System.Drawing.Point(317, 9);
            this.textBox_GaussianBlur.Name = "textBox_GaussianBlur";
            this.textBox_GaussianBlur.Size = new System.Drawing.Size(25, 20);
            this.textBox_GaussianBlur.TabIndex = 2;
            // 
            // comboBox_Camera
            // 
            this.comboBox_Camera.FormattingEnabled = true;
            this.comboBox_Camera.Location = new System.Drawing.Point(119, 9);
            this.comboBox_Camera.Name = "comboBox_Camera";
            this.comboBox_Camera.Size = new System.Drawing.Size(102, 21);
            this.comboBox_Camera.TabIndex = 30;
            this.comboBox_Camera.SelectedIndexChanged += new System.EventHandler(this.comboBox_Camera_SelectedIndexChanged);
            // 
            // button_Gaussian
            // 
            this.button_Gaussian.Enabled = false;
            this.button_Gaussian.Location = new System.Drawing.Point(348, 6);
            this.button_Gaussian.Name = "button_Gaussian";
            this.button_Gaussian.Size = new System.Drawing.Size(97, 25);
            this.button_Gaussian.TabIndex = 4;
            this.button_Gaussian.Text = "Gaussian Mask";
            this.button_Gaussian.UseVisualStyleBackColor = true;
            this.button_Gaussian.Click += new System.EventHandler(this.button_Compute_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "or";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_Gaussian);
            this.panel1.Controls.Add(this.comboBox_Camera);
            this.panel1.Controls.Add(this.textBox_GaussianBlur);
            this.panel1.Controls.Add(this.checkBox_GaussianBlur);
            this.panel1.Controls.Add(this.button_LoadImage);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(331, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 37);
            this.panel1.TabIndex = 31;
            // 
            // VisualLaneDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 741);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_ColorHistogram);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_Complete);
            this.Controls.Add(this.textBox_Intercept);
            this.Controls.Add(this.textBox_Slope);
            this.Controls.Add(this.label_Intercept);
            this.Controls.Add(this.label_Slope);
            this.Controls.Add(this.label_Answer);
            this.Controls.Add(this.button_SaveLine);
            this.Controls.Add(this.label_CenterLine);
            this.Controls.Add(this.pictureBox_CenterLine);
            this.Controls.Add(this.button_SaveSegmentation);
            this.Controls.Add(this.button_SaveGaussian);
            this.Controls.Add(this.label_Segmentation);
            this.Controls.Add(this.pictureBox_Segmentation);
            this.Controls.Add(this.chart_GrayscaleChannel);
            this.Controls.Add(this.chart_Channels);
            this.Controls.Add(this.label_Blurred);
            this.Controls.Add(this.label_Original);
            this.Controls.Add(this.pictureBox_Grayscale);
            this.Controls.Add(this.pictureBox_Original);
            this.Name = "VisualLaneDetection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual Lane Detection";
            this.Load += new System.EventHandler(this.VisualLaneDetection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Grayscale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Channels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_GrayscaleChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Segmentation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CenterLine)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Graph;
        private System.Windows.Forms.Button button_Smooth;
        private System.Windows.Forms.PictureBox pictureBox_Original;
        private System.Windows.Forms.PictureBox pictureBox_Grayscale;
        private System.Windows.Forms.Label label_Original;
        private System.Windows.Forms.Label label_Blurred;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Channels;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_GrayscaleChannel;
        private System.Windows.Forms.PictureBox pictureBox_Segmentation;
        private System.Windows.Forms.Label label_Segmentation;
        private System.Windows.Forms.Button button_SaveGaussian;
        private System.Windows.Forms.Button button_SaveSegmentation;
        private System.Windows.Forms.Button button_FindLine;
        private System.Windows.Forms.PictureBox pictureBox_CenterLine;
        private System.Windows.Forms.Label label_CenterLine;
        private System.Windows.Forms.Button button_SaveLine;
        public System.Windows.Forms.Label label_Answer;
        private System.Windows.Forms.Label label_Slope;
        private System.Windows.Forms.Label label_Intercept;
        private System.Windows.Forms.TextBox textBox_Slope;
        private System.Windows.Forms.TextBox textBox_Intercept;
        private System.Windows.Forms.Button button_Complete;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_ColorHistogram;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_GaussianMask;
        private System.Windows.Forms.Button button_LoadImage;
        private System.Windows.Forms.CheckBox checkBox_GaussianBlur;
        private System.Windows.Forms.TextBox textBox_GaussianBlur;
        private System.Windows.Forms.ComboBox comboBox_Camera;
        private System.Windows.Forms.Button button_Gaussian;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}

