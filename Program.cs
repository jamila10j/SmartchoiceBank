using System;
using System.Data.SqlClient;
using System.Text;
using System.Dynamic;

namespace BankingApplication
{
    class Program
    {
        public BankAccount newBankAccount;


        //**using System.Dynamic**//
        public static dynamic SQLBuilder = new ExpandoObject();

        static void Main(string[] args)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "jamilbankappserver.database.windows.net";
            builder.UserID = "*********";
            builder.Password = "************";
            builder.InitialCatalog = "jamilBankApp-database";

            SQLBuilder = builder;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT name, collation_name FROM sys.databases";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                        }
                    }
                }
            }


            Program bankApp = new Program();
            bankApp.Start();
        }

        public void Start()
        {

            Console.WriteLine("Welcome!\n 1. Log In\n 2. Register for New Account");
            int startChoice = Convert.ToInt32(Console.ReadLine());

            if (startChoice == 1)
            {
                newBankAccount = new BankAccount();
                newBankAccount.LogIn();
            }
            else if (startChoice == 2)
            {
                newBankAccount = new BankAccount();
                newBankAccount.FirstTimeUser();
            }
              
        }
    }
}
