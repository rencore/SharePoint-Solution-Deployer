using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SPSD.VisualStudio.Wizard.Properties;
using System.Reflection;

namespace SPSD.VisualStudio.Wizard
{
    public class LicenceStep : WizardStep
    {
        #region Private Fields

        private ColorPair headerPair = new ColorPair();
        private Image bindingImage;
        private string subtitle = "Please read the following important information before continuing.";
        private string title = "License Agreement.";
        private Font warningFont = new Font("Microsoft Sans", 8.25f, GraphicsUnit.Point);
        private string warning = "Please read the following license agreement. You must accept the terms  of this agreement before continuing.";
        private readonly RichTextBox rtbLicense = new RichTextBox();
        private readonly RadioButton rbtnAccept = new RadioButton();
        private readonly RadioButton rbtnDecline = new RadioButton();
        private bool? accepted = false;
        private string licenseFile = @".\Licenses\SFoG.rtf";

        #endregion

        public event GenericEventHandler<bool> AgreementChanged;

        #region Constructor

        public LicenceStep()
        {
          
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            InitializeComponent();
            ResetBindingImage();
            ResetSubtitleAppearence();
            ResetTitleAppearence();
            ResetWarningFont();
            rtbLicense.ReadOnly = true;
            rtbLicense.BackColor = Color.White;
            
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            RectangleF titleRect;
            RectangleF subtitleRect;
            RectangleF descRect;
            GetTextBounds(out titleRect, out subtitleRect, out descRect);

            rtbLicense.Location = new Point(50, 99);
            rtbLicense.Name = "rtbLicense";
            rtbLicense.Size = new Size(Size.Width - 100, Size.Height -200);
            rtbLicense.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            rtbLicense.TabIndex = 0;
            rtbLicense.Text = "Please choose license file.";

            rbtnAccept.AutoSize = true;
            rbtnAccept.Location = new Point(50, 314);
            rbtnAccept.Name = "rbtnAccept";
            rbtnAccept.TabIndex = 1;
            rbtnAccept.TabStop = true;
            rbtnAccept.Text = "I &accept the agreement";
            rbtnAccept.UseVisualStyleBackColor = true;
            rbtnAccept.CheckedChanged += rbtnAccept_CheckedChanged;

            rbtnDecline.AutoSize = true;
            rbtnDecline.Location = new Point(50, 337);
            rbtnDecline.Name = "rbtnDecline";
            rbtnDecline.TabIndex = 2;
            rbtnDecline.TabStop = true;
            rbtnDecline.Text = "I do &not accept the agreement";
            rbtnDecline.UseVisualStyleBackColor = true;
            rbtnDecline.CheckedChanged += rbtnDecline_CheckedChanged;
            Controls.AddRange(new Control[] {rtbLicense, rbtnAccept, rbtnDecline});
            ResumeLayout();
        }


        #endregion

        #region Virtual Methods

        protected virtual void GetTextBounds(out RectangleF titleRect, out RectangleF subtitleRect, out RectangleF descriptionRect, Graphics graphics)
        {
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Trimming = StringTrimming.EllipsisCharacter;
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.None;
            SizeF sz = graphics.MeasureString(Title, HeaderTitleFont, Width, format);
            titleRect = new RectangleF(HeaderSubTitleFont.SizeInPoints, HeaderSubTitleFont.SizeInPoints, sz.Width, sz.Height);
            SizeF sz1 = graphics.MeasureString(Subtitle, HeaderSubTitleFont, Width, format);
            subtitleRect = new RectangleF(2 * HeaderSubTitleFont.SizeInPoints, titleRect.Height + HeaderSubTitleFont.SizeInPoints, sz1.Width, sz1.Height);
            SizeF sz2 = graphics.MeasureString(warning, warningFont, (int) (Width - (4 * warningFont.SizeInPoints)), format);
            descriptionRect = new RectangleF(2 * warningFont.SizeInPoints, HeaderRect.Height + warningFont.SizeInPoints, sz2.Width, sz2.Height);
        }

        #endregion

        #region Private Methods

        protected void GetTextBounds(out RectangleF titleRect, out RectangleF subtitleRect, out RectangleF descriptionRect)
        {
            Graphics graphics = CreateGraphics();
            try
            {
                GetTextBounds(out titleRect, out subtitleRect, out descriptionRect, graphics);
                rtbLicense.Location = new Point((int)(2 * warningFont.SizeInPoints), (int) (descriptionRect.Bottom + warningFont.SizeInPoints));
                rtbLicense.Size = new Size((int)(Width - 4*warningFont.SizeInPoints), (int)(Height - descriptionRect.Height - rbtnAccept.Height - HeaderRect.Height - 38));
                rbtnAccept.Location = new Point((int)(2 * warningFont.SizeInPoints), rtbLicense.Bottom + 10);
                rbtnDecline.Location = new Point((int)(2 * warningFont.SizeInPoints) + rtbLicense.Width/2, rtbLicense.Bottom + 10);
            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                }
            }
        }

        protected Region GetTextBounds()
        {
            RectangleF titleRect;
            RectangleF subtitleRect;
            RectangleF descriptionRect;
            GetTextBounds(out titleRect, out subtitleRect, out descriptionRect);
            return GetTextBounds(titleRect, subtitleRect);
        }

        protected Region GetTextBounds(RectangleF titleRect, RectangleF subtitleRect)
        {
            if (!titleRect.IsEmpty)
            {
                if (!subtitleRect.IsEmpty)
                {
                    return new Region(new RectangleF(6f, Width - 12, (Width - 66), (6f + titleRect.Height) + subtitleRect.Height));
                }
                else
                {
                    return new Region(titleRect);
                }
            }
            else
            {
                if (!subtitleRect.IsEmpty)
                {
                    return new Region(subtitleRect);
                }
                return new Region(RectangleF.Empty);
            }
        }

        private void DrawText(RectangleF empty, Graphics graphics, RectangleF titleRect, RectangleF warningRect)
        {
            if (!titleRect.IsEmpty)
            {
                DrawText(graphics,  titleRect, title, HeaderTitleFont);
            }
            if (!empty.IsEmpty)
            {
              DrawText(graphics, empty, subtitle, HeaderSubTitleFont);
            }
            if (!warningRect.IsEmpty)
            {
                graphics.DrawString(warning, warningFont, new SolidBrush(ForeColor), warningRect);
            }
        }

        #endregion

        #region Override

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            Rectangle rect = HeaderRect;
            Rectangle rectangle;
            RectangleF titleRect;
            RectangleF subtitleRect;
            RectangleF warningRect;
            GetTextBounds(out titleRect, out subtitleRect, out warningRect);
            if (bindingImage != null)
            {
                graphics.DrawImage(bindingImage, rect);
                rectangle = new Rectangle(rect.Left, rect.Bottom, rect.Width, 2);
                ControlPaint.DrawBorder3D(graphics, rectangle);
            }
            else
            {
                using (Brush brush = new LinearGradientBrush(rect, headerPair.BackColor1, headerPair.BackColor2, headerPair.Gradient))
                {
                    graphics.FillRectangle(brush, rect);
                    rectangle = new Rectangle(rect.Left, rect.Bottom, rect.Width, 2);
                    ControlPaint.DrawBorder3D(graphics, rectangle);
                }
            }
            DrawText(subtitleRect, graphics, titleRect, warningRect);
            //this.WizardControl.NextButton.Enabled = rbtnAccept.Checked;

        }

        internal override void Reset()
        {
            ResetBindingImage();
            ResetSubtitleAppearence();
            ResetAccepted();
            ResetTitleAppearence();
            BackgroundImage = null;
            ResetWarningFont();
            Title = "License Agreement.";
            Subtitle = "Please read the following important information before continuing.";
        }

        #endregion

        #region Public Property

        [Description("Appearence of header."), Category("Appearance")]
        public ColorPair HeaderPair
        {
            get { return headerPair; }
            set
            {
                if (value != headerPair)
                {
                    headerPair = value;
                    Invalidate(HeaderRect);
                }
            }
        }

        [Description("The background image of the panel."), Category("Appearance")]
        public Image BindingImage
        {
            get { return bindingImage; }
            set
            {
                if (value != bindingImage)
                {
                    bindingImage = value;
                    Invalidate(HeaderRect);
                    OnBindingImageChanged();
                }
            }
        }

        protected virtual Rectangle HeaderRect
        {
            get { return new Rectangle(0, 0, Width, 60); }
        }
        
        
        [Description("The warning text appearence of step."), Category("Appearance")]
        public Font WarningFont
        {
            get { return warningFont; }
            set
            {
                if (warningFont != value)
                {
                    warningFont = value;
                    Invalidate();
                }
            }
        }
        [Category("Appearance"), DefaultValue("Please read and accept the following important information before continuing."), Description("The subtitle of the step."), Editor(typeof (MultilineStringEditor), typeof (UITypeEditor))]
        public string Subtitle
        {
            get { return subtitle; }
            set
            {
                if (value != subtitle)
                {
                    Region refreshRegion = GetTextBounds();
                    subtitle = value;
                    refreshRegion.Union(GetTextBounds());
                    Invalidate(refreshRegion);
                }
            }
        }
        [Category("Appearance"), Description("Warning text."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Warning
        {
            get { return warning; }
            set
            {
                if (value != warning)
                {
                    warning = value;
                    Invalidate();
                }
            }
        }
        [Description("License Agreement."), Editor(typeof (MultilineStringEditor), typeof (UITypeEditor)), Category("Appearance")]
        public string Title
        {
            get { return title; }
            set
            {
                if (value != title)
                {
                    Region refreshRegion = GetTextBounds();
                    title = value;
                    refreshRegion.Union(GetTextBounds());
                    Invalidate(refreshRegion);
                }
            }
        }
        [Description("Accept text."), Category("Appearance")]
        public string AcceptText
        {
            get { return rbtnAccept.Text; }
            set
            {
                if(rbtnAccept.Text != value)
                {
                    rbtnAccept.Text = value;
                    Invalidate();
                }
            }
        }

        [Description("Decline text."), Category("Appearance")]
        public string DeclineText
        {
            get { return rbtnDecline.Text; }
            set
            {
                if (rbtnDecline.Text != value)
                {
                    rbtnDecline.Text = value;
                    Invalidate();
                }
            }
        }
        [Description("Status of license agreement."), Category("Behavior")]
        public bool? Accepted
        {
            get { return accepted; }
            set
            {
                if (accepted != value)
                {
                    accepted = value;
                    if(!accepted.HasValue)
                    {
                        rbtnAccept.Checked = false;
                        rbtnDecline.Checked = false;
                    }
                    else if(accepted.HasValue && accepted.Value)
                    {
                        rbtnAccept.Checked = true;
                        rbtnDecline.Checked = false;
                    }
                    else
                    {
                        rbtnAccept.Checked = false;
                        rbtnDecline.Checked = true;
                    }
                    Invalidate();
                }
            }
        }
        [Editor(typeof(CustomFileNameEditor), typeof(UITypeEditor))]
        [Description("License file to display."), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string LicenseFile
        {
            get { return licenseFile; }
            set
            {
                if (licenseFile != value)
                {
                    if(string.IsNullOrEmpty(value))
                    {
                        rtbLicense.Text = "Please select the licence file.";
                        licenseFile = value;
                    }
                    else
                    {
                        try
                        {
                            // override to load embeded license file
                            value = @"SPSD.VisualStudio.Wizard.Licenses.SFoG.rtf";
                            rtbLicense.LoadFile(Assembly.GetAssembly(this.GetType()).GetManifestResourceStream(value), RichTextBoxStreamType.RichText);
                            licenseFile = value;
                        }
                        catch
                        {
                            licenseFile = null;
                        }
                    }
                }
            }
        }

        #endregion

        protected virtual void ResetTitleAppearence()
        {
        }

        protected virtual void ResetSubtitleAppearence()
        {
        }

        protected virtual void ResetBindingImage()
        {
					bindingImage = Resources.Top;
        }

        protected virtual void ResetWarningFont()
        {
            warningFont = new Font("Microsoft Sans", 8.25f, GraphicsUnit.Point);
        }

        protected virtual void ResetAccepted()
        {
            accepted = null;
        }

        protected virtual void ResetAcceptText()
        {
            AcceptText = "I &accept the agreement";
        }

        protected virtual void ResetDeclineText()
        {
            DeclineText = "I do &not accept the agreement";
        }

        protected virtual void ResetHeaderPair()
        {
            headerPair = new ColorPair();
        }

        protected virtual bool ShouldSerializeTitleAppearence()
        {
          return false;
        }

        protected virtual bool ShouldSerializeSubtitleAppearence()
        {
          return false;
        }

        protected virtual bool ShouldSerializeWarningFont()
        {
            return warningFont != new Font("Microsoft Sans", 8.25f, GraphicsUnit.Point);
        }

        protected virtual bool ShouldSerializeBindingImage()
        {
					return bindingImage != Resources.Top;
        }

        protected virtual bool ShouldSerializeAccepted()
        {
            return accepted != null;
        }

        protected virtual bool ShouldSerializeAcceptText()
        {
            return AcceptText != "I &accept the agreement";
        }

        protected virtual bool ShouldSerializeDeclineText()
        {
            return DeclineText != "I do &not accept the agreement";
        }

        protected virtual bool ShouldSerializeHeaderPair()
        {
            return headerPair != new ColorPair();
        }


        private void rbtnDecline_CheckedChanged(object sender, EventArgs e)
        {
            this.accepted = false;
            OnAgreementChanged();
        }

        private void rbtnAccept_CheckedChanged(object sender, EventArgs e)
        {
            this.accepted = true;
            OnAgreementChanged();
        }

        protected virtual void OnAgreementChanged()
        {
            if(AgreementChanged != null)
            {
                AgreementChanged(this, new GenericEventArgs<bool>(rbtnAccept.Checked));
            }
        }
    }
}