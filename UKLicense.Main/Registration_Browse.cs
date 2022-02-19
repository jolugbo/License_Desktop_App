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
    public partial class Registration_Browse : Form
    {
        LicenseDBEntities context = new LicenseDBEntities();
        public Registration_Browse()
        {
            InitializeComponent();
        }

        #region METHODS
        private void initializeListView()
        {

            registrationlistView.BeginUpdate();
            registrationlistView.View = View.Details;
            registrationlistView.GridLines = true;
           // registrationlistView.CheckBoxes = true;
            registrationlistView.MultiSelect = false;
            registrationlistView.FullRowSelect = true;
            registrationlistView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            registrationlistView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            registrationlistView.Activation = ItemActivation.TwoClick;
            registrationlistView.Columns.Add("S/No", 50, HorizontalAlignment.Left);
            registrationlistView.Columns.Add("License Id", 150, HorizontalAlignment.Left);
            registrationlistView.Columns.Add("Full Name", 200, HorizontalAlignment.Left);
            registrationlistView.Columns.Add("Date Registered", 150, HorizontalAlignment.Left);
            registrationlistView.Columns.Add("Date Expired", 150, HorizontalAlignment.Left);
            registrationlistView.Columns.Add("Nationalty", 200, HorizontalAlignment.Left);
            registrationlistView.Columns.Add("Inspection Officer", 150, HorizontalAlignment.Left);
            registrationlistView.EndUpdate();
        }
        private void LoadRecord()
        {
            try
            {

                var vehicles = (from driver in context.Driver_details
                                join admin in context.Admins on driver.DataCapturedBy equals admin.Admin_id
                                select new
                                {
                                    id = driver.id_driver,
                                    LicenseId = driver.physical_id,
                                    OwnerName = driver.driver_last_name + " " + driver.driver_first_name + " " + driver.driver_middle_name,
                                    DateReg = driver.date_registered,
                                    DateExp = driver.expire_date,
                                    Nationalty = driver.nationalty,
                                    InspectionOfficer = admin.UserName,
                                }).ToList();
                int i = 0;
                registrationlistView.Items.Clear();
                if (vehicles.Count > 0)
                {
                    foreach (var row in vehicles.OrderBy(x => x.DateReg))
                    {
                        i++;
                        var itm = new ListViewItem();
                        itm.Tag = row.id;
                        itm.Text = i.ToString();
                        itm.SubItems.Add(row.LicenseId);
                        itm.SubItems.Add(row.OwnerName);
                        itm.SubItems.Add(string.Format("{0:dd/MM/yyyy}", row.DateReg));
                        itm.SubItems.Add(string.Format("{0:dd/MM/yyyy}", row.DateExp));
                        itm.SubItems.Add(row.Nationalty);
                        itm.SubItems.Add(row.InspectionOfficer);
                        registrationlistView.Items.Add(itm);
                    }
                    totalNoOfRecordlabel.Text = "Total No of Records " + i.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        private void insertButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();

                Registration_update frm = new Registration_update();
                frm.Closed += (s, arg) => this.Close();
                FormHelper.ShowChildForm(this.MdiParent, new Registration_update(ActionTypes.NewEntry, Guid.NewGuid()));
            }
            catch (Exception ex)
            {
                StaticHelpers.ShowErrorMessage(ex, true);
            }
        }

        private void Registration_Browse_Load(object sender, EventArgs e)
        {
            try
            {
                //this.totalNoOfChkItemslabel.Text += "= 0";
                initializeListView();
                LoadRecord();
                //this.totalNoOfRecordlabel.Text =  "Total No of Records";
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string errmsg;
            if (registrationlistView.SelectedItems.Count == 0)
            {
                errmsg = "No Record selected";
                MessageBox.Show(errmsg);
                return;
            }

            if (registrationlistView.SelectedItems.Count > 1)
            {
                errmsg = "You can not check more than one item at a time";
                MessageBox.Show(errmsg);
                return;
            }

            Guid VehicleId = Guid.Parse(registrationlistView.SelectedItems[0].Tag.ToString());
            FormHelper.ShowChildForm(this.MdiParent, new VehicleLicensePrintOut(VehicleId));
        }

        private void changeButton_Click(object sender, EventArgs e)
        {

            string errmsg;
            if (registrationlistView.SelectedItems.Count == 0)
            {
                errmsg = "No Record selected";
                MessageBox.Show(errmsg);
                return;
            }

            if (registrationlistView.SelectedItems.Count > 1)
            {
                errmsg = "You can not check more than one item at a time";
                MessageBox.Show(errmsg);
                return;
            }

            Guid requestDetailsId = Guid.Parse(registrationlistView.SelectedItems[0].Tag.ToString());
            FormHelper.ShowChildForm(this.MdiParent, new Registration_update(ActionTypes.EditEntry, requestDetailsId));

        }
    }
}
