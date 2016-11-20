using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using TouchlessLib;


namespace Visual_Lane_Detection
{
    static public class Functions
    {
        #region Image Functions
        
        static public Bitmap InputImage(string path)
        {
            Bitmap Image = new Bitmap(path);
            return Image;

        }

        static public Bitmap LoadImage(string path)
        {
            Bitmap Image = new Bitmap(path);
            return Image;
        }

        static public List<byte[,]> ReadImage(Bitmap Image)
        {
            byte[] byteArray;
            BitmapData ImageData = Image.LockBits(
                new Rectangle(0, 0, Image.Width, Image.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byteArray = new byte[ImageData.Stride * ImageData.Height]; //where stride = width*4

            List<byte[,]> Arrays_2DChannels = new List<byte[,]>();

            for (int i = 0; i < 4; i++)
                Arrays_2DChannels.Add(new byte[ImageData.Width, ImageData.Height]);

            Marshal.Copy(ImageData.Scan0, byteArray, 0, byteArray.Length); // 6th function overload

            for (int i = 0; i < byteArray.Length; i += 4)
            {
                for (int j = 0; j < 3; j++)
                    Arrays_2DChannels[j + 1][(i >> 2) % ImageData.Width, ((i >> 2) - ((i >> 2) % ImageData.Width)) / ImageData.Width] = byteArray[i + j];

                Arrays_2DChannels[0][(i >> 2) % ImageData.Width, ((i >> 2) - ((i >> 2) % ImageData.Width)) / ImageData.Width] = (byte)Math.Round(.11d * byteArray[i] + .59d * byteArray[i + 1] + .3d * byteArray[i + 2], 0);
            }
            Image.UnlockBits(ImageData);

            return Arrays_2DChannels;
            
        }

        static public byte[,] GaussianMasking(sbyte[,] Mask_2D, byte[,] Array_Input)
        {
            int maskXLength = Mask_2D.GetUpperBound(0) + 1, maskYLength = Mask_2D.GetUpperBound(1) + 1;
            byte[,] Array_GaussianBlurred = new byte[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];

            int totalMask = 0;

            for (int y = 0; y < maskYLength; y++)
                for (int x = 0; x < maskXLength; x++)
                    totalMask += Mask_2D[x, y];

            for (int y = ((maskYLength - 1) >> 1); y < Array_Input.GetUpperBound(1) + 1 - ((maskYLength - 1) >> 1); y++)
                for (int x = ((maskXLength - 1) >> 1); x < Array_Input.GetUpperBound(0) + 1 - ((maskXLength - 1) >> 1); x ++)
                {
                    float accumulator = 0;
                    for (int j = 0; j < maskYLength; j++)
                        for (int i = 0; i < maskXLength; i ++)
                            accumulator += Mask_2D[i, j] * Array_Input[x - ((maskXLength - 1) >> 1) + i, y - ((maskYLength - 1) >> 1) + j];
                    Array_GaussianBlurred[x, y] = (byte)Math.Round((accumulator / totalMask), MidpointRounding.AwayFromZero);
                }
            return Array_GaussianBlurred;
        }

        static public byte[,] EdgeDetectionMasking(sbyte[,] Mask_X, sbyte[,] Mask_Y, byte[,] Array_Input)
        {
            byte[,] Array_Convolved = new byte[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];
            for (int y = ((Mask_Y.GetUpperBound(1) + 1 - 1) >> 1); y < Array_Input.GetUpperBound(1) + 1 - ((Mask_Y.GetUpperBound(1) + 1 - 1) >> 1); y++)
                for (int x = ((Mask_X.GetUpperBound(0) + 1 - 1) >> 1); x < Array_Input.GetUpperBound(0) + 1 - ((Mask_X.GetUpperBound(0) + 1 - 1) >> 1); x ++)
                {
                    float accumulatorX = 0f;
                    float accumulatorY = 0f;
                    for (int j = 0; j < Mask_Y.GetUpperBound(1) + 1; j++)
                        for (int i = 0; i < (Mask_X.GetUpperBound(0) + 1); i ++)
                        {
                            accumulatorX += Mask_X[i, j] * Array_Input[x - ((Mask_X.GetUpperBound(0) + 1 - 1) >> 1) + i, y - ((Mask_X.GetUpperBound(1) + 1 - 1) >> 1) + j];
                            accumulatorY += Mask_Y[i, j] * Array_Input[x - ((Mask_Y.GetUpperBound(0) + 1 - 1) >> 1) + i, y - ((Mask_Y.GetUpperBound(1) + 1 - 1) >> 1) + j];

                        }
                    Array_Convolved[x, y] = (byte)Math.Sqrt(accumulatorX * accumulatorX + accumulatorY * accumulatorY);
                }
            return Array_Convolved;
        }

        static public Bitmap OutputImage(byte[,] Array_Input, BitmapData ImageData)
        {
            byte[] finishedArray = new byte[(Array_Input.GetUpperBound(0) + 1)* 4 * (Array_Input.GetUpperBound(1) + 1)];

            for (int i = 0; i < Array_Input.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < Array_Input.GetUpperBound(1) + 1; j++)
                {
                    for (int k = 0; k < 3; k++)
                        finishedArray[((j * (Array_Input.GetUpperBound(0) + 1) + i) <<2) + k] = Array_Input[i, j];
                    finishedArray[((j * (Array_Input.GetUpperBound(0) + 1) + i) << 2) + 3] = 255;
                }

            Bitmap tempBitmap = new Bitmap((Array_Input.GetUpperBound(0) + 1), Array_Input.GetUpperBound(1) + 1);
            
            ImageData = tempBitmap.LockBits(
                new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);
            Marshal.Copy(finishedArray, 0, ImageData.Scan0, finishedArray.Length);
            tempBitmap.UnlockBits(ImageData);
            return tempBitmap;
        }

        static public byte[,] FindCrossSection(List<byte[]> Arrays_RangeOfInterest, List<byte[,]> Arrays_2D)
        {
            byte[,] Array_Image = new byte[Arrays_2D[0].GetUpperBound(0) + 1, Arrays_2D[0].GetUpperBound(1) + 1];
            for (int y = 0; y < Arrays_2D[0].GetUpperBound(1) + 1; y++)
                for (int x = 0; x < (Arrays_2D[0].GetUpperBound(0) + 1); x++)
                    Array_Image[x, y] = 255;
            for (int i = 0; i < 4; i++)
                for (int y = 0; y < Arrays_2D[i].GetUpperBound(1) + 1; y++)
                    for (int x = 0; x < (Arrays_2D[i].GetUpperBound(0) + 1); x++)
                        if (!(Arrays_2D[i][x, y] < Arrays_RangeOfInterest[i][1] && Arrays_2D[i][x, y] > Arrays_RangeOfInterest[i][0]))
                            Array_Image[x, y] = 0;

            return Array_Image;
        }

        static public Tuple<byte[,], List<int[]>> GrowAreaOfInterest2(byte[,] Array_Input)
        {
            
            byte[,] Array_Grown = new byte[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];
            List<int[]> ToCheck = new List<int[]>();
            List<int[]> Array_Edge = new List<int[]>();

            bool[,] Bool_Edge = new bool[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];
            bool[,] Bool_Checked = new bool[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];

            ToCheck.Add(new int[2] { (Array_Input.GetUpperBound(0) + 1) >> 1, (int)Math.Round(((Array_Input.GetUpperBound(1) + 1) * .95d), 0, MidpointRounding.AwayFromZero) });
            Bool_Checked[ToCheck[0][0], ToCheck[0][1]] = true;
            List<int[]> ArPnts= new List<int[]>();

            while (ToCheck.Count > 0)
            {
                ArPnts.Clear();

                ArPnts.Add(new int[2] { ToCheck[0][0] + 1, ToCheck[0][1] });
                ArPnts.Add(new int[2] { ToCheck[0][0] - 1, ToCheck[0][1] });
                ArPnts.Add(new int[2] { ToCheck[0][0], ToCheck[0][1] + 1 });
                ArPnts.Add(new int[2] { ToCheck[0][0], ToCheck[0][1] - 1 });

                for (int i = 0; i < ArPnts.Count; i++)
                    if (Array_Input[ArPnts[i][0], ArPnts[i][1]] == 255)
                    {
                        if (!Bool_Checked[ArPnts[i][0], ArPnts[i][1]])
                        {
                            ToCheck.Add(ArPnts[i]);
                            //Array_Grown[ArPnts[i][0], ArPnts[i][1]] = 255;
                            Bool_Checked[ArPnts[i][0], ArPnts[i][1]] = true;
                        }
                    }
                    else if (!Bool_Edge[ArPnts[i][0], ArPnts[i][1]] && !Bool_Checked[ArPnts[i][0], ArPnts[i][1]])
                    {
                        Array_Edge.Add(ArPnts[i]);
                        Array_Grown[ArPnts[i][0], ArPnts[i][1]] = 255;

                        Bool_Edge[ArPnts[i][0], ArPnts[i][1]] = true;
                        Bool_Checked[ArPnts[i][0], ArPnts[i][1]] = true;
                        
                    }
                ToCheck.RemoveAt(0);
            }

            Array_Edge.Sort(delegate(int[] x, int[] y)
            {
                if (x[1] == y[1]) return x[0].CompareTo(y[0]);
                else return x[1].CompareTo(y[1]);
            });
            return Tuple.Create<byte[,], List<int[]>>(Array_Grown, Array_Edge);
        }

        static public Tuple<byte[,],List<int[]>> GrowAreaOfInterest(byte[,] Array_Input)
        {
            byte[,] Array_Grown = new byte[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];
            List<int[]> Checked = new List<int[]>();
            List<int[]> ToCheck = new List<int[]>();
            List<int[]> Array_Edge = new List<int[]>();
            
            bool[,] Bool_Edge = new bool[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];
            bool[,] Bool_Checked = new bool[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];

            ToCheck.Add(new int[2] {(Array_Input.GetUpperBound(0) + 1) >> 1, (int)Math.Round(((Array_Input.GetUpperBound(1) + 1) * .95d), 0, MidpointRounding.AwayFromZero)});
            while(ToCheck.Count > 0)
            {
                List<int[]> ArPnts = new List<int[]>();
                
                ArPnts.Add(new int[2] {ToCheck[0][0] + 1, ToCheck[0][1]});
                ArPnts.Add(new int[2] {ToCheck[0][0] - 1, ToCheck[0][1]});
                ArPnts.Add(new int[2] {ToCheck[0][0], ToCheck[0][1] + 1});
                ArPnts.Add(new int[2] {ToCheck[0][0], ToCheck[0][1] - 1});

                for (int i = 0; i < ArPnts.Count; i++)
                    if (Array_Input[ArPnts[i][0], ArPnts[i][1]] == 255)
                    {
                        if (!Checked.Exists(x => x[0] == ArPnts[i][0] && x[1] == ArPnts[i][1]) && !ToCheck.Exists(x => x[0] == ArPnts[i][0] && x[1] == ArPnts[i][1]))
                        {
                            ToCheck.Add(ArPnts[i]);
                            //Array_Grown[ArPnts[i][0], ArPnts[i][1]] = 255;
                        }
                    }
                    else if (!Array_Edge.Exists((x => x[0] == ArPnts[i][0] && x[1] == ArPnts[i][1])))
                    {
                        Array_Edge.Add(ArPnts[i]);
                        Array_Grown[ArPnts[i][0], ArPnts[i][1]] = 255;
                    }

                Array_Edge.Sort(delegate(int[] x, int[] y)
                {
                    if (x[1] == y[1]) return x[0].CompareTo(y[0]);
                    else return x[1].CompareTo(y[1]);
                });

                Checked.Add(ToCheck[0]);
                ToCheck.RemoveAt(0);
            }
            return Tuple.Create<byte[,], List<int[]>>(Array_Grown, Array_Edge);
        }


        static public Tuple<byte[,], double[]> FindLine(List<int[]> Array_Edge, byte[,] Array_Input)    
        {
            byte[,] Array_Grown = new byte[Array_Input.GetUpperBound(0) + 1, Array_Input.GetUpperBound(1) + 1];
            bool beginningHitEdge=false, endHitEdge = false;

            double m, b;

            List<int[]> leftSide = new List<int[]>(), rightSide = new List<int[]>(), centerSide = new List<int[]>();

            for (int i = Array_Edge[0][1]; !beginningHitEdge && i < Array_Input.GetUpperBound(1) + 1 - 5; i++)
                for (int x = 0; x < Array_Edge.Count; x++)
                    if (Array_Edge[x][1] == i)
                    {
                        beginningHitEdge = Array_Edge[x][0] < 10;
                        leftSide.Add(new int[2] {Array_Edge[x][0], i });
                        break;
                    }
                //if (Array_Edge.Exists(item => item[1] == i))
                //{
                //    int x_value = Array_Edge.Find(item => item[1] == i)[0];
                //    //Array_Grown[x_value, i] = 255;
                //    beginningHitEdge = x_value < 10;

                //    leftSide.Add(new int[2] { x_value, i });
                //}



            for (int i = Array_Edge[0][1]; !endHitEdge && i < Array_Input.GetUpperBound(1) + 1 - 5; i++)
                for (int x = Array_Edge.Count-1; x >= 0; x--)
                    if (Array_Edge[x][1] == i)
                    {
                        endHitEdge = Array_Edge[x][0] > Array_Input.GetUpperBound(0) + 1 - 10;
                        rightSide.Add(new int[2] { Array_Edge[x][0], i });

                        for (int j = 0; j < leftSide.Count; j++)
                            if (leftSide[j][1] == i)
                            {
                                centerSide.Add(new int[2] { (leftSide[j][0] + Array_Edge[x][0]) >> 1, i });
                                Array_Grown[centerSide[centerSide.Count - 1][0], centerSide[centerSide.Count - 1][1]] = 255;
                                Debug.WriteLine(((leftSide[j][0] + Array_Edge[x][0]) >> 1).ToString() + "," + i.ToString());
                                break;
                            }
                        break;
                    }
                //if (Array_Edge.Exists(item => item[1] == i))
                //{
                //    int x_value = Array_Edge.FindLast(item => item[1] == i)[0];
                //    //Array_Grown[x_value, i] = 255;
                //    endHitEdge = x_value > Array_Input.GetUpperBound(0) + 1 - 10;
                //    rightSide.Add(new int[2] {x_value, i});

                //    if (leftSide.Exists(item => item[1] == i))
                //    {
                //        centerSide.Add(new int[2] { (leftSide.Find(item => item[1] == i)[0] + x_value) >> 1, i });
                //        Array_Grown[centerSide[centerSide.Count-1][0], centerSide[centerSide.Count-1][1]] = 255;
                //    }
                //}
            

            int sumOfYandX = 0, sumOfY =0, sumOfX =0, sumOfXSquared=0;
            for (int i = 0; i < centerSide.Count; i++)
            {
                sumOfYandX += centerSide[i][0] * centerSide[i][1];
                sumOfY += centerSide[i][1];
                sumOfX += centerSide[i][0];
                sumOfXSquared += centerSide[i][0] * centerSide[i][0];
            }

            //m = (float)(sumOfXSquared* sumOfY) / (float)(sumOfXSquared - sumOfX * sumOfX);
            //b = sumOfY - m * sumOfX;

            b = (centerSide.Count * sumOfXSquared - sumOfX * sumOfX) == 0 ? 0 : (float)((sumOfY - sumOfYandX * sumOfX / sumOfXSquared) * sumOfXSquared / (centerSide.Count * sumOfXSquared - sumOfX * sumOfX));
            m = (float)((sumOfYandX - b * sumOfX) / sumOfXSquared);

            //b = (centerSide.Count * sumOfXSquared - sumOfX * sumOfX) == 0 ? 0 : (float)(sumOfY * sumOfXSquared - sumOfYandX * sumOfX) / (centerSide.Count*sumOfXSquared-sumOfX*sumOfX);
            //m = (float)(centerSide.Count * sumOfYandX - sumOfX * sumOfY) / (centerSide.Count * sumOfXSquared - sumOfX * sumOfX);

            //m = (sumOfY - centerSide.Count * sumOfYandX / sumOfX) / (sumOfX - centerSide.Count * sumOfXSquared / sumOfX);
            //b = (sumOfYandX - sumOfXSquared * m) / sumOfX;

            return Tuple.Create<byte[,], double[]>(Array_Grown,new double[2]{m,b});
        }
        #endregion


        #region Graph Functions
        static public double[] ArrayConversion2Dto1D(byte[,] Array_2D)
        {
            double[] Array_OriginalChannel = new double[256];
            //byte[,] graphArray = new byte[256, 100];
            for (int y = 0; y < Array_2D.GetUpperBound(1) + 1; y++)
                for (int x = 0; x < Array_2D.GetUpperBound(0) + 1; x += 4)
                    Array_OriginalChannel[Array_2D[x, y]]++;
            return Array_OriginalChannel;
        }

        static public double[] Masking1D(double[] Mask_1D, double[] Array_InputChannel)
        {
            double[] Array_BlurredChannel = new double[Array_InputChannel.Length];
            for (int i = (Mask_1D.Length - 1) >> 1; i < Array_InputChannel.Length - ((Mask_1D.Length - 1) >> 1); i++)
            {
                double accumulator = 0;
                for (int j = 0; j < Mask_1D.Length; j++)
                    accumulator += Mask_1D[j] * Array_InputChannel[i - ((Mask_1D.Length - 1) >> 1) + j];
                Array_BlurredChannel[i] = accumulator;
            }
            return Array_BlurredChannel;
        }

        static public Tuple<List<int>, List<int>> FindExtrema(double[] Array_InputChannel)
        {
            List<int> List_Maxima = new List<int>();
            List<int> List_Minima = new List<int>();

            bool wasGoingUp = false;
            bool wasGoingDown = false;

            for (int i = 1; i < Array_InputChannel.Length; i++)
                if (Array_InputChannel[i] > Array_InputChannel[i - 1])
                    if (wasGoingUp)
                        List_Maxima[List_Maxima.Count - 1] = i;
                    else
                    {
                        List_Minima.Add(i - 1);
                        wasGoingUp = true;
                        wasGoingDown = false;
                        List_Maxima.Add(i);
                    }

                else if (Array_InputChannel[i] < Array_InputChannel[i - 1])
                    if (wasGoingDown)
                        List_Minima[List_Minima.Count - 1] = i;
                    else
                    {
                        List_Maxima.Add(i - 1);
                        wasGoingDown = true;
                        wasGoingUp = false;
                        List_Minima.Add(i);
                    }
                else
                    wasGoingUp = wasGoingDown = false;
            return Tuple.Create<List<int>, List<int>>(List_Maxima, List_Minima);
        }

        static public byte[] FindRangeOfInterest(List<int> List_Maxima, List<int> List_Minima, byte[,] Array_InputChannel)
        {
            byte[] RangeOfInterest = new byte[2];
            for (int i = 0; i < List_Minima.Count; i++)
                if (Array_InputChannel[(Array_InputChannel.GetUpperBound(0) + 1) >> 1, (int)Math.Round(((Array_InputChannel.GetUpperBound(1) + 1) * .95d), 0, MidpointRounding.AwayFromZero)] < List_Minima[i])
                {
                    if (i == 0)
                        RangeOfInterest[0] = (byte)List_Minima[0];
                    else
                        RangeOfInterest[0] = (byte)List_Minima[i - 1];
                    RangeOfInterest[1] = (byte)List_Minima[i];
                    break;
                }
            return RangeOfInterest;
        }
           
        #endregion
    }
}
