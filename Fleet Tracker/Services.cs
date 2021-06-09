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

namespace Fleet_Tracker
{
    public partial class Services : Form
    {
        List<MotorcyclesModel> bikes = new List<MotorcyclesModel>();
        List<ExpensesModel> exp = new List<ExpensesModel>();
        bool Done = false;
        DataTable ETable = new DataTable();
        public Services()
        {
            InitializeComponent();
            dtpbox.Format = DateTimePickerFormat.Short;

            if (ETable.Columns.Count == 0)
            {
                ETable.Columns.Add("ID");
                ETable.Columns.Add("Motorcycle");
                ETable.Columns.Add("Date");
                ETable.Columns.Add("Amount");
                ETable.Columns.Add("Type");
                ETable.Columns.Add("Vendor");
                ETable.Columns.Add("Notes");

            }

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
           bikes = SqliteDataAccess.LoadBike();
            motorcyclebox.DataSource = bikes;
            motorcyclebox.DisplayMember = "Plate";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExpensesModel em = new ExpensesModel();
            em.Motorcycle = motorcyclebox.Text.ToString();
            em.Type = expensebox.Text.ToString();
            em.Date = dtpbox.Text.ToString();
            em.Vendor = vendorbox.Text.ToString();
            em.Amount = amountbox.Text.ToString();
            em.Notes = notebox.Text.ToString();

            SqliteDataAccess.SaveExpense(em);

            MessageBox.Show("Add was Succesfull", "Message", MessageBoxButtons.OK);

            motorcyclebox.SelectedIndex = -1;
            expensebox.SelectedIndex = -1;
            dtpbox.Text = DateTime.Now.ToString("dd/MM/yyyy");
            vendorbox.Text = "";
            amountbox.Text = "";
            notebox.Text = "";

            DGVRefresh();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Done == true)
            {
                Done = false;
                dataGridView1.DataSource = ETable;
                dataGridView1.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns["Notes"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                timer.Stop();
            }
        }

        private void Services_Load(object sender, EventArgs e)
        {
            try
            {
                timer.Start();
                Thread Expenses = new(LoadExpenses);
                Expenses.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Error", MessageBoxButtons.OK);
            }
        }

        private void LoadExpenses()
        {
            exp = SqliteDataAccess.LoadExpenses();

            foreach(ExpensesModel str in exp)
            {
                DataRow row = ETable.NewRow();
                row["ID"] = str.ID;
                row["Motorcycle"] = str.Motorcycle;
                row["Date"] = str.Date;
                row["Amount"] = str.Amount;
                row["Type"] = str.Type;
                row["Vendor"] = str.Vendor;
                row["Notes"] = str.Notes;

                ETable.Rows.Add(row);     
            }

            Done = true;
        }

        private void DGVRefresh()
        {
            try
            {
                dataGridView1.DataSource = null;
                ETable.Clear();
                timer.Start();
                Thread Expenses = new(LoadExpenses);
                Expenses.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Error", MessageBoxButtons.OK);
            }
        }
    }
}
