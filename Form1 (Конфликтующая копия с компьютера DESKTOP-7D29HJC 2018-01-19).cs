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
using Microsoft.Xna.Framework.Graphics;
namespace TryToOnTopAndImageCatch
{
    public partial class Form1 : Form
    {
        //public Form1()
        //{
        //    InitializeComponent();
          
        // //   timer1.Start();

        //}

        private void KeyEvent(object sender, KeyEventArgs e)
        {
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text += ClassCatchWindow.checkdxversion_dxdiag();
            ClassCatchWindow.SetTopMost(this.Handle);
        }
        public void DoMinimize()
        {
            this.WindowState=FormWindowState.Minimized;
        }
        public void  DoMaximize()
        {
            this.WindowState=FormWindowState.Minimized;
        }
        Bitmap b;
        public void setImage()
        {
            //ClassCatchWindow.Capture();
         //   ClassCatchWindow.CaptureRegionDirect3DSharpDx("firefox");
            ClassCatchWindow.Direct3DCapture.CaptureWindow("firefox");
           // b = ClassCatchWindow.CaptureRegionDirect3DSharpDx("SRS", new Rectangle(0,0,1000,1000));
            pictureBox1.Image = ClassCatchWindow.bit;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
               
                
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

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Minimized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            ClassCatchWindow.SetTopMost(this.Handle);
        }


        ///////////////////////////////////////
        // Directx graphics device
        GraphicsDevice dev = null;
        BasicEffect effect = null;

        // Wheel vertexes
        VertexPositionColor[] v = new VertexPositionColor[100];

        // Wheel rotation
        float rot = 0;

        public Form1()
        {
            InitializeComponent();
            KeyboardHook.SetHook();
            //   this.TopMost = true;
            this.KeyDown += new KeyEventHandler(KeyEvent);
            textBox1.Text+= ClassCatchWindow.checkdxversion_dxdiag();
            StartPosition = FormStartPosition.CenterScreen;
            Size = new System.Drawing.Size(500, 500);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders

            TopMost = true;        // make the form always on top                     
            Visible = true;        // Important! if this isn't set, then the form is not shown at all

            //// Set the form click-through
            //int initialStyle = GetWindowLong(this.Handle, -20);
            //SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            //// Create device presentation parameters
            //PresentationParameters p = new PresentationParameters();
            //p.IsFullScreen = false;
            //p.DeviceWindowHandle = this.Handle;
            //p.BackBufferFormat = SurfaceFormat.Vector4;
            //p.PresentationInterval = PresentInterval.One;

            //// Create XNA graphics device
            //dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, p);

            //// Init basic effect
            //effect = new BasicEffect(dev);

            //// Extend aero glass style on form init
            //OnResize(null);
        }


        protected override void OnResize(EventArgs e)
        {
            int[] margins = new int[] { 0, 0, Width, Height };

            // Extend aero glass style to whole form
            DwmExtendFrameIntoClientArea(this.Handle, ref margins);
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing here to stop window normal background painting
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            //// Clear device with fully transparent black
            //dev.Clear(new Microsoft.Xna.Framework.Color(0, 0, 0, 0.0f));

            //// Rotate wheel a bit
            //rot += 0.1f;

            //// Make the wheel vertexes and colors for vertexes
            //for (int i = 0; i < v.Length; i++)
            //{
            //    if (i % 3 == 1)
            //        v[i].Position = new Microsoft.Xna.Framework.Vector3((float)Math.Sin((i + rot) * (Math.PI * 2f / (float)v.Length)), (float)Math.Cos((i + rot) * (Math.PI * 2f / (float)v.Length)), 0);
            //    else if (i % 3 == 2)
            //        v[i].Position = new Microsoft.Xna.Framework.Vector3((float)Math.Sin((i + 2 + rot) * (Math.PI * 2f / (float)v.Length)), (float)Math.Cos((i + 2 + rot) * (Math.PI * 2f / (float)v.Length)), 0);

            //    v[i].Color = new Microsoft.Xna.Framework.Color(1 - (i / (float)v.Length), i / (float)v.Length, 0, i / (float)v.Length);
            //}

            //// Enable position colored vertex rendering
            //effect.VertexColorEnabled = true;
            //foreach (EffectPass pass in effect.CurrentTechnique.Passes) pass.Apply();

            //// Draw the primitives (the wheel)
            //dev.DrawUserPrimitives(PrimitiveType.TriangleList, v, 0, v.Length / 3, VertexPositionColor.VertexDeclaration);

            //// Present the device contents into form
            //dev.Present();

            //// Redraw immediatily
            //Invalidate();
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

    }
}
