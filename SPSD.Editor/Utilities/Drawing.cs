#region

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SPSD.Editor.Properties;

#endregion

namespace SPSD.Editor.Utilities
{
    public static class Drawing
    {
        public static void DataGridViewRenderValidationError(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }
            //Paint everything except the ErrorIcon the standard way.
            e.Paint(e.ClipBounds, e.PaintParts & ~DataGridViewPaintParts.ErrorIcon);
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                //Paint the ErrorIcon, if necessary, the custom way.
                if ((e.PaintParts & DataGridViewPaintParts.ErrorIcon) == DataGridViewPaintParts.ErrorIcon)
                {
                    DataGridViewCell cell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if (!string.IsNullOrEmpty(e.ErrorText) && !cell.ReadOnly)
                    {
                        GraphicsState gstate = e.Graphics.Save();
                        Rectangle iconRect = new Rectangle(e.CellBounds.Right - 20, e.CellBounds.Top + 2, 17, 17);
                        Color backColor;
                        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                        {
                            backColor = e.CellStyle.SelectionBackColor;
                        }
                        else
                        {
                            backColor = e.CellStyle.BackColor;
                        }

                        //Restrict drawing within cell boundaries.
                        e.Graphics.SetClip(e.CellBounds);

                        //Clear background area behind the icon.
                        using (SolidBrush brush = new SolidBrush(backColor))
                        {
                            e.Graphics.FillRectangle(brush, iconRect);
                        }

                            //Draw the icon.
                            e.Graphics.DrawIcon(Resources.warning_icon, iconRect);
                            //Get inline rectancle to draw red line
                            Rectangle rect = new Rectangle(e.CellBounds.Location, e.CellBounds.Size);
                            if (e.ColumnIndex == 0)
                            {
                                rect.X++;
                                rect.Width -= 2;
                            }
                            else
                            {
                                rect.Width--;
                            }
                            rect.Height--;
                            ControlPaint.DrawBorder(e.Graphics, rect, Color.Red, ButtonBorderStyle.Solid);
                            e.Graphics.Restore(gstate);
                    }
                }
            }
            finally
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Toggles the "enabled" status of a cell in a DataGridView. There is no native
        ///     support for disabling a cell, hence the need for this method. The disabled state
        ///     means that the cell is read-only and grayed out.
        /// </summary>
        /// <param name="dc">Cell to enable/disable</param>
        /// <param name="enabled">Whether the cell is enabled or disabled</param>
        public static void SetCellEnabledState(this DataGridViewCell dc, bool enabled)
        {
            //toggle read-only state
            if (enabled)
            {
                //restore cell style to the default value
                dc.Style.BackColor = dc.OwningColumn.DefaultCellStyle.BackColor;
                dc.Style.ForeColor = dc.OwningColumn.DefaultCellStyle.ForeColor;
                dc.Style.SelectionBackColor = dc.OwningColumn.DefaultCellStyle.SelectionBackColor;
                dc.Style.SelectionForeColor = dc.OwningColumn.DefaultCellStyle.SelectionForeColor;
            }
            else
            {
                //gray out the cell
                dc.Style.BackColor = SystemColors.Control;
                dc.Style.ForeColor = SystemColors.ControlDark;
                dc.Style.SelectionBackColor = SystemColors.Control;
                dc.Style.SelectionForeColor = SystemColors.ControlDark;
            }
            dc.ReadOnly = !enabled;
        }

        public static void SetDataGridViewEnabledState(this DataGridView dg)
        {
            foreach (DataGridViewRow row in dg.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.SetCellEnabledState(dg.Enabled);
                }
            }
            dg.ColumnHeadersDefaultCellStyle.ForeColor = dg.Enabled ? SystemColors.WindowText : SystemColors.ControlDark;
        }
    }
}