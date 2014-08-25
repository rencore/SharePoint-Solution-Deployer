using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SPSD.VisualStudio.Wizard
{
    internal class WizardDesigner : ParentControlDesigner
    {
        #region Private Fields

        private readonly DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
        private bool forwardOnDrag;
        private bool isSelected;
        private WizardDesignerActionList wizardDesignerActionList;

        #endregion

        protected override void Dispose(bool disposing)
        {
            ISelectionService service = (ISelectionService) GetService(typeof (ISelectionService));
            if (service != null)
            {
                service.SelectionChanged -= OnSelectionChanged;
            }
            WizardControl control = (WizardControl) Control;
            control.CurrentStepIndexChanged -= CurrentStepIndexChanged;
            control.WizardSteps.Inserted -= RefreshComponent;
            IDesignerHost host = (IDesignerHost) GetService(typeof (IDesignerHost));
            IEnumerator enumerator = control.WizardSteps.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    WizardStep component = (WizardStep) enumerator.Current;
                    host.DestroyComponent(component);
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void CurrentStepIndexChanged(object sender, EventArgs e)
        {
            RefreshComponent(-1, null);
        }

        private WizardStepDesigner GetDesigner()
        {
            WizardControl control = Control as WizardControl;
            WizardStep component = null;
            IDesignerHost service = null;
            WizardStepDesigner designer = null;
            if (control != null && control.WizardSteps.Count >= 0)
            {
                component = control.WizardSteps[control.CurrentStepIndex];
                service = (IDesignerHost) GetService(typeof (IDesignerHost));
                designer = null;
            }
            if (service == null)
            {
                return designer;
            }
            if (component == null)
            {
                return designer;
            }
            designer = (WizardStepDesigner) service.GetDesigner(component);
            return designer;
        }

        ///<summary>
        ///Indicates whether a mouse click at the specified point should be handled by the control.
        ///</summary>
        ///
        ///<returns>
        ///true if a click at the specified point is to be handled by the control; otherwise, false.
        ///</returns>
        ///
        ///<param name="point">A <see cref="T:System.Drawing.Point"></see> indicating the position at which the mouse was clicked, in screen coordinates. </param>
        protected override bool GetHitTest(Point point)
        {
            WizardControl control;
            if (!isSelected)
            {
                return false;
            }
            control = (WizardControl) Control;
            if (control.WizardSteps.Count <= 0)
            {
                return false;
            }
            return HitTestBack(control, point);
        }

        private static bool HitTestBack(WizardControl control, Point point)
        {
            if (!control.BackButton.Visible)
            {
                return HitTestNext(control, point);
            }
            Point pt = control.BackButton.PointToClient(point);
            Rectangle rect = control.BackButton.ClientRectangle;
            if (!rect.Contains(pt))
            {
                return HitTestNext(control, point);
            }
            return true;
        }

        private static bool HitTestNext(WizardControl control, Point point)
        {
            if (!control.NextButton.Visible)
            {
                return HitTestCancel(control, point);
            }
            Point pt = control.NextButton.PointToClient(point);
            Rectangle rect = control.NextButton.ClientRectangle;
            if (!rect.Contains(pt))
            {
                return HitTestCancel(control, point);
            }
            return true;
        }

        private static bool HitTestCancel(WizardControl control, Point point)
        {
            if (!control.CancelButton.Visible)
            {
                return HitTestHelp(control, point);
            }
            Point pt = control.CancelButton.PointToClient(point);
            Rectangle rect = control.CancelButton.ClientRectangle;
            if (!rect.Contains(pt))
            {
                return HitTestHelp(control, point);
            }
            return true;
        }

        private static bool HitTestHelp(WizardControl control, Point point)
        {
            if (!control.HelpButton.Visible)
            {
                return false;
            }
            Point pt = control.HelpButton.PointToClient(point);
            Rectangle rect = control.HelpButton.ClientRectangle;
            if (!rect.Contains(pt))
            {
                return false;
            }
            return true;
        }

        private WizardStepDesigner GetWizardStepDesigner(IComponent step)
        {
            IDesignerHost service = (IDesignerHost) GetService(typeof (IDesignerHost));
            WizardStepDesigner designer = null;
            if (service == null)
            {
                return designer;
            }
            designer = (WizardStepDesigner) service.GetDesigner(step);
            return designer;
        }

        ///<summary>
        ///Initializes the designer with the specified component.
        ///</summary>
        ///
        ///<param name="component">The <see cref="T:System.ComponentModel.IComponent"></see> to associate with the designer. </param>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            AutoResizeHandles = true;
            ISelectionService service = (ISelectionService) GetService(typeof (ISelectionService));
            if (service != null)
            {
                service.SelectionChanged += OnSelectionChanged;
            }
            WizardControl control = (WizardControl) Control;
            wizardDesignerActionList = new WizardDesignerActionList(control);
            actionListCollection.Add(wizardDesignerActionList);
            control.CurrentStepIndexChanged += CurrentStepIndexChanged;
            control.WizardSteps.Inserted += RefreshComponent;
        }

        ///<param name="defaultValues">TBD</param>
        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
            WizardControl control = Control as WizardControl;
            if (control == null)
            {
                return;
            }
            IDesignerHost service = (IDesignerHost) GetService(typeof (IDesignerHost));
            if (service == null)
            {
                return;
            }
            StartStep step = (StartStep) service.CreateComponent(typeof (StartStep));
            control.WizardSteps.Add(step);
            LicenceStep licStep = (LicenceStep)service.CreateComponent(typeof(LicenceStep));
            control.WizardSteps.Add(licStep);
            IntermediateStep step2 = (IntermediateStep) service.CreateComponent(typeof (IntermediateStep));
            control.WizardSteps.Add(step2);
            FinishStep step3 = (FinishStep) service.CreateComponent(typeof (FinishStep));
            control.WizardSteps.Add(step3);
        }

        ///<summary>
        ///Called in order to clean up a drag-and-drop operation.
        ///</summary>
        ///
        ///<param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs"></see> that provides data for the event.</param>
        protected override void OnDragComplete(DragEventArgs de)
        {
            if (forwardOnDrag)
            {
                forwardOnDrag = false;
                GetDesigner().OnDragCompleteInternal(de);
            }
            else
            {
                base.OnDragComplete(de);
            }
        }

        ///<summary>
        ///Called when a drag-and-drop object is dropped onto the control designer view.
        ///</summary>
        ///
        ///<param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs"></see> that provides data for the event. </param>
        protected override void OnDragDrop(DragEventArgs de)
        {
            if (forwardOnDrag)
            {
                forwardOnDrag = false;
                WizardStepDesigner currentWizardStepDesigner = GetDesigner();
                if (currentWizardStepDesigner != null)
                {
                    currentWizardStepDesigner.OnDragDropInternal(de);
                }
            }
            else
            {
                de.Effect = DragDropEffects.None;
            }
        }

        ///<summary>
        ///Called when a drag-and-drop operation enters the control designer view.
        ///</summary>
        ///
        ///<param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs"></see> that provides data for the event. </param>
        protected override void OnDragEnter(DragEventArgs de)
        {
            WizardControl control = (WizardControl) Control;
            if (control.WizardSteps.Count <= 0)
            {
                base.OnDragEnter(de);
                return;
            }
            WizardStep step = control.WizardSteps[control.CurrentStepIndex];
            Point pt = step.PointToClient(new Point(de.X, de.Y));
            Rectangle clientRectangle = step.ClientRectangle;
            if (!clientRectangle.Contains(pt))
            {
                base.OnDragEnter(de);
                return;
            }
            GetWizardStepDesigner(step).OnDragEnterInternal(de);
            forwardOnDrag = true;
        }

        ///<summary>
        ///Called when a drag-and-drop operation leaves the control designer view.
        ///</summary>
        ///
        ///<param name="e">An <see cref="T:System.EventArgs"></see> that provides data for the event. </param>
        protected override void OnDragLeave(EventArgs e)
        {
            if (forwardOnDrag)
            {
                forwardOnDrag = false;
                WizardStepDesigner currentWizardStepDesigner = GetDesigner();
                if (currentWizardStepDesigner == null)
                {
                    return;
                }
                currentWizardStepDesigner.OnDragLeaveInternal(e);
                return;
            }
            base.OnDragLeave(e);
        }

        ///<summary>
        ///Called when a drag-and-drop object is dragged over the control designer view.
        ///</summary>
        ///
        ///<param name="de">A <see cref="T:System.Windows.Forms.DragEventArgs"></see> that provides data for the event. </param>
        protected override void OnDragOver(DragEventArgs de)
        {
            WizardControl control = Control as WizardControl;
            if (control == null || control.WizardSteps.Count <= 0)
            {
                de.Effect = DragDropEffects.None;
                return;
            }
            WizardStep step = control.WizardSteps[control.CurrentStepIndex];
            Point pt = step.PointToClient(new Point(de.X, de.Y));
            WizardStepDesigner wizardStepDesigner = GetWizardStepDesigner(step);
            Rectangle clientRectangle = step.ClientRectangle;
            if (!clientRectangle.Contains(pt))
            {
                if (!forwardOnDrag)
                {
                    de.Effect = DragDropEffects.None;
                    return;
                }
                forwardOnDrag = false;
                wizardStepDesigner.OnDragLeaveInternal(EventArgs.Empty);
                base.OnDragEnter(de);
                return;
            }
            else
            {
                if (!forwardOnDrag)
                {
                    base.OnDragLeave(EventArgs.Empty);
                    wizardStepDesigner.OnDragEnterInternal(de);
                    forwardOnDrag = true;
                    return;
                }
                wizardStepDesigner.OnDragOverInternal(de);
                return;
            }
        }

        ///<summary>
        ///Receives a call when a drag-and-drop operation is in progress to provide visual cues based on the location of the mouse while a drag operation is in progress.
        ///</summary>
        ///
        ///<param name="e">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs"></see> that provides data for the event. </param>
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            if (forwardOnDrag)
            {
                WizardStepDesigner currentWizardStepDesigner = GetDesigner();
                if (currentWizardStepDesigner == null)
                {
                    return;
                }
                currentWizardStepDesigner.OnGiveFeedbackInternal(e);
            }
            else
            {
                base.OnGiveFeedback(e);
            }
        }

        ///<summary>
        ///Called when the control that the designer is managing has painted its surface so the designer can paint any additional adornments on top of the control.
        ///</summary>
        ///
        ///<param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that provides data for the event. </param>
        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            WizardControl control = (WizardControl) Control;
            if (control == null)
            {
                return;
            }
            if (control.WizardSteps.Count != 0)
            {
                return;
            }
            Pen pen = new Pen(SystemColors.ControlDark);
            try
            {
                pen.DashStyle = DashStyle.Dash;
                Rectangle rect = control.Bounds;
                rect.Location = new Point(0, 0);
                rect.Width--;
                rect.Height--;
                pe.Graphics.DrawRectangle(pen, rect);
                return;
            }
            finally
            {
                pen.Dispose();
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            ISelectionService service = (ISelectionService) GetService(typeof (ISelectionService));
            if (service == null)
            {
                return;
            }
            isSelected = false;
            ICollection selectedComponents = service.GetSelectedComponents();
            if (selectedComponents == null)
            {
                return;
            }
            WizardControl control = (WizardControl) Control;
            IEnumerator enumerator = selectedComponents.GetEnumerator();
            if (enumerator == null)
            {
                return;
            }
            try
            {
                while (enumerator.MoveNext())
                {
                    object current = enumerator.Current;
                    if (current == control)
                    {
                        isSelected = true;
                        break;
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null) disposable.Dispose();
            }
        }

        private void RefreshComponent(int index, WizardStep value)
        {
            DesignerActionUIService service = GetService(typeof (DesignerActionUIService)) as DesignerActionUIService;
            if (service == null)
            {
                return;
            }
            service.Refresh(Control);
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
            get { return actionListCollection; }
        }

        ///<summary>
        ///Gets the collection of components associated with the component managed by the designer.
        ///</summary>
        ///
        ///<returns>
        ///The components that are associated with the component managed by the designer.
        ///</returns>
        ///
        public override ICollection AssociatedComponents
        {
            get { return ((WizardControl) Control).WizardSteps; }
        }
    }
}