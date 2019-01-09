using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
namespace TryToOnTopAndImageCatch
{
    class ClassWorking
    {
        public static string s;
        public static void Acr()
        {

        }
        public static void Apply()
        {
            Program.f.setImage();
        }
        public static void Answer(Bitmap i)
        {
            //  pictureBox1.Image = i;
                 List<Bitmap> lb = NewClassWorkWithImage.WorkWithDarkImage(i, new Bitmap(i));
                //    for (int i2 = 0; i2 < lb.Count; i2++)
                //for (int i1 = 0; i1 < lb.Count; i1++)
                //    comboBox1.Items.Add(i1.ToString());
                List<Double[,]> listM = new List<double[,]>();
                for (int i1 = 0; i1 < lb.Count; i1++)
                    listM.Add(NewClassWorkWithImage.ConvertImageToMatrix(lb[i1]));
            string AnswerHesh = NewClassWorkWithImage.StrongHashing(listM);
            ConnectWithServer.SendMessage(AnswerHesh);
        }
    }
}
