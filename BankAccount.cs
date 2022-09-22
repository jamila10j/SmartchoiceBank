using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using BankingApplication;
using System.Dynamic;

public class BankAccount
{
	public string customerName;
	string[] accounts = new string[] { "Checking", "Savings", "Investment" };
	//public double accountBalance;
    public string accountType;
	public double openingBalance;
	public int accountNumber;
	public LogIn bankLogIn;
	

	public string customerAddress;
	public string oloUN;
	public string oloPASS;
	public int customerID;

	public string loginUNReturn;
	public string loginPASSReturn;
	public string loginNameReturn;
	public string loginIDReturn;
	public string ADDReturn;
	public string oloUNReturn;
	public string oloPASSReturn;
	public string CIDReturn;
	public string ACReturn;
	public string accountListItem;



	public BankAccount()
	{
	}


	public void LogIn()
    {
		LogIn bankLogIn = new LogIn();
		Console.WriteLine("Please Enter Your Username");
		oloUN = Console.ReadLine();

		while (string.IsNullOrEmpty(oloUN) == true)
		{
			Console.WriteLine("Please Enter a Valid Username");
			oloUN = Console.ReadLine();
		}

		loginUNReturn = bankLogIn.SQLUNQuery(oloUN);

			while (loginUNReturn == null)
			{
				Console.WriteLine("Username not in File, Please Try Again");
				oloUN = Console.ReadLine();

				while (string.IsNullOrEmpty(oloUN) == true)
				{
					Console.WriteLine("Please Enter a Valid Username");
					oloUN = Console.ReadLine();
				}

				loginUNReturn = bankLogIn.SQLUNQuery(oloUN);
			}

		if (loginUNReturn != null && loginUNReturn.Equals(oloUN))
		{
			Console.WriteLine("Please Enter Your Password");
			oloPASS = Console.ReadLine();

			while (string.IsNullOrEmpty(oloPASS) == true)
			{
				Console.WriteLine("Please Enter a Valid Password");
				oloPASS = Console.ReadLine();
			}

			loginPASSReturn = bankLogIn.SQLPASSQuery(oloUN, oloPASS);

			while (loginPASSReturn == null)
			{
				Console.WriteLine("Password is not Associated With this Account. Please Try Again");
				oloPASS = Console.ReadLine();

				loginPASSReturn = bankLogIn.SQLPASSQuery(oloUN, oloPASS);
			}

			if (loginPASSReturn != null && loginPASSReturn.Equals(oloPASS))
            {
				customerName = bankLogIn.GetCustomerName();
				customerID = bankLogIn.GetCustomerID();
				Menu(customerName, customerID);
			}

		}

	}

	public void Menu(string customerName, int customerID)
    {
		Console.WriteLine($"Welcome {customerName}! Make a choice to get started");
		Console.WriteLine("\n1. Open New Account \n2. View Accounts \n3. Quick Deposit \n4. Quick Withdrawal");
		int menuSelection = Convert.ToInt32(Console.ReadLine());

		if (menuSelection == 1)
        {
			OpenNewAccount(customerName);
        }
		else if (menuSelection == 2)
        {
			GetAccounts(customerID, customerName);
        }
		else if (menuSelection == 3)
        {
			AccountInfo accountOBJ = new AccountInfo();
			Console.WriteLine("Which account Would You Like to Deposit to?");
			accountNumber = accountOBJ.AccountSelector(customerID);
			Console.WriteLine(accountOBJ.Deposit(accountNumber, customerName, customerID));
			Menu(customerName, customerID);
        }
		else if (menuSelection == 4)
        {
			AccountInfo accountOBJ = new AccountInfo();
			Console.WriteLine("Which account Would You Like to Withdrawal From?");
			accountNumber = accountOBJ.AccountSelector(customerID);
			Console.WriteLine(accountOBJ.Withdrawal(accountNumber, customerName, customerID));
			Menu(customerName, customerID);
		}

	}

	public void FirstTimeUser()
	{

		Console.WriteLine("Glad to have you here! Enter your first \n and last name to get started");
		string customerName = Console.ReadLine();
		
		while (string.IsNullOrEmpty(customerName) == true)
        {
			Console.WriteLine("Please Enter a Valid Name");
			customerName = Console.ReadLine();
		}


		Console.WriteLine("Please Enter Your Home Address");
		string customerAddress = Console.ReadLine();

		while (string.IsNullOrEmpty(customerAddress) == true)
		{
			Console.WriteLine("Please Enter a Valid Address");
			customerAddress = Console.ReadLine();
		}

		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT customer_address FROM customerInfo " +
						$"WHERE EXISTS (SELECT customer_address FROM customerInfo WHERE customerInfo.customer_address LIKE " +
						$"'{customerAddress}');";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine("{0}", reader.GetString(0));
						ADDReturn = reader.GetString(0);
					}
				}
			}
		}

		if (ADDReturn != null)
		{
			while (ADDReturn.Equals(customerAddress))
			{
				Console.WriteLine("Address is Already Registered With an Account. Validate Your Entry, or To Get Help, Call Us.");
				customerAddress = Console.ReadLine();
			}
		}



		//USERNAME CHECK
		Console.WriteLine($"Lets create a new username {customerName}! Enter one below");


		string oloUN = Console.ReadLine();
		while (string.IsNullOrEmpty(oloUN) == true)
		{
			Console.WriteLine("Please Enter a Valid Username");
			oloUN = Console.ReadLine();
		}



		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT customer_oloUN FROM customerInfo " +
						$"WHERE EXISTS (SELECT customer_oloUN FROM customerInfo WHERE customerInfo.customer_oloUN LIKE " +
						$"'{oloUN}');";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine("{0}", reader.GetString(0));
						oloUNReturn = reader.GetString(0);
					}
				}
			}
		}


		if (oloUNReturn != null)
		{
			while (oloUNReturn.Equals(oloUN))
			{
				Console.WriteLine("Username Taken. Please enter a new username");
				oloUN = Console.ReadLine();
			}
		}
		

		//PASSWORD CHECK

		Console.WriteLine("Enter a New Password");
		string oloPASS = Console.ReadLine();
		while (string.IsNullOrEmpty(oloPASS) == true)
		{
			Console.WriteLine("Please Enter a Valid Password");
			oloPASS = Console.ReadLine();
		}

		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT customer_oloPASS FROM customerInfo " +
						$"WHERE EXISTS (SELECT customer_oloPASS FROM customerInfo WHERE customerInfo.customer_oloPASS LIKE " +
						$"'{oloPASS}');";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine("{0}", reader.GetString(0));
						oloPASSReturn = reader.GetString(0);
					}
				}
			}
		}


		if (oloPASSReturn != null)
		{
			while (oloPASSReturn.Equals(oloPASS))
			{
				Console.WriteLine("Password Taken. Please enter a new username");
				oloPASS = Console.ReadLine();
			}
		}

		//SQL customerInfo INSERT
		AccountInfo bankAccountcID = new AccountInfo();
		customerID = bankAccountcID.GenerateCustomerID();
		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT customer_ID FROM customerInfo " +
						$"WHERE EXISTS (SELECT customer_ID FROM customerInfo WHERE customerInfo.customer_ID = " +
						$"{customerID});";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine("{0}", reader.GetString(0));
						CIDReturn = reader.GetString(0);
					}
				}
			}
		}

		if (CIDReturn != null)
		{
			while (Convert.ToInt32(CIDReturn) == customerID)
			{
				customerID = bankAccountcID.GenerateCustomerID();
			}
		}

		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"INSERT INTO customerInfo (customer_name, customer_id, customer_address, " +
							$"customer_oloUN, customer_oloPASS) VALUES ('{customerName}', {customerID}, '{customerAddress}'," +
							$" '{oloUN}', '{oloPASS}');";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
					}
				}
			}
		}

		OpenNewAccount(customerName);

	}

	public void GetAccounts(int customerID, string customerName)
    {
		AccountInfo accountOBJ = new AccountInfo();
		Console.WriteLine("Which Account Would You Like to View?");

		accountOBJ.AccountSelectLauncher(customerID, customerName);


		Menu(customerName, customerID);
	
	}

	public void OpenNewAccount(string customerName)
	{

		Console.WriteLine($"Which type of account are you looking to open {customerName}?");
		Console.WriteLine("\n1. Checking \n2. Savings \n3. Investment");
		int accountChoice = Convert.ToInt32(Console.ReadLine());
		if (accountChoice == 1)
		{
			accountType = accounts[0];
			Checking();

		}
		else if (accountChoice == 2)
		{
			accountType = accounts[1];
			Savings();
		}
		else if (accountChoice == 3)
		{
			accountType = accounts[2];
			Investment();
		}
	}

	public void Checking()
    {
		Console.WriteLine("How much are you looking to add to your new checking account?");
		openingBalance = Convert.ToDouble(Console.ReadLine());

		SuccessCreate(customerID, accountType, openingBalance); //double check
	}

	public void Savings()
	{
		Console.WriteLine("How much are you looking to add to your new savings account?");
		openingBalance = Convert.ToDouble(Console.ReadLine());

		SuccessCreate(customerID, accountType, openingBalance);
	}

	public void Investment()
	{
		Console.WriteLine("Under Maintenance. Please Try Again Later");
		//openingBalance = Convert.ToDouble(Console.ReadLine());

		//SuccessCreate(customerID, accountType, openingBalance);
		OpenNewAccount(customerName);
	}

	public void SuccessCreate(int customerID, string accountType, double openingBalance)
    {
		AccountInfo bankaccountcAN = new AccountInfo();
		int accountNumber = bankaccountcAN.GenerateAccountNum();

		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT account_number FROM accounts " +
						$"WHERE EXISTS (SELECT account_number FROM accounts WHERE accounts.account_number = " +
						$"{accountNumber});";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine("{0}", reader.GetString(0));
						ACReturn = reader.GetString(0);
					}
				}
			}
		}

		if (ACReturn != null)
		{
			while (Convert.ToInt32(ACReturn) == accountNumber)
			{
				accountNumber = bankaccountcAN.GenerateCustomerID();
			}
		}

		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"INSERT INTO accounts VALUES ({customerID}, '{accountType}', {accountNumber}, {openingBalance});";

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

		
		
		Console.WriteLine("Congratulations on your new Account with the Bank!");
		Menu(customerName, customerID);
		
	}

	public string Deposit(int accountNumber, string customerName, int customerID)
    {
		double accountBalance;
		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT account_balance FROM accounts WHERE account_number = {accountNumber};";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string var = Convert.ToString(reader.GetValue(0));
						accountBalance = Convert.ToDouble(var);

					}
				}
			}
		}
		Console.WriteLine("How Much Would You Like to Deposit?");
		double depositAmount = Convert.ToDouble(Console.ReadLine());
        accountBalance += depositAmount;

		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"UPDATE accounts SET account_balance = '{accountBalance}' WHERE account_number = {accountNumber};";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
					}
				}
			}
		}

		string confirmation = $"Your Deposit of {depositAmount} was Confirmed!\nAccount {accountNumber} Now Has a Balance of {accountBalance}";
		return confirmation;
    }

	public string Withdrawal(int accountNumber, string customerName, int customerID)
    {
		double accountBalance;
		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT account_balance FROM accounts WHERE account_number = {accountNumber};";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string var = Convert.ToString(reader.GetValue(0));
						accountBalance = Convert.ToDouble(var);
					}
				}
			}
		}

		Console.WriteLine("How Much Would You Like to Withdrawal?");
		double withdrawalAmount = Convert.ToDouble(Console.ReadLine());
		accountBalance -= withdrawalAmount;

		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"UPDATE accounts SET account_balance = '{accountBalance}' WHERE account_number = {accountNumber};";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
					}
				}
			}
		}
		string confirmation = $"Your Withdrawal of {withdrawalAmount} was Confirmed!\nAccount {accountNumber} Now Has a Balance of {accountBalance}";
		return confirmation;
	}

}
