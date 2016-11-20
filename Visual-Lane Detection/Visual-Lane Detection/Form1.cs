using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using TouchlessLib;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Diagnostics;
namespace Visual_Lane_Detection
{
    public partial class VisualLaneDetection : Form
    {
        TouchlessMgr manager;


        Variables variables;

        Point lastLocation = new Point(460, 250);

        public VisualLaneDetection()
        {
            InitializeComponent();
            checkBox_GaussianBlur.Checked = true;
        }
        [DllImport("user32.dll")]
                 static extern void SwitchToThisWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        

        private void VisualLaneDetection_Load(object sender, EventArgs e)
        {
            manager = new TouchlessMgr();

            foreach (Camera item in manager.Cameras)
                comboBox_Camera.Items.Add(item);

            variables = new Variables();


            


            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;

        }

        private void comboBox_Camera_SelectedIndexChanged(object sender, EventArgs e)
        {
            //initializing the camera to be used based on the selection
            manager.CurrentCamera = (Camera)comboBox_Camera.SelectedItem;

            //Setting the Event handler for the camera
            manager.CurrentCamera.OnImageCaptured += new EventHandler<CameraEventArgs>(CurrentCamera_OnImageCaptured);

            button_Gaussian.Enabled = true;
            button_Complete.Enabled = true;
        }

        void CurrentCamera_OnImageCaptured(object sender, CameraEventArgs e)
        {
            //Giving the feed of the camera to the picturepox
            variables.Image = manager.CurrentCamera.GetCurrentImage();
            pictureBox_Original.Image = variables.Image;
            //System.Timers.Timer moveTimer = new System.Timers.Timer();
            //moveTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            //moveTimer.Interval = 500;
            //moveTimer.Enabled = true;
        }

        private void button_LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = " Bitmap (.bmp)|*.bmp| JPEG (.jpg)|*.jpg| All Files (*.*)|*.*";
            string curdirectory = Environment.CurrentDirectory;
            curdirectory = Path.Combine(curdirectory, @"bmp");
            dlg.InitialDirectory = curdirectory;
            

            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
                variables.Image= Functions.LoadImage(dlg.FileName);

            pictureBox_Original.Image = variables.Image;
            button_Gaussian.Enabled = true;
            button_Complete.Enabled = true;

        }

        private void button_Compute_Click(object sender, EventArgs e)
        {
            variables.Arrays_2DChannels = Functions.ReadImage(variables.Image);

            variables.Arrays_GaussianBlurred = variables.Arrays_2DChannels;

            if (checkBox_GaussianBlur.Checked)
                for (int i = 0; i < int.Parse(textBox_GaussianBlur.Text); i++)
                    for (int j = 0; j < variables.Arrays_2DChannels.Count; j++)
                        variables.Arrays_GaussianBlurred[j]= Functions.GaussianMasking(variables.Mask_Gaussian2D, variables.Arrays_GaussianBlurred[j]);

            pictureBox_Grayscale.Image = Functions.OutputImage(variables.Arrays_2DChannels[0], variables.ImageData);
            button_SaveGaussian.Enabled = pictureBox_Grayscale.Image != null;
            button_Graph.Enabled = true;

        }

        private void checkBox_GuassianBlur_CheckedChanged(object sender, EventArgs e)
        {
            textBox_GaussianBlur.Enabled = !textBox_GaussianBlur.Enabled;
            textBox_GaussianBlur.Text = "1";
        }

        private void button_Graph_Click(object sender, EventArgs e)
        {
            variables.Arrays_OriginalChannels.Clear();
            variables.Arrays_BlurredChannels.Clear();
            for (int i =0; i < chart_GrayscaleChannel.Series.Count;i++)
                chart_GrayscaleChannel.Series[i].Points.Clear();

            for (int i = 0; i < 4; i++)
                variables.Arrays_OriginalChannels.Add(Functions.ArrayConversion2Dto1D(variables.Arrays_GaussianBlurred[i]));
            
            for (int i = 1; i < variables.Arrays_OriginalChannels.Count; i++)
            {
                chart_Channels.Series[i - 1].Points.Clear();
                for (int j = 0; j < variables.Arrays_OriginalChannels[1].Length; j++)
                    chart_Channels.Series[i - 1].Points.AddXY(j, variables.Arrays_OriginalChannels[i][j]);
            }
            for (int i = 0; i < variables.Arrays_OriginalChannels[0].Length; i++)
                chart_GrayscaleChannel.Series["Grayscale"].Points.AddXY(i, variables.Arrays_OriginalChannels[0][i]);
            
            variables.Arrays_BlurredChannels = variables.Arrays_OriginalChannels;
            
            button_Smooth.Enabled = true;
            textBox_GaussianMask.Text = "6";
        }

        private void button_Smooth_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < int.Parse(textBox_GaussianMask.Text); k++)
            {
                variables.List_Maxima.Clear();
                variables.List_Minima.Clear();

                for (int i = 0; i < chart_GrayscaleChannel.Series.Count; i++)
                    chart_GrayscaleChannel.Series[i].Points.Clear();

                for (int i = 0; i < variables.Arrays_BlurredChannels.Count; i++)
                {
                    for (int j = 0; j < 10; j++)
                        variables.Arrays_BlurredChannels[i] = Functions.Masking1D(variables.Mask_Gaussian1D, variables.Arrays_BlurredChannels[i]);
                    Tuple<List<int>, List<int>> tempTuple;
                    tempTuple = Functions.FindExtrema(variables.Arrays_BlurredChannels[i]);
                    variables.List_Maxima.Add(tempTuple.Item1);
                    variables.List_Minima.Add(tempTuple.Item2);
                }
            }

            for (int i = 1; i < variables.Arrays_BlurredChannels.Count; i++)
            {
                chart_Channels.Series[i - 1].Points.Clear();
                for (int j = 0; j < variables.Arrays_BlurredChannels[1].Length; j++)
                    chart_Channels.Series[i - 1].Points.AddXY(j, variables.Arrays_BlurredChannels[i][j]);
            }

            for (int i = 0; i < variables.Arrays_BlurredChannels[0].Length; i++)
                chart_GrayscaleChannel.Series["Grayscale"].Points.AddXY(i, variables.Arrays_BlurredChannels[0][i]);

            for (int i = 0; i < variables.List_Maxima[0].Count; i++)
                chart_GrayscaleChannel.Series[1].Points.AddXY(variables.List_Maxima[0][i], variables.Arrays_BlurredChannels[0][variables.List_Maxima[0][i]]);

            for (int i = 0; i < variables.List_Minima[0].Count; i++)
                chart_GrayscaleChannel.Series[2].Points.AddXY(variables.List_Minima[0][i], variables.Arrays_BlurredChannels[0][variables.List_Minima[0][i]]);



            variables.Arrays_RangeOfInterest.Clear();
            for (int i = 0; i < variables.List_Maxima.Count; i++)
                variables.Arrays_RangeOfInterest.Add(Functions.FindRangeOfInterest(variables.List_Maxima[i], variables.List_Minima[i], variables.Arrays_2DChannels[i]));

            variables.Array_Segmented = Functions.FindCrossSection(variables.Arrays_RangeOfInterest, variables.Arrays_2DChannels);
            pictureBox_Segmentation.Image = Functions.OutputImage(variables.Array_Segmented, variables.ImageData);

            button_SaveSegmentation.Enabled = pictureBox_Segmentation != null;


            button_FindLine.Enabled = true;

        }

        private void button_SaveGaussian_Click(object sender, EventArgs e)
        {
            pictureBox_Grayscale.Image.Save("bmp/export/Gaussian.bmp");
        }

        private void button_SaveSegmentation_Click(object sender, EventArgs e)
        {
            pictureBox_Segmentation.Image.Save("bmp/export/Segmentation.bmp");
        }

        private void button_SaveLine_Click(object sender, EventArgs e)
        {
            pictureBox_CenterLine.Image.Save("bmp/export/CenterLine.bmp");
        }

        private void button_FindLine_Click(object sender, EventArgs e)
        {
            Tuple<byte[,], List<int[]>> Tuple_GAOI = Functions.GrowAreaOfInterest2(variables.Array_Segmented);
            variables.Arrays_Edge = Tuple_GAOI.Item2;
            Tuple<byte[,], double[]> Tuple_FL = Functions.FindLine(variables.Arrays_Edge, Tuple_GAOI.Item1);
            variables.equation = Tuple_FL.Item2;
            pictureBox_CenterLine.Image = Functions.OutputImage(Tuple_FL.Item1, variables.ImageData);
            button_SaveLine.Enabled = pictureBox_CenterLine != null;
            textBox_Slope.Text = variables.equation[0].ToString();
            textBox_Intercept.Text = variables.equation[1].ToString();
        }

        private void button_Complete_Click(object sender, EventArgs e)
        {
            variables.Arrays_2DChannels = Functions.ReadImage(variables.Image);

            variables.Arrays_GaussianBlurred = variables.Arrays_2DChannels;

            if (checkBox_GaussianBlur.Checked)
                for (int i = 0; i < int.Parse(textBox_GaussianBlur.Text); i++)
                    for (int j = 0; j < variables.Arrays_2DChannels.Count; j++)
                        variables.Arrays_GaussianBlurred[j] = Functions.GaussianMasking(variables.Mask_Gaussian2D, variables.Arrays_GaussianBlurred[j]);

            pictureBox_Grayscale.Image = Functions.OutputImage(variables.Arrays_2DChannels[0], variables.ImageData);
            button_SaveGaussian.Enabled = pictureBox_Grayscale.Image != null;
            button_Graph.Enabled = true;


            variables.Arrays_OriginalChannels.Clear();
            variables.Arrays_BlurredChannels.Clear();
            for (int i = 0; i < chart_GrayscaleChannel.Series.Count; i++)
                chart_GrayscaleChannel.Series[i].Points.Clear();

            for (int i = 0; i < 4; i++)
                variables.Arrays_OriginalChannels.Add(Functions.ArrayConversion2Dto1D(variables.Arrays_GaussianBlurred[i]));

            for (int i = 1; i < variables.Arrays_OriginalChannels.Count; i++)
            {
                chart_Channels.Series[i - 1].Points.Clear();
                for (int j = 0; j < variables.Arrays_OriginalChannels[1].Length; j++)
                    chart_Channels.Series[i - 1].Points.AddXY(j, variables.Arrays_OriginalChannels[i][j]);
            }
            for (int i = 0; i < variables.Arrays_OriginalChannels[0].Length; i++)
                chart_GrayscaleChannel.Series["Grayscale"].Points.AddXY(i, variables.Arrays_OriginalChannels[0][i]);

            variables.Arrays_BlurredChannels = variables.Arrays_OriginalChannels;

            button_Smooth.Enabled = true;
            textBox_GaussianMask.Text = "6";




            for (int k = 0; k < int.Parse(textBox_GaussianMask.Text); k++)
            {
                variables.List_Maxima.Clear();
                variables.List_Minima.Clear();

                for (int i = 0; i < chart_GrayscaleChannel.Series.Count; i++)
                    chart_GrayscaleChannel.Series[i].Points.Clear();

                for (int i = 0; i < variables.Arrays_BlurredChannels.Count; i++)
                {
                    for (int j = 0; j < 10; j++)
                        variables.Arrays_BlurredChannels[i] = Functions.Masking1D(variables.Mask_Gaussian1D, variables.Arrays_BlurredChannels[i]);
                    Tuple<List<int>, List<int>> tempTuple;
                    tempTuple = Functions.FindExtrema(variables.Arrays_BlurredChannels[i]);
                    variables.List_Maxima.Add(tempTuple.Item1);
                    variables.List_Minima.Add(tempTuple.Item2);
                }
            }

            for (int i = 1; i < variables.Arrays_BlurredChannels.Count; i++)
            {
                chart_Channels.Series[i - 1].Points.Clear();
                for (int j = 0; j < variables.Arrays_BlurredChannels[1].Length; j++)
                    chart_Channels.Series[i - 1].Points.AddXY(j, variables.Arrays_BlurredChannels[i][j]);
            }

            for (int i = 0; i < variables.Arrays_BlurredChannels[0].Length; i++)
                chart_GrayscaleChannel.Series["Grayscale"].Points.AddXY(i, variables.Arrays_BlurredChannels[0][i]);

            for (int i = 0; i < variables.List_Maxima[0].Count; i++)
                chart_GrayscaleChannel.Series[1].Points.AddXY(variables.List_Maxima[0][i], variables.Arrays_BlurredChannels[0][variables.List_Maxima[0][i]]);

            for (int i = 0; i < variables.List_Minima[0].Count; i++)
                chart_GrayscaleChannel.Series[2].Points.AddXY(variables.List_Minima[0][i], variables.Arrays_BlurredChannels[0][variables.List_Minima[0][i]]);



            variables.Arrays_RangeOfInterest.Clear();
            for (int i = 0; i < variables.List_Maxima.Count; i++)
                variables.Arrays_RangeOfInterest.Add(Functions.FindRangeOfInterest(variables.List_Maxima[i], variables.List_Minima[i], variables.Arrays_2DChannels[i]));

            variables.Array_Segmented = Functions.FindCrossSection(variables.Arrays_RangeOfInterest, variables.Arrays_2DChannels);
            pictureBox_Segmentation.Image = Functions.OutputImage(variables.Array_Segmented, variables.ImageData);

            button_SaveSegmentation.Enabled = pictureBox_Segmentation != null;


            button_FindLine.Enabled = true;





            Tuple<byte[,], List<int[]>> Tuple_GAOI = Functions.GrowAreaOfInterest2(variables.Array_Segmented);
            variables.Arrays_Edge = Tuple_GAOI.Item2;
            Tuple<byte[,], double[]> Tuple_FL = Functions.FindLine(variables.Arrays_Edge, Tuple_GAOI.Item1);
            variables.equation = Tuple_FL.Item2;
            pictureBox_CenterLine.Image = Functions.OutputImage(Tuple_FL.Item1, variables.ImageData);
            button_SaveLine.Enabled = pictureBox_CenterLine != null;
            textBox_Slope.Text = variables.equation[0].ToString();
            textBox_Intercept.Text = variables.equation[1].ToString();
            
            MoveCursor();

        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void MoveCursor()
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = lastLocation; //690 250
            Cursor.Clip = new Rectangle(new Point(0, 0), new Size(1366, 768));

            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)lastLocation.X, (uint)lastLocation.Y, 0, 0);
            Cursor.Position = new Point((variables.equation[0] < 8 && variables.equation[0] > 0) ? 480 : 
                (variables.equation[0] > -8 && variables.equation[0] < 0) ? 440 : 
                460, 250);
            lastLocation = Cursor.Position;
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)lastLocation.X, (uint)lastLocation.Y, 0, 0);




            //if (variables.equation[0] < 8 && variables.equation[0] > 0)
            //{
            //    mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)lastLocation.X, (uint)lastLocation.Y, 0, 0);
            //    Cursor.Position = new Point(480, 280);
            //    lastLocation = Cursor.Position;
            //    mouse_event(MOUSEEVENTF_LEFTUP, (uint)lastLocation.X, (uint)lastLocation.Y, 0, 0);
            //}
            //else if (variables.equation[0] > -8 && variables.equation[0] < 0)
            //{
            //    mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)lastLocation.X, (uint)lastLocation.Y, 0, 0);
            //    Cursor.Position = new Point(440, 250);
            //    lastLocation = new Point(440, 250);
            //}
            //else
            //{
            //    //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 690, 250, 0, 0);

            //    mouse_event(MOUSEEVENTF_LEFTUP, 460, 250, 0, 0);
            //    //Cursor.Position = new Point(460, 250);
            //    lastLocation = new Point(460, 250);
            //}



            //            Process[] procs = Process.GetProcessesByName(ProcWindow);
            //            foreach (Process proc in procs)
            //            {
            //                if (proc.ProcessName == ProcWindow)
            //                {
            //                    SwitchToThisWindow(proc.MainWindowHandle);
            //                    break;
            //                }
            //;
            //            }
        }
    
        //private void OnTimedEvent(object source,  System.Timers.ElapsedEventArgs e)
        //{
        //    variables.Arrays_2DChannels = Functions.ReadImage(variables.Image);

        //    variables.Arrays_GaussianBlurred = variables.Arrays_2DChannels;

        //    if (checkBox_GaussianBlur.Checked)
        //        for (int i = 0; i < int.Parse(textBox_GaussianBlur.Text); i++)
        //            for (int j = 0; j < variables.Arrays_2DChannels.Count; j++)
        //                variables.Arrays_GaussianBlurred[j] = Functions.GaussianMasking(variables.Mask_Gaussian2D, variables.Arrays_GaussianBlurred[j]);

        //    pictureBox_Grayscale.Image = Functions.OutputImage(variables.Arrays_2DChannels[0], variables.ImageData);
        //    button_SaveGaussian.Enabled = pictureBox_Grayscale.Image != null;



        //    variables.Arrays_OriginalChannels.Clear();
        //    variables.Arrays_BlurredChannels.Clear();
        //    for (int i = 0; i < chart_GrayscaleChannel.Series.Count; i++)
        //        chart_GrayscaleChannel.Series[i].Points.Clear();

        //    for (int i = 0; i < 4; i++)
        //        variables.Arrays_OriginalChannels.Add(Functions.ArrayConversion2Dto1D(variables.Arrays_GaussianBlurred[i]));

        //    for (int i = 1; i < variables.Arrays_OriginalChannels.Count; i++)
        //    {
        //        chart_Channels.Series[i - 1].Points.Clear();
        //        for (int j = 0; j < variables.Arrays_OriginalChannels[1].Length; j++)
        //            chart_Channels.Series[i - 1].Points.AddXY(j, variables.Arrays_OriginalChannels[i][j]);
        //    }
        //    for (int i = 0; i < variables.Arrays_OriginalChannels[0].Length; i++)
        //        chart_GrayscaleChannel.Series["Grayscale Channel"].Points.AddXY(i, variables.Arrays_OriginalChannels[0][i]);

        //    variables.Arrays_BlurredChannels = variables.Arrays_OriginalChannels;





        //    for (int k = 0; k < 6; k++)
        //    {
        //        variables.List_Maxima.Clear();
        //        variables.List_Minima.Clear();

        //        for (int i = 0; i < chart_GrayscaleChannel.Series.Count; i++)
        //            chart_GrayscaleChannel.Series[i].Points.Clear();

        //        for (int i = 0; i < variables.Arrays_BlurredChannels.Count; i++)
        //        {
        //            for (int j = 0; j < 10; j++)
        //                variables.Arrays_BlurredChannels[i] = Functions.Masking1D(variables.Mask_Gaussian1D, variables.Arrays_BlurredChannels[i]);
        //            Tuple<List<int>, List<int>> tempTuple;
        //            tempTuple = Functions.FindExtrema(variables.Arrays_BlurredChannels[i]);
        //            variables.List_Maxima.Add(tempTuple.Item1);
        //            variables.List_Minima.Add(tempTuple.Item2);
        //        }
        //    }

        //    for (int i = 1; i < variables.Arrays_BlurredChannels.Count; i++)
        //    {
        //        chart_Channels.Series[i - 1].Points.Clear();
        //        for (int j = 0; j < variables.Arrays_BlurredChannels[1].Length; j++)
        //            chart_Channels.Series[i - 1].Points.AddXY(j, variables.Arrays_BlurredChannels[i][j]);
        //    }

        //    for (int i = 0; i < variables.Arrays_BlurredChannels[0].Length; i++)
        //        chart_GrayscaleChannel.Series["Grayscale Channel"].Points.AddXY(i, variables.Arrays_BlurredChannels[0][i]);

        //    for (int i = 0; i < variables.List_Maxima[0].Count; i++)
        //        chart_GrayscaleChannel.Series[1].Points.AddXY(variables.List_Maxima[0][i], variables.Arrays_BlurredChannels[0][variables.List_Maxima[0][i]]);

        //    for (int i = 0; i < variables.List_Minima[0].Count; i++)
        //        chart_GrayscaleChannel.Series[2].Points.AddXY(variables.List_Minima[0][i], variables.Arrays_BlurredChannels[0][variables.List_Minima[0][i]]);



        //    variables.Arrays_RangeOfInterest.Clear();
        //    for (int i = 0; i < variables.List_Maxima.Count; i++)
        //        variables.Arrays_RangeOfInterest.Add(Functions.FindRangeOfInterest(variables.List_Maxima[i], variables.List_Minima[i], variables.Arrays_2DChannels[i]));

        //    variables.Array_Segmented = Functions.FindCrossSection(variables.Arrays_RangeOfInterest, variables.Arrays_2DChannels);
        //    pictureBox_Segmentation.Image = Functions.OutputImage(variables.Array_Segmented, variables.ImageData);

        //    button_SaveSegmentation.Enabled = pictureBox_Segmentation != null;







        //    Tuple<byte[,], List<int[]>> Tuple_GAOI = Functions.GrowAreaOfInterest2(variables.Array_Segmented);
        //    variables.Arrays_Edge = Tuple_GAOI.Item2;
        //    Tuple<byte[,], double[]> Tuple_FL = Functions.FindLine(variables.Arrays_Edge, Tuple_GAOI.Item1);
        //    variables.equation = Tuple_FL.Item2;
        //    pictureBox_CenterLine.Image = Functions.OutputImage(Tuple_FL.Item1, variables.ImageData);
        //    button_SaveLine.Enabled = pictureBox_CenterLine != null;
        //    textBox_Slope.Text = variables.equation[0].ToString();
        //    textBox_Intercept.Text = variables.equation[1].ToString();

        // //   MoveCursor();
        //}

    }
}
