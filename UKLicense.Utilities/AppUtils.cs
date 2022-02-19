using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;

namespace UKLicense.Utility
{
    public class AppUtils
    {
        //Private setErrorNotification As New ErrorProvider
        ErrorProvider setErrorNotification = new ErrorProvider();
        ToolTip controlToolTips = new ToolTip();
        //Public Function NoEmptyControl(Optional ByVal containerControls As Control = Nothing, Optional ByVal controlsToCheck As Generic.IList(Of Control) = Nothing) As Boolean

        /// <summary>
        /// Check through a list of controls to see if their empty
        /// </summary>
        /// <param name="container"></param>
        /// <param name="controlsToCheck"></param>
        /// <returns>true/false</returns>
        public bool NoEmptyControl(Control container = null, System.Collections.Generic.IList<Control> controlsToCheck = null)
        {
            bool result = false;
            if (null == container && null == controlsToCheck)
            {
                return result;
            }

            //=========================Check First variable(containerControls)==========================
            //check if containercontrols isnot Nothing. If is not nothing then check if the control(or controls it contain) is empty 

            if (container != null)
            {
                if (container.GetType() == typeof(TextBox) || container.GetType() == typeof(ComboBox) || container.GetType() == typeof(Label))
                {
                    if (string.IsNullOrEmpty(container.Text.Trim()))
                    {
                        container.Focus();
                        setErrorNotification.SetError(container, "Please input data into the field.");
                        return result;
                    }
                    else
                    {
                        setErrorNotification.SetError(container, "");
                    }

                }

                foreach (object con in container.Controls)
                {
                    if (con.GetType() == typeof(TextBox))
                    {

                        if (string.IsNullOrEmpty(((TextBox)con).Text.Trim()))
                        {
                            ((TextBox)con).Focus();
                            setErrorNotification.SetError(((TextBox)con), "Please input data into the field.");
                            return result;
                        }
                        else
                        {
                            setErrorNotification.SetError(((TextBox)con), "");
                        }
                    }

                    if (con.GetType() == typeof(ComboBox))
                    {

                        if (string.IsNullOrEmpty(((ComboBox)con).Text.Trim()))
                        {
                            ((ComboBox)con).Focus();
                            setErrorNotification.SetError(((ComboBox)con), "Please input data into the field.");
                            return result;
                        }
                        else
                        {
                            setErrorNotification.SetError(((ComboBox)con), "");
                        }
                    }
                }//End foreach


            }


            if (controlsToCheck != null)
            {
                foreach (object conObject in controlsToCheck)
                {
                    if (conObject.GetType() == typeof(TextBox))
                    {
                        if (string.IsNullOrEmpty(((TextBox)conObject).Text.Trim()))
                        {
                            ((TextBox)conObject).Focus();
                            setErrorNotification.SetError(((TextBox)conObject), "Please input data into the field.");
                            return result;
                        }
                        else
                        {
                            setErrorNotification.SetError(((TextBox)conObject), "");
                        }
                    }

                    if (conObject.GetType() == typeof(ComboBox))
                    {

                        if (string.IsNullOrEmpty(((ComboBox)conObject).Text.Trim()))
                        {
                            ((ComboBox)conObject).Focus();
                            setErrorNotification.SetError(((ComboBox)conObject), "Please input data into the field.");
                            return result;
                        }
                        else
                        {
                            setErrorNotification.SetError(((ComboBox)conObject), "");
                        }
                    }


                    if (conObject.GetType() == typeof(ContainerControl))
                    {
                        foreach (Control con in ((ContainerControl)conObject).Controls)
                        {
                            if (con.GetType() == typeof(TextBox))
                            {
                                if (string.IsNullOrEmpty(((TextBox)con).Text.Trim()))
                                {
                                    ((TextBox)con).Focus();
                                    setErrorNotification.SetError(((TextBox)con), "Please input data into the field.");
                                    return result;
                                }
                                else
                                {
                                    setErrorNotification.SetError(((TextBox)con), "");
                                }
                            }

                            if (con.GetType() == typeof(ComboBox))
                            {

                                if (string.IsNullOrEmpty(((ComboBox)con).Text.Trim()))
                                {
                                    ((ComboBox)con).Focus();
                                    setErrorNotification.SetError(((ComboBox)con), "Please input data into the field.");
                                    return result;
                                }
                                else
                                {
                                    setErrorNotification.SetError(((ComboBox)con), "");
                                }
                            }
                        }

                    }

                }
            }

            result = true;
            return result;
        }

        /// <summary>
        /// Clear data in controls
        /// </summary>
        /// <param name="controlList"></param>
        public void ClearDataInControls(IList<Control> controlList)
        {
            if (controlList == null) return;

            foreach (Control con in controlList)
            {
                if (con.GetType() == typeof(TextBox))
                {
                    ((TextBox)con).Text = string.Empty;
                    setErrorNotification.SetError(((TextBox)con), "");
                }
                else if (con.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)con).Text = string.Empty;
                    ((ComboBox)con).SelectedIndex = -1;
                    setErrorNotification.SetError(((ComboBox)con), "");
                }
                else if (con.GetType() == typeof(DateTimePicker))
                {
                    ((DateTimePicker)con).Value = DateTime.Now;
                    ((DateTimePicker)con).Checked = false;

                    setErrorNotification.SetError(((DateTimePicker)con), "");
                }

                else if (con.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)con).Checked = false;
                    setErrorNotification.SetError(((CheckBox)con), "");
                }
                else if (con.GetType() == typeof(RadioButton))
                {
                    ((RadioButton)con).Checked = false;
                    setErrorNotification.SetError(((RadioButton)con), "");
                }
                else if (con.GetType() == typeof(PictureBox))
                {
                    ((PictureBox)con).ImageLocation = null;
                    ((PictureBox)con).Image = null;
                    setErrorNotification.SetError(((PictureBox)con), "");
                }
            }
        }

        /// <summary>
        /// Check if a combo box has value
        /// </summary>
        /// <param name="comboName"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public bool checkDataInCombo(ComboBox comboName, string errorText = null)
        {
            bool result = false;
            int comboIndex;

            if (comboName.Text.Trim() == string.Empty && errorText == null)
            {
                setErrorProvider(comboName, "Empty Data. Enter data or select from the list.");
                return result;
            }
            else
            {
                setErrorProvider(comboName, string.Empty, false);
                result = true;
            }

            if (errorText == null)
            {
                comboIndex = comboName.FindStringExact(comboName.Text);
            }
            else
            {
                comboIndex = comboName.FindStringExact(errorText);
            }

            if (comboIndex < 0)
            {
                setErrorProvider(comboName, "Empty Data. Enter data or select from the list.");
                return result;
            }
            else
            {
                setErrorProvider(comboName, string.Empty, false);
                result = true;
            }


            return result;

        }

        private void setErrorProvider(Control contrlName, string mesageToDisplay = null, bool showError = true)
        {
            if (showError)
            {
                if (mesageToDisplay == null)
                {
                    setErrorNotification.SetError(contrlName, "Please input data into the field.The field is required for data Processing.");
                }
                else
                {
                    setErrorNotification.SetError(contrlName, mesageToDisplay);
                }
                contrlName.Focus();
                ((ComboBox)contrlName).SelectAll();

            }
            else
            {
                setErrorNotification.SetError(contrlName, "");
            }
        }



        void initializeToolTipsControl()
        {
            controlToolTips.BackColor = System.Drawing.Color.Transparent;
            controlToolTips.IsBalloon = true;
            controlToolTips.ShowAlways = true;
            controlToolTips.ToolTipTitle = "iBams::Simplex Solutions";
            controlToolTips.ToolTipIcon = ToolTipIcon.Info;
        }


        public void setControlToolTips(Control controlName, string strCaption)
        {
            initializeToolTipsControl();
            controlToolTips.SetToolTip(controlName, strCaption);
        }

        public void setControlToolTips(Dictionary<Control, string> collectionOfControlName, string strCaption)
        {
            initializeToolTipsControl();
            foreach (KeyValuePair<Control, string> contrl in collectionOfControlName)
            {
                controlToolTips.SetToolTip(contrl.Key, contrl.Value);
            }

        }

        public bool ValidateRSAPin(string input)
        {
            bool result = false;
            string expectedString1 = "PEN1";
            string expectedString2 = "PEN2";

            var inputString = input.Trim().Substring(0, 4);
            if (inputString == expectedString1 || inputString == expectedString2)
            {
                result = true;
                //
            }

            return result;

        }

    }
}
