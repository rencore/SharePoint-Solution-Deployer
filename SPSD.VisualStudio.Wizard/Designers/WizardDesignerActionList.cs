using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

namespace SPSD.VisualStudio.Wizard
{
    internal class WizardDesignerActionList : DesignerActionList
    {
        public WizardDesignerActionList(IComponent component) : base(component)
        {
        }

        protected internal virtual void AddFinishStep()
        {
            IDesignerHost service = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (WizardControl == null || service == null)
            {
                return;
            }
            FinishStep step = (FinishStep) service.CreateComponent(typeof (FinishStep));
            if (WizardControl.WizardSteps.Count != 0)
            {
                WizardControl.WizardSteps.Insert(WizardControl.CurrentStepIndex, step);
                RemoveWizardFromSelection();
                SelectWizard();
                return;
            }
            WizardControl.WizardSteps.Add(step);
            RemoveWizardFromSelection();
            SelectWizard();
        }

        protected internal virtual void AddCustomStep()
        {
            IDesignerHost service = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (WizardControl == null || service == null)
            {
                return;
            }
            IntermediateStep step = (IntermediateStep) service.CreateComponent(typeof (IntermediateStep));
            if (WizardControl.WizardSteps.Count != 0)
            {
                WizardControl.WizardSteps.Insert(WizardControl.CurrentStepIndex, step);
                RemoveWizardFromSelection();
                SelectWizard();
            }
            else
            {
                WizardControl.WizardSteps.Add(step);
                RemoveWizardFromSelection();
                SelectWizard();
            }
        }

        protected internal virtual void AddStartStep()
        {
            IDesignerHost service = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (WizardControl == null || service == null)
            {
                return;
            }
            StartStep step = (StartStep) service.CreateComponent(typeof (StartStep));
            if (WizardControl.WizardSteps.Count != 0)
            {
                WizardControl.WizardSteps.Insert(WizardControl.CurrentStepIndex, step);
                RemoveWizardFromSelection();
                SelectWizard();
                return;
            }
            else
            {
                WizardControl.WizardSteps.Add(step);
                RemoveWizardFromSelection();
                SelectWizard();
                return;
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            if (WizardControl == null)
            {
                return new DesignerActionItemCollection();
            }
            if (WizardControl.CurrentStepIndex != -1)
            {
                DesignerActionItemCollection items = new DesignerActionItemCollection();
                items.Add(new DesignerActionHeaderItem("Add Steps"));
                items.Add(new DesignerActionPropertyItem("WizardSteps", "New Wizard Step", "Add Steps"));
                items.Add(new DesignerActionMethodItem(this, "AddStartStep", "Add Start Step", "Add Steps", true));
                items.Add(new DesignerActionMethodItem(this, "AddCustomStep", "Add Custom Step", "Add Steps", true));
                items.Add(new DesignerActionMethodItem(this, "AddFinishStep", "Add Finish Step", "Add Steps", true));
                if (WizardControl.CurrentStepIndex == -1)
                {
                    return items;
                }
                items.Add(new DesignerActionHeaderItem("Remove Step"));
                items.Add(new DesignerActionMethodItem(this, "RemoveStep", "Remove Step", "Remove Step", true));
                items.Add(new DesignerActionMethodItem(this, "RemoveAllSteps", "Remove All Steps", "Remove Step", true));
                if (WizardControl.WizardSteps.Count >= 1)
                {
                    items.Add(new DesignerActionHeaderItem("Step navigation"));
                    if (WizardControl.CurrentStepIndex > 0)
                    {
                        items.Add(new DesignerActionMethodItem(this, "PreviousStep", "Previous Step", "Step navigation", true));
                    }
                    if (WizardControl.CurrentStepIndex != (WizardControl.WizardSteps.Count - 1))
                    {
                        items.Add(new DesignerActionMethodItem(this, "NextStep", "Next Step", "Step navigation", true));
                    }
                }
                items.Add(new DesignerActionHeaderItem("Layout"));
                items.Add(new DesignerActionPropertyItem("DockStyle", "Dock editor", "Layout"));
                return items;
            }
            return new DesignerActionItemCollection();
        }

        protected internal virtual void NextStep()
        {
            if (WizardControl == null)
            {
                return;
            }
            WizardControl.CurrentStepIndex++;
            RemoveWizardFromSelection();
            SelectWizard();
        }

        protected internal virtual void PreviousStep()
        {
            WizardControl wizardControl = WizardControl;
            if (wizardControl == null)
            {
                return;
            }
            wizardControl.CurrentStepIndex--;
            RemoveWizardFromSelection();
            SelectWizard();
        }

        protected internal virtual void RemoveAllSteps()
        {
            IDesignerHost service = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (WizardControl == null || service == null)
            {
                return;
            }
            if (MessageBox.Show(WizardControl.FindForm(), "Are you sure you want to remove all the steps?", "Remove Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            WizardStep[] array = new WizardStep[WizardControl.WizardSteps.Count];
            ((ICollection)WizardControl.WizardSteps).CopyTo(array, 0);
            WizardControl.WizardSteps.Clear();
            WizardStep[] stepArray2 = array;
            for (int index = 0; index < stepArray2.Length; index++)
            {
                WizardStep component = stepArray2[index];
                service.DestroyComponent(component);
                index++;
            }
            SelectWizard();
        }

        protected internal virtual void RemoveStep()
        {
            IDesignerHost service = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (WizardControl == null || service == null)
            {
                return;
            }
            if (MessageBox.Show(WizardControl.FindForm(), "Are you sure you want to remove the step?", "Remove Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                WizardStep step = WizardControl.WizardSteps[WizardControl.CurrentStepIndex];
                WizardControl.WizardSteps.Remove(step);
                service.DestroyComponent(step);
                step.Dispose();
            }
            SelectWizard();
        }

        protected void RemoveWizardFromSelection()
        {
            ISelectionService service = (ISelectionService)GetService(typeof(ISelectionService));
            if (WizardControl == null || service == null)
            {
                return;
            }
            object[] components = new object[] { WizardControl };
            service.SetSelectedComponents(components, SelectionTypes.Remove);
            return;
        }

        protected void SelectWizard()
        {
            ISelectionService service = (ISelectionService)GetService(typeof(ISelectionService));
            if (WizardControl == null || service == null)
            {
                return;
            }
            object[] components = new object[] {WizardControl};
            service.SetSelectedComponents(components, SelectionTypes.Replace);
            return;
        }

        protected virtual WizardControl WizardControl
        {
            get { return (WizardControl) Component; }
        }

        [TypeConverter(typeof(GenericCollectionConverter<GenericCollection<WizardStep>>)), Editor(typeof(WizardStepCollectionEditor), typeof(UITypeEditor))]
        public GenericCollection<WizardStep> WizardSteps
        {
            get
            {
                if (WizardControl == null)
                {
                    return null;
                }
                return WizardControl.WizardSteps;
            }
        }

        public virtual DockStyle DockStyle
        {
            get { return WizardControl.Dock; }
            set
            {
                if (WizardControl.Dock != value)
                {
                    WizardControl.Dock = value;
                    WizardControl.Invalidate();
                }
            }
        }
    }
}