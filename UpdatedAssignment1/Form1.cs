using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UpdatedAssignment1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private CRM crm;
        private VRM vrm;
        private RRM rrm;
        private Customers selectedCustomer;
        private Vehicles selectedVehicle;
        private Rental selectedRentals;

        bool gpsChecked;
        bool sunRoofChecked;
        public Form1()
        {
            InitializeComponent();
            SetupCRM();
            SetupVRM();
            SetupRRM();
            loadCustomersToGridView();
            loadVehicleToGridView();
            loadRentalsToGrid();
            RentVehicle();
            custGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            vehDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void vehicleTabPage_Click(object sender, EventArgs e)
        {
    
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void addCusBtn_Click(object sender, EventArgs e)
        {
            cusLbl.Text = "Add Customer";
            custPanel.Visible = true;
            cusAddBtn.Visible = true;
            saveModCusBtn.Visible = false;
            titleTxtBox.Text = "";
            firstNameTxtBox.Text = "";
            lastTxtBox.Text = "";
            genderCmbBox.Text = "Male";
            custDOBDatePicker.Text = "01/01/1930";

        }

        private void addVehBtn_Click(object sender, EventArgs e)
        {
            vehPanel.Visible = true;
            regoTxtBox.Enabled = true;
            addVehSaveBtn.Visible = true;
            vehSaveBtn.Visible = false;
            vehLbl.Text = "Add Vehicle";
            regoTxtBox.Text = "";
            makeTxtBox.Text = "";
            modelTxtBox.Text = "";
            classCmbBox.Text = "";
            yearTxtBox.Text = "";
            transCmbBox.Text = "";
            fuelCmbBox.Text = "";
            seatsTxtBox.Text = "";
            colourTxtBox.Text = "";
            dailyRateTxtBox.Text = "";
            if (gpsCheck.Checked == true)
            {
                gpsChecked = true;
            }
            else
            {
                gpsChecked = false;
            }
            if (sunRoofCheck.Checked == true )
            {
                sunRoofChecked = true;
            }
            else
            {
                sunRoofChecked = false;
            }
        }

        private void modifyCusBtn_Click(object sender, EventArgs e)
        {
            custPanel.Visible = true;
            saveModCusBtn.Visible = true;
            cusAddBtn.Visible = false;
            cusLbl.Text = "Modify Customer";

            if (selectedCustomer == null)
            {
                ;
            }
            else
            {
                titleTxtBox.Text = selectedCustomer.Title;
                firstNameTxtBox.Text = selectedCustomer.FirstName;
                lastTxtBox.Text = selectedCustomer.LastName;
                custDOBDatePicker.Text = selectedCustomer.DOB;
                genderCmbBox.Text = selectedCustomer.Gender;
            }
        }
        private void saveModCusBtn_Click(object sender, EventArgs e)
        {
            selectedCustomer.Title = titleTxtBox.Text;
            selectedCustomer.FirstName = firstNameTxtBox.Text;
            selectedCustomer.LastName = lastTxtBox.Text;
            selectedCustomer.Gender = genderCmbBox.Text;
            selectedCustomer.DOB = custDOBDatePicker.Text;
            loadCustomersToGridView();

            titleTxtBox.Text = "";
            firstNameTxtBox.Text = "";
            lastTxtBox.Text = "";
            genderCmbBox.Text = "Male";
            custDOBDatePicker.Text = "01/01/1930";
            custPanel.Visible = false;

        }

        private void modifyVehBtn_Click(object sender, EventArgs e)
        {
            vehPanel.Visible = true;
            addVehSaveBtn.Visible = false;
            vehSaveBtn.Visible = true;
            regoTxtBox.Enabled = false;
            if (selectedVehicle == null)
            {
                ;
            }
            else
            {
                regoTxtBox.Text = selectedVehicle.rego;
                makeTxtBox.Text = selectedVehicle.Make;
                modelTxtBox.Text = selectedVehicle.Model;
                classCmbBox.Text = selectedVehicle.VehClass;
                yearTxtBox.Text = selectedVehicle.Year.ToString();
                transCmbBox.Text = selectedVehicle.Transmission;
                fuelCmbBox.Text = selectedVehicle.Fuel;
                seatsTxtBox.Text = selectedVehicle.Seats.ToString();
                if (selectedVehicle.gps == true)
                {
                    gpsCheck.Checked = true;
                }
                else
                {
                    gpsCheck.Checked = false;
                }
                if (selectedVehicle.SunRoof == true)
                {
                    sunRoofCheck.Checked = true;
                }
                else
                {
                    sunRoofCheck.Checked = false;
                }
                dailyRateTxtBox.Text = selectedVehicle.DailyRate.ToString();
                colourTxtBox.Text = selectedVehicle.Colour;
            }
        }

        private void metroLabel12_Click(object sender, EventArgs e)
        {

        }

        private void cancelModVehBtn_Click(object sender, EventArgs e)
        {
            vehPanel.Visible = false;
            regoTxtBox.Text = "";
            makeTxtBox.Text = "";
            modelTxtBox.Text = "";
            classCmbBox.Text = "";
            yearTxtBox.Text = "";
            transCmbBox.Text = "";
            fuelCmbBox.Text = "";
            seatsTxtBox.Text = "";
        }

        private void SetupCRM()
        {
            crm = new CRM();
            try
            {
                crm.CustomerFileName = "customer.csv";
                crm.LoadFromFile();
            }
            catch
            {
                MessageBox.Show("Customer.csv File Not Found!");
            }
        }

        private void SetupVRM()
        {
            vrm = new VRM();
            try
            {
                vrm.FleetFileName = "fleet.csv";
                vrm.LoadFromFile();
            }
            catch
            {
                MessageBox.Show("Fleet.csv File Not Found!");
            }
        }

        private void SetupRRM()
        {
            rrm = new RRM();
            try
            {
                rrm.RentalFileName = "rentals.csv";
                rrm.LoadFromFile();
            }
            catch
            {
                MessageBox.Show("Rentals.csv File not Found!");
            }
        }

        public void loadRentalsToGrid()
        {
            rentalDataGrid.Rows.Clear();

            foreach (Rental rent in rrm.GetRental())
            {
                rentalDataGrid.Rows.Add(new string[] { rent.Rego, rent.customerID.ToString(), rent.RentDuration.ToString() });
            }
        }

        public void RentVehicle()
        {
            System.IO.StreamReader fileReader = new System.IO.StreamReader("rentals.csv");

            fileReader.ReadLine();
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                vrm.rentals.Add(parts[0], int.Parse(parts[1]));
            }
            fileReader.Close();

        }

        public void loadCustomersToGridView()
        {
            custGridView.Rows.Clear();

            foreach(Customers cust in crm.GetCustomer())
            {
                custGridView.Rows.Add(new string[] { cust.customerID.ToString(), cust.Title, cust.FirstName, cust.LastName, cust.Gender, cust.DOB });
            }
        }
        private void showAll_Click(object sender, EventArgs e)
        {
            rentalSearchDataGrid.Rows.Clear();
            foreach(Vehicles veh in vrm.GetVehiclesRented(false))
            {
                rentalSearchDataGrid.Rows.Add(new string[] {  veh.rego, veh.Make, veh.Model, veh.Year.ToString(), veh.VehClass, veh.Seats.ToString(), veh.Transmission,
                    veh.Fuel, veh.gps.ToString(), veh.SunRoof.ToString(), veh.Colour, veh.DailyRate.ToString()});
            }
        }

        public void loadVehicleToGridView()
        {
            vehDataGridView.Rows.Clear();

            foreach(Vehicles veh in vrm.GetVehicles())
            {
                vehDataGridView.Rows.Add(new string[] {  veh.rego, veh.Make, veh.Model, veh.Year.ToString(), veh.VehClass, veh.Seats.ToString(), veh.Transmission,
                    veh.Fuel, veh.gps.ToString(), veh.SunRoof.ToString(), veh.Colour, veh.DailyRate.ToString()});
            }
        }


        private void vehDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int rowsCount = vehDataGridView.SelectedRows.Count;

            if (rowsCount == 0 || rowsCount > 1)
            {
                selectedVehicle = null;
            }
            else
            {
                string selectedID = vehDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                selectedVehicle = vrm.GetVehicles(selectedID);
            }
        }

        public void ChangeTextBoxesVeh ()
        {
            regoTxtBox.Text = selectedVehicle.rego;
            makeTxtBox.Text = selectedVehicle.Make;
            modelTxtBox.Text = selectedVehicle.Model;
            classCmbBox.Text = selectedVehicle.VehClass;
            yearTxtBox.Text = selectedVehicle.Year.ToString();
            transCmbBox.Text = selectedVehicle.Transmission;
            fuelCmbBox.Text = selectedVehicle.Fuel;
            seatsTxtBox.Text = selectedVehicle.Seats.ToString();
            if (selectedVehicle.gps == true)
            {
                gpsCheck.Checked = true;
            }
            else
            {
                gpsCheck.Checked = false;
            }
            if (selectedVehicle.SunRoof == true)
            {
                sunRoofCheck.Checked = true;
            }
            else
            {
                sunRoofCheck.Checked = false;
            }
            dailyRateTxtBox.Text = selectedVehicle.DailyRate.ToString();
            colourTxtBox.Text = selectedVehicle.Colour;
        }

        private void removeVehBtn_Click(object sender, EventArgs e)
        {
            string confirmCust = vehDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            bool vehRent = vrm.IsRentingCar(confirmCust);
            if (selectedVehicle == null)
            {
                MessageBox.Show("Please Choose a Vehicle you Wish to Delete");
            }
            else if (vehRent == true)
            {
                MessageBox.Show("This Vehicle is currently Rented, and therefore you cannot remove it.");
            }
            else
            {
                DialogResult delCarRes = MessageBox.Show("Are you sure you want to delete this vehicle?", "Important Message", MessageBoxButtons.YesNo);
                if (delCarRes == DialogResult.Yes)
                {
                    vrm.RemoveVehicle(selectedVehicle);
                    loadVehicleToGridView();
                }
                else
                {
                    ;
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            bool validInt = false;
            int yearTxtInt, dailyRTxtInt;

            selectedVehicle.Make = makeTxtBox.Text;
            selectedVehicle.Model = modelTxtBox.Text;
            selectedVehicle.VehClass = classCmbBox.Text;
            validInt = Int32.TryParse(yearTxtBox.Text, out yearTxtInt);
            while (!validInt || yearTxtInt < 2000 || yearTxtInt > 2019)
            {
                yearTxtBox.Text = "2000";
                validInt = Int32.TryParse(yearTxtBox.Text, out yearTxtInt);
                MessageBox.Show("Please Enter a valid number for year of car (between 2000 - 2019)");
            } 
            selectedVehicle.Year = yearTxtInt;
            selectedVehicle.Transmission = transCmbBox.Text;
            selectedVehicle.Fuel = fuelCmbBox.Text;
            if (gpsCheck.Checked == true)
            {
                selectedVehicle.gps = true;
            }
            else
            {
                selectedVehicle.gps = false;
            }
            if (sunRoofCheck.Checked == true)
            {
                selectedVehicle.SunRoof = true;
            }
            else
            {
                selectedVehicle.SunRoof = false;
            }
            validInt = Int32.TryParse(dailyRateTxtBox.Text, out dailyRTxtInt);
            while (!validInt || dailyRTxtInt < 1)
            {
                dailyRateTxtBox.Text = "1";
                MessageBox.Show("Please enter a valid daily cost for vehicle");
            }
            selectedVehicle.DailyRate = dailyRTxtInt;
            selectedVehicle.Colour = colourTxtBox.Text;
            loadVehicleToGridView();
            vrm.SaveToFile();


        }

        public void enableSaveBtn()
        {
            if (string.IsNullOrWhiteSpace(regoTxtBox.Text) || string.IsNullOrWhiteSpace(makeTxtBox.Text) || string.IsNullOrWhiteSpace(modelTxtBox.Text)
                || string.IsNullOrWhiteSpace(classCmbBox.Text) || string.IsNullOrWhiteSpace(yearTxtBox.Text) || string.IsNullOrWhiteSpace(transCmbBox.Text)
                || string.IsNullOrWhiteSpace(fuelCmbBox.Text) || string.IsNullOrWhiteSpace(seatsTxtBox.Text) || string.IsNullOrWhiteSpace(dailyRateTxtBox.Text)
                || string.IsNullOrWhiteSpace(colourTxtBox.Text))
            {
                vehSaveBtn.Enabled = false;
                addVehSaveBtn.Enabled = false;
            }
            else
            {
                vehSaveBtn.Enabled = true;
                addVehSaveBtn.Enabled = true;
            }

            if (string.IsNullOrWhiteSpace(carTxtBox.Text) || carTxtBox.Text == "Enter Query ... (Seperate parameters with a Comma)" ||
                carTxtBox.Text == "Enter Query ... (Seperate parameters with a Comma" || carTxtBox.Text == "Enter Query ... (Seperate parameters with a Comm" ||
                carTxtBox.Text == "Enter Query ... (Seperate parameters with a Com" || carTxtBox.Text == "Enter Query ... (Seperate parameters with a Co" || carTxtBox.Text == "Enter Query ... (Seperate parameters with a C")
            {
                srchBtn.Enabled = false;
            }
            else
            {
                srchBtn.Enabled = true;
            }

            if (string.IsNullOrWhiteSpace(titleTxtBox.Text) || string.IsNullOrWhiteSpace(firstNameTxtBox.Text) || string.IsNullOrWhiteSpace(lastTxtBox.Text) ||
                string.IsNullOrWhiteSpace(custDOBDatePicker.Text) || string.IsNullOrWhiteSpace(genderCmbBox.Text))
            {
                saveModCusBtn.Enabled = false;
                cusAddBtn.Enabled = false;
            }
            else
            {
                saveModCusBtn.Enabled = true;
                cusAddBtn.Enabled = true;
            }
        }

        private void regoTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void makeTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void modelTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void classCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void yearTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void transCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void fuelCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void seatsTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void dailyRateTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void colourTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void sunRoofCheck_CheckedChanged(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void GpsCheck_CheckedChanged(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void addVehSaveBtn_Click(object sender, EventArgs e)
        {
            if (vrm.GetVehicles(regoTxtBox.Text) == null)
            {
                Vehicles newVehicle = new Vehicles(regoTxtBox.Text, makeTxtBox.Text, modelTxtBox.Text, int.Parse(yearTxtBox.Text), classCmbBox.Text, int.Parse(seatsTxtBox.Text),
                    transCmbBox.Text, fuelCmbBox.Text, gpsChecked, sunRoofChecked, colourTxtBox.Text, int.Parse(dailyRateTxtBox.Text));

                vrm.AddVehicle(newVehicle);

                loadVehicleToGridView();
                vrm.SaveToFile();
                vehPanel.Visible = false;
                regoTxtBox.Text = "";
                makeTxtBox.Text = "";
                modelTxtBox.Text = "";
                classCmbBox.Text = "";
                yearTxtBox.Text = "";
                transCmbBox.Text = "";
                fuelCmbBox.Text = "";
                seatsTxtBox.Text = "";
            }
            else
            {
                MessageBox.Show("Registration already in use");
            }


        }

        private void carTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void srchBtn_Click(object sender, EventArgs e)
        {
            rentalSearchDataGrid.Rows.Clear();
            int minCost = (int)minNum.Value;
            int maxCost = (int)maxNum.Value;
            foreach (Vehicles veh in vrm.GetVehiclesSearch(carTxtBox.Text, minCost, maxCost))
            {
                
                rentalSearchDataGrid.Rows.Add(new string[] { veh.rego, veh.Make, veh.Model, veh.Year.ToString(), veh.VehClass,
                    veh.Seats.ToString(), veh.Transmission, veh.Fuel, veh.gps.ToString(), veh.SunRoof.ToString(), veh.Colour, veh.DailyRate.ToString()});
            }
        }

        private void customerListCmb_Click(object sender, EventArgs e)
        {
            string customerList;
            customerListCmb.Items.Clear();
            foreach (Customers cus in crm.GetCustomer())
            {
                if (!vrm.IsRenting(cus.customerID))
                {
                    customerList = String.Format("{0} {1} {2} {3}", cus.Title, cus.FirstName, cus.LastName, cus.DOB);
                    customerListCmb.Items.Add(customerList);
                }
            }
        }

        private void titleTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void firstNameTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void lastTxtBox_Click(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void custDOBDatePicker_ValueChanged(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void genderCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableSaveBtn();
        }

        private void removeCusBtn_Click(object sender, EventArgs e)
        {
            string confirmCust = custGridView.SelectedRows[0].Cells[0].Value.ToString();
            bool cusRent = vrm.IsRenting(int.Parse(confirmCust));
            if (selectedCustomer == null)
            {
                MessageBox.Show("Please Choose a Customer you Wish to Delete");
            }
            else if (cusRent == true)
            {
                MessageBox.Show("This Vehicle is currently Rented, and therefore you cannot remove it.");
            }
            else
            {
                DialogResult delCusRes = MessageBox.Show("Are you sure you want to delete this Customer?", "Important Message", MessageBoxButtons.YesNo);
                if (delCusRes == DialogResult.Yes)
                {
                    crm.RemoveCustomer(selectedCustomer);
                    loadCustomersToGridView();
                }
                else
                {
                    ;
                }
            }
        }

        private void cusAddBtn_Click(object sender, EventArgs e)
        {
            int newCustomerID = getMaxCustomerID();

            Customers newCustomer = new Customers(newCustomerID, titleTxtBox.Text, firstNameTxtBox.Text, lastTxtBox.Text, genderCmbBox.Text, custDOBDatePicker.Text);
            crm.AddCustomer(newCustomer);

            loadCustomersToGridView();
            crm.SaveToFile();
            custPanel.Visible = false;
            titleTxtBox.Text = "";
            firstNameTxtBox.Text = "";
            lastTxtBox.Text = "";
            genderCmbBox.Text = "Male";
            custDOBDatePicker.Text = "01/01/1930";
        }

        public int getMaxCustomerID ()
        {
            var reader = new StreamReader(File.OpenRead(@"customer.csv"));
            var data = new List<List<string>>();
            int maxCusNum;


            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                data.Add(new List<String> { values[0] });
            }
            reader.Close();

            maxCusNum = data.Count() - 1;
            return maxCusNum;
        }

        private void rentBtn_Click(object sender, EventArgs e)
        {
            Rental newRental = new Rental(selectedVehicle.rego, customerListCmb.SelectedIndex - 1, int.Parse(rentUpDown.Value.ToString()));
            rrm.AddRental(newRental);
            rrm.SaveToFile();
            loadRentalsToGrid();
        }

        private void custGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void customerListCmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rentalSearchDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            int rowsCount = rentalSearchDataGrid.SelectedRows.Count;

            if (rowsCount == 0 || rowsCount >1)
            {
                selectedRentals = null;
            }
            else
            {
                string selectedId = rentalSearchDataGrid.SelectedRows[0].Cells[0].Value.ToString();
                selectedVehicle = vrm.GetVehicles(selectedId);
            }
        }

        private void custGridView_SelectionChanged(object sender, EventArgs e)
        {
            int rowsCount = custGridView.SelectedRows.Count;

            if (rowsCount == 0 || rowsCount > 1)
            {
                selectedCustomer = null;
            }
            else
            {
                string selectedId = custGridView.SelectedRows[0].Cells[0].Value.ToString();
                selectedCustomer = crm.GetCustomer(int.Parse(selectedId));
            }
        }

        private void returnVehicle_Click(object sender, EventArgs e)
        {
            if (selectedRentals == null)
            {
                MessageBox.Show("Please Choose a Vehicle You Want to Return");
            }
            else
            {
                DialogResult delCusRes = MessageBox.Show("Are you sure you want to return this vehicle?", "Important Message", MessageBoxButtons.YesNo);
                if (delCusRes == DialogResult.Yes)
                {
                    rrm.Removerental(selectedRentals);
                    loadRentalsToGrid();
                    rrm.SaveToFile();
                }
                else
                {
                    ;
                }

            }
        }

        private void rentalDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            int rowsCount = rentalDataGrid.SelectedRows.Count;

            if (rowsCount == 0 || rowsCount > 1)
            {
                selectedRentals = null;
            }
            else
            {
                string selectedId = rentalDataGrid.SelectedRows[0].Cells[1].Value.ToString();
                selectedRentals = rrm.GetRental(int.Parse(selectedId));
            }
        }
    }
}
