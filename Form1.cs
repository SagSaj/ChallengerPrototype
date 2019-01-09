using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Diagnostics;
namespace TryToOnTopAndImageCatch
{
    public partial class Form1 : Form
    {


        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public void DoMinimize()
        {
            this.WindowState=FormWindowState.Minimized;
        }
        public void  DoMaximize()
        {
            this.WindowState=FormWindowState.Minimized;
        }
        public void setImage()
        {
            //ClassCatchWindow.Direct3DCapture.CaptureWindow("firefox");
            //pictureBox1.Image = ClassCatchWindow.bit;
        }
        bool ShowQwestion = false;
        public void Apply()
        {
            if (ShowQwestion)
            {
                ShowQwestion = false;
            }
        }
        public void Decline()
        {
            if (ShowQwestion)
            {
                ShowQwestion = false;
            }
        }
        public void SetText(string s)
        {
            textBox1.Text = s;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            KeyboardHook.UnSetHook();
        }

        private void buttonImage1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public Form1()
        {
            InitializeComponent();
            KeyboardHook.SetHook();
            //   this.TopMost = true;
         //   this.KeyDown += new KeyEventHandler(KeyEvent);
            textBox1.Text+= ClassCatchWindow.checkdxversion_dxdiag();
            StartPosition = FormStartPosition.CenterScreen;
            AllowTransparency = true;
            this.BackColor = Color.Beige;
            this.TransparencyKey = BackColor;
          //  this.Opacity = 0;
            //Size = new System.Drawing.Size(500, 500);
            //  FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders

            Visible = true;    

       
        }



        MemoryDXManager MSGDX;
        private void button1_Click(object sender, EventArgs e)
        {
          

            
        }
       

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(this.WindowState != FormWindowState.Normal)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }





        /// <summary>
        /// TEST
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        private void button1_Click_1(object sender, EventArgs e)
        {
            //var p = Process.GetProcessesByName(comboBox1.Text);
            //MSGDX = new MemoryDXManager();
            //MSGDX.InitializeDX(p[0]);

            IntPtr hWnd = FindWindow(null, "Whatever is in the game title bar");
            RECT rect;
            GetWindowRect(hWnd, out rect);
            if (rect.X == -32000)
            {
                // the game is minimized
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Location = new Point(rect.X + 10, rect.Y + 10);
            }

        }
    }
   
}
