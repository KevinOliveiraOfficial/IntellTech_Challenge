using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellTech_challenge.Models
{
	public class DirectoryModel
	{
		private string Directory_guid;
		private string Directory_name;
		public DirectoryModel()
		{
		}
		public DirectoryModel(string Directory_guid, string Directory_name)
		{
			this.Directory_guid = Directory_guid;
			this.Directory_name = Directory_name;
		}
		public string directory_guid
		{
			get	{ return Directory_guid.ToUpper(); } set { Directory_guid = value.ToUpper(); }
		}
		public string directory_name
		{
			get { return Directory_name; } set { Directory_name = value; }
		}
		/* @ all Directory_guid are converted to uppercase @ */

		/* Insert New Directory in Database */
		public void CreateDirectory()
		{
			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
			/* local Database connection string */
			csb.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

			using (SqlConnection conn = new SqlConnection(csb.ConnectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand("INSERT INTO directory (directory_guid, directory_name) VALUES (@directory_guid, @directory_name)"))
				{
					Guid new_directory_guid = Guid.NewGuid();
					cmd.CommandType = CommandType.Text;
					cmd.Connection = conn;
					cmd.Parameters.AddWithValue("@directory_guid", new_directory_guid.ToString().ToUpper());
					cmd.Parameters.AddWithValue("@directory_name", directory_name);
					cmd.ExecuteNonQuery();
					directory_guid = new_directory_guid.ToString();
				}
			}
		}
		/* Get All Directories from Database */
		public List<DirectoryModel> ListDirectories(List<DirectoryModel> Response)
		{
			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
			/* local Database connection string */
			csb.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

			using (SqlConnection conn = new SqlConnection(csb.ConnectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM directory"))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Connection = conn;
					cmd.ExecuteNonQuery();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while(reader.Read())
						{
							/* Add Data to Response struct in function parameter */
							Response.Add(
								new DirectoryModel(reader["directory_guid"].ToString(), reader["directory_name"].ToString())
							);
						}
					}
				}
			}
			/* return Response struct */
			return Response;
		}
		/* Check Database for directory_guid existence */
		public int DirectoryExists()
		{
			/* Dabase result variable control */
			int Response = 0;
			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
			/* local Database connection string */
			csb.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

			using (SqlConnection conn = new SqlConnection(csb.ConnectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM directory WHERE directory_guid = @directory_guid"))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Connection = conn;
					cmd.Parameters.AddWithValue("@directory_guid", directory_guid);
					cmd.ExecuteNonQuery();

					/* Change result variable control*/
					Response = Convert.ToInt32(cmd.ExecuteScalar());
				}
			}
			/* return 0 if directory_guid not exists, 1 if exists */
			return Response;
		}
	}
}