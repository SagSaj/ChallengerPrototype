
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
namespace TryToOnTopAndImageCatch
{
    class NewClassWorkWithImage
    {
        public static byte SigmaD = 30;
        public static double cR = 0.3, cG = 0.59, cB = 0.11;
        static List<Bitmap> lb;


        static int IntellectualSigma(Color c)
        {

            if (cR * c.R + cG * c.G + cB * c.B < SigmaD)
            {
                return -1;
            }
            else { return 1; }
        }
        public static Bitmap Crop(Bitmap image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;

            // Check if it is a bitmap:
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");

            // Crop the image:
            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            // Release the resources:
            //   image.Dispose();

            return cropBmp;
        }
        public static List<Bitmap> WorkWithDarkImage(Bitmap img, Bitmap curimg)
        {
            lb = new List<Bitmap>();
            bool[,] BoolMass = new bool[img.Width, img.Height];
            //   Bitmap img = (Bitmap)im.Clone();
            for (int i = 0; i < img.Height; i++)
                for (int j = 0; j < img.Width; j++)
                {
                    BoolMass[j, i] = IntellectualSigma(img.GetPixel(j, i)) == 1;
                    if (BoolMass[j, i])
                        img.SetPixel(j, i, Color.FromArgb(255, 255, 255, 255));
                    else
                        img.SetPixel(j, i, Color.FromArgb(0, 0, 0, 0));
                }
            int temp = 0;
            for (int j = 0; j < img.Height; j++)
            {
                bool isSymbolStroke = false;
                for (int i = 0; i < img.Width; i++)
                {
                    if (BoolMass[i, j])
                        isSymbolStroke = true;
                }
                temp++;
                if (!isSymbolStroke)
                {
                    if (temp > 3)
                    {
                        Rectangle rect = new Rectangle(0, j - temp - 1, img.Width, temp + 2);
                        lb.Add(WorkWithDarkImageWord(Crop(curimg, rect)));
                    }
                    temp = 0;
                }
            }


            //if (temp > 2)
            //{
            //    Rectangle rect = new Rectangle(img.Width - temp + 1, 0, temp - 1, img.Height);
            //    lb.Add(Crop(curimg, rect));

            //}
            return lb;
        }
        public static Bitmap WorkWithDarkImageWord(Bitmap img)
        {
            int temp = 0, temp2 = img.Width;
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    int yt = img.GetPixel(i, j).B;
                    if (yt != 0)
                    {
                        temp = i;
                    }
                }
                if (temp != 0)
                    break;

            }
            for (int i = img.Width - 1; i >= 0; i--)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    int yt = img.GetPixel(i, j).B;
                    if (yt != 0)
                    {
                        temp2 = i;
                    }
                }
                if (temp2 != img.Width)
                    break;

            }
            Rectangle rect = new Rectangle(temp, 0, temp2 - temp, img.Height);
            return Crop(img, rect);


        }
        public static Double[,] ConvertImageToMatrix(Bitmap img)
        {
            Double[,] d = new Double[img.Width, img.Height];
            for (int i = 0; i < img.Height; i++)
                for (int j = 0; j < img.Width; j++)
                    d[j, i] = IntellectualSigma(img.GetPixel(j, i));
            return d;
        }
        public static int HashChar(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            int sum = 0;

            // Суммирование всех бит каждого байта
            foreach (byte b in bytes)
                for (int i = 0; i < 8; i++)
                    sum += (b >> i) & 1;
            return sum;
        }
        public static string Hashing(List<string> ss)
        {
            string temp = "";
            foreach (string s in ss)
            {
                int cc = HashChar(s) % 256;
                temp += (char)cc;
            }
            return temp;
        }
        public static string perceptiveHash(Double[,] dm)
        {
            string temp = "";
            int sum = 0, tempNum = 0;
            const int Avg = 8;
            int ddd;
            foreach (Double d in dm)
            {

                if (d <= 0)
                    ddd = 0;
                else
                    ddd = 1;
                sum += ddd * (int)Math.Pow(2, tempNum);
                tempNum++;
                if (tempNum == Avg)
                {
                    tempNum = 0;
                    if (sum != 0)
                        temp += (char)sum;
                    sum = 0;
                }
            }
            if (tempNum > 1)
            {
                tempNum = 0;
                if (sum != 0)
                    temp += (char)sum;
                sum = 0;
            }
            return temp;
        }
        public static string StrongHashing(List<Double[,]> matrixes)
        {
            string temp = "";
            foreach (Double[,] d in matrixes)
                temp += perceptiveHash(d);
            return temp;
        }
        public static byte[] ConnectStringHesh(string h1, string h2)
        {
            byte[] toBytes1 = Encoding.UTF8.GetBytes(h1);
            byte[] toBytes2 = Encoding.UTF8.GetBytes(h2);
            byte[] itog = new byte[Math.Max(toBytes1.Length, toBytes2.Length)];
            for (int i = 0; i < Math.Min(toBytes1.Length, toBytes2.Length); i++)
                if (toBytes1.Length > toBytes2.Length)
                    toBytes1[i] ^= toBytes2[i];
                else
                    toBytes2[i] ^= toBytes1[i];
            if (toBytes1.Length > toBytes2.Length)
                return toBytes1;
            else
                return toBytes2;
        }
        public static List<Bitmap> WorkWithDarkImageWordToLetters(Bitmap img, Bitmap curimg)
        {
            lb = new List<Bitmap>();
            bool[,] BoolMass = new bool[img.Width, img.Height];
            //   Bitmap img = (Bitmap)im.Clone();
            for (int i = 0; i < img.Height; i++)
                for (int j = 0; j < img.Width; j++)
                {
                    BoolMass[j, i] = IntellectualSigma(img.GetPixel(j, i)) == 1;
                    if (BoolMass[j, i])
                        img.SetPixel(j, i, Color.FromArgb(255, 255, 255, 255));
                    else
                        img.SetPixel(j, i, Color.FromArgb(0, 0, 0, 0));
                }
            int temp = 0;
            for (int j = 0; j < img.Height; j++)
            {
                bool isSymbolStroke = false;
                for (int i = 0; i < img.Width; i++)
                {
                    if (BoolMass[i, j])
                        isSymbolStroke = true;

                }
                temp++;
                if (!isSymbolStroke)
                {
                    if (temp > 3)
                    {
                        Rectangle rect = new Rectangle(0, j - temp - 1, img.Width, temp + 2);
                        lb.Add(WorkWithDarkImageWord(Crop(curimg, rect)));
                    }
                    temp = 0;
                }
            }


            //if (temp > 2)
            //{
            //    Rectangle rect = new Rectangle(img.Width - temp + 1, 0, temp - 1, img.Height);
            //    lb.Add(Crop(curimg, rect));

            //}
            return lb;
        }

    }
}
