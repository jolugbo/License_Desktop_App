using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UKLicense.Utilities;
using UKLicense.Utility;

namespace UKLicense.Main
{
    public partial class Registration_update : Form
    {
        private string driveableDescription ="";
        private LicenseDBEntities context = new LicenseDBEntities();
        private VideoCaptureDevice _videoSource = null;
        private AppUtils utils = new AppUtils();
        ActionTypes _operationType;
        private Guid _driverId;

        public Guid driverId_ { get { return this._driverId; } set { this._driverId = value; } }

        public ActionTypes FormOprationType { get { return this._operationType; } set { this._operationType = value; } }

        public Registration_update()
        {
            InitializeComponent();
        }
      
        public Registration_update(ActionTypes operationType, Guid DriverId)
        {
            InitializeComponent();

            FormOprationType = operationType;
            driverId_ = DriverId;
            if (operationType == ActionTypes.EditEntry)
            {
               // FillDropDowns();
                LoadPreviousData();
                //snapbutton.Visible = false;
                snapbutton.Text = "Re Upload";
                button1.Text = "Update";
            }
        }

        private void LoadPreviousData()
        {
            Guid reqId = driverId_;
            var targetRecord = context.Driver_details.Find(reqId);

            if (null != targetRecord)
            {
                txtSurname.Text = targetRecord.driver_last_name;
                txtFirstname.Text = targetRecord.driver_first_name;
                txtMiddleName.Text = targetRecord.driver_middle_name;
                if (targetRecord.gender == "F")
                {
                    this.btnF.Checked = true;
                    if (targetRecord.Title == "Mrs")
                    {
                        this.btnMrs.Checked = true;
                    }
                    else
                    {
                        this.btnMiss.Checked = true;
                    }
                }
                else
                {
                    this.btnM.Checked = true;
                }
                dateDOB.Text = targetRecord.date_of_birth.ToString();
                txtNationalty.Text = targetRecord.nationalty;
                txtPhoneNo.Text = targetRecord.phone_number;
                dateReg.Text = targetRecord.date_registered.ToString();
                dateExpire.Text = targetRecord.expire_date.ToString();
                txtAddress.Text = targetRecord.permanent_contact_address;
                pictureBox1.Image = Image.FromFile(targetRecord.passport_path);
                picPathlabel.Text = targetRecord.passport_path;
                if (targetRecord.imagetype_identifier == false)
                {
                    this.btnWheel.Checked = true;
                }
                else
                {
                    this.btnFlag.Checked = true;
                }
               dateAM10.Text = targetRecord.AM_10.ToString();
                loadCheckbox(targetRecord.AM_10.ToString(), 0, 0);
                dateAM11.Text = targetRecord.AM_11.ToString();
                loadCheckbox(targetRecord.AM_11.ToString(), 0, 1);
                txtAM.Text = targetRecord.AM_12;
               dateA110.Text = targetRecord.A1_10.ToString();
                loadCheckbox(targetRecord.A1_10.ToString(), 1, 0);
                dateA111.Text = targetRecord.A1_11.ToString();
                loadCheckbox(targetRecord.A1_11.ToString(), 1, 1);
                txtA2.Text = targetRecord.A_12;                        
               dateA210.Text = targetRecord.A2_10.ToString();
                loadCheckbox(targetRecord.A2_10.ToString(), 2, 0);
                dateA211.Text = targetRecord.A2_11.ToString();
                loadCheckbox(targetRecord.A2_11.ToString(), 2, 1);
                txtA2.Text = targetRecord.A2_12;                       
               dateA10.Text = targetRecord.A_10.ToString();
                loadCheckbox(targetRecord.A_10.ToString(), 3, 0);
                dateA11 .Text = targetRecord.A_11.ToString();
                loadCheckbox(targetRecord.A_11.ToString(), 3, 1);
                txtA.Text = targetRecord.A_12;                          
               dateB110.Text = targetRecord.B1_10.ToString();
                loadCheckbox(targetRecord.B1_10.ToString(), 4, 0);
                dateB111.Text = targetRecord.B1_11.ToString();
                loadCheckbox(targetRecord.B1_11.ToString(), 4, 1);
                txtB1.Text = targetRecord.B1_12;                        
               dateB10 .Text = targetRecord.B_10.ToString();
                loadCheckbox(targetRecord.B_10.ToString(), 5, 0);
                dateB11 .Text = targetRecord.B_11.ToString();
                loadCheckbox(targetRecord.B_11.ToString(), 5, 1);
                txtB.Text = targetRecord.B_12;
               dateC110.Text = targetRecord.C1_10.ToString();
                loadCheckbox(targetRecord.C1_10.ToString(), 6, 0);
                dateC111.Text = targetRecord.C1_11.ToString();
                loadCheckbox(targetRecord.C1_11.ToString(), 6, 1);
                txtC1.Text = targetRecord.C1_12;
               dateC10 .Text = targetRecord.C_10.ToString();
                loadCheckbox(targetRecord.C_10.ToString(), 7, 0);
                dateC11 .Text = targetRecord.C_11.ToString();
                loadCheckbox(targetRecord.C_11.ToString(), 7, 1);
                txtC.Text = targetRecord.C_12;
               dateD110.Text = targetRecord.D1_10.ToString();
                loadCheckbox(targetRecord.D1_10.ToString(), 8, 0);
                dateD111.Text = targetRecord.D1_11.ToString();
                loadCheckbox(targetRecord.D1_11.ToString(), 8, 1);
                txtD1.Text = targetRecord.D1_12;
               dateD10 .Text = targetRecord.D_10.ToString();
                loadCheckbox(targetRecord.D_10.ToString(), 9, 0);
                dateD11 .Text = targetRecord.D_11.ToString();
                loadCheckbox(targetRecord.D_11.ToString(), 9, 1);
                txtD.Text = targetRecord.D_12;
               dateBE10.Text = targetRecord.BE_10.ToString();
                loadCheckbox(targetRecord.BE_10.ToString(), 10, 0);
                dateBE11.Text = targetRecord.BE_11.ToString();
                loadCheckbox(targetRecord.BE_11.ToString(), 10, 1);
                txtBE.Text = targetRecord.BE_12;
               dateC1E10.Text = targetRecord.C1E_10.ToString();
                loadCheckbox(targetRecord.C1E_10.ToString(), 11, 0);
                dateC1E11 .Text = targetRecord.C1E_11.ToString();
                loadCheckbox(targetRecord.C1E_11.ToString(), 11, 1);
                txtC1E.Text = targetRecord.C1E_12;
               dateCE10  .Text = targetRecord.CE_10.ToString();
                loadCheckbox(targetRecord.CE_10.ToString(), 12, 0);
                dateCE11  .Text = targetRecord.CE_11.ToString();
                loadCheckbox(targetRecord.CE_11.ToString(), 12, 1);
                txtCE.Text = targetRecord.CE_12;
               dateD1E10 .Text = targetRecord.D1E_10.ToString();
                loadCheckbox(targetRecord.D1E_10.ToString(), 13, 0);
                dateD1E11 .Text = targetRecord.D1E_11.ToString();
                loadCheckbox(targetRecord.D1E_11.ToString(), 13, 1);
                txtD1E.Text = targetRecord.D1E_12;
               dateDE10  .Text = targetRecord.DE_10.ToString();
                loadCheckbox(targetRecord.DE_10.ToString(), 14, 0);
                dateDE11  .Text = targetRecord.DE_11.ToString();
                loadCheckbox(targetRecord.DE_11.ToString(), 14, 1);
                txtDE.Text = targetRecord.DE_12;
               dateFKQ10 .Text = targetRecord.FKQ_10.ToString();
                loadCheckbox(targetRecord.FKQ_10.ToString(), 15, 0);
                dateFKQ11 .Text = targetRecord.FKQ_11.ToString();
                loadCheckbox(targetRecord.FKQ_11.ToString(), 15, 1);
                txtfkq.Text = targetRecord.FKQ_12;

            }
        }
        


        #region WebCam Methods
        private void StartFrontCam()
        {
            if (String.IsNullOrWhiteSpace(StaticSettings.BackCameraName))
            {
                StaticHelpers.ShowExclamingMessage("No camera was found on this device Please contact your administrator");
                // ConfirmBox.Show("No camera was found on this device Please contact your administrator", "", ConfirmBox.Buttons.OK, ConfirmBox.Icon.Info, ConfirmBox.AnimateStyle.FadeIn);
                return;

            }
            _videoSource = new VideoCaptureDevice(StaticSettings.BackCameraName);
            _videoSource.NewFrame += new NewFrameEventHandler(video_NewFramePassport);
            CloseVideoSource();
            _videoSource.Start();
        }

        //eventhandler if new frame is ready
        private void video_NewFramePassport(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            //do processing here
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = img;
        }
        //close the device safely
        private void CloseVideoSource()
        {
            if ((_videoSource != null))
                if (_videoSource.IsRunning)
                {
                    _videoSource.SignalToStop();
                    _videoSource = null;
                }
        }
        private void InitializeCameras()
        {
            //getFrontCam();
            StartFrontCam();
        }

        #endregion
        private void loadCheckbox(string value, int positionX, int positionY)
        {
            CheckBox CheckBoxList = getRelevantCheckBox(positionX);
            DateTimePicker _DateTimePicker = new DateTimePicker();
            _DateTimePicker = getRelevantDateTimePicker(positionX, positionY);
            if (value == "")
            {
                CheckBoxList.Checked = false;
                _DateTimePicker.Value = new DateTime(1900, 01, 01);
            }
            else
            {
                CheckBoxList.Checked = true;
            }
        }
        private void snapbutton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                dialog.InitialDirectory = @"C:\";
                dialog.Title = "Please select an image file to encrypt.";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(dialog.FileName);
                    string path = StaticSettings.PassPortFolder;
                    string fileName = Guid.NewGuid().ToString();
                    string picPath = StaticHelpers.SaveImage(pictureBox1.Image, fileName, path);
                    this.picPathlabel.Text = picPath;
                }
                dialog.Dispose();
            }
            //    if (pictureBox1.Image == null)
            //    {
            //        return;
            //    }

            //    if (this.snapbutton.Text == "Snap")
            //    {
            //        string path = StaticSettings.PassPortFolder;
            //        string fileName = Guid.NewGuid().ToString();
            //        string picPath = StaticHelpers.SaveImage(pictureBox1.Image, fileName, path);
            //        this.picPathlabel.Text = picPath;
            //        CloseVideoSource();
            //        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //        this.pictureBox1.Image = Image.FromFile(picPath);
            //        snapbutton.Text = "Re Snap";
            //    }
            //    else
            //    {
            //        InitializeCameras();
            //        snapbutton.Text = "Snap";
            //    }
            //}
            catch (Exception ex)
            {
                StaticHelpers.ShowErrorMessage(ex);
                //ConfirmBox.Show(ex.Message, "", ConfirmBox.Buttons.OK, ConfirmBox.Icon.Error, ConfirmBox.AnimateStyle.FadeIn);
            }
        }

        private void Registration_update_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(generateRand());
            //dateDOB.Format = DateTimePickerFormat.Custom;
            //dateDOB.CustomFormat = "dd.mm.yyyy";
           // getDate(1,1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CloseVideoSource();
            try
            {
                this.Hide();

                Registration_Browse frm = new Registration_Browse();
                frm.Closed += (s, arg) => this.Close();
                FormHelper.ShowChildForm(this.MdiParent, new Registration_Browse());
            }
            catch (Exception ex)
            {
                StaticHelpers.ShowErrorMessage(ex, true);
            }
        }

        private DateTimePicker getRelevantDateTimePicker(int positionX, int positionY)
        {
            DateTimePicker[,] myDateTimePickerList = new DateTimePicker[16, 2];
            myDateTimePickerList[0, 0] = dateAM10; myDateTimePickerList[0, 1] = dateAM11; myDateTimePickerList[1, 0] = dateA110; myDateTimePickerList[1, 1] = dateA111;
            myDateTimePickerList[2, 0] = dateA210; myDateTimePickerList[2, 1] = dateA211; myDateTimePickerList[3, 0] = dateA10; myDateTimePickerList[3, 1] = dateA10;
            myDateTimePickerList[4, 0] = dateB110; myDateTimePickerList[4, 1] = dateB111; myDateTimePickerList[5, 0] = dateB10; myDateTimePickerList[5, 1] = dateB11;
            myDateTimePickerList[6, 0] = dateC110; myDateTimePickerList[6, 1] = dateC111; myDateTimePickerList[7, 0] = dateC10; myDateTimePickerList[7, 1] = dateC11;
            myDateTimePickerList[8, 0] = dateD110; myDateTimePickerList[8, 1] = dateD111; myDateTimePickerList[9, 0] = dateD10; myDateTimePickerList[9, 1] = dateD11;
            myDateTimePickerList[10, 0] = dateBE10; myDateTimePickerList[10, 1] = dateBE11; myDateTimePickerList[11, 0] = dateC1E10; myDateTimePickerList[11, 1] = dateC1E11;
            myDateTimePickerList[12, 0] = dateCE10; myDateTimePickerList[12, 1] = dateCE11; myDateTimePickerList[13, 0] = dateD1E10; myDateTimePickerList[13, 1] = dateD1E11;
            myDateTimePickerList[14, 0] = dateDE10; myDateTimePickerList[14, 1] = dateDE11; myDateTimePickerList[15, 0] = dateFKQ10; myDateTimePickerList[15, 1] = dateFKQ11;

            Console.WriteLine(myDateTimePickerList);
            return myDateTimePickerList[positionX, positionY];
        }

        private TextBox getRelevantItem12TextBox(int positionY)
        {
            TextBox[] item12TextBox = new TextBox[16];
            item12TextBox[0] = txtAM; item12TextBox[1] = txtA1;
            item12TextBox[2] = txtA2; item12TextBox[3] = txtA; 
            item12TextBox[4] = txtB1; item12TextBox[5] = txtB; 
            item12TextBox[6] = txtC1; item12TextBox[7] = txtC; 
            item12TextBox[8] = txtD1; item12TextBox[9] = txtD; 
            item12TextBox[10] = txtBE; item12TextBox[11] = txtC1E; 
            item12TextBox[12] = txtCE; item12TextBox[13] = txtD1E; 
            item12TextBox[14] = txtDE; item12TextBox[15] = txtfkq; 

            Console.WriteLine(item12TextBox);
            return item12TextBox[positionY];
        }

        private CheckBox getRelevantCheckBox(int positionY) {
            CheckBox[] CheckBoxList = new CheckBox[16];
            CheckBoxList[0] = cbAM; CheckBoxList[1] = cbA1; CheckBoxList[2] = cbA2; CheckBoxList[3] = cbA; CheckBoxList[4] = cbB1;
            CheckBoxList[5] = cbB; CheckBoxList[6] = cbC1; CheckBoxList[7] = cbC; CheckBoxList[8] = cbD1; CheckBoxList[9] = cbD;
            CheckBoxList[10] = cbBE; CheckBoxList[11] = cbC1E; CheckBoxList[12] = cbCE; CheckBoxList[13] = cbD1E; CheckBoxList[14] = cbDE; CheckBoxList[15] = cbfkq;

            return CheckBoxList[positionY];
        }

        private string getRelevantItem12( int positionY)
        {
            CheckBox CheckBoxList = getRelevantCheckBox(positionY);
            TextBox TextBoxList = getRelevantItem12TextBox(positionY);
            string item12 = null;
            DateTimePicker _DateTimePicker = new DateTimePicker();
            

            if (CheckBoxList.Checked)
            {
                item12 = TextBoxList.Text;
                if (CheckBoxList.Name == "cbfkq")
                {
                    driveableDescription += "f/k/q";
                }
                else
                {
                    driveableDescription += CheckBoxList.Name.Substring(2) + "/";
                }
            }
            else
            {
                item12 = null;
            }
            return item12;
        }

        private Boolean getLogoType()
        {
            Boolean logotype = false;
            if (this.btnWheel.Checked)
            {
                logotype = false;
            }
            else if (this.btnFlag.Checked)
            {
                logotype = true;
            }

            return logotype;
        }

        private DateTime? getRelevantDate(int positionX,int positionY,int positionZ) {
            Nullable<DateTime> valueDates = null;
           // DateTime valueDates = new DateTime();
            DateTimePicker _DateTimePicker = new DateTimePicker();
            _DateTimePicker = getRelevantDateTimePicker(positionX, positionY);

            CheckBox CheckBoxList = getRelevantCheckBox(positionZ);

            if (CheckBoxList.Checked)
            {
                valueDates = _DateTimePicker.Value.Date;
                //valueDates[0, 1] = dateAM11.Value.Date;
            }
            else
            {
                valueDates = null;
            }
            return valueDates;
        }

        private string GetGender()
        {
            string genderText = string.Empty;
          
                if (this.btnM.Checked)
                {
                    genderText = "M";
                }
                else if (this.btnF.Checked)
                {
                    genderText = "F";
                }
            
            return genderText;
        }

        private string GetPhysical_id()
        {
            string physical_id = string.Empty;

            #region names
            var surCount = txtSurname.Text.Length;
            string surAbriv = txtSurname.Text;
            string fNameConcat = txtFirstname.Text.Substring(0, 1) + "9";
            if (txtMiddleName.Text.Length > 0)
            {
                fNameConcat = txtFirstname.Text.Substring(0, 1) + txtMiddleName.Text.Substring(0, 1);
            }
            if (surAbriv.Length < 5)
            {
                while (surCount < 5)
                {
                    surAbriv = surAbriv + "9";
                    surCount++;
                }
            }
            if (surAbriv.Length >= 5)
            {
                    surAbriv = surAbriv.Substring(0, 5);
            }
            #endregion

            #region dates
            DateTime dob = dateDOB.Value;
            string dobYearAbriv = dob.Year.ToString().Substring(2,2);
            string dobMonthAbriv = dob.Month.ToString();
            if (dob.Month < 10)
            {
                dobMonthAbriv = "0" + dob.Month.ToString();
            }
            else
            {
                dobMonthAbriv = dob.Month.ToString();
            }
            dob.Month.ToString();
            if (btnF.Checked)
            {
                if (dob.Month < 10)
                {
                    dobMonthAbriv = "5" + dobMonthAbriv.Substring(1, 1);
                }
                else
                {
                    dobMonthAbriv = "6" + dobMonthAbriv.Substring(1, 1);
                }
            }
            string dobDayAbriv = dob.Day.ToString();
            #endregion

            #region random
            string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rand = new Random();
            string randStringAbriv = "9" + st[rand.Next(st.Length)] + st[rand.Next(st.Length)];
            string randIntAbriv = rand.Next(10, 99).ToString();
            #endregion

            physical_id = surAbriv + dobYearAbriv.Substring(0, 1) + dobMonthAbriv + dobDayAbriv + dobYearAbriv.Substring(1, 1) + fNameConcat + randStringAbriv + "  " + randIntAbriv;
            return physical_id;
        }

        private string generateRand()
        {
            string randomVal;
            Random rand = new Random();
            string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string num = "1234567890";
            string numResult = "";
            for (int i = 0; i < 8; i++)
            {
                numResult += num[rand.Next(num.Length)];
            }
            randomVal = ""+st[rand.Next(25)] + st[rand.Next(25)]  + numResult.ToString();
            return randomVal;
        }

        private bool SaveNewRegistration()
        {
            bool result = false;
            var address = this.txtAddress.Text.Replace("\n", "\r\n");
            Guid id_Driver = Guid.NewGuid();
            string title = "";
            if (GetGender() == "F")
            {
                if (this.btnMiss.Checked)
                {
                    title = "Miss";
                }
                else if (this.btnMrs.Checked)
                {
                    title = "Mrs";
                }
            }
            try
            {
                var Driver = new Driver_details()
                {
                    id_driver = id_Driver,
                    physical_id = GetPhysical_id().ToUpper(),
                    driver_first_name = this.txtFirstname.Text.Trim().ToUpper(),
                    driver_last_name = this.txtSurname.Text.ToString().ToUpper(),
                    driver_middle_name = this.txtMiddleName.Text.Trim().ToUpper(),
                    gender = GetGender(),
                    Title = title,
                    date_of_birth = this.dateDOB.Value.Date,
                    nationalty = this.txtNationalty.Text.ToUpper(),
                    phone_number = this.txtPhoneNo.Text,
                    permanent_contact_address = address.ToUpper(),
                    date_registered = dateReg.Value.Date,
                    expire_date = dateExpire.Value.Date,
                    passport_path = picPathlabel.Text.Trim(),
                    DataCapturedBy = GlobalCredentials.AdminId,
                    imagetype_identifier = getLogoType(),
                    back_code = generateRand(),
                    AM_10 = getRelevantDate(0,0,0),
                    AM_11 = getRelevantDate(0, 1, 0),
                    AM_12 = getRelevantItem12(0),
                    A1_10 = getRelevantDate(1, 0, 1),
                    A1_11 = getRelevantDate(1, 1, 1),
                    A1_12 = getRelevantItem12(1),
                    A2_10 = getRelevantDate(2, 0, 2),
                    A2_11 = getRelevantDate(2, 1, 2),
                    A2_12 = getRelevantItem12(2),
                    A_10 = getRelevantDate(3, 0, 3),
                    A_11 = getRelevantDate(3, 1, 3),
                    A_12 = getRelevantItem12(3),
                    B1_10 = getRelevantDate(4, 0, 4),
                    B1_11 = getRelevantDate(4, 1, 4),
                    B1_12 = getRelevantItem12(4),
                    B_10 = getRelevantDate(5, 0, 5),
                    B_11 = getRelevantDate(5, 1, 5),
                    B_12 = getRelevantItem12(5),
                    C1_10 = getRelevantDate(6, 0, 6),
                    C1_11 = getRelevantDate(6, 1, 6),
                    C1_12 = getRelevantItem12(7),
                    C_10 = getRelevantDate(7, 0, 7),
                    C_11 = getRelevantDate(7, 1, 7),
                    C_12 = getRelevantItem12(7),
                    D1_10 = getRelevantDate(8, 0, 8),
                    D1_11 = getRelevantDate(8, 1, 8),
                    D1_12 = getRelevantItem12(8),
                    D_10 = getRelevantDate(9, 0, 9),
                    D_11 = getRelevantDate(9, 1, 9),
                    D_12 = getRelevantItem12(9),
                    BE_10 = getRelevantDate(10, 0, 10),
                    BE_11 = getRelevantDate(10, 1, 10),
                    BE_12 = getRelevantItem12(10),
                    C1E_10 = getRelevantDate(11, 0, 11),
                    C1E_11 = getRelevantDate(11, 1, 11),
                    C1E_12 = getRelevantItem12(11),
                    CE_10 = getRelevantDate(12, 0, 12),
                    CE_11 = getRelevantDate(12, 1, 12),
                    CE_12 = getRelevantItem12(12),
                    D1E_10 = getRelevantDate(13, 0, 13),
                    D1E_11 = getRelevantDate(13, 1, 13),
                    D1E_12 = getRelevantItem12(13),
                    DE_10 = getRelevantDate(14, 0, 14),
                    DE_11 = getRelevantDate(14, 1, 14),
                    DE_12 = getRelevantItem12(14),
                    FKQ_10 = getRelevantDate(15, 0, 15),
                    FKQ_11 = getRelevantDate(15, 1, 15),
                    FKQ_12 = getRelevantItem12(15),
                    driveable_description = driveableDescription,
                };
                context.Driver_details.Add(Driver);
                context.SaveChanges();
                return result = true;
            }
            catch (Exception ex)
            {

                return result = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var ctrl = new List<Control>();
            ctrl.AddRange(new[] { this.txtAddress, txtFirstname,txtSurname,txtNationalty});
            ctrl.AddRange(new[] { dateDOB, dateReg });
            if (_operationType == ActionTypes.NewEntry)
            {
                if (!utils.NoEmptyControl(this, ctrl))
                {
                    StaticHelpers.ShowExclamingMessage("Please fill all require fields");
                    //ConfirmBox.Show(, "", ConfirmBox.Buttons.OK, ConfirmBox.Icon.Error, ConfirmBox.AnimateStyle.FadeIn);

                    //MessageBox.Show(ctrl.First().Name);

                    return;
                }
            }
            
            if (_operationType == ActionTypes.NewEntry)
            {
                if (string.IsNullOrWhiteSpace(picPathlabel.Text.Trim()))
                {
                    StaticHelpers.ShowExclamingMessage("Please snap the applicant's picture");
                    //ConfirmBox.Show("Please snap the applicant's picture", "", ConfirmBox.Buttons.OK, ConfirmBox.Icon.Error, ConfirmBox.AnimateStyle.FadeIn);
                    return;
                }
            }
            try
            {
                if (_operationType == ActionTypes.NewEntry)
                {
                    if (this.SaveNewRegistration())
                    {
                        StaticHelpers.ShowInfoMessage("Record Captured successfully");
                        FormHelper.ShowChildForm(this.MdiParent, new Registration_Browse());
                        this.Close();
                    }
                }
                else
                {
                    if (this.UpdateRegistration())
                    {
                        StaticHelpers.ShowInfoMessage("Record was updated successfully");
                        //ConfirmBox.Show("Record was updated successfully", "", ConfirmBox.Buttons.OK,
                        //    ConfirmBox.Icon.Info, ConfirmBox.AnimateStyle.FadeIn);
                        FormHelper.ShowChildForm(this.MdiParent, new Registration_Browse());
                        this.Close();
                    }
                }

            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                var fullMessageError = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, "Exact Message " + fullMessageError);

                StaticHelpers.ShowCustomErrorMessage(exceptionMessage);
                //ConfirmBox.Show(exceptionMessage, "", ConfirmBox.Buttons.OK, ConfirmBox.Icon.Error,
                //    ConfirmBox.AnimateStyle.FadeIn);

            }

            catch (Exception ex)
            {
                StaticHelpers.ShowCustomErrorMessage(StaticHelpers.FormattedException(ex));
                //ConfirmBox.Show(StaticHelpers.FormattedException(ex), "", ConfirmBox.Buttons.OK, ConfirmBox.Icon.Error, ConfirmBox.AnimateStyle.FadeIn);

            }
        }

        private bool UpdateRegistration()
        {
            bool result = false;
            var driverRow = context.Driver_details.Find(_driverId);
            driverRow.date_of_birth = this.dateDOB.Value;
            driverRow.driver_first_name = this.txtFirstname.Text.Trim();
            driverRow.driver_last_name = this.txtSurname.Text.ToString();
            driverRow.driver_middle_name = this.txtMiddleName.Text.Trim();
            driverRow.gender = GetGender();
            if (GetGender() == "F")
            {
                if (this.btnMiss.Checked)
                {
                    driverRow.Title = "Miss";
                }
                else if (this.btnMrs.Checked)
                {
                    driverRow.Title = "Mrs";
                }
            }
            driverRow.nationalty = this.txtNationalty.Text.Trim();
            driverRow.phone_number = this.txtPhoneNo.Text.Trim();
            driverRow.date_registered = this.dateReg.Value;
            driverRow.expire_date  = this.dateExpire.Value;
            driverRow.permanent_contact_address = this.txtAddress.Text.Trim();
            driverRow.passport_path = picPathlabel.Text.Trim();
            driverRow.imagetype_identifier = getLogoType();
            driverRow.AM_10 = getRelevantDate(0, 0, 0);
            driverRow.AM_11 = getRelevantDate(0, 1, 0);
            driverRow.AM_12 = getRelevantItem12(0);
            driverRow.A1_10 = getRelevantDate(1, 0, 1);
            driverRow.A1_11 = getRelevantDate(1, 1, 1);
            driverRow.A1_12 = getRelevantItem12(1);
            driverRow.A2_10 = getRelevantDate(2, 0, 2);
            driverRow.A2_11 = getRelevantDate(2, 1, 2);
            driverRow.A2_12 = getRelevantItem12(2);
            driverRow.A_10 = getRelevantDate(3, 0, 3);
            driverRow.A_11 = getRelevantDate(3, 1, 3);
            driverRow.A_12 = getRelevantItem12(3);
            driverRow.B1_10 = getRelevantDate(4, 0, 4);
            driverRow.B1_11 = getRelevantDate(4, 1, 4);
            driverRow.B1_12 = getRelevantItem12(4);
            driverRow.B_10 = getRelevantDate(5, 0, 5);
            driverRow.B_11 = getRelevantDate(5, 1, 5);
            driverRow.B_12 = getRelevantItem12(5);
            driverRow.C1_10 = getRelevantDate(6, 0, 6);
            driverRow.C1_11 = getRelevantDate(6, 1, 6);
            driverRow.C1_12 = getRelevantItem12(7);
            driverRow.C_10 = getRelevantDate(7, 0, 7);
            driverRow.C_11 = getRelevantDate(7, 1, 7);
            driverRow.C_12 = getRelevantItem12(7);
            driverRow.D1_10 = getRelevantDate(8, 0, 8);
            driverRow.D1_11 = getRelevantDate(8, 1, 8);
            driverRow.D1_12 = getRelevantItem12(8);
            driverRow.D_10 = getRelevantDate(9, 0, 9);
            driverRow.D_11 = getRelevantDate(9, 1, 9);
            driverRow.D_12 = getRelevantItem12(9);
            driverRow.BE_10 = getRelevantDate(10, 0, 10);
            driverRow.BE_11 = getRelevantDate(10, 1, 10);
            driverRow.BE_12 = getRelevantItem12(10);
            driverRow.C1E_10 = getRelevantDate(11, 0, 11);
            driverRow.C1E_11 = getRelevantDate(11, 1, 11);
            driverRow.C1E_12 = getRelevantItem12(11);
            driverRow.CE_10 = getRelevantDate(12, 0, 12);
            driverRow.CE_11 = getRelevantDate(12, 1, 12);
            driverRow.CE_12 = getRelevantItem12(12);
            driverRow.D1E_10 = getRelevantDate(13, 0, 13);
            driverRow.D1E_11 = getRelevantDate(13, 1, 13);
             driverRow.D1E_12 = getRelevantItem12(13);
             driverRow.DE_10 = getRelevantDate(14, 0, 14);
             driverRow.DE_11 = getRelevantDate(14, 1, 14);
             driverRow.DE_12 = getRelevantItem12(14);
             driverRow.FKQ_10 = getRelevantDate(15, 0, 15);
             driverRow.FKQ_11 = getRelevantDate(15, 1, 15);
             driverRow.FKQ_12 = getRelevantItem12(15);
            driverRow.driveable_description = driveableDescription;

            if (context.SaveChanges() > 0)
            {
                result = true;
            }

            return result;
        }

        private void rdbFemale_CheckedChanged(object sender, EventArgs e)
        {
            gbTitle.Enabled = true;
        }

        private void rdbMale_CheckedChanged(object sender, EventArgs e)
        {
            gbTitle.Enabled = false;
        }
    }
}
