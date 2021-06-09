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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Motorcycles newMDIChild = new Motorcycles();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Services newMDIChild = new Services();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Fuel newMDIChild = new Fuel();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Alarms newMDIChild = new Alarms();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Milage newMDIChild = new Milage();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Documents newMDIChild = new Documents();
            newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            
        }



    }
}
