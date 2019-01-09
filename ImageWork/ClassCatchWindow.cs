using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Xml;
namespace TryToOnTopAndImageCatch
{
    class ClassCatchWindow
    {
        private static int VersionOfDX = 9;
        public static int checkdxversion_dxdiag()
        {
            Process.Start("dxdiag", "/x dxv.xml");
            while (!File.Exists("dxv.xml"))
                Thread.Sleep(1000);
            XmlDocument doc = new XmlDocument();
            doc.Load("dxv.xml");
            XmlNode dxd = doc.SelectSingleNode("//DxDiag");
            XmlNode dxv = dxd.SelectSingleNode("//DirectXVersion");
            VersionOfDX = Convert.ToInt32(dxv.InnerText.Split(' ')[1]);
            return VersionOfDX;
        }

            #region SlimDX
            //private static SlimDX.Direct3D9.Direct3D _direct3D9 = new SlimDX.Direct3D9.Direct3D();
            //private static Dictionary<IntPtr, SlimDX.Direct3D9.Device> _direct3DDeviceCache = new Dictionary<IntPtr, SlimDX.Direct3D9.Device>();

            /// <param name="hWnd"></param>
            /// <returns></returns>
            //public static Bitmap CaptureWindow(string nameProcess)
            //{
            //    System.Diagnostics.Process[] mass = System.Diagnostics.Process.GetProcessesByName(nameProcess);
            //    IntPtr handle = mass[0].MainWindowHandle;
            //    return CaptureRegionDirect3D(handle, NativeMethods.GetAbsoluteClientRect(handle));
            //}

            /// <param name="handle">The handle of a window</param>
            /// <param name="region">The region to capture (in screen coordinates)</param>
            /// <returns>A bitmap containing the captured region, this should be disposed of appropriately when finished with it</returns>
            //    public static Bitmap CaptureRegionDirect3D(IntPtr handle, Rectangle region)
            //    {

            //        IntPtr hWnd = handle;
            //        Bitmap bitmap = null;
            //        try
            //        {
            //            // We are only supporting the primary display adapter for Direct3D mode
            //            SlimDX.Direct3D9.AdapterInformation adapterInfo = _direct3D9.Adapters.DefaultAdapter;
            //            SlimDX.Direct3D9.Device device;

            //            #region Get Direct3D Device
            //            // Retrieve the existing Direct3D device if we already created one for the given handle
            //            if (_direct3DDeviceCache.ContainsKey(hWnd))
            //            {
            //                device = _direct3DDeviceCache[hWnd];
            //            }
            //            // We need to create a new device
            //            else
            //            {
            //                // Setup the device creation parameters
            //                SlimDX.Direct3D9.PresentParameters parameters = new SlimDX.Direct3D9.PresentParameters();
            //                parameters.BackBufferFormat = adapterInfo.CurrentDisplayMode.Format;
            //                Rectangle clientRect = NativeMethods.GetAbsoluteClientRect(hWnd);
            //                parameters.BackBufferHeight = clientRect.Height;
            //                parameters.BackBufferWidth = clientRect.Width;
            //                parameters.Multisample = SlimDX.Direct3D9.MultisampleType.None;
            //                parameters.SwapEffect = SlimDX.Direct3D9.SwapEffect.Discard;
            //                parameters.DeviceWindowHandle = hWnd;
            //                parameters.PresentationInterval = SlimDX.Direct3D9.PresentInterval.Default;
            //                parameters.FullScreenRefreshRateInHertz = 0;

            //                // Create the Direct3D device
            //                device = new SlimDX.Direct3D9.Device(_direct3D9, adapterInfo.Adapter, SlimDX.Direct3D9.DeviceType.Hardware, hWnd, SlimDX.Direct3D9.CreateFlags.SoftwareVertexProcessing, parameters);
            //                _direct3DDeviceCache.Add(hWnd, device);
            //            }
            //            #endregion

            //            // Capture the screen and copy the region into a Bitmap
            //            using (SlimDX.Direct3D9.Surface surface = SlimDX.Direct3D9.Surface.CreateOffscreenPlain(device, adapterInfo.CurrentDisplayMode.Width, adapterInfo.CurrentDisplayMode.Height, SlimDX.Direct3D9.Format.A8R8G8B8, SlimDX.Direct3D9.Pool.SystemMemory))
            //            {
            //                device.GetFrontBufferData(0, surface);

            //                // Update: thanks digitalutopia1 for pointing out that SlimDX have fixed a bug
            //                // where they previously expected a RECT type structure for their Rectangle
            //                bitmap = new Bitmap(SlimDX.Direct3D9.Surface.ToStream(surface, SlimDX.Direct3D9.ImageFileFormat.Bmp, new Rectangle(region.Left, region.Top, region.Width, region.Height)));
            //                // Previous SlimDX bug workaround: new Rectangle(region.Left, region.Top, region.Right, region.Bottom)));

            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //        return bitmap;
            //    }
            //}
            #endregion
            public static Bitmap CaptureRegionDirect3DSharpDx(String nameProcess)
        {
            Bitmap bitmap = null;
            //if (VersionOfDX >= 11)
            //{
            //    try
            //    {
            //        System.Diagnostics.Process[] mass = System.Diagnostics.Process.GetProcessesByName(nameProcess);
            //        IntPtr handle = mass[0].MainWindowHandle;
            //        var factory = new SharpDX.DXGI.Factory1();
            //        var adapter = factory.Adapters1[0];
            //        var output = adapter.Outputs[0];
            //        var device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware,
            //                                                               DeviceCreationFlags.BgraSupport |
            //                                                               DeviceCreationFlags.Debug);
            //        var dev1 = device.QueryInterface<SharpDX.DXGI.Device1>();
            //        var output1 = output.QueryInterface<Output1>();
            //        var duplication = output1.DuplicateOutput(dev1);
            //        OutputDuplicateFrameInformation frameInfo;
            //        SharpDX.DXGI.Resource desktopResource;
            //        duplication.AcquireNextFrame(50, out frameInfo, out desktopResource);
            //        var desktopSurface = desktopResource.QueryInterface<SharpDX.DXGI.Surface>();
            //        DataStream dataStream;
            //        desktopSurface.Map(SharpDX.DXGI.MapFlags.Read, out dataStream);
            //        bitmap = new Bitmap(desktopSurface.Description.Width, desktopSurface.Description.Height);
            //        // Graphics g = bi
            //        for (int y = 0; y < desktopSurface.Description.Width; y++)
            //        {
            //            for (int x = 0; x < desktopSurface.Description.Height; x++)
            //            {
            //                // read DXGI_FORMAT_B8G8R8A8_UNORM pixel:
            //                byte b = dataStream.Read<byte>();
            //                byte g = dataStream.Read<byte>();
            //                byte r = dataStream.Read<byte>();
            //                byte a = dataStream.Read<byte>();
            //                // color (r, g, b, a) and pixel position (x, y) are available
            //                // TODO: write to bitmap or process otherwise
            //                bitmap.SetPixel(x, y, Color.FromArgb(a, r, g, b));
            //            }
            //        }
            //        desktopSurface.Unmap();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
                
            return bitmap;

        }
        public enum CaptureMode
            {
                Screen,
                Window
            }

            [DllImport("user32.dll")]
            private static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public static void SetTopMost(IntPtr it)
        {
            SetWindowPos(it, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
        public static Bitmap bit;
        public static void Capture(CaptureMode screenCaptureMode = CaptureMode.Screen, string nameProcess = "", int L = 0, int T = 0, int R = 0, int B = 0)
            {
                Rectangle bounds;
                if (L != 0 || T != 0 || R != 0 || B != 0)
                    bounds = new Rectangle(L, T, R, B);
                else
                {
                    var handle = GetForegroundWindow();
                    var rect = new Rect();
                    GetWindowRect(handle, ref rect);

                    bounds = new Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
                }
                try
                {
                    if (screenCaptureMode == CaptureMode.Screen)
                    {

                        var result = new Bitmap(bounds.Width, bounds.Height);

                        using (var g = Graphics.FromImage(result))
                        {
                            g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

                            //Graphics.FromHwnd
                        }

                        bit = result;
                    }
                    else
                    {
                        System.Diagnostics.Process[] mass = System.Diagnostics.Process.GetProcessesByName(nameProcess);
                        IntPtr it = mass[0].MainWindowHandle;
                        Graphics gr = Graphics.FromHwnd(it);
                        bit = new Bitmap(bounds.Width, bounds.Height);
                        gr.DrawImage(bit, new Point(0, 0));

                        //CursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }
        #region Native Win32 Interop
        [Serializable, StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }

            public Rectangle AsRectangle
            {
                get
                {
                    return new Rectangle(this.Left, this.Top, this.Right - this.Left, this.Bottom - this.Top);
                }
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public static RECT FromRectangle(Rectangle rect)
            {
                return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
        }
        [System.Security.SuppressUnmanagedCodeSecurity()]
        internal sealed class NativeMethods
        {
            [DllImport("user32.dll")]
            internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

            /// &lt;summary&gt;
            /// Get a windows client rectangle in a .NET structure
            /// &lt;/summary&gt;
            /// &lt;param name=&quot;hwnd&quot;&gt;The window handle to look up&lt;/param&gt;
            /// &lt;returns&gt;The rectangle&lt;/returns&gt;
            internal static Rectangle GetClientRect(IntPtr hwnd)
            {
                RECT rect = new RECT();
                GetClientRect(hwnd, out rect);
                return rect.AsRectangle;
            }

            /// &lt;summary&gt;
            /// Get a windows rectangle in a .NET structure
            /// &lt;/summary&gt;
            /// &lt;param name=&quot;hwnd&quot;&gt;The window handle to look up&lt;/param&gt;
            /// &lt;returns&gt;The rectangle&lt;/returns&gt;
            internal static Rectangle GetWindowRect(IntPtr hwnd)
            {
                RECT rect = new RECT();
                GetWindowRect(hwnd, out rect);
                return rect.AsRectangle;
            }

            internal static Rectangle GetAbsoluteClientRect(IntPtr hWnd)
            {
                Rectangle windowRect = NativeMethods.GetWindowRect(hWnd);
                Rectangle clientRect = NativeMethods.GetClientRect(hWnd);

                // This gives us the width of the left, right and bottom chrome - we can then determine the top height
                int chromeWidth = (int)((windowRect.Width - clientRect.Width) / 2);

                return new Rectangle(new Point(windowRect.X + chromeWidth, windowRect.Y + (windowRect.Height - clientRect.Height - chromeWidth)), clientRect.Size);
            }
        }
        #endregion
        public static Point CursorPosition
            {
                get;
                protected set;
            }
    }
}
