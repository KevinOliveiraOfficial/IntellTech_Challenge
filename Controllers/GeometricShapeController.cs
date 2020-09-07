using IntellTech_challenge.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntellTech_challenge.Controllers
{
    [RoutePrefix("api")]
    [Route("geometric_shape")]
    public class GeometricShapeController : ApiController
    {
        private static List<GeometricShapeModel> Response = new List<GeometricShapeModel>();
        private static List<ErrorModel> Error = new List<ErrorModel>();

        [HttpPost]
        public dynamic insert_geometric_shape([FromBody] JObject data)
        {
            Error.Clear();
            int ErrorCount = 0;
            if (data != null)
            {
                string Geometric_shape_directory_guid = data["geometric_shape_directory_guid"].ToString().ToUpper();
                string Geometric_shape_name = data["geometric_shape_name"].ToString();
                int Geometric_shape_type = (data["geometric_shape_type"].ToString() == String.Empty) ? 0 : int.Parse(data["geometric_shape_type"].ToString());
                string Geometric_shape_color = data["geometric_shape_color"].ToString();
                int Geometric_shape_pixels = (data["geometric_shape_pixels"].ToString() == String.Empty) ? 0 : int.Parse(data["geometric_shape_pixels"].ToString());
                if (Geometric_shape_directory_guid == String.Empty)
                {
                    ErrorModel error = new ErrorModel("Invalid_Directory_Guid");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (Geometric_shape_name == String.Empty)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Name");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (Geometric_shape_type < 1 || Geometric_shape_type > 2)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Type");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (Geometric_shape_color == String.Empty)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Color");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (Geometric_shape_pixels <= 0)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Pixels");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (ErrorCount > 0) return Error;
                Response.Clear();
                DirectoryModel directories = new DirectoryModel(Geometric_shape_directory_guid, null);
                int DirectoryExists = directories.DirectoryExists();
                if (DirectoryExists == 0)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Directory_Guid");
                    Error.Add(error);
                    return Error;
                }
                GeometricShapeModel geometric_shape = new GeometricShapeModel(null, Geometric_shape_directory_guid, Geometric_shape_name, Geometric_shape_type, Geometric_shape_color, Geometric_shape_pixels);
                geometric_shape.CreateGeometricShape();
                Response.Add(geometric_shape);
                return Response;
            }
            else
            {
                ErrorModel error = new ErrorModel("Invalid_JSON_Request");
                Error.Add(error);
                return Error;
            }
        }
        // GET api/geometric_shape/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
        //[HttpGet("{directory_guid}")]
        [HttpGet]
        public List<GeometricShapeModel> ListDirectories()
        {
            Response.Clear();
            GeometricShapeModel geometric_shapes = new GeometricShapeModel(null, null, null, 0, null, 0);
            Response = geometric_shapes.ListGeometricShapes(Response);
            return Response;
        }
    }
}
