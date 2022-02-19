//using iBams.CurrentMain.MainProjectForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using UKLicense.Utilities;
//using iBams.Utilities;
//using iBams.Data.iBamsDataModel;
//using iBams.CurrentMain.Classes;
//using iBams.Data.Classes;
//using BamSettings = iBams.CurrentMain.Classes.BamSettings;


namespace UKLicense.Main
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        private LicenseDBEntities context = new LicenseDBEntities();

        private void MainForm_Load(object sender, EventArgs e)
        {
            //ChangePassword();
            this.usernametextBox.Focus();
            this.usernametextBox.Text = "admin";
             this.passwordtextBox.Text = "pass";
            //loginPanel.CenterControl();
            //var currInfo = System.Globalization.CultureInfo.CurrentCulture.Name;
            //DateTimeFormatInfo dInfo = new CultureInfo(currInfo, false).DateTimeFormat;
            //DateTimeFormatInfo britainInfo = new CultureInfo("en-GB", false).DateTimeFormat;
            //var date = DateTime.UtcNow.Date;
            //var convertedDate = Convert.ToDateTime("24/07/2015", britainInfo).ToString(dInfo.ShortDatePattern);
            //string dd = "24/07/2015";

            var currTime = DateTime.UtcNow.ToShortTimeString();


            //MessageBox.Show(string.Format("{0:HH:ss}", DateTime.Now.Date) + "Date: " + DateTime.Now.Date);

            //if (BamSettings.GetSetting(AppConstant.UseDeskTopDropBoxAppKey) == "true")
            //{
            //    backgroundWorker1.RunWorkerAsync();
            //}


        }

        void ChangePassword()
        {
            //var password = StaticHelpers.EncryptString("zainab");

            //var targetUser =
            //    context.UserLoginProfiles.Where(
            //        x => x.UserName == "mohammed" && x.Password == password);

            //if (targetUser.Any())
            //{
            //    var theUser = targetUser.First();
            //    theUser.UserName = "ibs";
            //    theUser.Password = StaticHelpers.EncryptString("vaug");
            //    context.SaveChanges();
            //}
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            var ctrl = new List<Control>();
            ctrl.AddRange(new[] { usernametextBox, passwordtextBox });
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var password = StaticHelpers.EncryptString(this.passwordtextBox.Text.Trim());

                var isValidUser =
                    context.Admins.Any(
                        x => x.UserName == this.usernametextBox.Text.Trim() && x.Password == password);

                if (isValidUser)
                {
                    var currentUser =
                        context.Admins.First(
                            x => x.UserName == this.usernametextBox.Text.Trim() && x.Password == password);
                    GlobalCredentials.UserName = currentUser.UserName;
                    GlobalCredentials.AdminId = currentUser.Admin_id;

                    this.usernametextBox.Focus();
                    var tab = new Tabs();
                    tab.ShowDialog();
                }
                else
                {
                    StaticHelpers.ShowCustomErrorMessage("Wrong username or password");

                }

            }
            catch (Exception ex)
            {
                StaticHelpers.ShowErrorMessage(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usernametextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == ((int)Keys.Enter).ToString())
            {
                this.loginButton.PerformClick();
            }
        }

    }
}