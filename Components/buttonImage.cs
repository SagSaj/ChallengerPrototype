using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
namespace TryToOnTopAndImageCatch
{
    public partial class buttonImage : PictureBox
    {
       private System.Drawing.Bitmap ImageCur;
        
        private System.Drawing.Bitmap ImageEnter;
        private System.Drawing.Bitmap ImageClick;
        public System.Drawing.Bitmap _ImageCurrent
        {
            get { return ImageCur; }
            set { ImageCur = value; }
        }
        public System.Drawing.Bitmap _ImageEnter
        {
            get { return ImageEnter; }
            set { ImageEnter = value; }
        }
        public System.Drawing.Bitmap _ImageClick
        {
            get { return ImageClick; }
            set { ImageClick = value; }
        }
        public buttonImage()
        {
            InitializeComponent();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Image = ImageEnter;
          //  Cursor.Current = Cursors.Hand;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Image = ImageCur;
          //  Cursor.Current = Cursors.Arrow;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.Image = ImageClick;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.Image = ImageCur;
        }
        public buttonImage(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.Cursor = Cursors.Hand;
        }
        
    }
}
