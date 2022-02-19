using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Management;

namespace UKLicense.Utilities
{
    public static class StaticHelpers
    {
        /// <summary>
        /// Show Message Box with exception details
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="showInnerException"></param>
        /// <param name="title"></param>
        /// <param name="custommsg"></param>
        public static void ShowErrorMessage(Exception ex, bool showInnerException = false, string title = "UKLicense:: Techboss", string custommsg = "")
        {
            string returnMsg = ex.Message;
            if (showInnerException)
            {
                returnMsg += "Inner Exception ---> " + ex.InnerException.Message + " " + ex.InnerException.StackTrace +
                             " " + ex.StackTrace;
            }

            MessageBox.Show(returnMsg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public static string FormattedException(Exception ex)
        {
            return ex.Message + " Inner Exception ---> " + ex.InnerException.Message + " " + ex.InnerException.StackTrace +
                             " " + ex.StackTrace;
        }

        /// <summary>
        /// Allows Caller to pass their custom error message rather tham ex.Message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="title"></param>
        public static void ShowCustomErrorMessage(string errorMessage, string title = "UKLicense:: Techboss")
        {
            MessageBox.Show(errorMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Show Information Message to User
        /// </summary>
        /// <param name="infoMessage"></param>
        /// <param name="title"></param>
        public static void ShowInfoMessage(string infoMessage, string title = "UKLicense:: Techboss")
        {
            MessageBox.Show(infoMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }


        public static void ShowWarningMessage(string infoMessage, string title = "UKLicense:: Techboss")
        {
            MessageBox.Show(infoMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        public static void ShowExclamingMessage(string infoMessage, string title = "UKLicense:: Techboss")
        {
            MessageBox.Show(infoMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        /// <summary>
        /// Show a Yes or No Dialog Box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ConfirmResult"></param>
        /// <param name="title"></param>
        /// <returns>true/false</returns>
        public static bool ShowConfirmDialog(string message, bool ConfirmResult, string title = "UKLicense::Confirm")

        {
            bool finalResult = false;
            if (ConfirmResult == true)
            {
                finalResult = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                              DialogResult.Yes;
            }
            else
            {
                finalResult = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
            }
            
            return finalResult;

        }

        /// <summary>
        /// Checks if the string value is a valid email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true/false</returns>
        public static bool IsValidEmail(string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }

        /// <summary>
        /// Ensures that an inputed string value is a number
        /// </summary>
        /// <param name="value"></param>
        /// <returns>true/false</returns>
        public static bool IsNumeric(string value)
        {
            bool isNum = false;
            Int32 output;
            decimal decOut;
            float floatOut;
            if (Int32.TryParse(value, out output) ||
                decimal.TryParse(value, out decOut) || float.TryParse(value, out floatOut))
            {
                isNum = true;
            }

            return isNum;
        }



        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Right(string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }

        /// <summary>
        /// Get substring of specified number of characters on the left.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(0, length);
        }

        public static bool PingInternet()
        {
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

                System.Net.NetworkInformation.PingReply pingStatus =
                    ping.Send(IPAddress.Parse("208.69.34.231"), 1000);

                if (pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }



        public static bool IsInternetConnectionAvailable()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // discard because of standard reasons
                if ((ni.OperationalStatus != OperationalStatus.Up) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                    continue;
                // this allow to filter modems, serial, etc.
                // I use 10000000 as a minimum speed for most cases
                // discard virtual cards (virtual box, virtual pc, etc.)
                if ((ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0))
                    continue;
                // discard "Microsoft Loopback Adapter", it will not show as NetworkInterfaceType.Loopback but as Ethernet Card.
                if (ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                    continue;
                return true;
            }
            return false;
        }






        public static String Sec_Key()
        {
            return "IBS_Simplex_System_Encryption_Text";
        }
        public static int DaysDiff(String NewerDate1, String OlderDate2)
        {
            String Diff = DateTime.Parse(NewerDate1).Subtract(DateTime.Parse(OlderDate2)).ToString().Split(new char[] { '.' })[0].ToString();
            if (DateTime.Parse(NewerDate1).Subtract(DateTime.Parse(OlderDate2)).ToString() == "00:00:00")
                return 0;
            else
                return int.Parse(Diff);
        }
        public static int monthDifference(String startDate, String endDate)
        {
            DateTime StartDate = DateTime.Parse(startDate);
            DateTime EndDate = DateTime.Parse(endDate);

            int monthsApart = 12 * (StartDate.Year - EndDate.Year) + StartDate.Month - EndDate.Month;
            return Math.Abs(monthsApart);
        }
        public static int yearDifference(String startDate, String endDate)
        {
            DateTime StartDate = DateTime.Parse(startDate);
            DateTime EndDate = DateTime.Parse(endDate);

            int yearApart = (StartDate.Year - EndDate.Year);
            return Math.Abs(yearApart);
        }
        public static String EncryptString(String inputtext)
        {
            String passwordKey = Sec_Key();

            if (inputtext == "")
                return inputtext;

            //We are now going to create an instance of theRihndael class. 
            RijndaelManaged rijndaelcipher = new RijndaelManaged();
            //First we need to turn the input strings into a byte array.
            Byte[] plaintext = System.Text.Encoding.Unicode.GetBytes(inputtext);
            //We are using salt to make it harder to guess our key using a dictionary attack.
            Byte[] salt = Encoding.ASCII.GetBytes(passwordKey);
            //The (Secret Key) will be generated from the specified password and salt.
            Rfc2898DeriveBytes secretkey = new Rfc2898DeriveBytes(passwordKey, salt);
            //Create a encryptor from the existing SecretKey bytes.
            //We use 32 bytes for the secret key.The default Rijndael key length is 256 bit = 32 bytes)
            //then 16 bytes for the IV (initialization vector).The default Rijndael IV length is 128 bit = 16 bytes)
            ICryptoTransform encryptor = rijndaelcipher.CreateEncryptor(secretkey.GetBytes(32), secretkey.GetBytes(16));
            //Create a MemoryStream that is going to hold the encrypted bytes
            MemoryStream memstream = new MemoryStream();
            //Create a CryptoStream through which we are going to be processing our data.
            //CryptoStreamMode.Write means that we are going to be writing data
            //to the stream and the output will be written in the MemoryStream
            //we have provided. (always use write mode for encryption)
            CryptoStream mycryptostream = new CryptoStream(memstream, encryptor, CryptoStreamMode.Write);
            //Start the encryption process.
            mycryptostream.Write(plaintext, 0, plaintext.Length);
            //Finish encrypting.
            mycryptostream.FlushFinalBlock();
            //Convert our encrypted data from a memoryStream into a byte array.
            Byte[] cipherbytes = memstream.ToArray();
            //Close both streams.
            memstream.Close();
            mycryptostream.Close();
            //Convert encrypted data into a base64-encoded string.
            //A common mistake would be to use an Encoding class for that.
            //It does not work, because not all byte values can be
            //represented by characters. We are going to be using Base64 encoding
            //That is designed exactly for what we are trying to do.
            String encrypteddata = Convert.ToBase64String(cipherbytes);
            //Return encrypted string.
            return encrypteddata;
        }

        public static String DecryptString(String inputtext)
        {

            String password = Sec_Key();

            if (inputtext == "")
                return inputtext;

            RijndaelManaged rijndaelcipher = new RijndaelManaged();
            Byte[] encrypteddata = Convert.FromBase64String(inputtext);
            Byte[] salt = Encoding.ASCII.GetBytes(password);
            Rfc2898DeriveBytes secretkey = new Rfc2898DeriveBytes(password, salt);
            // Create a decryptor from the existing SecretKey bytes.
            ICryptoTransform decryptor = rijndaelcipher.CreateDecryptor(secretkey.GetBytes(32), secretkey.GetBytes(16));
            MemoryStream memStream = new MemoryStream(encrypteddata);
            // Create a CryptoStream. (always use Read mode for decryption).
            CryptoStream mycryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);
            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold EncryptedData;
            // DecryptedData is never longer than EncryptedData.
            Byte[] plaintext = new Byte[encrypteddata.Length];
            // Start decrypting.
            int decryptedcount = mycryptoStream.Read(plaintext, 0, plaintext.Length);
            memStream.Close();
            mycryptoStream.Close();
            // Convert decrypted data into a string.
            String DecryptedData = Encoding.Unicode.GetString(plaintext, 0, decryptedcount);
            // Return decrypted string.  
            return DecryptedData;
        }


        public static string DropBoxRootFolderName
        {
            get { return "/Mark"; }
        }

        /// <summary>
        /// Replaces list of token with a supplied text
        /// </summary>
        /// <param name="src"></param>
        /// <param name="replacements"></param>
        /// <returns></returns>
        public static string ReplaceTokensInText(string src, IDictionary<string, object> replacements)
        {
            return Regex.Replace(src, @"{(\w+)}", (m) =>
            {
                object replacement;
                var key = m.Groups[1].Value;
                if (replacements.TryGetValue(key, out replacement))
                {
                    return Convert.ToString(replacement);
                }
                else
                {
                    return m.Groups[0].Value;
                }
            });
        }

        public static string SaveImage(Image image, string filename, string path, string imgExt = ".png")
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = string.Empty;
            if (!path.EndsWith(@"\"))
            {
                path += @"\";
            }

            fullPath = path + filename + imgExt;

            image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
            return fullPath;

        }

        static public Bitmap ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            Bitmap bmp = new Bitmap(newImage);

            return bmp;
        }
        public static Image resizeImageFromFile(int newWidth, int newHeight, string stPhotoPath)
        {
            Image imgPhoto = Image.FromFile(stPhotoPath);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();
            return bmPhoto;
        }


        public static Image resizeImage(int newWidth, int newHeight, Image img)
        {
            Image imgPhoto = img;

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();
            return bmPhoto;
        }

        public static string OverLayImage(Image bigImage, Image thumb, string savePath)
        {
            Image dcoImg = bigImage;

            int imgWidth = dcoImg.Width;
            int height = dcoImg.Height;

            Bitmap OutputImg = new Bitmap(imgWidth, height);
            Graphics g = Graphics.FromImage(OutputImg);
            g.Clear(SystemColors.AppWorkspace);
            Image dcoImg0 = bigImage;
            Image passPortImg = resizeImage(90, 90, thumb); //Image.FromFile(@"C:\Users\oadesina\Pictures\mark.jpg");//
            g.DrawImage(dcoImg0, new Point(0, 0));
            imgWidth = dcoImg0.Width;
            g.DrawImage(passPortImg, new Point(imgWidth - (passPortImg.Width + 10), 0));
            dcoImg.Dispose();
            g.Dispose();
            string _fileName = SaveImage(OutputImg, Guid.NewGuid().ToString(), StaticSettings.DocumentsFolder);
            return _fileName;
        }


        public static byte[] convertImageToByte(string imageDirectory)
        {

            return File.ReadAllBytes(imageDirectory);

        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Image ByteToImage(byte[] imgbyte)
        {
            Image img = null;
            MemoryStream ms = new MemoryStream(imgbyte);
            return img = Image.FromStream(ms);
        }

        public static void getImageFromSystemDirectory(PictureBox pictureBoxName)
        {
            var dlg = new OpenFileDialog()
            {
                Title = "Import Image",
                DefaultExt = "jpg",
                Filter = "Image Files(*.Bmp;*.JPG;*.GIF;*.PNG)| *.Bmp;*.JPG;*.GIF;*.PNG |All Files (*.*) | *.*",
                InitialDirectory = "C:/",
                ShowHelp = false,
                Multiselect = false
            };

            DialogResult dialogResult = dlg.ShowDialog();
            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
            {
                pictureBoxName.ImageLocation = dlg.FileName;
            }

        }


        public static string GetMacAddress()
        {
            //string macAddresses = "";
            var stringBuilder = new StringBuilder();
            var computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            var nics = NetworkInterface.GetAllNetworkInterfaces();



            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                PhysicalAddress address = adapter.GetPhysicalAddress();
                byte[] bytes = address.GetAddressBytes();
                for (int i = 0; i < bytes.Length; i++)
                {

                    stringBuilder.AppendLine(bytes[i].ToString("X2"));

                }

            }

            return stringBuilder.ToString();
        }

        public static string FormatNumber(string input)
        {
            if (input == null) return null;
            if (input.Contains("-")) input.Replace("-", "");
            return Convert.ToDecimal(input).ToString("#,##0.00");
        }

        public static string CalCulateAge(DateTime birthDate, DateTime currentDate, bool showMonthsAndDays = false)
        {
            string resultText;
            int years = new DateTime(currentDate.Subtract(birthDate).Ticks).Year - 1;
            DateTime pastYearDate = birthDate.AddYears(years);
            int months = 0;
            int days = 0;

            for (int i = 1; i <= 12; i++)
            {
                if (pastYearDate.AddMonths(i) == currentDate)
                {
                    months = i;
                    break;
                }
                else if (pastYearDate.AddMonths(i) > currentDate)
                {
                    months = i - 1;
                    break;
                }

            }

            days = currentDate.Subtract(pastYearDate.AddMonths(months)).Days;
            if (showMonthsAndDays)
            {
                resultText = years.ToString() + " Years " + months.ToString() + " Month(s) " + days.ToString() + " Days";
            }
            else
            {
                resultText = years.ToString();
            }



            return resultText;
        }


        public static DateTime ShowDateDialog(string caption, string unscannedCount = "0")
        {
            var prompt = new Form()
            {
                AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F),
                ClientSize = new System.Drawing.Size(420, 170),
                AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                ShowInTaskbar = false,
                BackColor = Color.FromArgb(202, 225, 255),
                Padding = new Padding(5)
            };

            var instruction = string.Format("It was detected that {0} document has not been scanned, please pick a convinient next appointment date", unscannedCount);

            var pnlInstruction = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 48
            };

            var instuctionLabel = new Label()
            {
                Location = new System.Drawing.Point(2, 9),
                AutoSize = false,
                Text = instruction,
                Size = new System.Drawing.Size(419, 37),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
            };

            pnlInstruction.Controls.Add(instuctionLabel);

            var pnlFooter = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 40
            };

            var confirm = new Button()
            {
                
                Name = "button1",
                Size = new System.Drawing.Size(75, 23),
                TabIndex = 3,
                Text = "Ok",
                UseVisualStyleBackColor = true,
                Dock = DockStyle.Right
            };

            pnlFooter.Controls.Add(confirm);

            var pnlContent = new Panel()
            {
                Dock = DockStyle.Fill,
                Height = 40
            };


            var lbl = new Label()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(5, 80),
                Name = "label2",
                Size = new System.Drawing.Size(117, 13),
                TabIndex = 2,
                Text = "Appointment Date",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
            };


            var picker = new DateTimePicker()
            {
                //Location = new System.Drawing.Point(134, 75),
                Location = new System.Drawing.Point(127, 80),
                Size = new System.Drawing.Size(200, 20),
                Format = DateTimePickerFormat.Custom,
                MinDate = DateTime.Now.AddDays(1),
                CustomFormat = "dd/MM/yyyy"
            };

            pnlContent.Controls.Add(lbl);
            pnlContent.Controls.Add(picker);
        

            confirm.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(pnlInstruction);
            prompt.Controls.Add(pnlContent);
            //prompt.Controls.Add(picker);
            prompt.Controls.Add(pnlFooter);
            prompt.AcceptButton = confirm;
            prompt.ShowDialog();
            return picker.Value.Date;
        }

        public static bool CountDigit(string value)
        {
            bool checkdig = value.Length < 10;

            return checkdig;
        }

      

        public static string CompareAge(DateTime DOB, int MinimumAge)
        {
            string resultingText = string.Empty;
            DateTime todaysDate = DateTime.Today;
            int Age;
            Age = new DateTime(todaysDate.Subtract(DOB).Ticks).Year;
            if (Age < MinimumAge)
                resultingText = "Retiree cannot be less than " + MinimumAge.ToString() + " yrs." + Environment.NewLine + "Kindly check the Date Of Birth.";
            return resultingText;
        }


        public static string ConvertToUpper(string inputString)
        {
            return inputString.ToUpper();
        }


        public static string getExcelFromSystemDirectory()
        {
            string filePath = string.Empty;
            var dlg = new OpenFileDialog()
            {
                Title = "Import Excel File",
                Filter = "Excel Files(*.xls;*.xlsx) | *.xls;*.xlsx",
                InitialDirectory = "C:/",
                ShowHelp = false,
                Multiselect = false
            };

            DialogResult dialogResult = dlg.ShowDialog();
            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
            {
                filePath = dlg.FileName;
            }

            return filePath;

        }

    }
}
