#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SPSD.Editor.Sections;

#endregion

namespace SPSD.Editor.Controls
{
    public partial class VariablesButton : UserControl
    {
        public VariablesButton()
        {
            InitializeComponent();
        }

        private void Button_Variables_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Point location = control.PointToScreen(Point.Empty);
            location.Y += control.Height;
            contextMenu_Variables.Show(location);
        }

        private void InitVariablesMenu()
        {
            CreateUserVariableMenu();
            CreateSystemEnvMenu(varMenu_All);
            InitToolStripRecursive(varMenu_All.DropDownItems);
            InitToolStripRecursive(varMenu_Folders.DropDownItems);
            InitToolStripRecursive(varMenu_Machine.DropDownItems);
            InitToolStripRecursive(varMenu_User.DropDownItems);
        }

        private void CreateUserVariableMenu()
        {
            List<KeyValuePair<string, string>> vars = VariablesSection.GetVariables();
            varMenu_Custom.DropDownItems.Clear();
            foreach (KeyValuePair<string, string> var in vars)
            {
                ToolStripItem mItem = varMenu_Custom.DropDownItems.Add(var.Key);
                mItem.ToolTipText = var.Value;
                mItem.Tag = string.Format("$({0})", mItem.Text);
                mItem.Click += VariableMenuItemClick;
            }
        }

        private static void CreateSystemEnvMenu(ToolStripMenuItem item)
        {
            foreach (string key in Environment.GetEnvironmentVariables().Keys)
            {
                item.DropDownItems.Add(key);
            }
        }

        private void InitToolStripRecursive(ToolStripItemCollection items)
        {
            foreach (object item in items)
            {
                if (item.GetType() == typeof (ToolStripMenuItem))
                {
                    var mItem = item as ToolStripMenuItem;
                    string env = Environment.GetEnvironmentVariable(mItem.Text);
                    if (!string.IsNullOrEmpty(env) && (mItem.Tag == null || mItem.Tag.ToString() == ""))
                    {
                        mItem.ToolTipText = env;
                        mItem.Tag = string.Format("$(env:{0})", mItem.Text);
                        InitToolStripRecursive(mItem.DropDownItems);
                        mItem.Click += VariableMenuItemClick;
                    }
                }
            }
        }

        public event EventHandler VariableMenuItemClick;

        private void VariablesButton_Load(object sender, EventArgs e)
        {
            InitVariablesMenu();
        }
    }
}