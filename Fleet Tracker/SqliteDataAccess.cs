using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet_Tracker
{
    class SqliteDataAccess
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        public static List<MotorcyclesModel> LoadMotorCycle()
        {

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var ouput = cnn.Query<MotorcyclesModel>("Select * from Motorcycles", new DynamicParameters());
                    return ouput.ToList();
                }
            

        }

        public static List<MotorcyclesModel> LoadBike()
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var ouput = cnn.Query<MotorcyclesModel>("Select Plate from Motorcycles", new DynamicParameters());
                return ouput.ToList();
            }


        }

        public static void SaveMotorcyle(MotorcyclesModel motorcycle)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Motorcycles(Name, Make, Model, Color, Plate, Chassis, Driver, Picture) values (@Name, @Make, @Model, @Color, @Plate, @Chassis, @Driver, @Picture)", motorcycle);
            }
        }


        public static void UpdateMotorcycle(MotorcyclesModel motorcycle)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("UPDATE Motorcycles SET Name = @Name, Make = @Make, Model = @Model, Color = @Color, Plate = @Plate, Chassis = @Chassis, Driver = @Driver, Picture = @Picture WHERE ID = @ID", motorcycle);
            }
        }

        public static List<ExpensesModel> LoadExpenses()
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var ouput = cnn.Query<ExpensesModel>("Select * from Expenses", new DynamicParameters());
                return ouput.ToList();
            }


        }

        public static void SaveExpense(ExpensesModel expense)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Expenses(Motorcycle, Date, Amount, Type, Vendor, Notes) values (@Motorcycle, @Date, @Amount, @Type, @Vendor, @Notes)", expense);
            }
        }
    }
}
