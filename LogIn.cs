using System;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Dynamic;
using BankingApplication;

public class LogIn : BankAccount
{
	public string SQLUNQuery(string oloUN)
	{
		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT customer_oloUN FROM customerInfo " +
						$"WHERE customerInfo.customer_oloUN = '{oloUN}' COLLATE SQL_Latin1_General_CP1_CS_AS;";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						loginUNReturn = reader.GetString(0).ToString();
					}
				}
			}
		}
		return loginUNReturn;
	}

	public string SQLPASSQuery(string oloUN, string oloPASS)
	{
		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT customer_oloPASS FROM customerInfo " +
				$"WHERE customerInfo.customer_oloUN = '{oloUN}' COLLATE SQL_Latin1_General_CP1_CS_AS" +
				$" AND customerInfo.customer_oloPASS = '{oloPASS}' COLLATE SQL_Latin1_General_CP1_CS_AS;";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						loginPASSReturn = reader.GetString(0);
					}
				}
			}
		}
		return loginPASSReturn;
	}

	public string GetCustomerName()
    {
		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT customer_name FROM customerInfo " +
				$"WHERE customerInfo.customer_oloUN = '{loginUNReturn}' COLLATE SQL_Latin1_General_CP1_CS_AS" +
				$" AND customerInfo.customer_oloPASS = '{loginPASSReturn}' COLLATE SQL_Latin1_General_CP1_CS_AS;";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						loginNameReturn = reader.GetString(0);
						customerName = loginNameReturn;
					}
				}
			}
		}
		return customerName;
	}

	public int GetCustomerID()
	{
		using (SqlConnection connection = new SqlConnection(Program.SQLBuilder.ConnectionString))
		{
			String sql = $"SELECT customer_id FROM customerInfo " +
				$"WHERE customerInfo.customer_oloUN = '{loginUNReturn}' COLLATE SQL_Latin1_General_CP1_CS_AS" +
				$" AND customerInfo.customer_oloPASS = '{loginPASSReturn}' COLLATE SQL_Latin1_General_CP1_CS_AS;";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						loginIDReturn = reader.GetString(0);
						customerID = Convert.ToInt32(loginIDReturn);

					}
				}
			}
		}
		return customerID;
	}

}
