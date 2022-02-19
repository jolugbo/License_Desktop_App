using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;    // Add reference to System.Design
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Printing;
using UKLicense.Utilities;

namespace UKLicense.Main
{
    public partial class VehicleLicensePrintOut : Form
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbfont, uint cbfont, IntPtr pdv, [In] ref uint pcfonts); 
        private Guid DriveId;
        LicenseDBEntities context = new LicenseDBEntities();
        public VehicleLicensePrintOut(Guid _DriveId)
        {
            InitializeComponent();
            DriveId = _DriveId;
            backPNL.Visible = false;
        }
        FontFamily ff;
        Font font;
        public void setIcon(bool? imagetype_identifier) {
            if (imagetype_identifier ==true)
            {
                iconPic.Size = new Size(120,80);
                iconPic.Location = new Point(690, 180);
                iconPic.Image = global::UKLicense.Main.Properties.Resources.icon_flag1;
            }
            else
            {
                iconPic.Image = global::UKLicense.Main.Properties.Resources.wheel;
            }
        }

        public void loadRecord()
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, backFadedOvalPassport.Width -3, backFadedOvalPassport.Height ); //10, 10, 100, 20
            Region rg = new Region(gp);
            backFadedOvalPassport.Region = rg;

            var targetRec = context.Driver_details.Find(DriveId);
            this.lblSurname.Text = targetRec.driver_last_name.ToUpper();
            if (targetRec.gender == "F")
            {
                this.lblFirstName.Text = targetRec.Title.ToUpper() + " " + targetRec.driver_first_name.ToUpper() + " " + targetRec.driver_middle_name.ToUpper();
            }
            else
            {
                this.lblFirstName.Text =  targetRec.driver_first_name.ToUpper() + " " + targetRec.driver_middle_name.ToUpper();
            }
            setIcon(targetRec.imagetype_identifier);
            backFadedOvalPassport.Image = ChangeImageOpacity(SetGrayscale(targetRec.passport_path),0.3);
           backFadedOvalPassport.BackColor = Color.Transparent;
            DriverPic.Image = ChangeImageOpacity(sepiaImg(SetGrayscale(targetRec.passport_path)),0.9);//ChangeImageOpacity(SetGrayscale(targetRec.passport_path), 0.4);
           // ovalShape.Image = ChangeImageOpacity(global::UKLicense.Main.Properties.Resources.Oval_shape, 1);
            this.pictureBox3.Image = ChangeImageOpacity(global::UKLicense.Main.Properties.Resources.Spiral,0.5);
            this.lblDOB.Text = targetRec.date_of_birth.Value.Day.ToString("d2") + "."+ targetRec.date_of_birth.Value.Month.ToString("d2") + "."+ targetRec.date_of_birth.Value.Year +  "  " + targetRec.nationalty.ToUpper();
            this.lblRegDate.Text =  targetRec.date_registered.Value.Day.ToString("d2") + "." + targetRec.date_registered.Value.Month.ToString("d2") + "." + targetRec.date_registered.Value.Year;
            this.lblDVLA.Text = "DVLA";
            this.lblExpireDate.Text = targetRec.expire_date.Value.Day.ToString("d2") + "." + targetRec.expire_date.Value.Month.ToString("d2") + "." + targetRec.expire_date.Value.Year;
            this.LBLPhysicalId.Text = targetRec.physical_id.ToUpper();//this.DriverPic.Image = AdjustContrast(SetGrayscale(targetRec.passport_path),0);
            lblBakPixDate.Text = String.Format("{0:MMM}", targetRec.expire_date.Value).ToUpper()+ String.Format("{0:yy}", targetRec.expire_date.Value);
            lblBackCode.Text = targetRec.back_code;//this.DriverPic.ImageLocation = targetRec.passport_path;
            lblPixDate.Text = targetRec.expire_date.Value.Day.ToString("d2") + "." + targetRec.expire_date.Value.Month.ToString("d2") + "." + targetRec.expire_date.Value.Year;
            lblAddress.Text = targetRec.permanent_contact_address.ToUpper();lblDrivable.Text = targetRec.driveable_description;
            setBackLabelValue(lblAM10, targetRec.AM_10); setBackLabelValue(lblAM11, targetRec.AM_11);    setBackLabelValueItem12(lblAM12, targetRec.AM_12);
            setBackLabelValue(lblA110, targetRec.A1_10); setBackLabelValue(lblA111, targetRec.A1_11);    setBackLabelValueItem12(lblA112, targetRec.A1_12);
            setBackLabelValue(lblA210, targetRec.A2_10); setBackLabelValue(lblA211, targetRec.A2_11);    setBackLabelValueItem12(lblA212, targetRec.A2_12);
            setBackLabelValue(lblA10, targetRec.A_10);   setBackLabelValue(lblA11, targetRec.A_11);      setBackLabelValueItem12(lblA12,  targetRec.A_12);
            setBackLabelValue(lblB110, targetRec.B1_10); setBackLabelValue(lblB111, targetRec.B1_11);    setBackLabelValueItem12(lblB112, targetRec.B1_12);
            setBackLabelValue(lblB10, targetRec.B_10);   setBackLabelValue(lblB11, targetRec.B_11);      setBackLabelValueItem12(lblB12,  targetRec.B_12);
            setBackLabelValue(lblC110, targetRec.C1_10); setBackLabelValue(lblC111, targetRec.C1_11);    setBackLabelValueItem12(lblC112, targetRec.C1_12);
            setBackLabelValue(lblC10, targetRec.C_10);   setBackLabelValue(lblcC11, targetRec.C_11);     setBackLabelValueItem12(lblC12,  targetRec.C_12);
            setBackLabelValue(lblD110, targetRec.D1_10); setBackLabelValue(lblD111, targetRec.D1_11);    setBackLabelValueItem12(lblD112, targetRec.D1_12);
            setBackLabelValue(lblD10, targetRec.D_10);   setBackLabelValue(lblD11, targetRec.D_11);      setBackLabelValueItem12(lblD12,  targetRec.D_12);
            setBackLabelValue(lblBE10, targetRec.BE_10); setBackLabelValue(lblBE11, targetRec.BE_11);    setBackLabelValueItem12(lblBE12, targetRec.BE_12);
            setBackLabelValue(lblC1E10, targetRec.C1E_10);setBackLabelValue(lblC1E11, targetRec.C1E_11); setBackLabelValueItem12(lblC1E12,targetRec.C1E_12);
            setBackLabelValue(lblCE10, targetRec.CE_10); setBackLabelValue(lblCE11, targetRec.CE_11);    setBackLabelValueItem12(lblCE12, targetRec.CE_12);
            setBackLabelValue(lblD1E10, targetRec.D1E_10);setBackLabelValue(lblD1E11, targetRec.D1E_11); setBackLabelValueItem12(lblD1E12,targetRec.D1E_12);
            setBackLabelValue(lblDE10, targetRec.DE_10); setBackLabelValue(lblDE11, targetRec.DE_11);    setBackLabelValueItem12(lblDE12, targetRec.DE_12);
            setBackLabelValue(lblfkq10, targetRec.FKQ_10);setBackLabelValue(lblfkq11, targetRec.FKQ_11); setBackLabelValueItem12(lblfkq12,targetRec.FKQ_12);
        }

        // METHOD TO DETERMINE IF BACK LABEL HAS A DATE OR SHOULD TAKE A LINE IMAGE
        private void setBackLabelValue(Label lbl,DateTime? date)
        {
            if (date == null)
            {
                Label[] array = { lblAM10,lblA110, lblA210,lblA10, lblB110,
                                  lblB10, lblC110, lblC10, lblD110, lblD10,
                                  lblBE10,lblC1E10,lblCE10,lblD1E10,lblDE10,lblfkq10};
                if (array.Contains(lbl))
                {
                    Image im = global::UKLicense.Main.Properties.Resources.line5;//Image.FromFile("image.png"); // read in image
                    lbl.Size = new Size(im.Width, im.Height / 4); //set label to correct size
                    lbl.Image = im; // put image on label
                }
                else {
                    Image i = global::UKLicense.Main.Properties.Resources.line4;//Image.FromFile("image.png"); // read in image
                    lbl.Size = new Size(i.Width, i.Height / 4); //set label to correct size
                    lbl.Image = i; // put image on label
                }
            }
            else
            {
                lbl.Text = date.Value.Day.ToString("d2") + "." + date.Value.Month.ToString("d2") + "." + String.Format("{0:yy}", date.Value);
            }
        }

        private void setBackLabelValueItem12(Label lbl, string value)
        {
            if (value == null)
            {

            }
            else
            {
                lbl.Text = value;
            }
        }
        private void loadFont() {
            byte[] fontArray = UKLicense.Main.Properties.Resources.CA_ALMOST;
            int dataLenght = UKLicense.Main.Properties.Resources.CA_ALMOST.Length;
            IntPtr ptrData= Marshal.AllocCoTaskMem(dataLenght);
            Marshal.Copy(fontArray, 0, ptrData, dataLenght);
            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddMemoryFont(ptrData, dataLenght);
            Marshal.FreeCoTaskMem(ptrData);
            ff = pfc.Families[0];
            font = new Font(ff, 4f, FontStyle.Regular);
        }

        private void setBg() {
            this.lblSurname.Parent = frontBG;
            this.lblFirstName.Parent = frontBG;
            backBG.Parent = backPNL;
            frontBG.Parent = frontPNL;
            backFadedOvalPassport.Parent = backBG;

            //this.backFadedOvalPassport.Location = new Point(131, 352);
            this.DriverPic.Parent = frontBG; this.DriverPic.Location = new Point(47, 138);
            //this.pictureBox3.Location = new Point(-10, 5);
            this.pictureBox3.Parent = DriverPic; this.pictureBox3.Location = new Point(-20, -20); 
            this.lblDOB.Parent = frontBG; this.iconPic.Parent = frontBG;
            this.lblRegDate.Parent = frontBG;
            this.lblExpireDate.Parent = frontBG; signature.Parent = frontBG; lblDVLA.Parent = frontBG; 
            this.LBLPhysicalId.Parent = frontBG; lblBakPixDate.Parent = backFadedOvalPassport;
            this.lblBakPixDate.Location = new Point(0, backFadedOvalPassport.Height/2);
            lblPixDate.Parent = DriverPic; this.lblPixDate.Location = new Point(0,200);
            lblAddress.Parent = frontBG;lblDrivable.Parent = frontBG; lblSurname.Parent = frontBG;
            this.lblA10.Parent =  backBG;   this.lblA11.Parent =  backBG;   this.lblA110.Parent = backBG;
            this.lblA111.Parent = backBG;  this.lblA112.Parent =  backBG;  this.lblA12.Parent =   backBG;
            this.lblA210.Parent = backBG;  this.lblA211.Parent =  backBG;  this.lblAM10.Parent =  backBG;
            this.lblAM11.Parent = backBG;  this.lblAM12.Parent =  backBG;  this.lblB10.Parent =   backBG;
            this.lblB11.Parent =  backBG;   this.lblB110.Parent = backBG;  this.lblB112.Parent =  backBG;
            this.lblB12.Parent =  backBG;   this.lblBE10.Parent = backBG;  this.lblBE11.Parent =  backBG;
            this.lblBE12.Parent = backBG;  this.lblC10.Parent =   backBG;   this.lblC110.Parent = backBG;
            this.lblC111.Parent = backBG;  this.lblC112.Parent =  backBG;  this.lblC12.Parent =   backBG;
            this.lblC1E10.Parent =backBG; this.lblC1E11.Parent =  backBG; this.lblC1E12.Parent =  backBG;
            this.lblcC11.Parent = backBG;  this.lblCE10.Parent =  backBG;  this.lblCE11.Parent =  backBG;
            this.lblCE12.Parent = backBG;  this.lblD10.Parent =   backBG;   this.lblD11.Parent =  backBG;
            this.lblD110.Parent = backBG;  this.lblD111.Parent =  backBG;  this.lblD112.Parent =  backBG;
            this.lblD12.Parent =  backBG; this.lblD1E10.Parent =  backBG; this.lblD1E11.Parent =  backBG;
            this.lblD1E12.Parent =backBG; this.lblDE10.Parent =   backBG;  this.lblDE11.Parent =  backBG;
            this.lblDE12.Parent = backBG;  this.lblfkq10.Parent = backBG; this.lblfkq11.Parent =  backBG;
            this.lblfkq12.Parent =backBG;  this.lblBackCode.Parent = backBG;
            this.lblA212.Parent = backBG; this.lblB111.Parent =  backBG; 
        }

        public Bitmap SetGrayscale(string imgPath)
        {

            Bitmap originalImage = new Bitmap(imgPath);
            Bitmap blackAndWhiteImage = new Bitmap(originalImage.Width, originalImage.Height);
            Graphics g = this.CreateGraphics();
            Color c = Color.Black;
            Color v = Color.Black;
            int av = 0;
            for (int i = 0; i < originalImage.Width; i++)
            {
                for (int j = 0; j < originalImage.Height; j++)
                {
                    c = originalImage.GetPixel(i, j);
                    av = (c.R + c.G + c.B) / 3;
                    v = Color.FromArgb(c.A, av, av, av);
                    blackAndWhiteImage.SetPixel(i, j, v);
                }
            }
            return ChangeImageOpacity(blackAndWhiteImage,0.4);
            //return blackAndWhiteImage;
        }
        
        public static Bitmap ChangeImageOpacity(Bitmap originalImage, double opacity)
        {
             int bytesPerPixel = 4;
            if ((originalImage.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
            {
                // Cannot modify an image with indexed colors
                return originalImage;
            }
            Bitmap bmp = (Bitmap)originalImage.Clone();
            // Specify a pixel format.
            PixelFormat pxf = PixelFormat.Format32bppArgb;
            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
            // Declare an array to hold the bytes of the bitmap.
            // This code is specific to a bitmap with 32 bits per pixels 
            // (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
            int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
            byte[] argbValues = new byte[numBytes];
            // Copy the ARGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);
            // Manipulate the bitmap, such as changing the
            // RGB values for all pixels in the bitmap.
            for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
            {
                // argbValues is in format BGRA (Blue, Green, Red, Alpha)
                // If 100% transparent, skip pixel
                if (argbValues[counter + bytesPerPixel - 1] == 0)
                    continue;
                int pos = 0;
                pos++; // B value
                pos++; // G value
                pos++; // R value
                argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
            }
            // Copy the ARGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);
            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private void doFonts()
        {
            
            AllocFont(FontStyle.Regular, lblPixDate, 10);
            lblPixDate.ForeColor = Color.FromArgb(38, 39, 44);
            AllocFont(FontStyle.Regular, lblBakPixDate, 18);
            lblBakPixDate.ForeColor = Color.FromArgb(200,109, 108, 106);
            AllocFont(FontStyle.Bold, lblSurname, 18); 
            lblSurname.ForeColor = Color.FromArgb(1, 61, 52, 53);
            //lblSurname.Font = FontStyle.Bold;
            AllocFont(FontStyle.Regular, lblFirstName, 16);
            lblFirstName.ForeColor = Color.FromArgb(1, 61, 52, 53);
            AllocFont(FontStyle.Regular, lblDOB, 16);
            lblDOB.ForeColor = Color.FromArgb(1, 61, 52, 53);
            AllocFont(FontStyle.Regular, lblRegDate, 16);
            lblRegDate.ForeColor = Color.FromArgb(1, 61, 52, 53);
            AllocFont(FontStyle.Regular, lblExpireDate, 16);
            lblExpireDate.ForeColor = Color.FromArgb(1, 61, 52, 53);
            AllocFont(FontStyle.Regular, LBLPhysicalId, 16);
            LBLPhysicalId.ForeColor = Color.FromArgb(1, 61, 52, 53);
            AllocFont(FontStyle.Regular, lblDrivable, 16); 
            lblDrivable.ForeColor = Color.FromArgb(1, 61, 52, 53);
            AllocFont(FontStyle.Regular, lblDVLA, 16);
            lblDVLA.ForeColor = Color.FromArgb(1, 61, 52, 53);
            AllocFont(FontStyle.Regular, lblAddress, 16);
            lblAddress.ForeColor = Color.FromArgb(1, 61, 52, 53);


             AllocFont(FontStyle.Regular, this.lblA10  , 10); AllocFont(FontStyle.Regular, this.lblA11, 10);   AllocFont(FontStyle.Regular, this.lblA110 , 10);
             AllocFont(FontStyle.Regular, this.lblA111 , 10); AllocFont(FontStyle.Regular,  this.lblA112 , 10);   AllocFont(FontStyle.Regular, this.lblA12  , 10);
             AllocFont(FontStyle.Regular, this.lblA210 , 10); AllocFont(FontStyle.Regular, this.lblA211 , 10); AllocFont(FontStyle.Regular, this.lblAM10 , 10);
             AllocFont(FontStyle.Regular, this.lblAM11 , 10); AllocFont(FontStyle.Regular, this.lblAM12 , 10); AllocFont(FontStyle.Regular, this.lblB10  , 10);
             AllocFont(FontStyle.Regular, this.lblB11  , 10); AllocFont(FontStyle.Regular, this.lblB110 , 10); AllocFont(FontStyle.Regular, this.lblB112 , 10);
             AllocFont(FontStyle.Regular, this.lblB12  , 10); AllocFont(FontStyle.Regular, this.lblBE10 , 10); AllocFont(FontStyle.Regular, this.lblBE11 , 10);
             AllocFont(FontStyle.Regular, this.lblBE12 , 10); AllocFont(FontStyle.Regular, this.lblC10  , 10); AllocFont(FontStyle.Regular, this.lblC110 , 10);
             AllocFont(FontStyle.Regular, this.lblC111 , 10); AllocFont(FontStyle.Regular, this.lblC112 , 10); AllocFont(FontStyle.Regular, this.lblC12  , 10);
             AllocFont(FontStyle.Regular, this.lblC1E10, 10); AllocFont(FontStyle.Regular, this.lblC1E11, 10); AllocFont(FontStyle.Regular, this.lblC1E12, 10);
             AllocFont(FontStyle.Regular, this.lblcC11 , 10); AllocFont(FontStyle.Regular, this.lblCE10 , 10); AllocFont(FontStyle.Regular, this.lblCE11 , 10);
             AllocFont(FontStyle.Regular, this.lblCE12 , 10); AllocFont(FontStyle.Regular, this.lblD10  , 10); AllocFont(FontStyle.Regular, this.lblD11  , 10);
             AllocFont(FontStyle.Regular, this.lblD110 , 10); AllocFont(FontStyle.Regular, this.lblD111 , 10); AllocFont(FontStyle.Regular, this.lblD112 , 10);
             AllocFont(FontStyle.Regular, this.lblD12  , 10); AllocFont(FontStyle.Regular, this.lblD1E10, 10); AllocFont(FontStyle.Regular, this.lblD1E11, 10);
             AllocFont(FontStyle.Regular, this.lblD1E12, 10); AllocFont(FontStyle.Regular, this.lblDE10 , 10); AllocFont(FontStyle.Regular, this.lblDE11 , 10);
             AllocFont(FontStyle.Regular, this.lblDE12 , 10); AllocFont(FontStyle.Regular, this.lblfkq10, 10); AllocFont(FontStyle.Regular, this.lblfkq11, 10);
             AllocFont(FontStyle.Regular, this.lblfkq12, 10); AllocFont(FontStyle.Regular, this.iconPic , 10); AllocFont(FontStyle.Regular, this.lblBackCode, 13); 
             AllocFont(FontStyle.Regular, this.lblA212 , 10); AllocFont(FontStyle.Regular, this.lblB111 , 10);

            this.lblA10 .ForeColor =  Color.FromArgb(1,97,85,89); this.lblA11.ForeColor =   Color.FromArgb(1,97,85,89);   this.lblA110.ForeColor =  Color.FromArgb(1,97,85,89);
            this.lblA111.ForeColor =  Color.FromArgb(1,97,85,89); this.lblA112.ForeColor =  Color.FromArgb(1,97,85,89);  this.lblA12 .ForeColor =   Color.FromArgb(1,97,85,89); 
            this.lblA210.ForeColor =  Color.FromArgb(1,97,85,89); this.lblA211.ForeColor =  Color.FromArgb(1,97,85,89);  this.lblAM10.ForeColor =   Color.FromArgb(1,97,85,89); 
            this.lblAM11.ForeColor =  Color.FromArgb(1,97,85,89); this.lblAM12.ForeColor =  Color.FromArgb(1,97,85,89);  this.lblB10 .ForeColor =   Color.FromArgb(1,97,85,89); 
            this.lblB11 .ForeColor =  Color.FromArgb(1,97,85,89); this.lblB110.ForeColor =  Color.FromArgb(1,97,85,89);  this.lblB112.ForeColor =   Color.FromArgb(1,97,85,89); 
            this.lblB12 .ForeColor =  Color.FromArgb(1,97,85,89); this.lblBE10.ForeColor =  Color.FromArgb(1,97,85,89);  this.lblBE11.ForeColor =   Color.FromArgb(1,97,85,89); 
            this.lblBE12.ForeColor =  Color.FromArgb(1,97,85,89); this.lblC10 .ForeColor =  Color.FromArgb(1,97,85,89);  this.lblC110.ForeColor =   Color.FromArgb(1,97,85,89); 
            this.lblC111.ForeColor =  Color.FromArgb(1,97,85,89); this.lblC112.ForeColor =  Color.FromArgb(1,97,85,89);  this.lblC12.ForeColor =    Color.FromArgb(1,97,85,89);
            this.lblC1E10.ForeColor = Color.FromArgb(1,97,85,89); this.lblC1E11.ForeColor = Color.FromArgb(1,97,85,89);  this.lblC1E12.ForeColor =  Color.FromArgb(1,97,85,89);
            this.lblcC11 .ForeColor = Color.FromArgb(1,97,85,89); this.lblCE10 .ForeColor = Color.FromArgb(1,97,85,89);  this.lblCE11 .ForeColor =  Color.FromArgb(1,97,85,89);
            this.lblCE12 .ForeColor = Color.FromArgb(1,97,85,89); this.lblD10  .ForeColor = Color.FromArgb(1,97,85,89);  this.lblD11  .ForeColor =  Color.FromArgb(1,97,85,89);
            this.lblD110 .ForeColor = Color.FromArgb(1,97,85,89); this.lblD111 .ForeColor = Color.FromArgb(1,97,85,89);  this.lblD112 .ForeColor =  Color.FromArgb(1,97,85,89);
            this.lblD12  .ForeColor = Color.FromArgb(1,97,85,89); this.lblD1E10.ForeColor = Color.FromArgb(1,97,85,89);  this.lblD1E11.ForeColor =  Color.FromArgb(1,97,85,89);
            this.lblD1E12.ForeColor = Color.FromArgb(1,97,85,89); this.lblDE10 .ForeColor = Color.FromArgb(1,97,85,89);  this.lblDE11 .ForeColor =  Color.FromArgb(1,97,85,89);
            this.lblDE12 .ForeColor = Color.FromArgb(1,97,85,89); this.lblfkq10.ForeColor = Color.FromArgb(1,97,85,89);  this.lblfkq11.ForeColor =  Color.FromArgb(1,97,85,89);
            this.lblfkq12.ForeColor = Color.FromArgb(1,97,85,89); this.iconPic .ForeColor = Color.FromArgb(1,97,85,89); this.lblBackCode.ForeColor =Color.FromArgb(1,97,85,89);
            this.lblA212.ForeColor =  Color.FromArgb(1,97,85,89); this.lblB111.ForeColor =  Color.FromArgb(1,97,85,89);
        }

        private void AllocFont(FontStyle f, Control c,float size)
        {
            FontStyle fontStyle = FontStyle.Regular;
            c.Font = new Font(ff, size, fontStyle);
        }

        private void VehicleLicensePrintOut_Load(object sender, EventArgs e)
        {
            loadFont();
            loadRecord();
            setBg();
            doFonts();
        }

        private void btnFront_Click(object sender, EventArgs e)
        {
            btnFront.Enabled = false;
            backPNL.Visible = false;
            frontPNL.Visible = true;
            btnBack.Enabled = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            btnBack.Enabled = false;
            frontPNL.Visible = false;
            backPNL.Visible = true;
            backBG.Visible = true;
            btnFront.Enabled = true;
        }

    
        class cls_convertImagesByte
        {

            public static Image GetImageFromByte(byte[] byteArrayIn)
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }

            public static byte[] GetByteArrayFromImage(System.Drawing.Image imageIn)
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
        private Bitmap sepiaImg(Bitmap originalImage)
        {
            //read image
            Bitmap bmp = originalImage;// new Bitmap(originalImage); ;
            //get image dimension
            int width = bmp.Width;
            int height = bmp.Height;

            //color of pixel
            Color p;

            //sepia
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //get pixel value
                    p = bmp.GetPixel(x, y);
                    //extract pixel component ARGB
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;
                    //calculate temp value
                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);
                    //set new RGB value
                    if (tr > 255)
                    {
                        r = 255;
                    }
                    else
                    {
                        r = tr;
                    }

                    if (tg > 255)
                    {
                        g = 255;
                    }
                    else
                    {
                        g = tg;
                    }

                    if (tb > 255)
                    {
                        b = 255;
                    }
                    else
                    {
                        b = tb;
                    }
                    //set the new RGB value in image pixel
                    bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            return bmp;

        }

        private void PrintPdf(Panel pnl)
        {
            //PrintDialog myPrintDialog = new PrintDialog();
            //Bitmap memoryImage = new Bitmap(pnl.Width, pnl.Height);
            //pnl.DrawToBitmap(memoryImage, pnl.ClientRectangle);
            //if (myPrintDialog.ShowDialog() == DialogResult.OK)
            //{
            //    PrinterSettings values;
            //    values = myPrintDialog.PrinterSettings;
            //    myPrintDialog.Document = printDocument1;
            //    printDocument1.PrintController = new StandardPrintController();
            //    printDocument1.Print();
            //}
            //printDocument1.Dispose();
            Bitmap bmp = new Bitmap(pnl.Width, pnl.Height);

            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            

            string path = StaticSettings.PassPortFolder;
            string fileName = Guid.NewGuid().ToString();
            string picPath = StaticHelpers.SaveImage(bmp, fileName, path);

            // or use it somewhere

            //pictureBox1.Image = bmp;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintPdf(frontPNL);
            StaticHelpers.ShowInfoMessage("Front View Printed");
            frontPNL.Visible = false;
            backPNL.Visible = true;
            backFadedOvalPassport.Visible = true;
            PrintPdf(backPNL);
            StaticHelpers.ShowInfoMessage("Back View Printed");
            frontPNL.Visible = true;
            btnBack.Enabled = false;
            btnFront.Enabled = true;
        }
    }
}
