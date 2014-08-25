using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SPSD.VisualStudio.Wizard
{
    internal class WizardStepDesigner : ParentControlDesigner
    {
        private WizardStep wizardStep;

        public override void Initialize(IComponent component)
        {
            wizardStep = (WizardStep) component;
            base.Initialize(component);
        }

        public WizardStepDesigner()
        {
            AutoResizeHandles = true;
        }

        ///<summary>
        ///Gets the design-time action lists supported by the component associated with the designer.
        ///</summary>
        ///
        ///<returns>
        ///The design-time action lists supported by the component associated with the designer.
        ///</returns>
        ///
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
                WizardStepDesignerActionList designerActionList = new WizardStepDesignerActionList(wizardStep);
                actionListCollection.Add(designerActionList);
                return actionListCollection;
            }
        }

        ///<summary>
        ///Indicates if this designer's control can be parented by the control of the specified designer.
        ///</summary>
        ///
        ///<returns>
        ///true if the control managed by the specified designer can parent the control managed by this designer; otherwise, false.
        ///</returns>
        ///
        ///<param name="parentDesigner">The <see cref="T:System.ComponentModel.Design.IDesigner"></see> that manages the control to check. </param>
        public override bool CanBeParentedTo(IDesigner parentDesigner)
        {
            if (parentDesigner == null)
            {
                return false;
            }
            return (parentDesigner.Component is WizardControl);
        }

        internal void OnDragCompleteInternal(DragEventArgs de)
        {
            OnDragComplete(de);
        }

        internal void OnDragDropInternal(DragEventArgs de)
        {
            OnDragDrop(de);
        }

        internal void OnDragEnterInternal(DragEventArgs de)
        {
            OnDragEnter(de);
        }

        internal void OnDragLeaveInternal(EventArgs e)
        {
            OnDragLeave(e);
        }

        internal void OnDragOverInternal(DragEventArgs e)
        {
            OnDragOver(e);
        }

        internal void OnGiveFeedbackInternal(GiveFeedbackEventArgs e)
        {
            OnGiveFeedback(e);
        }

        public override SelectionRules SelectionRules
        {
            get { return (base.SelectionRules & ~(SelectionRules.Moveable | SelectionRules.AllSizeable)); }
        }


        internal class WizardStepDesignerActionList : DesignerActionList
        {
            public WizardStepDesignerActionList(IComponent component) : base(component)
            {
            }

            protected virtual WizardStep WizardStep
            {
                get { return (WizardStep)Component; }
            }

            ///<summary>
            ///Gets or sets a value indicating whether the smart tag panel should automatically be displayed when it is created.
            ///</summary>
            ///
            ///<returns>
            ///true if the panel should be shown when the owning component is created; otherwise, false. The default is false.
            ///</returns>
            ///
            public override bool AutoShow
            {
                get
                {
                    return true;
                }
                set
                {
                    base.AutoShow = value;
                }
            }

            ///<summary>
            ///Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"></see> objects contained in the list.
            ///</summary>
            ///
            ///<returns>
            ///A <see cref="T:System.ComponentModel.Design.DesignerActionItem"></see> array that contains the items in this list.
            ///</returns>
            ///
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                DesignerActionItemCollection items = new DesignerActionItemCollection();
                items.Add(new DesignerActionHeaderItem("Appearence", "Appearence"));
                items.Add(new DesignerActionMethodItem(this, "ResetAppearence", "Reset Appearence", "Appearence", true));
                if(WizardStep is StartStep)
                {
                    items.Add(new DesignerActionPropertyItem("StartTitle", "Title", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("StartSubTitle", "SubTitle", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("StartBindingImage", "BindingImage", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("StartIcon", "Icon", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("StartLeftPair", "Left pane appearence", "Appearence"));
                }

                if (WizardStep is LicenceStep)
                {
                    items.Add(new DesignerActionPropertyItem("LicenceTitle", "Title", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("LicenceSubTitle", "SubTitle", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("LicenceBindingImage", "BindingImage", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("LicenceHeaderPair", "HeaderPair", "Appearence"));
                    items.Add(new DesignerActionHeaderItem("Licence", "Licence"));
                    items.Add(new DesignerActionPropertyItem("LicenceAccepted", "Licence Accepted", "Licence"));
                    items.Add(new DesignerActionPropertyItem("LicenceAcceptText", "Licence AcceptText", "Licence"));
                    items.Add(new DesignerActionPropertyItem("LicenceDeclineText", "Licence DeclineText", "Licence"));
                    items.Add(new DesignerActionPropertyItem("LicenceLicenseFile", "License File", "Licence"));
                }

                if (WizardStep is IntermediateStep)
                {
                    items.Add(new DesignerActionPropertyItem("IntermediateTitle", "Title", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("IntermediateSubTitle", "SubTitle", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("IntermediateBindingImage", "BindingImage", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("IntermediateHeaderPair", "HeaderPair", "Appearence"));
                }

                if(WizardStep is FinishStep)
                {
                    items.Add(new DesignerActionPropertyItem("FinishBindingImage", "BindingImage", "Appearence"));
                    items.Add(new DesignerActionPropertyItem("FinishPair", "Pair", "Appearence"));
                }
                return items;
            }

            #region Start page actions

            public virtual Image StartBindingImage
            {
                get { return ((StartStep)WizardStep).BindingImage ; }
                set
                {
                    if (((StartStep)WizardStep).BindingImage != value)
                    {
                        ((StartStep)WizardStep).BindingImage = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
            public virtual string StartTitle
            {
                get { return ((StartStep)WizardStep).Title; }
                set
                {
                    if (((StartStep)WizardStep).Title != value)
                    {
                        ((StartStep)WizardStep).Title = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
            public virtual string StartSubTitle
            {
                get { return ((StartStep)WizardStep).Subtitle; }
                set
                {
                    if (((StartStep)WizardStep).Subtitle != value)
                    {
                        ((StartStep)WizardStep).Subtitle = value;
                        WizardStep.Invalidate();
                    }
                }
            }

            public virtual Image StartIcon
            {
                get { return ((StartStep)WizardStep).Icon; }
                set
                {
                    if (((StartStep)WizardStep).Icon != value)
                    {
                        ((StartStep)WizardStep).Icon = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            public virtual ColorPair StartLeftPair
            {
                get { return ((StartStep) WizardStep).LeftPair; }
                set
                {
                    if (((StartStep)WizardStep).LeftPair != value)
                    {
                        ((StartStep)WizardStep).LeftPair = value;
                        WizardStep.Invalidate();
                    }
                }
            }

            #endregion

            #region Licence page actions

            public virtual Image LicenceBindingImage
            {
                get { return ((LicenceStep)WizardStep).BindingImage; }
                set
                {
                    if (((LicenceStep)WizardStep).BindingImage != value)
                    {
                        ((LicenceStep)WizardStep).BindingImage = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
            public virtual string LicenceTitle
            {
                get { return ((LicenceStep)WizardStep).Title; }
                set
                {
                    if (((LicenceStep)WizardStep).Title != value)
                    {
                        ((LicenceStep)WizardStep).Title = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
            public virtual string LicenceSubTitle
            {
                get { return ((LicenceStep)WizardStep).Subtitle; }
                set
                {
                    if (((LicenceStep)WizardStep).Subtitle != value)
                    {
                        ((LicenceStep)WizardStep).Subtitle = value;
                        WizardStep.Invalidate();
                    }
                }
            }

            public virtual bool? LicenceAccepted
            {
                get { return ((LicenceStep)WizardStep).Accepted; }
                set
                {
                    if (((LicenceStep)WizardStep).Accepted != value)
                    {
                        ((LicenceStep)WizardStep).Accepted = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            [Editor(typeof(CustomFileNameEditor), typeof(UITypeEditor))]
            public virtual string LicenceLicenseFile
            {
                get { return ((LicenceStep)WizardStep).LicenseFile; }
                set
                {
                    if (((LicenceStep)WizardStep).LicenseFile != value)
                    {
                        ((LicenceStep)WizardStep).LicenseFile = value;
                        WizardStep.Invalidate();
                    }
                }
            }

            public virtual string LicenceAcceptText
            {
                get { return ((LicenceStep)WizardStep).AcceptText; }
                set
                {
                    if (((LicenceStep)WizardStep).AcceptText != value)
                    {
                        ((LicenceStep)WizardStep).AcceptText = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            public virtual string LicenceDeclineText
            {
                get { return ((LicenceStep)WizardStep).DeclineText; }
                set
                {
                    if (((LicenceStep)WizardStep).DeclineText != value)
                    {
                        ((LicenceStep)WizardStep).DeclineText = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            public virtual ColorPair LicenceHeaderPair
            {
                get { return ((LicenceStep)WizardStep).HeaderPair; }
                set
                {
                    ((LicenceStep)WizardStep).HeaderPair = value;
                    WizardStep.Invalidate();
                }
            }
            #endregion

            #region Licence page actions

            public virtual Image IntermediateBindingImage
            {
                get { return ((IntermediateStep)WizardStep).BindingImage; }
                set
                {
                    if (((IntermediateStep)WizardStep).BindingImage != value)
                    {
                        ((IntermediateStep)WizardStep).BindingImage = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
            public virtual string IntermediateTitle
            {
                get { return ((IntermediateStep)WizardStep).Title; }
                set
                {
                    if (((IntermediateStep)WizardStep).Title != value)
                    {
                        ((IntermediateStep)WizardStep).Title = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
            public virtual string IntermediateSubTitle
            {
                get { return ((IntermediateStep)WizardStep).Subtitle; }
                set
                {
                    if (((IntermediateStep)WizardStep).Subtitle != value)
                    {
                        ((IntermediateStep)WizardStep).Subtitle = value;
                        WizardStep.Invalidate();
                    }
                }
            }
            public virtual ColorPair IntermediateHeaderPair
            {
                get { return ((IntermediateStep)WizardStep).HeaderPair; }
                set
                {
                    if (((IntermediateStep)WizardStep).HeaderPair != value)
                    {
                        ((IntermediateStep)WizardStep).HeaderPair = value;
                        WizardStep.Invalidate();
                    }
                }
            }

            #endregion

            #region Finish Step actions

            public virtual ColorPair FinishPair
            {
                get { return ((FinishStep)WizardStep).Pair; }
                set
                {
                    if (((FinishStep)WizardStep).Pair != value)
                    {
                        ((FinishStep)WizardStep).Pair = value;
                        WizardStep.Invalidate();
                    }
                }
            }

            public virtual Image FinishBindingImage
            {
                get { return ((FinishStep)WizardStep).BindingImage; }
                set
                {
                    if (((FinishStep)WizardStep).BindingImage != value)
                    {
                        ((FinishStep)WizardStep).BindingImage = value;
                        WizardStep.Invalidate();
                    }
                }
            }

            #endregion



            protected virtual void ResetAppearence()
            {
                WizardStep.Reset();
                WizardStep.Invalidate();
            }
        }
    }
}