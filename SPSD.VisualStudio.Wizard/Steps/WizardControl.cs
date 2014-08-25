using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace SPSD.VisualStudio.Wizard
{
    [DefaultEvent("Click"), Designer(typeof (WizardDesigner))]
    public class WizardControl : Control
    {
        #region Private Fields

        protected internal Button BackButton = new Button();
        private readonly Panel buttonHost = new Panel();
        protected internal Button CancelButton = new Button();
        private int currentStepIndex = -1;
        private string finishButtonText;
        protected internal Button HelpButton = new Button();
        protected internal Button NextButton = new Button();
        private string nextButtonText;
        internal int indexer;
        private readonly GenericCollection<WizardStep> wizardStepCollection;
        private readonly Panel controlHost = new Panel();
        
        #endregion

        #region Events

        [Category("Button events"), Description("The back button is clicked.")]
        public event CancelEventHandler BackButtonClick;

        [Description("The cancel button is clicked."), Category("Button events")]
        public event EventHandler CancelButtonClick;

        [Category("Property Changed"), Description("Ocurres after a current step index is changed.")]
        public event EventHandler CurrentStepIndexChanged;

        [Description("The finish button is clicked."), Category("Button events")]
        public event EventHandler FinishButtonClick;

        [Description("The next button is clicked."), Category("Button events")]
        public event GenericCancelEventHandler<WizardControl> NextButtonClick;

        [Description("The help button is clicked."), Category("Button events")]
        public event EventHandler HelpButtonClick;
        
        #endregion

        #region Constructor

        public WizardControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            finishButtonText = "Finish";
            nextButtonText = "Next >";
            InitializeComponent();
            wizardStepCollection = new GenericCollection<WizardStep>(this);
            wizardStepCollection.Inserted += wizardStepCollection_Inserted;
            wizardStepCollection.Cleared += wizardStepCollection_Cleared;
            wizardStepCollection.Removed += wizardStepCollection_Removed;
        }

        void wizardStepCollection_Removed(int index, WizardStep value)
        {
            value.Dispose();
            if (wizardStepCollection.Count != 1)
            {
                UpdateButtons();
            }
            else
            {
                OnSetFirstStep();
            }
        }

        private void wizardStepCollection_Cleared()
        {
            OnResetWizardSteps();
        }

        private void wizardStepCollection_Inserted(int index, WizardStep value)
        {
            if (wizardStepCollection.Count != 1)
            {
                UpdateButtons();
            }
            else
            {
                OnSetFirstStep();
            }
        }

        #endregion

        #region Private Methods

        private void InitializeComponent()
        {
            SuspendLayout();
            //BackButton.Location = new Point(213, 7);
            BackButton.Location = new Point(288, 7);
            BackButton.Size = new Size(75, 23);
            BackButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            BackButton.Text = "< Back";
            BackButton.Name = "BackButton";
            BackButton.Click += OnBackButtonClick;   
         
            //NextButton.Location = new Point(288, 7);
            NextButton.Location = new Point(370, 7);
            NextButton.Size = new Size(75, 23);
            NextButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            NextButton.Text = "Next >";
            NextButton.Name = "NextButton";
            NextButton.Click += delegate(object sender, EventArgs e)
                                    {
                                        if (CurrentStepIndex == (WizardSteps.Count - 1))
                                        {
                                            if (FinishButtonClick != null)
                                            {
                                                FinishButtonClick(sender, e);
                                            }
                                            return;
                                        }
                                        OnNextButtonClick(sender, e);
                                    };
            //CancelButton.Location = new Point(370, 7);
            CancelButton.Location = new Point(453, 7);
            CancelButton.Size = new Size(75, 23);
            CancelButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            CancelButton.Text = "Cancel";
            CancelButton.Name = "CancelButton";
            CancelButton.Click += delegate (object sender, EventArgs e)
                                      {
                                          if (CancelButtonClick != null)
                                          {
                                              CancelButtonClick(sender, e);
                                          }
                                      };
            HelpButton.Location = new Point(0, 7);
            HelpButton.Size = new Size(75, 23);
            HelpButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            HelpButton.Text = "Help";
            HelpButton.Name = "HelpButton";
            HelpButton.Click += delegate (object sender, EventArgs e)
                                    {
                                        if (HelpButtonClick != null)
                                        {
                                            HelpButtonClick(sender, e);
                                        }
                                    };
            controlHost.Size = new Size(534, 363);
            controlHost.Location = Point.Empty;
            controlHost.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            controlHost.Name = "WizardStepsPanel";
            controlHost.Visible = false;
            buttonHost.Size = new Size(534, 38);
            buttonHost.Location = new Point(0, 365);
            buttonHost.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            buttonHost.Name = "ButtonsPanel";
            buttonHost.Visible = false;            
            buttonHost.Controls.Add(BackButton);
            buttonHost.Controls.Add(NextButton);
            buttonHost.Controls.Add(CancelButton);
            buttonHost.Controls.Add(HelpButton);
            Size = new Size(534, 403);
            Controls.Add(controlHost);
            Controls.Add(buttonHost);
            ResumeLayout();
        }

        private void DoReLayout(int newIndex)
        {
          WizardStep nextstep = WizardSteps[newIndex];
          if (!nextstep.Enabled)
          {
            if (CurrentStepIndex < newIndex)
            {
              newIndex++;
            }
            else
            {
              newIndex--;
            }
          }

            SuspendLayout();
            if (controlHost.Controls.Count > 0)
            {
                controlHost.Controls.RemoveAt(0);
            }
            controlHost.Controls.Add(WizardSteps[newIndex]);
            currentStepIndex = newIndex;
            if (CurrentStepIndex != 0)
            {
                BackButton.Enabled = true;
            }
            else
            {
                BackButton.Enabled = false;
            }
            if (CurrentStepIndex != (wizardStepCollection.Count - 1))
            {
                NextButton.Text = nextButtonText;
            }
            else
            {
                NextButton.Text = finishButtonText;
            }
            ResumeLayout();
        }

        private void ResetBackButtonEnabled()
        {
            if (currentStepIndex <= 0)
            {
                BackButton.Enabled = false;
            }
            else
            {
                if (currentStepIndex > 0)
                {
                    BackButton.Enabled = true;
                }
            }
        }

        private void ResetBackButtonVisible()
        {
            BackButtonVisible = true;
        }

        private void ResetCancelButtonEnabled()
        {
            CancelButtonEnabled = true;
        }

        private void ResetCancelButtonVisible()
        {
            CancelButtonVisible = true;
        }

        private void ResetHelpButtonEnabled()
        {
            HelpButtonEnabled = true;
        }

        private void ResetHelpButtonVisible()
        {
            HelpButtonVisible = true;
        }

        private void ResetNextButtonEnabled()
        {
            NextButtonEnabled = true;
        }

        private void ResetNextButtonVisible()
        {
            NextButtonVisible = true;
        }

        internal void UpdateButtons()
        {
            SuspendLayout();
            if (CurrentStepIndex != 0)
            {
                BackButton.Enabled = true;
            }
            else
            {
                BackButton.Enabled = false;
            }
            if (CurrentStepIndex != (wizardStepCollection.Count - 1))
            {
                NextButton.Text = nextButtonText;
            }
            else
            {
                NextButton.Text = finishButtonText;
            }
            ResumeLayout();
        }

        #endregion

        #region Virtual Methods

        protected virtual void OnBackButtonClick(object sender, EventArgs e)
        {
            if (CurrentStepIndex == 0)
            {
                return;
            }
            if (DesignMode)
            {
                CurrentStepIndex--;
                return;
            }
            if (BackButtonClick == null)
            {
                int backStepIndex = WizardSteps[CurrentStepIndex].BackStepIndex;
                if (backStepIndex != -1)
                {
                    CurrentStepIndex = backStepIndex;
                    return;
                }
                CurrentStepIndex--;
                return;
            }
            else
            {
                CancelEventArgs args = new CancelEventArgs();
                BackButtonClick(this, args);
                if (args.Cancel)
                {
                    return;
                }
                int num = WizardSteps[CurrentStepIndex].BackStepIndex;
                if (num != -1)
                {
                    CurrentStepIndex = num;
                    return;
                }
                CurrentStepIndex--;
                return;
            }
        }

        protected internal virtual void OnChangeCurrentStepIndex(int newIndex, bool force)
        {
            if (newIndex < 0 || newIndex >= WizardSteps.Count)
            {
                throw new ArgumentOutOfRangeException("newIndex", "The new index must be a valid index of the WizardSteps collection property.");
            }
            if (CurrentStepIndex != newIndex)
            {
                DoReLayout(newIndex);
                if (CurrentStepIndexChanged != null)
                {
                    CurrentStepIndexChanged(this, EventArgs.Empty);
                }
            }
            else if (force)
            {
                DoReLayout(newIndex);
            }
        }

        protected virtual void OnNextButtonClick(object sender, EventArgs e)
        {
            int num;
            if (DesignMode)
            {
                CurrentStepIndex++;
                return;
            }
            else
            {
                num = 0;
                if (!(WizardSteps[CurrentStepIndex] is StartStep))
                {
                    if ((WizardSteps[CurrentStepIndex] is FinishStep))
                    {
                        num = -1;
                    }
                }
                else
                {
                    num = 1;
                }
            }
            if (NextButtonClick == null)
            {
                bool noFinish = false;
                int num2 = 0;
                if (!(WizardSteps[CurrentStepIndex + 1] is StartStep))
                {
                    if (!(WizardSteps[CurrentStepIndex + 1] is FinishStep))
                    {
                        noFinish = true;
                    }
                    else
                    {
                        num2 = -1;
                    }
                }
                else
                {
                    num2 = 1;
                }
                if (((indexer + num) + num2) >= 0)
                {
                    if ((((indexer + num) + num2) != 0) || !noFinish)
                    {
                        WizardSteps[CurrentStepIndex + 1].BackStepIndex = CurrentStepIndex;
                        CurrentStepIndex++;
                        indexer += num;
                    }
                }
                else
                {
                    throw new InvalidOperationException("The steps must be well formed, so there cannot be an IntermediateStep without enclosing.");
                }
            }
            else
            {
                GenericCancelEventArgs<WizardControl> args = new GenericCancelEventArgs<WizardControl>(this);
                NextButtonClick(this, args);
                if (args.Cancel)
                {
                    return;
                }
                int nextStep = GetNextStep();
                if ( nextStep != -1)
                {
                    WizardSteps[nextStep].BackStepIndex = CurrentStepIndex;
                    CurrentStepIndex = nextStep;
                    indexer += num;
                    return;
                }
                WizardSteps[CurrentStepIndex + 1].BackStepIndex = CurrentStepIndex;
                CurrentStepIndex++;
                indexer += num;
                return;
            }
        }

        private int GetNextStep()
        {
            int num;
            num = 0;
            if (!(WizardSteps[CurrentStepIndex] is StartStep))
            {
                if ((WizardSteps[CurrentStepIndex] is FinishStep))
                {
                    num = -1;
                }
            }
            else
            {
                num = 1;
            }
            bool noFinish = false;
            int num2 = 0;
            if (!(WizardSteps[CurrentStepIndex + 1] is StartStep))
            {
                if (!(WizardSteps[CurrentStepIndex + 1] is FinishStep))
                {
                    noFinish = true;
                }
                else
                {
                    num2 = -1;
                }
            }
            else
            {
                num2 = 1;
            }
            if (((indexer + num) + num2) >= 0 && ((indexer + num) + num2) != 0 || !noFinish)
            {
                return CurrentStepIndex + 1;
            }
            else
            {
                throw new InvalidOperationException("The step must be well formed, so there cannot be a Finishstep without a Startstep.");
            }
        }

        protected internal virtual void OnResetWizardSteps()
        {
            SuspendLayout();
            if (controlHost.Controls.Count > 0)
            {
                controlHost.Controls.RemoveAt(0);
            }
            buttonHost.Visible = false;
            controlHost.Visible = false;
            BackButton.Enabled = true;
            currentStepIndex = -1;
            Rectangle rectangle = new Rectangle(buttonHost.Left, buttonHost.Top - 2, buttonHost.Width, 2);
            Invalidate(rectangle, false);
            ResumeLayout();
            if (CurrentStepIndexChanged != null)
            {
                CurrentStepIndexChanged(this, EventArgs.Empty);
            }
        }

        protected internal virtual void OnSetFirstStep()
        {
            CurrentStepIndex = 0;
            SuspendLayout();
            controlHost.Visible = true;
            buttonHost.Visible = true;
            Rectangle rectangle = new Rectangle(buttonHost.Left, buttonHost.Top - 2, buttonHost.Width, 2);
            Invalidate(rectangle, false);
            ResumeLayout();
        }

        #endregion

        #region Overrides

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        ///</summary>
        ///
        ///<param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (WizardSteps.Count != 0)
            {
                ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(buttonHost.Left, buttonHost.Top - 2, buttonHost.Width, 2), Border3DStyle.Etched, Border3DSide.Top);
            }
            base.OnPaint(e);
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

        ///<summary>
        ///Gets or sets the font of the text displayed by the control.
        ///</summary>
        ///
        ///<returns>
        ///The <see cref="T:System.Drawing.Font"></see> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont"></see> property.
        ///</returns>
        ///<filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        ///<summary>
        ///Gets or sets the foreground color of the control.
        ///</summary>
        ///
        ///<returns>
        ///The foreground <see cref="T:System.Drawing.Color"></see> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor"></see> property.
        ///</returns>
        ///<filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        ///<summary>
        ///Gets or sets a value indicating whether the control can accept data that the user drags onto it.
        ///</summary>
        ///
        ///<returns>
        ///true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.
        ///</returns>
        ///<filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AllowDrop
        {
            get { return base.AllowDrop; }
            set { base.AllowDrop = true; }
        }

        ///<summary>
        ///Gets or sets the background color for the control.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Drawing.Color"></see> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"></see> property.
        ///</returns>
        ///<filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = SystemColors.Control; }
        }

        ///<summary>
        ///Gets or sets the background image displayed in the control.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Drawing.Image"></see> that represents the image to display in the background of the control.
        ///</returns>
        ///<filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = null; }
        }

        ///<summary>
        ///Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout"></see> enumeration.
        ///</summary>
        ///
        ///<returns>
        ///One of the values of <see cref="T:System.Windows.Forms.ImageLayout"></see> (<see cref="F:System.Windows.Forms.ImageLayout.Center"></see> , <see cref="F:System.Windows.Forms.ImageLayout.None"></see>, <see cref="F:System.Windows.Forms.ImageLayout.Stretch"></see>, <see cref="F:System.Windows.Forms.ImageLayout.Tile"></see>, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom"></see>). <see cref="F:System.Windows.Forms.ImageLayout.Tile"></see> is the default value.
        ///</returns>
        ///
        ///<exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified enumeration value does not exist. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = ImageLayout.None; }
        }

        ///<summary>
        ///Gets the default size of the control.
        ///</summary>
        ///
        ///<returns>
        ///The default <see cref="T:System.Drawing.Size"></see> of the control.
        ///</returns>
        ///
        protected override Size DefaultSize
        {
            get { return new Size(534, 403); }
        }

        ///<summary>
        ///Gets or sets the text associated with this control.
        ///</summary>
        ///
        ///<returns>
        ///The text associated with this control.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        [Browsable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        ///<summary>
        ///Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
        ///</summary>
        ///
        ///<returns>
        ///One of the <see cref="T:System.Windows.Forms.RightToLeft"></see> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit"></see>.
        ///</returns>
        ///
        ///<exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.RightToLeft"></see> values. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Browsable(false)]
        public override RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
            set { base.RightToLeft = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                for (int i = 0; i < WizardSteps.Count; i++)
                {
                    WizardSteps[i].Dispose();
                    WizardSteps[i] = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Public Property

        [Category("WizardControl Buttons Behavior"), Description("Defines if the back button is enabled or disabled.")]
        public bool BackButtonEnabled
        {
            get { return BackButton.Enabled; }
            set { BackButton.Enabled = value; }
        }

        [Description("Gets or sets the back button text."), Category("WizardControl Buttons Appearance"), DefaultValue("< Back")]
        public string BackButtonText
        {
            get { return BackButton.Text; }
            set { BackButton.Text = value; }
        }

        [Description("Defines the visibility of the back button."), Category("WizardControl Buttons Behavior")]
        public bool BackButtonVisible
        {
            get { return BackButton.Visible; }
            set { BackButton.Visible = value; }
        }

        [Description("Defines if the cancel button is enabled or disabled."), Category("WizardControl Buttons Behavior")]
        public bool CancelButtonEnabled
        {
            get { return CancelButton.Enabled; }
            set { CancelButton.Enabled = value; }
        }

        [Description("Gets or sets the cancel button text."), DefaultValue("Cancel"), Category("WizardControl Buttons Appearance")]
        public string CancelButtonText
        {
            get { return CancelButton.Text; }
            set { CancelButton.Text = value; }
        }

        [Description("Defines the visibility of the cancel button."), Category("WizardControl Buttons Behavior")]
        public bool CancelButtonVisible
        {
            get { return CancelButton.Visible; }
            set { CancelButton.Visible = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Gets or sets the value of the current wizard step index based on the WizardSteps collection property."), DefaultValue(0), Category("Behavior")]
        public int CurrentStepIndex
        {
            get { return currentStepIndex; }
            set { OnChangeCurrentStepIndex(value, false); }
        }


        [Description("Gets or sets the finish button text."), DefaultValue("Finish"), Category("WizardControl Buttons Appearance")]
        public string FinishButtonText
        {
            get { return finishButtonText; }
            set
            {
                finishButtonText = value;
                if (CurrentStepIndex == (wizardStepCollection.Count - 1))
                {
                    NextButton.Text = finishButtonText;
                }
                else
                {
                    NextButton.Text = nextButtonText;
                }
            }
        }




        [Description("Defines if the help button is enabled or disabled."), Category("WizardControl Buttons Behavior")]
        public bool HelpButtonEnabled
        {
            get { return HelpButton.Enabled; }
            set { HelpButton.Enabled = value; }
        }

        [Category("WizardControl Buttons Appearance"), Description("Gets or sets the help button text."), DefaultValue("Help")]
        public string HelpButtonText
        {
            get { return HelpButton.Text; }
            set { HelpButton.Text = value; }
        }

        [Category("WizardControl Buttons Behavior"), Description("Defines the visibility of the help button.")]
        public bool HelpButtonVisible
        {
            get { return HelpButton.Visible; }
            set { HelpButton.Visible = value; }
        }

        [Description("Defines if the next button is enabled or disabled."), Category("WizardControl Buttons Behavior")]
        public bool NextButtonEnabled
        {
            get { return NextButton.Enabled; }
            set { NextButton.Enabled = value; }
        }

        [DefaultValue("Next >"), Category("WizardControl Buttons Appearance"), Description("Gets or sets the next button text.")]
        public string NextButtonText
        {
            get { return nextButtonText; }
            set
            {
                nextButtonText = value;
                if (CurrentStepIndex != (wizardStepCollection.Count - 1))
                {
                    NextButton.Text = nextButtonText;
                    return;
                }
                NextButton.Text = finishButtonText;
            }
        }



        [Category("WizardControl Buttons Behavior"), Description("Defines the visibility of the next button.")]
        public bool NextButtonVisible
        {
            get { return NextButton.Visible; }
            set { NextButton.Visible = value; }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            private set { base.TabIndex = 0; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            private set { base.TabStop = false; }
        }

        [Editor(typeof(WizardStepCollectionEditor), typeof(UITypeEditor)), Description("Gets a collection containing the step. This property returns the same collection than the Controls property."), TypeConverter(typeof(GenericCollectionConverter<WizardStep>)), Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual GenericCollection<WizardStep> WizardSteps
        {
            get { return wizardStepCollection; }
        }

        #endregion
    }
}