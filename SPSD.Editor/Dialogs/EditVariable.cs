#region

using System;
using System.Windows.Forms;

#endregion

namespace SPSD.Editor.Dialogs
{
    public partial class EditVariable : Form
    {
        public EditVariable()
        {
            InitializeComponent();
        }

        public string Variable
        {
            get { return textBox_variable.Text.Trim(); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    textBox_variable.Text = value.Trim();
                }
                else textBox_variable.Text = string.Empty;
            }
        }

        private void VariableMenuItem_Click(object sender, EventArgs e)
        {
            string insertText = (sender as ToolStripMenuItem).Tag.ToString();
            if (!string.IsNullOrEmpty(insertText))
            {
                int cursorPosition = textBox_variable.SelectionStart;
                textBox_variable.Text = textBox_variable.Text.Insert(cursorPosition, insertText);
                textBox_variable.SelectionStart = cursorPosition + insertText.Length;
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Hide();
        }

        private void Clear()
        {
            textBox_variable.Text = "";
        }

        public event EventHandler SaveVariable;

        private void button_Save_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void EditVariable_Load(object sender, EventArgs e)
        {
            button_Save.Click += SaveVariable;
        }

        private void variablesButton1_Load(object sender, EventArgs e)
        {
        }
    }
}