using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SPSD.VisualStudio.Wizard
{
    internal class CustomFileNameEditor : FileNameEditor
    {
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            base.InitializeDialog(openFileDialog);
            openFileDialog.Filter = "Rich text format ( *.rtf )| *.rtf|Text documents ( *.txt )|*.txt|Word document ( *.doc ) |*.doc|All Files|*.*";
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
        }
    }
}
