using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SPSD.VisualStudio.Wizard.Properties;

namespace SPSD.VisualStudio.Wizard
{
    [ToolboxItem(false), Designer(typeof (WizardStepDesigner)), DefaultEvent("Click")]
    public class FinishStep : WizardStep
    {
        private ColorPair pair = new ColorPair();

        public FinishStep()
        {          
            Reset();
        }

        internal override void Reset()
        {
            BackColor = SystemColors.ControlLightLight;
						BindingImage = Resources.back;
            BackgroundImageLayout = ImageLayout.Tile;
        }

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        ///</summary>
        ///
        ///<param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Brush brush = new LinearGradientBrush(ClientRectangle, pair.BackColor1, pair.BackColor2, pair.Gradient))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Description("Backgroung of the finish step."), Category("Appearance")]
        public Image BindingImage
        {
            get { return base.BackgroundImage; }
            set
            {
                if (value != base.BackgroundImage)
                {
                    base.BackgroundImage = value;
                    Invalidate();
                    OnBindingImageChanged();
                }
            }
        }
        [Description("Appearence of body."), Category("Appearance")]
        public ColorPair Pair
        {
            get { return pair; }
            set
            {
                if (pair != value)
                {
                    pair = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        public bool ShouldSerializeBindingImage()
        {
					return BindingImage != Resources.back;
        }

        public void ResetBindingImage()
        {
					BindingImage = Resources.back; ;
        }

        public bool ShouldSerializePair()
        {
            return pair != new ColorPair();
        }

        public void ResetPair()
        {
            pair = new ColorPair();
        }
    }
}