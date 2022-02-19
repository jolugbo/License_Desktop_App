using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UKLicense.Utilities
{
    public static class FormHelper
    {
             
        /// <summary>
        /// Load a normal form in no-dialog or dialog form
        /// </summary>
        /// <param name="moduleMainFormName"></param>
        /// <param name="FormName"></param>
        /// <param name="initializeControl"></param>
        /// <param name="isDialogForm"></param>
        /// <param name="setFormSize"></param>
        public static void LoadForm(Form moduleMainFormName, Form FormName, bool initializeControl = true, bool isDialogForm = true, bool setFormSize = true)
        {

            try
            {
                Form f = new Form();
                f = FormName;
                if (initializeControl) ControlDefaultSettings(FormName);
                if (setFormSize)
                {
                    f.Size = f.MaximumSize = f.Size;
                }
                f.Icon = moduleMainFormName.Icon;
                f.StartPosition = FormStartPosition.CenterParent;
                f.AutoScroll = true;
                if (isDialogForm)
                {
                    f.ShowDialog();
                }
                else
                {
                    f.Show();
                }

                f.Dispose();
                f = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Loads a form in a Maximised windows state
        /// </summary>
        /// <param name="projectMainFormName"></param>
        /// <param name="moduleMainFormName"></param>
        public static void LoadMainModuleForm(Form projectMainFormName, Form moduleMainFormName)
        {
            try
            {
                if (projectMainFormName != null)
                {
                    projectMainFormName.Hide();
                }
                
                moduleMainFormName.Icon = projectMainFormName.Icon;
                moduleMainFormName.WindowState = FormWindowState.Maximized;
                moduleMainFormName.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                projectMainFormName.Show();
            }
        }

        public static void ShowChildForm(Form parentForm, Form childForm)
        {
            
            var f = new Form();
            f = childForm;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            Point parentPoint = parentForm.Location;// parentForm.Location;


            int parentHeight = parentForm.Height;
            int parentWidth = parentForm.Width;
            f.MdiParent = parentForm;
            f.Size = new System.Drawing.Size((parentWidth-215), (parentHeight - 50));//- 200
            

            f.StartPosition = FormStartPosition.Manual;

            f.Location = new Point(0, 0);
            
            f.TopMost = true;
            f.Show();
            

        }

        static void ControlDefaultSettings(Control controlName)
        {
            try
            {
                foreach (Control con in controlName.Controls)
                {
                    if (con is ComboBox)
                    {
                        var normCtrl = (ComboBox)con;
                        normCtrl.DropDownHeight = 120;
                        if (normCtrl.DropDownStyle == ComboBoxStyle.DropDown)
                        {
                            normCtrl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            normCtrl.AutoCompleteSource = AutoCompleteSource.ListItems;

                        }
                        normCtrl.SelectedIndex = -1;

                    }//End DropDown
                    else if (con is Button)
                    {
                        var normBtn = (Button)con;
                        normBtn.FlatStyle = FlatStyle.Flat;
                        normBtn.Cursor = Cursors.Hand;
                    }
                    else if (con is ListView)
                    {
                        var normListView = (ListView)con;
                        normListView.MultiSelect = false;
                        normListView.FullRowSelect = true;
                    }
                    else
                    {
                        ControlDefaultSettings(con);
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void CenterControl(this Control control)
        {
            control.CenterHorizontally();
            control.CenterVertically();
        }



        private static void CenterHorizontally(this Control control)
        {
            Rectangle parentRect = control.Parent.ClientRectangle;
            control.Left = (parentRect.Width - control.Width) / 2;

        }



        private static void CenterVertically(this Control control)
        {
            Rectangle parentRect = control.Parent.ClientRectangle;
            control.Top = (parentRect.Height - control.Height) / 2;

        }




        //

        public static void CenterNamedControl(Control parentControl, Control childControl)
        {
            CenterNamedHorizontally(parentControl, childControl);
            CenterNamedVertically(parentControl, childControl);
        }



        private static void CenterNamedHorizontally(Control parentControl,Control childControl)
        {
            Rectangle parentRect = parentControl.ClientRectangle;
            childControl.Left = (parentRect.Width - childControl.Width) / 2;

        }



        private static void CenterNamedVertically(Control parentControl, Control childControl)
        {
            Rectangle parentRect = parentControl.ClientRectangle;
            childControl.Top = (parentRect.Height - childControl.Height) / 2;

        }
    
    
    }
}
