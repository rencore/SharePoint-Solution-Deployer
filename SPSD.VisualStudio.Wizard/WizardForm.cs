using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnvDTE;

namespace SPSD.VisualStudio.Wizard
{
  public partial class WizardForm : Form
  {
    internal List<string> TargetProjects;
    internal List<string> AdditionalWSPs;

    public WizardForm(object automationObject)
    {
      InitializeComponent();

      LoadProjects(automationObject);

      initVariablesMenu();
      initControls();

    }

    private void initControls()
    {
        comboBox_SPVersion.SelectedIndex = 1;
        comboBox_SPLicense.SelectedIndex = 0;

        Wizard_CurrentStepIndexChanged(null, null);

        checkBox_PrereqSolutions_CheckedChanged(null, null);

        checkBox_RecycleAppPool_CheckedChanged(null, null);
        checkBox_IISReset_CheckedChanged(null, null);
        checkBox_SPUserCodeV4_CheckedChanged(null, null);
        checkBox_SPAdminV4_CheckedChanged(null, null);
        checkBox_SPTimerV4_CheckedChanged(null, null);
        checkBox_Warmup_CheckedChanged(null, null);
    }
    private void LoadProjects(object automationObject)
    {
      checkedListBox1.Items.Clear();
      if (automationObject is EnvDTE.DTE)
      {
        foreach(Project project in Helpers.GetAllProjects(automationObject as EnvDTE.DTE))
        {
          checkedListBox1.Items.Add(project.Name, true);
        }
      }
    }

    internal string get_CustomMessage()
    {
      return "hallo";
    }

    private void Wizard_FinishButtonClick(object sender, EventArgs e)
    {
      TargetProjects = new List<string>();
      if (checkBox_IncludeWSPs.Checked)
      {
        foreach (object s in checkedListBox1.CheckedItems)
        {
          TargetProjects.Add(s.ToString());
        }
      }

      AdditionalWSPs = new List<string>();
      if (checkBox_AdditionalWSPs.Checked)
      {
        if (this.textBox_wspfiles.Text != "")
        {
          char[] sep = new char[] { ';' };
          foreach (string s in this.textBox_wspfiles.Text.Split(sep))
          {
            AdditionalWSPs.Add(s.ToString());
          }
        }
      }

      this.DialogResult = System.Windows.Forms.DialogResult.OK;
      Close();
    }

    private void Wizard_CancelButtonClick(object sender, EventArgs e)
    {
      this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      Close();
    }

    private void WizardForm_Load(object sender, EventArgs e)
    {

    }

    private void Wizard_HelpButtonClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process process = new System.Diagnostics.Process();
      process.StartInfo.UseShellExecute = true;
      process.StartInfo.FileName = "http://spsd.codeplex.com";
      process.Start();

    }

    private void checkBox_AdditionalWSPs_CheckedChanged(object sender, EventArgs e)
    {
      panel_AdditionalWSPs.Enabled = checkBox_AdditionalWSPs.Checked;
    }

    private void checkBox_IncludeWSPs_CheckedChanged(object sender, EventArgs e)
    {
      panel_IncludeWSPs.Enabled = checkBox_IncludeWSPs.Checked;
    }

    private void openFileDialogWSP_FileOk(object sender, CancelEventArgs e)
    {
      this.textBox_wspfiles.Text = GetFilenames(openFileDialogWSP.FileNames);
    }

    private string GetFilenames(string[] p)
    {
      
      string result = "";
      foreach (string s in p)
      {
        if (result != "")
        {
          result += ";";
        }
        result += s;
      }
      return result;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      openFileDialogWSP.ShowDialog();
    }

    private void label14_Click(object sender, EventArgs e)
    {

    }

    private void intermediateStep3_Click(object sender, EventArgs e)
    {

    }

    public string ForceSolutionDeployment { get { return checkBox_ForceSolutionDeployment.Checked.ToString().ToLower();  } }

    public string OverwriteExistingSolutions { get { return checkBox_OverwriteExistingSolutions.Checked.ToString().ToLower(); } }

    public string SharePointVersion { get { return comboBox_SPVersion.SelectedItem.ToString(); } }

    public string AllowGACDeployment { get { return checkBox_AllowGACDeployment.Checked.ToString().ToLower(); } }

    public string AllowCASPolicies { get { return checkBox_AllowCASPolicies.Checked.ToString().ToLower(); } }

    public string DisplayWizards { get { return checkBox_DisplayWizards.Checked.ToString().ToLower(); } }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process process = new System.Diagnostics.Process();
      process.StartInfo.UseShellExecute = true;
      process.StartInfo.FileName = "http://spalm.codeplex.com";
      process.Start();
    }

    private void ConfigRestrictions_Click(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void Wizard_CurrentStepIndexChanged(object sender, EventArgs e)
    {
        //progressBar.Maximum = this.Wizard.WizardSteps.Count;
        //progressBar.Value = this.Wizard.CurrentStepIndex;
        //progressBar.Text = string.Format("Step {0} of {1}", progressBar.Value, progressBar.Maximum);
    }

    private void Introduction_Click(object sender, EventArgs e)
    {

    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void ConfigActions_Click(object sender, EventArgs e)
    {

    }

    private void checkBox_Warmup_CheckedChanged(object sender, EventArgs e)
    {
        checkBox_Warmup_deploy.Enabled = checkBox_Warmup.Checked;
        checkBox_Warmup_update.Enabled = checkBox_Warmup.Checked;
        checkBox_Warmup_AllWebApps.Enabled = checkBox_Warmup.Checked;
        checkBox_Warmup_AllSites.Enabled = checkBox_Warmup.Checked;
        textBox_Warmup_customUrls.Enabled = checkBox_Warmup.Checked;
    }

    private void checkBox_SPTimerV4_CheckedChanged(object sender, EventArgs e)
    {
        
        checkBox_SPTimerV4_deploy.Enabled = checkBox_SPTimerV4.Checked;
        checkBox_SPTimerV4_update.Enabled = checkBox_SPTimerV4.Checked;
        checkBox_SPTimerV4_retract.Enabled = checkBox_SPTimerV4.Checked;
        checkBox_SPTimerV4_force.Enabled = checkBox_SPTimerV4.Checked;
    }

    private void checkBox_SPAdminV4_CheckedChanged(object sender, EventArgs e)
    {
        checkBox_SPAdminV4_deploy.Enabled = checkBox_SPAdminV4.Checked;
        checkBox_SPAdminV4_update.Enabled = checkBox_SPAdminV4.Checked;
        checkBox_SPAdminV4_retract.Enabled = checkBox_SPAdminV4.Checked;
        checkBox_SPAdminV4_force.Enabled = checkBox_SPAdminV4.Checked;
    }

    private void checkBox_SPUserCodeV4_CheckedChanged(object sender, EventArgs e)
    {
        checkBox_SPUserCodeV4_deploy.Enabled = checkBox_SPUserCodeV4.Checked;
        checkBox_SPUserCodeV4_update.Enabled = checkBox_SPUserCodeV4.Checked;
        checkBox_SPUserCodeV4_retract.Enabled = checkBox_SPUserCodeV4.Checked;
        checkBox_SPUserCodeV4_force.Enabled = checkBox_SPUserCodeV4.Checked;
    }

    private void checkBox_IISReset_CheckedChanged(object sender, EventArgs e)
    {
        checkBox_IISReset_deploy.Enabled = checkBox_IISReset.Checked;
        checkBox_IISReset_update.Enabled = checkBox_IISReset.Checked;
        checkBox_IISReset_retract.Enabled = checkBox_IISReset.Checked;
        checkBox_IISReset_force.Enabled = checkBox_IISReset.Checked;
    }

    private void checkBox_RecycleAppPool_CheckedChanged(object sender, EventArgs e)
    {
        checkBox_RecycleAppPool_All.Enabled = checkBox_RecycleAppPool.Checked;
        checkBox_RecycleAppPool_deploy.Enabled = checkBox_RecycleAppPool.Checked;
        checkBox_RecycleAppPool_update.Enabled = checkBox_RecycleAppPool.Checked;
        checkBox_RecycleAppPool_retract.Enabled = checkBox_RecycleAppPool.Checked;
    }

    private void button_Warmup_variables_Click(object sender, EventArgs e)
    {
        Control control = sender as Control;
        Point location = control.PointToScreen(Point.Empty);
        location.Y += control.Height;
        contextMenu_Variables.Show(location);
    }

    private void textBox_Integer_Validating(object sender, CancelEventArgs e)
    {
        TextBox tb = sender as TextBox;
        try
        {
            int value = Int32.Parse(tb.Text);
            if (value < 0)
            {
                value = value * -1;
                tb.Text = value.ToString();
            }
        }
        catch
        {
            MessageBox.Show("The value of this textbox has to be a positive integer value", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tb.Text = "0";
        }

    }

    private void addVariable_Click(object sender, EventArgs e)
    {
        
    }

    private void initVariablesMenu()
    {
        createSystemEnvMenu(varMenu_All);
        initToolStripRecursive(varMenu_All.DropDownItems);
        initToolStripRecursive(varMenu_Folders.DropDownItems);
        initToolStripRecursive(varMenu_Machine.DropDownItems);
        initToolStripRecursive(varMenu_User.DropDownItems);
    }
    private void initToolStripRecursive(ToolStripItemCollection items)
    {
        foreach(var item in items){
            if (item.GetType() == typeof(ToolStripMenuItem))
            {
                var mItem = item as ToolStripMenuItem;
                string env = Environment.GetEnvironmentVariable(mItem.Text);
                if (!string.IsNullOrEmpty(env))
                {
                    mItem.ToolTipText = env;
                    mItem.Tag = string.Format("$(env:{0})", mItem.Text);
                    initToolStripRecursive(mItem.DropDownItems);
                    mItem.Click += addVariable_Click;
                }
            }
        }
    }
    private void createSystemEnvMenu(ToolStripMenuItem item)
    {
        foreach (string key in Environment.GetEnvironmentVariables().Keys)
        {
              item.DropDownItems.Add(key);   
        }
    }

    private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
    {

    }

    private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
    {

    }

    private void wizardControl1_HelpButtonClick(object sender, EventArgs e)
    {

    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void checkBox_PrereqSolutions_CheckedChanged(object sender, EventArgs e)
    {
        dataGrid_prereqSolutions.Enabled = checkBox_prereqSolutions.Checked;
    }

    private void copyRowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        DataGridViewRow row = (DataGridViewRow)dataGrid_prereqSolutions.CurrentRow.Clone();
        Clipboard.SetData("SolutionRow", row);
    }

    private void pasteRowToolStripMenuItem_Click(object sender, EventArgs e)
    {
            DataGridViewRow row = Clipboard.GetData("SolutionRow") as DataGridViewRow;
            if (row != null)
            {
                dataGrid_prereqSolutions.Rows.Add(row);
            }
    }

    private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        dataGrid_prereqSolutions.Rows.Remove(dataGrid_prereqSolutions.CurrentRow);
    }

    private void contextMenu_GridView_Opening(object sender, CancelEventArgs e)
    {
        bool inRow = ((ContextMenuStrip) sender).SourceControl is Button;

        DataGridViewRow row = Clipboard.GetData("SolutionRow") as DataGridViewRow;
        pasteRowToolStripMenuItem.Enabled = row != null;
        copyRowToolStripMenuItem.Enabled = inRow;
        deleteRowToolStripMenuItem.Enabled = inRow;
    }


      private void dataGrid_prereqSolutions_CellClick(object sender, DataGridViewCellEventArgs e)
      {
      DataGridView dg = (DataGridView)sender;
      DataGridViewCell dc = dg.Rows[e.RowIndex].Cells[e.ColumnIndex];
      if (dc is DataGridViewButtonCell)
        {
            DataGridViewButtonCell control = dc as DataGridViewButtonCell;

            Point location = dg.PointToScreen(Point.Empty);
            location.Y += dg.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Bottom;
            location.X += dg.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Left;
            contextMenu_GridView.Show(new Button(), location);
        }
      }
  }
}
