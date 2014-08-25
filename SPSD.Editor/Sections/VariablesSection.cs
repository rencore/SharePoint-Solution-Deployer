#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SPSD.Editor.Dialogs;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Model;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Sections
{
    public partial class VariablesSection : UserControl, IFileHandler<SPSDEnvironmentVariables>
    {
        private static DataGridViewRowCollection _dgRowsSingelton;
        private readonly EditVariable _editVarDialog = new EditVariable();

        public VariablesSection()
        {
            InitializeComponent();
            _editVarDialog.SaveVariable += SaveVariable_Click;
            _dgRowsSingelton = dataGrid_Variables.Rows;
        }

        public void SetDefault()
        {
            dataGrid_Variables.Rows.Clear();
        }

        public void LoadEnv(SPSDEnvironmentVariables node)
        {
            if (node == null)
            {
                node = new SPSDEnvironmentVariables {};
            }
            dataGrid_Variables.Rows.Clear();
            if (node.Variable != null)
            {
                foreach (SPSDEnvironmentVariablesVariable variable in node.Variable)
                {
                    dataGrid_Variables.Rows.Add(new[] {variable.Name, variable.Value});
                }
            }
        }

        public SPSDEnvironmentVariables SaveEnv(SPSDEnvironmentVariables node)
        {
            if (node == null)
            {
                node = new SPSDEnvironmentVariables();
            }
            if (dataGrid_Variables.Rows.Count > 0)
            {
                ArrayList list = new ArrayList();
                foreach (DataGridViewRow row in dataGrid_Variables.Rows)
                {
                    object name = row.Cells["VariableName"].Value;
                    object value = row.Cells["VariableValue"].Value;
                    if (name != null && value != null)
                    {
                        list.Add(new SPSDEnvironmentVariablesVariable
                            {
                                Name = name.ToString(),
                                Value = value.ToString()
                            });
                    }
                }
                node.Variable =
                    list.Cast<SPSDEnvironmentVariablesVariable>().ToArray<SPSDEnvironmentVariablesVariable>();
            }
            return node;
        }

        private void SaveVariable_Click(object sender, EventArgs e)
        {
            if (dataGrid_Variables.CurrentRow != null)
            {
                dataGrid_Variables.CurrentRow.Cells["VariableValue"].Value = _editVarDialog.Variable;
            }
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void DataGrid_Variables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                DataGridView dg = (DataGridView) sender;
                Point location = dg.PointToScreen(Point.Empty);
                location.Y += dg.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Top;
                location.X += dg.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Left;
                ShowContextMenu(dg, !dg.Rows[e.RowIndex].IsNewRow, location);
            }
        }

        private void ShowContextMenu(DataGridView dg, bool allowDelete, Point location)
        {
            deleteRowToolStripMenuItem.Enabled = allowDelete;
            contextMenu_GridView.Show(new Button(), location);
        }


        private void CopyRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Assert(dataGrid_Variables.CurrentRow != null, "dataGrid_Variables.CurrentRow != null");
            DataGridViewRow row = (DataGridViewRow) dataGrid_Variables.CurrentRow.Clone();
            Clipboard.SetData("VariableRow", row);
        }

        private void PasteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Clipboard.GetData("VariableRow") as DataGridViewRow;
            if (row != null)
            {
                dataGrid_Variables.Rows.Add(row);
            }
        }

        private void DeleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGrid_Variables.Rows.Remove(dataGrid_Variables.CurrentRow);
            if (dataGrid_Variables.Rows.Count == 0)
            {
                dataGrid_Variables.Rows.Add();
            }
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        //private void ContextMenu_GridView_Opening(object sender, CancelEventArgs e)
        //{
        //    bool inRow = false; // dataGrid_Variables.GetChildAtPoint(savedClickLocation) is DataGridViewRow;
        //    DataGridViewRow row = Clipboard.GetData("VariableRow") as DataGridViewRow;
        //    pasteRowToolStripMenuItem.Enabled = row != null;
        //    editVariableToolStripMenuItem.Enabled = inRow;
        //    copyRowToolStripMenuItem.Enabled = inRow;
        //    // if disabled because of "isNewRow" then leave it that way
        //    deleteRowToolStripMenuItem.Enabled = deleteRowToolStripMenuItem.Enabled && inRow;
        //}

        private void EditVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Debug.Assert(dataGrid_Variables.CurrentRow != null, "dataGrid_Variables.CurrentRow != null");
            _editVarDialog.Variable = dataGrid_Variables.CurrentRow.Cells["VariableValue"].Value as string;
            _editVarDialog.ShowDialog();
        }


        public static List<KeyValuePair<string, string>> GetVariables()
        {
            List<KeyValuePair<string, string>> vars = new List<KeyValuePair<string, string>>();
            if (_dgRowsSingelton != null)
            {
                foreach (DataGridViewRow row in _dgRowsSingelton)
                {
                    if (!row.DataGridView.Enabled)
                    {
                        return vars;
                    }
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    object name = row.Cells["VariableName"].Value;
                    object value = row.Cells["VariableValue"].Value;

                    if (!string.IsNullOrEmpty(row.Cells["VariableName"].ErrorText) ||
                        !string.IsNullOrEmpty(row.Cells["VariableValue"].ErrorText))
                    {
                        EnvironmentFileHandler.AddNotification(
                            "The variables datagrid has still invalid or missing values.");
                    }

                    if (name != null && value != null)
                    {
                        vars.Add(new KeyValuePair<string, string>(name.ToString(), value.ToString()));
                    }
                }
                vars.Sort((x, y) => String.Compare(x.Key, y.Key, StringComparison.Ordinal));
            }
            return vars;
        }

        private void DataGrid_Variables_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            EnvironmentFileHandler.MakeSingletonDirty();
        }

        private void DataGrid_Variables_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Drawing.DataGridViewRenderValidationError(sender, e);
        }

        private void DataGrid_Variables_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = dataGrid_Variables.Rows[e.RowIndex];

            if (row.IsNewRow)
            {
                return;
            }
            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            object varName = cell == row.Cells["VariableName"] ? e.FormattedValue : row.Cells["VariableName"].Value;
            object varValue = cell == row.Cells["VariableValue"] ? e.FormattedValue : row.Cells["VariableValue"].Value;

            // general required field validation check
            if (varName == null || string.IsNullOrEmpty(varName.ToString()))
            {
                row.Cells["VariableName"].ErrorText = "This is a required field.";
            }
            else
            {
                row.Cells["VariableName"].ErrorText = "";
            }

            if (varValue == null || string.IsNullOrEmpty(varValue.ToString()))
            {
                row.Cells["VariableValue"].ErrorText = "This is a required field.";
            }
            else
            {
                row.Cells["VariableValue"].ErrorText = "";
            }
        }

        private void DataGrid_Variables_EnabledChanged(object sender, EventArgs e)
        {
            dataGrid_Variables.SetDataGridViewEnabledState();
        }
    }
}