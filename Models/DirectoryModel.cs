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
		public void CreateDirectory()
		{
			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
			csb.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KevinOliveira\source\repos\IntellTech_challenge\App_Data\Database1.mdf;Integrated Security=True";

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
					directory_guid = new_directory_guid.ToString().ToUpper();
				}
			}
		}
		public List<DirectoryModel> ListDirectories(List<DirectoryModel> Response)
		{
			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
			csb.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KevinOliveira\source\repos\IntellTech_challenge\App_Data\Database1.mdf;Integrated Security=True";

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
							Response.Add(
								new DirectoryModel(reader["directory_guid"].ToString(), reader["directory_name"].ToString())
							);
						}
					}
				}
			}
			return Response;
		}
		public int DirectoryExists()
		{
			int Response = 0;
			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
			csb.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KevinOliveira\source\repos\IntellTech_challenge\App_Data\Database1.mdf;Integrated Security=True";

			using (SqlConnection conn = new SqlConnection(csb.ConnectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM directory WHERE directory_guid = @directory_guid"))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Connection = conn;
					cmd.Parameters.AddWithValue("@directory_guid", directory_guid);
					cmd.ExecuteNonQuery();
					Response = Convert.ToInt32(cmd.ExecuteScalar());
				}
			}
			return Response;
		}
	}
}