using System;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Dynamic;
using BankingApplication;
using System.Collections.Generic;
using System.Linq;

public class AccountInfo : BankAccount
{
	
	Random rng = new Random();

	public string[] customerAccountNums;
	public string[] customerAccounts;

	public int genAccNum;
	public int genID;
    public List<string> customerAccountNumList = new List<string>();
	public List<string> customerAccountList = new List<string>();

	public int GenerateAccountNum()
    {
		return rng.Next(1012131010, 1989801979);
	}

	public int GenerateCustomerID()
    {
        return rng.Next(10100, 98980);
	}

	public string[] SQLANPull(int customerID)
    {
		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT account_number FROM accounts WHERE customer_id = '{customerID}';";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						try
						{
							for (int i = 0; i <= 0; i++)
							{
								customerAccountNumList.Add(reader.GetString(i));

								//Console.WriteLine(reader.GetString(0));
								//Array.ForEach(customerAccounts, Console.WriteLine);
								//reader.GetString(i);


							}
						}
						catch (IndexOutOfRangeException)
						{
							break;
						}
						
					}
				}
			}
		}

		return customerAccountNums = customerAccountNumList.ToArray();
	}

	public string[] SQLATPull(string[] customerAccountNums)
	{
		for (int i = 0; i <= customerAccountNums.Length; i++)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
				{
					String sql = $"SELECT account_type FROM accounts WHERE account_number = '{customerAccountNums[i]}';";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								try
								{
									for (int a = 0; a <= 0; a++)
									{
										customerAccountList.Add(reader.GetString(a));

									}

								}
								catch (IndexOutOfRangeException)
								{
									break;
								}
							}
						}
					}
				}
			}
			catch (IndexOutOfRangeException)
			{
				break;
			}
		}

		return customerAccounts = customerAccountList.ToArray();

	}

	public void AccountViewer(int accountNumber, int customerID, string customerName, string accountType)
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

		Console.WriteLine($"{accountType} {accountNumber}");
		Console.WriteLine($"\nBalance: {accountBalance}\n\nWhat Would You Like to Do?");
		Console.WriteLine("1. Deposit	2. Withdrawal	3. View Transaction History   4. Go Back To Main Menu");

		int accountAction = Convert.ToInt32(Console.ReadLine());

		if (accountAction == 1)
        {
			Deposit(accountNumber, customerName, customerID);
			AccountViewer(accountNumber, customerID, customerName, accountType);
        }
		else if (accountAction == 2)
        {
			Withdrawal(accountNumber, customerName, customerID);
			AccountViewer(accountNumber, customerID, customerName, accountType);
		}
		else if (accountAction == 3)
        {
			Console.WriteLine("This Section is Under Maintenance. Returning to Account Viewer");
			AccountViewer(accountNumber, customerID, customerName, accountType);
		}
		else if (accountAction == 4)
        {
			Menu(customerName, customerID);
        }
    }

	public void AccountSelectLauncher(int customerID, string customerName)
    {

		SQLANPull(customerID);
		SQLATPull(customerAccountNums);
		List<string> accountViewerList = new List<string>();

		for (int i = 0; i <= customerAccounts.Length; i++)
		{
			try
			{
				accountViewerList.Add($"{i + 1}. {customerAccounts[i]} {customerAccountNums[i]}");
				//Console.WriteLine($"{i + 1}. " + customerAccounts[i] + ' ' + customerAccountNums[i]);
				
			}
			catch (IndexOutOfRangeException)
			{
				break;
			}
			Console.WriteLine(accountViewerList[i]);

		}

		int accountSelection = Convert.ToInt32(Console.ReadLine()) - 1;

		foreach (string account in accountViewerList)
		{
			if (accountSelection == accountViewerList.IndexOf(account))
			{
				accountNumber = Convert.ToInt32(customerAccountNums[accountSelection]);
				accountType = customerAccounts[accountSelection];
				AccountViewer(accountNumber, customerID, customerName, accountType);
			}
		}


	}

	public int AccountSelector(int customerID)
	{

		SQLANPull(customerID);
		SQLATPull(customerAccountNums);
		List<string> accountViewerList = new List<string>();

		for (int i = 0; i <= customerAccounts.Length; i++)
		{
			try
			{
				accountViewerList.Add($"{i + 1}. {customerAccounts[i]} {customerAccountNums[i]}");
				//Console.WriteLine($"{i + 1}. " + customerAccounts[i] + ' ' + customerAccountNums[i]);

			}
			catch (IndexOutOfRangeException)
			{
				break;
			}
			Console.WriteLine(accountViewerList[i]);

		}

		int accountSelection = Convert.ToInt32(Console.ReadLine()) - 1;

		foreach (string account in accountViewerList)
		{
			if (accountSelection == accountViewerList.IndexOf(account))
			{
				accountNumber = Convert.ToInt32(customerAccountNums[accountSelection]);
				
				
			}
		}
		return accountNumber;

	}


}
