using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION.UTILERIAS
{
    public static class ThemeConfig
    {
        public static Color Principal = Color.FromArgb(5,54,34);
        public static Color Dark = Color.FromArgb(51,45,45);
        public static Color Light = Color.FromArgb(241, 241, 241);
        public static Color Enfasis = Color.FromArgb(24,168,82);

        public static Color Basep;

        public static void ThemeControls(Form frm, bool dark = false)
        {
            Basep = dark?Dark:Light;   
            frm.ForeColor = Principal;
            frm.BackColor = Basep;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.WindowState = FormWindowState.Normal;


            foreach (Control ctrl in frm.Controls)
            {

                if (ctrl is GroupBox)
                {
                    ((GroupBox)ctrl).ForeColor = Enfasis;

                    if (((GroupBox)ctrl).Controls.Count > 0)
                    {
                        RecorrerControles((GroupBox)ctrl);
                    }

                }
                else if (ctrl is Button)
                {

                    ((Button)ctrl).BackColor = Enfasis;
                    ((Button)ctrl).ForeColor = Principal;
                    ((Button)ctrl).Padding = new Padding(3, 0, 3, 0);

                }
                else if (ctrl is ContextMenuStrip)
                {

                    ((ContextMenuStrip)ctrl).BackColor = Basep;
                    ((ContextMenuStrip)ctrl).ForeColor = Principal;

                }else if(ctrl is Panel)
                {

                    if (((Panel)ctrl).Controls.Count > 0)
                    {
                        RecorrerControles((Panel)ctrl);
                    }
                }else if(ctrl is MenuStrip)
                {
                    ((MenuStrip)ctrl).BackColor=Enfasis;
                    ((MenuStrip)ctrl).ForeColor=Principal;
                    ((MenuStrip)ctrl).Font = new Font("Arial", 10f, FontStyle.Bold);
                    ((MenuStrip)ctrl).AutoSize = false;
                    ((MenuStrip)ctrl).Height = 40;
                    foreach(var item in ((MenuStrip)ctrl).Items)
                    {
                        foreach( var ditem in ((ToolStripMenuItem)item).DropDownItems)
                        {
                            ((ToolStripDropDownItem)ditem).BackColor = Enfasis;
                           // ((ToolStripDropDownItem)ditem).ForeColor = 
                        }
                    }
                }


            }
        }

        private static void RecorrerControles(Control ctrl)
        {
            foreach (Control ctrlG in ctrl.Controls)
            {
                if(ctrlG is GroupBox)
                {
                    RecorrerControles((GroupBox)ctrlG);
                }
                else if (ctrlG is Label)
                {
                    ((Label)ctrlG).ForeColor = Principal;
                    ((Label)ctrlG).BackColor = Basep;
                }
                else if (ctrlG is Button)
                {
                    ((Button)ctrlG).BackColor = Enfasis;
                    ((Button)ctrlG).Padding = new Padding(3, 0, 3, 0);
                }
                else if (ctrlG is ToolStrip)
                {
                    ((ToolStrip)ctrlG).ForeColor = Principal;
                    ((ToolStrip)ctrlG).BackColor = Basep;
                }
                else if (ctrlG is DataGridView)
                {
                    ((DataGridView)ctrlG).BackgroundColor = Basep;
                    ((DataGridView)ctrlG).ForeColor = Principal;
                    ((DataGridView)ctrlG).MultiSelect = false;

                    if (((DataGridView)ctrlG).ContextMenuStrip != null)
                    {
                        ((DataGridView)ctrlG).ContextMenuStrip.BackColor = Basep;
                        ((DataGridView)ctrlG).ContextMenuStrip.ForeColor = Enfasis;
                    }
                }
                else if (ctrlG is TextBox)
                {
                    ((TextBox)ctrlG).CharacterCasing = CharacterCasing.Upper;
                }
            }
        }

        public static void LimpiarControles(Form frm)
        {
            foreach (Control ctrl in frm.Controls)
            {
                if (ctrl is GroupBox)
                {
                    foreach (Control ctrlG in ((GroupBox)ctrl).Controls)
                    {
                        if (ctrlG is TextBox)
                        {
                            ((TextBox)ctrlG).Clear();
                        }
                        else if (ctrlG is DateTimePicker)
                        {
                            ((DateTimePicker)ctrlG).Value = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}
