using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fleet_Tracker
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        int Count = 0;

        private void timer_Tick(object sender, EventArgs e)
        {
            

            if(Count == 2)
            {

                this.Close();
            }
            else
            {
                Count++;
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
