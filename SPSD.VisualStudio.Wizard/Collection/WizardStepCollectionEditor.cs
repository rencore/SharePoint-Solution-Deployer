using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace SPSD.VisualStudio.Wizard
{
    internal class WizardStepCollectionEditor : CollectionEditor
    {
        public WizardStepCollectionEditor(Type type) : base(type)
        {
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] {typeof (StartStep), typeof(LicenceStep), typeof (IntermediateStep), typeof (FinishStep)};
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            GenericCollection<WizardStep> steps = (GenericCollection<WizardStep>) value;
            WizardControl owner = (WizardControl) steps.Owner;
            IDesignerHost container = (IDesignerHost) context.Container;
            int count = steps.Count;
            object obj2 = base.EditValue(context, provider, value);
            if (steps.Count >= count)
            {
                return obj2;
            }
            SelectWizard(owner, container);
            return obj2;
        }

        private static void SelectWizard(IComponent wizardControl, IDesignerHost host)
        {
            if (wizardControl == null)
            {
                return;
            }
            if (host == null)
            {
                return;
            }
            while (true)
            {
                WizardDesigner designer = (WizardDesigner) host.GetDesigner(wizardControl);
                if (designer == null)
                {
                    return;
                }
                ISelectionService service = (ISelectionService) host.GetService(typeof (ISelectionService));
                if (service == null)
                {
                    return;
                }
                object[] components = new object[] {wizardControl};
                service.SetSelectedComponents(components, SelectionTypes.Replace);
                return;
            }
        }
    }
}