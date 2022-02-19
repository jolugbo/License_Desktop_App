using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UKLicense.Utilities;

namespace UKLicense.Main
{
    public partial class Tabs : Form
    {
        private VideoCaptureDevice _videoSource = null;
        private FilterInfoCollection videoDevices;
        public Tabs()
        {
            InitializeComponent();
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            this.Size = new Size(screenWidth, screenHeight);
            //this.Size = new System.Drawing.Size(screenWidth - 100, screenHeight - 50);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Registratio_form_Click(object sender, EventArgs e)
        {
            try
            {
                //this.logopanel.Visible = false;
                FormHelper.ShowChildForm(this, new Registration_Browse());
            }
            catch (Exception ex)
            {
                StaticHelpers.ShowErrorMessage(ex, true);
            }
        }

        private void Tabs_Load(object sender, EventArgs e)
        {
            // this.logoPicture.CenterControl();
            //FormHelper.CenterNamedControl(this.logopanel,this.logoPicture);
            //if (GlobalCredentials.UserName.ToUpper() == "IBS" || GlobalCredentials.UserName.ToUpper() == "RCS")
            //{
            //    //this.globalSettingslink.Visible = true;
            //    mgtbutton.Visible = true;
            //}
            //else
            //{
            //    mgtbutton.Visible = false;
            //    //this.globalSettingslink.Visible = false;
            //}

           // this.backgroundWorker1.RunWorkerAsync();


            MdiClient ctlMDI;


            //' Loop through all of the form's controls looking
            //' for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;
                    //ctl.BackColor = Color.Transparent;
                }
                catch (InvalidCastException)
                {
                    // Catch and ignore the error if casting failed.
                }
            }

            //Get Cameras
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                //if (String.IsNullOrWhiteSpace(StaticSettings.BackCameraName))
                //{
                //    StaticSettings.BackCameraName = videoDevices[0].MonikerString;
                //    return;
                //}

                if (videoDevices.Count == 0)
                {
                    StaticHelpers.ShowCustomErrorMessage("No camera was found on this device");
                    //ConfirmBox.Show(, "", ConfirmBox.Buttons.OK, ConfirmBox.Icon.Info, ConfirmBox.AnimateStyle.FadeIn);
                    return;
                }

                //if (videoDevices.Count == 1)
                //{
                //    ConfirmBox.Show("This device has only one camera attached to it", "", ConfirmBox.Buttons.OK,
                //        ConfirmBox.Icon.Info, ConfirmBox.AnimateStyle.FadeIn);
                //    return;
                //}
                //else

                if (String.IsNullOrWhiteSpace(StaticSettings.BackCameraName))
                {
                    StaticSettings.BackCameraName = videoDevices[0].MonikerString;
                }


                //if (String.IsNullOrWhiteSpace(StaticSettings.FrontCameraName))
                //{
                //    StaticSettings.FrontCameraName = videoDevices[1].MonikerString;
                //}
            }
            catch (Exception ex)
            {
                StaticHelpers.ShowCustomErrorMessage("There was an error initalizing camera on the device");
                // ConfirmBox.Show("There was an error initalizing camera on the device" + ex.Message, "", ConfirmBox.Buttons.OK, ConfirmBox.Icon.Info, ConfirmBox.AnimateStyle.FadeIn);
            }



        }
    }
}
