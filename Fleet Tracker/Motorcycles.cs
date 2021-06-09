using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Fleet_Tracker
{
    public partial class Motorcycles : Form
    {

        List<MotorcyclesModel> bikes = new List<MotorcyclesModel>();
        DataTable BTable = new DataTable();
        bool Done = false;
        Bitmap image = null;
        string Imagestring = null;
        MemoryStream ms = new MemoryStream();

        public Motorcycles()
        {
            InitializeComponent();

            if(BTable.Columns.Count == 0)
            {
                BTable.Columns.Add("ID");
                BTable.Columns.Add("Name");
                BTable.Columns.Add("Make");
                BTable.Columns.Add("Model");
                BTable.Columns.Add("Color");
                BTable.Columns.Add("Plate");
                BTable.Columns.Add("Chassis");
                BTable.Columns.Add("Driver");
                BTable.Columns.Add("Picture");
                BTable.Columns.Add("Notes");
            }
        }

        private void Motorcycles_Load(object sender, EventArgs e)
        {
            try
            {
                timer.Start();
                Thread Bikes = new(LoadMotorcycles);
                Bikes.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Error", MessageBoxButtons.OK);
            }


            
        }

        private void LoadMotorcycles()
        {
            bikes = SqliteDataAccess.LoadMotorCycle();

            foreach(MotorcyclesModel str in bikes)
            {
                DataRow row = BTable.NewRow();
                row["ID"] = str.ID;
                row["Name"] = str.Name;
                row["Make"] = str.Make;
                row["Model"] = str.Model;
                row["Color"] = str.Color;
                row["Plate"] = str.Plate;
                row["Chassis"] = str.Chassis;
                row["Driver"] = str.Driver;
                row["Picture"] = str.Picture;
                row["Notes"] = str.Notes;

                BTable.Rows.Add(row);     
            }

            Done = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                button1.Text = "Update";
            }
            if(checkBox1.Checked == false)
            {
                button1.Text = "Add";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                MotorcyclesModel mc = new MotorcyclesModel();
                mc.ID = Convert.ToInt16(IDLabel.Text);
                mc.Name = namebox.Text;
                mc.Model = modelbox.Text;
                mc.Plate = platebox.Text;
                mc.Make = makebox.Text;
                mc.Color = colorbox.Text.ToString();
                mc.Chassis = chassisbox.Text;
                mc.Driver = driverbox.Text;
                mc.Notes = notebox.Text;
                mc.Picture = ms;
                SqliteDataAccess.UpdateMotorcycle(mc);

                namebox.Text = "";
                modelbox.Text = "";
                platebox.Text = "";
                makebox.Text = "";
                colorbox.SelectedIndex = -1;
                chassisbox.Text = "";
                notebox.Text = "";
                driverbox.Text = "";
                picturebox.Image = null;
                IDLabel.Text = "";

                MessageBox.Show("Update was Succesfull", "Message", MessageBoxButtons.OK);

                DGVRefresh();
            }
            if (checkBox1.Checked == false)
            {
                MotorcyclesModel mc = new MotorcyclesModel();
                mc.Name = namebox.Text;
                mc.Model = modelbox.Text;
                mc.Plate = platebox.Text;
                mc.Make = makebox.Text;
                mc.Color = colorbox.Text.ToString();
                mc.Chassis = chassisbox.Text;
                mc.Driver = driverbox.Text;
                mc.Notes = notebox.Text;
                mc.Picture = ms;
                SqliteDataAccess.SaveMotorcyle(mc);

                namebox.Text = "";
                modelbox.Text = "";
                platebox.Text = "";
                makebox.Text = "";
                colorbox.SelectedIndex = -1;
                chassisbox.Text = "";
                notebox.Text = "";
                driverbox.Text = "";
                picturebox.Image = null;

                MessageBox.Show("Add was Succesfull", "Message", MessageBoxButtons.OK);

                DGVRefresh();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(Done == true)
            {
                Done = false;
                dataGridView1.DataSource = BTable;
                dataGridView1.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns["Notes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                timer.Stop();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Photo";
            dlg.Filter = "Image File (*.jpg; *.jpeg; *.png;) | *.jpg; *.jpeg; *.png;";

            if(dlg.ShowDialog() == DialogResult.OK)
            {

                image = new Bitmap(dlg.FileName);
                picturebox.Image = image;
            }

            
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.ToArray();

            Imagestring = ms.ToString();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            picturebox.CancelAsync();
            picturebox.Image.Dispose();
            picturebox.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                BTable.Clear();
                timer.Start();
                Thread Bikes = new(LoadMotorcycles);
                Bikes.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Error", MessageBoxButtons.OK);
            }
        }

        private void DGVRefresh()
        {
            try
            {
                dataGridView1.DataSource = null;
                BTable.Clear();
                timer.Start();
                Thread Bikes = new(LoadMotorcycles);
                Bikes.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Error", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string ID = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                string Name = dataGridView1.SelectedRows[0].Cells[1].Value + string.Empty;
                string CompInp = dataGridView1.SelectedRows[0].Cells[2].Value + string.Empty;
                string PONUM = dataGridView1.SelectedRows[0].Cells[3].Value + string.Empty;
                string ProjCode = dataGridView1.SelectedRows[0].Cells[4].Value + string.Empty;
                string Region = dataGridView1.SelectedRows[0].Cells[5].Value + string.Empty;
                string Site = dataGridView1.SelectedRows[0].Cells[6].Value + string.Empty;
                string POCEX = dataGridView1.SelectedRows[0].Cells[7].Value + string.Empty;
                string POCIN = dataGridView1.SelectedRows[0].Cells[8].Value + string.Empty;
                string DateRec = dataGridView1.SelectedRows[0].Cells[9].Value + string.Empty;

                IDLabel.Text = ID;
                namebox.Text = Name;
                modelbox.Text = CompInp;
                platebox.Text = PONUM;
                makebox.Text = ProjCode;
                colorbox.Text = Region;
                chassisbox.Text = Site;
                driverbox.Text = POCEX;
                notebox.Text = POCIN;
                picturebox.Text = DateRec;


            }
        }
    }
}
