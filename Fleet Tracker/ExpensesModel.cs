using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet_Tracker
{
    class ExpensesModel
    {
        public int ID { get; set; }
        public string Motorcycle { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Type { get; set; }
        public string Vendor { get; set; }
        public string Notes { get; set; }

    }
}
