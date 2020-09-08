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
    /* GeometricShapeController: capture http data and send to GeometricShapeModel */
    [RoutePrefix("api")]
    [Route("geometric_shape")]
    public class GeometricShapeController : ApiController
    {
        private static List<GeometricShapeModel> Response = new List<GeometricShapeModel>(); /* Generic Response Struct */
        private static List<ErrorModel> Error = new List<ErrorModel>(); /* Generic Error Struct */

        [HttpPost]
        public dynamic insert_geometric_shape([FromBody] JObject data)
        {
            /* Clear Error Response Array */
            Error.Clear();

            /* Error counter for error return */
            int ErrorCount = 0;

            /* If is valid JSON */
            if (data != null)
            {
                /* @ Validation data @ */
                /* String fields cant be null or empty  */
                /* Integer fields cant be zero or out of limit */
                if (data["geometric_shape_directory_guid"] == null || data["geometric_shape_directory_guid"].ToString() == String.Empty)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Directory_Guid");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (data["geometric_shape_name"] == null || data["geometric_shape_name"].ToString() == String.Empty)
                {
                    ErrorModel error = new ErrorModel("Invalid_geometric_shape_same");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (data["geometric_shape_type"] == null || data["geometric_shape_type"].ToString() == String.Empty || int.Parse(data["geometric_shape_type"].ToString()) < 1 || int.Parse(data["geometric_shape_type"].ToString()) > 2)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Type");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (data["geometric_shape_color"] == null || data["geometric_shape_color"].ToString() == String.Empty)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Color");
                    Error.Add(error);
                    ErrorCount++;
                }
                if (data["geometric_shape_pixels"] == null || data["geometric_shape_pixels"].ToString() == String.Empty || int.Parse(data["geometric_shape_pixels"].ToString()) <= 0)
                {
                    ErrorModel error = new ErrorModel("Invalid_Geometric_Shape_Pixels");
                    Error.Add(error);
                    ErrorCount++;
                }
                /* If have any field error, return */
                if (ErrorCount > 0) return Error;

                /* @ If all data is valid, then start creation of new geometric shape @ */
                /* variable declaration */
                string Geometric_shape_directory_guid = data["geometric_shape_directory_guid"].ToString().ToUpper();
                string Geometric_shape_name = data["geometric_shape_name"].ToString();
                int Geometric_shape_type = int.Parse(data["geometric_shape_type"].ToString());
                string Geometric_shape_color = data["geometric_shape_color"].ToString();
                int Geometric_shape_pixels = int.Parse(data["geometric_shape_pixels"].ToString());

                /* Clear Response Array */
                Response.Clear();

                /* Validation of directory_guid */
                DirectoryModel directories = new DirectoryModel(Geometric_shape_directory_guid, null);
                int DirectoryExists = directories.DirectoryExists();
                if (DirectoryExists == 0)
                {
                    ErrorModel error = new ErrorModel("Nonexistent_Geometric_Shape_Directory_Guid");
                    Error.Add(error);
                    return Error;
                }
                /* If valid directory_guid, then create geometric_shape and return Response */
                GeometricShapeModel geometric_shape = new GeometricShapeModel(null, Geometric_shape_directory_guid, Geometric_shape_name, Geometric_shape_type, Geometric_shape_color, Geometric_shape_pixels);
                geometric_shape.CreateGeometricShape();
                Response.Add(geometric_shape);
                return Response;
            }
            else
            {
                /* If invalid JSON Request, exit with Invalid JSON Error message */
                ErrorModel error = new ErrorModel("Invalid_JSON_Request");
                Error.Add(error);
                return Error;
            }
        }
        /* List All GeometricShape HTTP GET */
        [HttpGet]
        public List<GeometricShapeModel> ListGeometricShape()
        {
            /* Clear Response Array */
            Response.Clear();

            /* Create new instance of GeometricShapeModel for data listing */
            GeometricShapeModel geometric_shapes = new GeometricShapeModel(null, null, null, 0, null, 0);
            Response = geometric_shapes.ListGeometricShapes(Response);
            return Response;
        }
    }
}
