#region

using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace SPSD.Editor.Dialogs
{
    public partial class EditUrls : Form
    {
        public EditUrls()
        {
            InitializeComponent();
        }

        public string Urls
        {
            get
            {
                return string.Join(";",
                                   (from url in listBox_Urls.Items.Cast<string>()
                                    where !string.IsNullOrEmpty(url.Trim())
                                    select url.Trim()).ToArray<string>());
            }
            set
            {
                listBox_Urls.Items.Clear();
                if (!string.IsNullOrEmpty(value))
                {
                    listBox_Urls.Items.AddRange(
                        (from url in value.Trim().Split(';')
                         where !string.IsNullOrEmpty(url.Trim())
                         select url.Trim()).ToArray());
                }
            }
        }

        private void VariableMenuItem_Click(object sender, EventArgs e)
        {
            string insertText = (sender as ToolStripMenuItem).Tag.ToString();
            if (!string.IsNullOrEmpty(insertText))
            {
                int cursorPosition = textBox_Url.SelectionStart;
                textBox_Url.Text = textBox_Url.Text.Insert(cursorPosition, insertText);
                textBox_Url.SelectionStart = cursorPosition + insertText.Length;
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_Url.Text))
            {
                listBox_Urls.Items.Add(textBox_Url.Text.Trim());
                textBox_Url.Text = "";
            }
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            if (listBox_Urls.SelectedIndices.Count > 0)
            {
                ArrayList list = new ArrayList();
                foreach (int index in listBox_Urls.SelectedIndices)
                {
                    list.Add(listBox_Urls.Items[index]);
                }
                foreach (object item in list)
                {
                    listBox_Urls.Items.Remove(item);
                }
            }
        }

        private void listBox_Urls_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_Edit.Enabled = listBox_Urls.SelectedIndex > -1;
        }

        private void listBox_Urls_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control c = listBox_Urls.GetChildAtPoint(e.Location);

            if (listBox_Urls.SelectedIndex > -1)
            {
                textBox_Url.Text = listBox_Urls.SelectedItem.ToString().Trim();
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Hide();
        }

        private void Clear()
        {
            listBox_Urls.Items.Clear();
            textBox_Url.Text = "";
        }

        public event EventHandler SaveUrls;

        private void button_Save_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void EditUrls_Load(object sender, EventArgs e)
        {
            button_Save.Click += SaveUrls;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            if (listBox_Urls.SelectedIndex > -1)
            {
                textBox_Url.Text = listBox_Urls.SelectedItem.ToString().Trim();
                listBox_Urls.Items.RemoveAt(listBox_Urls.SelectedIndex);
            }
            button_Edit.Enabled = false;
        }
    }
}