using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SPSD.VisualStudio.Wizard.Properties;
using System;

namespace SPSD.VisualStudio.Wizard
{
    [Designer(typeof (WizardStepDesigner)), ToolboxItem(false), DefaultEvent("Click")]
    public class StartStep : WizardStep
    {
        private Image iconImage;
        private ColorPair leftPair = new ColorPair(Color.Orange, Color.White, 270);
        private Image bindingImage;
        private string subtitle = "Enter a brief description of the wizard here.";
        private string title = "Welcome to the DemoWizard.";
        
        public StartStep()
        {
          
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = SystemColors.ControlLightLight;
            Icon = null; // Resources.icon;
						BindingImage = Resources.left;
            ResetSubtitleAppearence();
            ResetTitleAppearence();
            ResetLeftPair();
            leftPair.AppearenceChanged += leftPair_AppearenceChanged;
        }

        private void leftPair_AppearenceChanged(object sender, GenericEventArgs<bool> tArgs)
        {
            Invalidate();
        }

        protected void GetTextBounds(out RectangleF titleRect, out RectangleF subtitleRect)
        {
            Graphics graphics = CreateGraphics();
            try
            {
                GetTextBounds(out titleRect, out subtitleRect, graphics);
            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                }
            }
        }

        protected virtual void GetTextBounds(out RectangleF titleRect, out RectangleF subtitleRect, Graphics graphics)
        {
            StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox);
            format.Trimming = StringTrimming.EllipsisCharacter;
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.None;
            if (bindingImage != null)
            {
              SizeF sz = graphics.MeasureString(Title, this.HeaderTitleFont, Width - bindingImage.Width, format);
              titleRect = new RectangleF(bindingImage.Width + HeaderTitleFont.SizeInPoints, HeaderTitleFont.SizeInPoints, sz.Width, sz.Height);
                SizeF sz1 = graphics.MeasureString(Subtitle, HeaderSubTitleFont, Width - bindingImage.Width, format);
                subtitleRect = new RectangleF(bindingImage.Width + HeaderSubTitleFont.SizeInPoints, titleRect.Height + HeaderSubTitleFont.SizeInPoints, sz1.Width, sz1.Height);
            }
            else
            {
                SizeF sz = graphics.MeasureString(Title, HeaderTitleFont, Width - LeftRectangle.Width, format);
                titleRect = new RectangleF(LeftRectangle.Width + HeaderTitleFont.SizeInPoints, HeaderTitleFont.SizeInPoints, sz.Width, sz.Height);
                SizeF sz1 = graphics.MeasureString(Subtitle, HeaderSubTitleFont, Width - LeftRectangle.Width, format);
                subtitleRect = new RectangleF(LeftRectangle.Width + HeaderSubTitleFont.SizeInPoints, titleRect.Height + HeaderSubTitleFont.SizeInPoints, sz1.Width, sz1.Height);
            }
        }

        protected Region GetTextBounds()
        {
            RectangleF titleRect;
            RectangleF subtitleRect;
            GetTextBounds(out titleRect, out subtitleRect);
            return GetTextBounds(titleRect, subtitleRect);
        }

        protected Region GetTextBounds(RectangleF titleRect, RectangleF subtitleRect)
        {
            if (titleRect.IsEmpty)
            {
                if (!subtitleRect.IsEmpty)
                {
                    return new Region(subtitleRect);
                }
                return new Region(RectangleF.Empty);
            }
            else
            {
                if (!subtitleRect.IsEmpty)
                {
                    return new Region(new RectangleF(172f, 8f, (Width - 180), (8f + titleRect.Height) + subtitleRect.Height));
                }
                return new Region(titleRect);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect;
            Rectangle iconRect;
            RectangleF titleRect;
            RectangleF subtitleRect;
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            rect = LeftRectangle;
            GetTextBounds(out titleRect, out subtitleRect);
            if (bindingImage != null)
            {
                graphics.DrawImage(bindingImage, rect);
                iconRect = IconRectangle;
                iconRect.Inflate(-1, -1);
                if (iconImage != null)
                {
                    graphics.DrawImage(iconImage, iconRect);
                }
            }
            else
            {
                using (Brush brush = new LinearGradientBrush(rect, leftPair.BackColor1, leftPair.BackColor2, leftPair.Gradient))
                {
                    graphics.FillRectangle(brush, rect);
                    iconRect = IconRectangle;
                    iconRect.Inflate(-1, -1);
                    if (iconImage != null)
                    {
                        graphics.DrawImage(iconImage, iconRect);
                    }
                }
            }
            DrawText(graphics, titleRect, title, HeaderTitleFont);
            DrawText(graphics, subtitleRect, subtitle, HeaderSubTitleFont);
            //this.WizardControl.NextButton.Enabled = true;
        }

        internal override void Reset()
        {
            ResetLeftPair();
            ResetBindingImage();
            ResetIcon();
            BackColor = SystemColors.ControlLightLight;
            BackgroundImage = null;
            BackgroundImageLayout = ImageLayout.Tile;
            ForeColor = SystemColors.ControlText;
            Title = "Welcome to the DemoWizard.";
            Subtitle = "Enter a brief description of the wizard here.";
        }

        [DefaultValue(typeof (Color), "ControlLightLight")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Description("The icon image of the step."), Category("Appearance")]
        public virtual Image Icon
        {
            get { return iconImage; }
            set
            {
                if (value != iconImage)
                {
                    iconImage = value;
                    Invalidate();
                }
            }
        }

        protected virtual Rectangle IconRectangle
        {
            get { return new Rectangle(104, 12, 48, 48); }
        }

        [Description("The back color appearence of the left panel."), Category("Appearance")]
        public virtual ColorPair LeftPair
        {
            get { return leftPair; }
            set
            {
                if (leftPair == value)
                {
                    return;
                }
                leftPair = value;
                Invalidate(LeftRectangle);
            }
        }

        [Category("Appearance"), Description("The background image of the panel.")]
        public virtual Image BindingImage
        {
            get { return bindingImage; }
            set
            {
                if (value != bindingImage)
                {
                    bindingImage = value;
                    OnBindingImageChanged();
                    Invalidate();
                }
            }
        }

        protected virtual Rectangle LeftRectangle
        {
            get { return new Rectangle(0, 0, 230, 364); }
        }

        [Category("Appearance"), DefaultValue("Enter a brief description of the wizard here."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), Description("The subtitle of the step.")]
        public virtual string Subtitle
        {
            get { return subtitle; }
            set
            {
                if (subtitle == value)
                {
                    return;
                }
                Region refreshRegion = GetTextBounds();
                subtitle = value;
                refreshRegion.Union(GetTextBounds());
                Invalidate(refreshRegion);
                return;
            }
        }

        [DefaultValue("Welcome to the DemoWizard."), Description("The title of the step."), Category("Appearance"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public virtual string Title
        {
            get { return title; }
            set
            {
                if (title == value)
                {
                    return;
                }
                Region refreshRegion = GetTextBounds();
                title = value;
                refreshRegion.Union(GetTextBounds());
                Invalidate(refreshRegion);
            }
        }

        protected virtual bool ShouldSerializeSubtitleAppearence()
        {
          return false;
        }

        protected virtual bool ShouldSerializeTitleAppearence()
        {
          return false;
        }

        protected virtual void ResetTitleAppearence()
        {

           // HeaderTitleFont = new Font("Microsoft Sans", 15.25f, GraphicsUnit.Point);
        }

        protected virtual void ResetSubtitleAppearence()
        {
            HeaderSubTitleFont = new Font("Microsoft Sans", 8.25f, GraphicsUnit.Point);
        }

        protected virtual bool ShouldSerializeLeftPair()
        {
            ColorPair pa = new ColorPair(Color.Orange, Color.White, 270);
            return leftPair != pa;
        }

        private void ResetLeftPair()
        {
            leftPair = new ColorPair(Color.Orange, Color.White, 270);
        }

        protected virtual bool ShouldSerializeBindingImage()
        {
            return BindingImage != Resources.left;
        }

        private void ResetBindingImage()
        {
					BindingImage = Resources.left;
        }

        protected virtual bool ShouldSerializeIcon()
        {
					return Icon != Resources.icon;
        }

        private void ResetIcon()
        {
					Icon = Resources.icon;
        }
    }
}