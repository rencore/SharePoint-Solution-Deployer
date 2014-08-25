using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SPSD.VisualStudio.Wizard
{
    [ToolboxItem(false), DefaultEvent("Click"), Designer(typeof (WizardStepDesigner))]
    public abstract class WizardStep : ContainerControl
    {
        #region Private Fields

        private int backStepIndex = -1;
        private WizardControl wizardControl;

        #endregion

        #region Constructor

        internal WizardStep()
        {
            Dock = DockStyle.Fill;

            headerTitleFont = new Font("Microsoft Sans", 9.75f, FontStyle.Bold, GraphicsUnit.Point);
            headerSubTitleFont = new Font("Microsoft Sans", 8.25f, GraphicsUnit.Point);
        }

        #endregion

        #region Overrides

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.LocationChanged"></see> event.
        ///</summary>
        ///
        ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
        protected override void OnLocationChanged(EventArgs e)
        {
            base.Location = Point.Empty;
        }

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.MarginChanged"></see> event. 
        ///</summary>
        ///
        ///<param name="e">A <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnMarginChanged(EventArgs e)
        {
            base.Margin = Padding.Empty;
        }

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.TabIndexChanged"></see> event.
        ///</summary>
        ///
        ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
        protected override void OnTabIndexChanged(EventArgs e)
        {
            base.TabIndex = 0;
        }

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.TabStopChanged"></see> event.
        ///</summary>
        ///
        ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
        protected override void OnTabStopChanged(EventArgs e)
        {
            base.TabStop = false;
        }

        public override string ToString()
        {
            if (Site == null)
            {
                return GetType().FullName;
            }
            return Site.Name;
        }

        #endregion



        #region Abstract Declaration

        internal abstract void Reset();

        #endregion

        #region Public Property

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AllowDrop
        {
            get { return base.AllowDrop; }
            set
            {
                base.AllowDrop = value;
                base.AllowDrop = true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set
            {
                base.Anchor = value;
                base.Anchor = AnchorStyles.None;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set
            {
                if (value != base.BackgroundImage)
                {
                    base.BackgroundImage = value;
                    Invalidate();
                }
            }
        }

        internal int BackStepIndex
        {
            get { return backStepIndex; }
            set { backStepIndex = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                base.Dock = value;
                base.Dock = DockStyle.Fill;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Point Location
        {
            get { return base.Location; }
            set
            {
                base.Location = value;
                base.Location = Point.Empty;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Margin
        {
            get { return base.Margin; }
            set
            {
                base.Margin = value;
                base.Margin = Padding.Empty;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Size MaximumSize
        {
            get { return base.MaximumSize; }
            set
            {
                base.MaximumSize = value;
                base.MaximumSize = Size.Empty;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set
            {
                base.MinimumSize = value;
                base.MinimumSize = Size.Empty;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int StepIndex
        {
            get
            {
                if (wizardControl == null)
                {
                    return -1;
                }
                if (wizardControl.WizardSteps.Count != 0)
                {
                    for (int i = 0; i < wizardControl.WizardSteps.Count; i++)
                    {
                        if (wizardControl.WizardSteps[i].Name != Name)
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
            set { base.RightToLeft = value; }
        }

        private Color headerColor = Color.Black;
        [Description("License file to display."), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color HeaderColor
        {
          get { return this.headerColor; }
          set { this.headerColor = value; }
        }

        private Font headerTitleFont;
        [Description("License file to display."), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font HeaderTitleFont
        {
          get { return this.headerTitleFont; }
          set { this.headerTitleFont = value; }
        }

        private Font headerSubTitleFont;
        [Description("License file to display."), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font HeaderSubTitleFont
        {
          get { return this.headerSubTitleFont; }
          set { this.headerSubTitleFont = value; }
        }

      
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            private set { base.TabIndex = 0; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            private set { base.TabStop = false; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        internal WizardControl WizardControl
        {
            get { return wizardControl; }
            set { wizardControl = value; }
        }

        #endregion

        public void DrawText(Graphics graphics, RectangleF rectangle, string text, Font font)
        {
            if (!rectangle.IsEmpty)
            {
                //graphics.DrawString(text, app.Font, new SolidBrush(app.TextShadowColor), rectangle);
                //rectangle.X -= app.Xshift;
                //rectangle.Y -= app.Yshift;
              graphics.DrawString(text, font, new SolidBrush(this.HeaderColor), rectangle);
            }
        }
        public event EventHandler BindingImageChanged;

        protected void OnBindingImageChanged()
        {
            if (BindingImageChanged != null)
            {
                BindingImageChanged(this, EventArgs.Empty);
            }
        }
    }
}