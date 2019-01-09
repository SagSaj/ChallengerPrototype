using System;
using System.Diagnostics;

using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Direct2D1;
using SharpDX.Windows;
using Color = SharpDX.Color;
using System.Windows.Forms;
using SharpDX.Mathematics;
using System.Collections.Generic;

namespace TryToOnTopAndImageCatch
{
    class ClassDebagForDirectX
    {
        public static void Initiate()
        {
            var form = new RenderForm("SharpDX - MiniCube Direct3D9 Sample");
            // Creates the Device
            var direct3D = new Direct3D();
            var device = new SharpDX.Direct3D9.Device(direct3D, 0, DeviceType.Hardware, form.Handle, CreateFlags.HardwareVertexProcessing, new PresentParameters(form.ClientSize.Width, form.ClientSize.Height));

            // Prepare matrices
            var view = Matrix.LookAtLH(new Vector3(0, 0, -5), new Vector3(0, 0, 0), Vector3.UnitY);
            var proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, form.ClientSize.Width / (float)form.ClientSize.Height, 0.1f, 100.0f);
            var viewProj = Matrix.Multiply(view, proj);

            // Use clock
            var clock = new Stopwatch();
            clock.Start();

            string Game = "WOT";
            
            switch (Game)
            {
                case "WOT":
                    _list.Add(new DrawTextureAndCatchIt(SharpDX.Direct3D9.Texture.FromFile(device, "Resources/error_enter.png"), SharpDX.Direct3D9.Texture.FromFile(device, "Resources/error_6473.png"), new SharpDX.Mathematics.Interop.RawRectangle(1, 1, 100, 100), new SharpDX.Vector3(100, 100, 0)));
                    _list.Add(new DrawTextureAndCatchIt(SharpDX.Direct3D9.Texture.FromFile(device, "Resources/edit-addenter.png"), SharpDX.Direct3D9.Texture.FromFile(device, "Resources/edit-add.png"), new SharpDX.Mathematics.Interop.RawRectangle(1, 1, 100, 100), new SharpDX.Vector3(100, 400, 0)));
                    break;
            }
            //choose game

           
            RenderLoop.Run(form, () =>
             {
               

                 device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
                 device.BeginScene();
                 foreach (DrawTextureAndCatchIt dr in _list)
                 {
                     dr.CheckCursorPosition();
                     dr.DrawTexture(device);
                 }

                 device.EndScene();
                 device.Present();
             });
            
            device.Dispose();
            direct3D.Dispose();
                        

    }
        static List<DrawTextureAndCatchIt> _list = new List<DrawTextureAndCatchIt>();
        class DrawTextureAndCatchIt
        {
            public SharpDX.Direct3D9.Texture texOut = null;
            public SharpDX.Direct3D9.Texture texIn = null;
            public SharpDX.Mathematics.Interop.RawRectangle rectangle=new SharpDX.Mathematics.Interop.RawRectangle(1, 1, 100, 100);
            public SharpDX.Vector3 pos = new SharpDX.Vector3(0, 0, 0);
            public bool isIn = false;
            public DrawTextureAndCatchIt(SharpDX.Direct3D9.Texture _texOut, SharpDX.Direct3D9.Texture _texIn, SharpDX.Mathematics.Interop.RawRectangle _rectangle, SharpDX.Vector3 _pos)
            {
                texOut = _texOut;
                texIn = _texIn;
                rectangle = _rectangle;
                pos = _pos;
            }
            public void DrawTexture(SharpDX.Direct3D9.Device device)
            {
                Sprite sprite = null;
                sprite = new SharpDX.Direct3D9.Sprite(device);
                // to resize/rotate/position sprite.Transform = some 4x4 affine transform matrix (SharpDX.Matrix)

                sprite.Begin(SharpDX.Direct3D9.SpriteFlags.None);
                SharpDX.Color color = new SharpDX.ColorBGRA(0xffffffff);
                
                if(isIn)
                    sprite.Draw(texIn, color, rectangle, null, pos);
                else
                    sprite.Draw(texOut, color, rectangle, null, pos);
                // Finish drawing
                sprite.End();
            }

            public void CheckCursorPosition()
            {
                System.Drawing.Point p = Cursor.Position;
                if (p.X - rectangle.Left > 0 && p.X - rectangle.Right < 0 && p.Y - rectangle.Top > 0 && p.Y - rectangle.Bottom < 0)
                    isIn = true;
                else
                    isIn = false;
            }
        }
        #region DrawLine
        static void WotDraw(SharpDX.Direct3D9.Device device)
        {
            DrawTexture(device);
            Text(device, "Text", 1, 1);
        }
        static void DrawLine(SharpDX.Direct3D9.Device device, SharpDX.Vector2[] vLine, ColorBGRA col,int width)
        {
                Line line = null;
                line = new Line(device);
                line.Width = width;
                line.Draw(vLine, col);
        }
        #endregion DrawLine
        #region Text
        public static  void Text(SharpDX.Direct3D9.Device device, string hook, int x, int y)
        {
                using (SharpDX.Direct3D9.Font font = new SharpDX.Direct3D9.Font(device, new FontDescription()
                {
                    Height = 18,
                    FaceName = "Arial",
                    Italic = false,
                    Width = 0,
                    MipLevels = 1,
                    CharacterSet = FontCharacterSet.Default,
                    OutputPrecision = FontPrecision.Default,
                    Quality = FontQuality.ClearTypeNatural,
                    PitchAndFamily = FontPitchAndFamily.Default | FontPitchAndFamily.DontCare,
                    Weight = FontWeight.Bold
                }))
                {
                    {
                        font.DrawText(null, hook, x, y, new SharpDX.ColorBGRA(255, 0, 0, (byte)Math.Round((Math.Abs(6.0f) * 255f))));
                        //font.DrawText(null, String.Format("{0:N0} fps", 111122121122211), 5, 5, SharpDX.Color.Red);
                    }
                }

        }
        #endregion
        #region DrawTexture
        static void DrawTexture(SharpDX.Direct3D9.Device device)
        {
            SharpDX.Direct3D9.Texture tex = null;
            Sprite sprite = null;
            tex = SharpDX.Direct3D9.Texture.FromFile(device, "Resources/fire_seamless_tile_by_suicidecrew.jpg");
            sprite = new SharpDX.Direct3D9.Sprite(device);
            // to resize/rotate/position sprite.Transform = some 4x4 affine transform matrix (SharpDX.Matrix)

            sprite.Begin(SharpDX.Direct3D9.SpriteFlags.None);
            SharpDX.Vector3 pos = new SharpDX.Vector3(0, 0, 0);
            SharpDX.Color color = new SharpDX.ColorBGRA(0xffffffff);

            sprite.Draw(tex, color, new SharpDX.Mathematics.Interop.RawRectangle(1,1,100,100), null, pos);

            // Finish drawing
            sprite.End();
        }
        #endregion
    }
}
