using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellTech_challenge.Models
{
	public class GeometricShapeModel
	{
		private string Geometric_shape_guid;
		private string Geometric_shape_directory_guid;
		private string Geometric_shape_name;
		private dynamic Geometric_shape_type;
		private string Geometric_shape_color;
		private int Geometric_shape_pixels;
		private enum Geometric_shape_types
		{
			Quadrado = 1,
			Triangulo = 2
		}
		public GeometricShapeModel()
		{
		}
		public GeometricShapeModel(string Geometric_shape_guid, string Geometric_shape_directory_guid,
		string Geometric_shape_name, dynamic Geometric_shape_type, string Geometric_shape_color, int Geometric_shape_pixels)
		{
			this.Geometric_shape_directory_guid = Geometric_shape_directory_guid;
			this.Geometric_shape_guid = Geometric_shape_guid;
			this.Geometric_shape_name = Geometric_shape_name;
			this.Geometric_shape_type = Geometric_shape_type;
			this.Geometric_shape_color = Geometric_shape_color;
			this.Geometric_shape_pixels = Geometric_shape_pixels;
		}
		public string geometric_shape_guid
		{
			get { return Geometric_shape_guid.ToUpper(); }
			set { Geometric_shape_guid = value.ToUpper(); }
		}
		public string geometric_shape_directory_guid
		{
			get { return Geometric_shape_directory_guid.ToUpper(); }
			set { Geometric_shape_directory_guid = value.ToUpper(); }
		}
		public string geometric_shape_name
		{
			get { return Geometric_shape_name; }
			set { Geometric_shape_name = value; }
		}
		public dynamic geometric_shape_type
		{
			get { return Geometric_shape_type; }
			set { Geometric_shape_type = value; }
		}
		public string geometric_shape_color
		{
			get { return Geometric_shape_color; }
			set { Geometric_shape_color = value; }
		}
		public int geometric_shape_pixels
		{
			get { return Geometric_shape_pixels; }
			set { Geometric_shape_pixels = value; }
		}
		/* @ all Geometric_shape_directory_guid && Geometric_shape_guid are converted to uppercase @ */

		/* Insert New GeometricShape in Database */
		public void CreateGeometricShape()
		{
			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
			/* local Database connection string */
			csb.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

			using (SqlConnection conn = new SqlConnection(csb.ConnectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand("INSERT INTO geometric_shapes (geometric_shape_guid, geometric_shape_directory_guid, geometric_shape_name, geometric_shape_type, geometric_shape_color, geometric_shape_pixels) VALUES (@geometric_shape_guid, @geometric_shape_directory_guid, @geometric_shape_name, @geometric_shape_type, @geometric_shape_color, @geometric_shape_pixels)"))
				{
					Guid new_geometric_shape_guid = Guid.NewGuid();
					geometric_shape_type = (Geometric_shape_types)geometric_shape_type;
					cmd.CommandType = CommandType.Text;
					cmd.Connection = conn;
					cmd.Parameters.AddWithValue("@geometric_shape_guid", new_geometric_shape_guid.ToString().ToUpper());
					cmd.Parameters.AddWithValue("@geometric_shape_directory_guid", geometric_shape_directory_guid);
					cmd.Parameters.AddWithValue("@geometric_shape_name", geometric_shape_name);
					cmd.Parameters.AddWithValue("@geometric_shape_type", geometric_shape_type.ToString());
					cmd.Parameters.AddWithValue("@geometric_shape_color", geometric_shape_color);
					cmd.Parameters.AddWithValue("@geometric_shape_pixels", geometric_shape_pixels);
					cmd.ExecuteNonQuery();
					Geometric_shape_guid = new_geometric_shape_guid.ToString();
					geometric_shape_type = geometric_shape_type.ToString();
				}
			}
		}
		/* Get All GeometricShapes from Database */
		public List<GeometricShapeModel> ListGeometricShapes(List<GeometricShapeModel> Response)
		{
			SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
			/* local Database connection string */
			csb.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

			using (SqlConnection conn = new SqlConnection(csb.ConnectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM geometric_shapes"))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Connection = conn;
					cmd.ExecuteNonQuery();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							/* Add Data to Response struct in function parameter */
							Response.Add(
								new GeometricShapeModel(reader["geometric_shape_guid"].ToString(), reader["geometric_shape_directory_guid"].ToString(), reader["geometric_shape_name"].ToString(), reader["geometric_shape_type"].ToString(), reader["geometric_shape_color"].ToString(), int.Parse(reader["geometric_shape_pixels"].ToString()))
							);
						}
					}
				}
			}
			/* return Response struct */
			return Response;
		}
	}
}