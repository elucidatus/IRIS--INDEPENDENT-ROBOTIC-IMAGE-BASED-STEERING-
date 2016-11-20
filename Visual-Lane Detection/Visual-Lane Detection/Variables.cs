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
using System.Runtime.InteropServices;

namespace Visual_Lane_Detection
{
    class Variables
    {
        #region Image Variables
        public BitmapData ImageData = new BitmapData(); 
        public Bitmap Image;
        public List<byte[,]> Arrays_2DChannels = new List<byte[,]>(),
            Arrays_GaussianBlurred = new List<byte[,]>();
        public byte[,] Array_Segmented;

        public double[] equation = new double[2] { 0, 0 }; //slope (m), y-intercept (b)

        public List<int[]> Arrays_Edge = new List<int[]>();

        public sbyte[,] Mask_Gaussian2D = new sbyte[5, 5] {
            { 1 , 4 , 7 , 4 , 1 },
            { 4 , 16, 26, 16, 4 },
            { 7 , 26, 41, 26, 7 },
            { 4 , 16, 26, 16, 4 },
            { 1 , 4 , 7 , 4 , 1 }};

        #endregion


        #region Graph Variables
        public List<List<int>> List_Maxima = new List<List<int>>(), List_Minima = new List<List<int>>();   //ORDER IS ALWAYS BGR     
        public List<double[]> Arrays_ToGraph = new List<double[]>();
        public List<double[]> Arrays_OriginalChannels = new List<double[]>(); //ORDER IS ALWAYS Grayscale,BGR
        public List<double[]> Arrays_BlurredChannels = new List<double[]>(); //ORDER IS ALWAYS Grayscale,BGR
        public List<byte[]> Arrays_RangeOfInterest = new List<byte[]>();
        //public byte[,] Array_AreaOfInterest;
        public double[] Mask_Gaussian1D = new double[7] {
            .006d, .061d, .242d, .383d, .242d, .061d, .006d }; 
        #endregion

    }
}
