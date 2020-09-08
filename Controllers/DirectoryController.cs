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
    /* DirectoryController: capture http data and send to DirectoryModel */
    [RoutePrefix("api")]
    [Route("directory")]
    public class DirectoryController : ApiController
    {
        private static List<DirectoryModel> Response = new List<DirectoryModel>(); /* Generic Response Struct */
        private static List<ErrorModel> Error = new List<ErrorModel>(); /* Generic Error Struct */

        /* Create Directory HTTP POST */
        [HttpPost]
        public dynamic create_directory([FromBody] JObject data)
		{
            /* Clear Error Response Array */
            Error.Clear();

            /* If is valid JSON */
            if (data != null)
            {
                /* @ Validation data @ */
                /* directory_name field cant be null or empty  */
                if (data["directory_name"] == null || data["directory_name"].ToString() == String.Empty)
                {
                    /* Exit with Invalid Directory Error message */
                    ErrorModel error = new ErrorModel("Invalid_directory_name");
                    Error.Add(error);
                    return Error;
                }
                /* If data is valid, then create directory */
                string directory_name = data["directory_name"].ToString();

                /* Clear Response Array */
                Response.Clear();

                /* Create new instance of DirectoryModel for data insertion, then return Response */
                DirectoryModel directory = new DirectoryModel(null, directory_name);
                directory.CreateDirectory();
                Response.Add(directory);
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
        /* List All Directories HTTP GET */
        [HttpGet]
        public List<DirectoryModel> ListDirectories()
        {
            /* Clear Response Array */
            Response.Clear();

            /* Create new instance of DirectoryModel for data listing */
            DirectoryModel directories = new DirectoryModel(null, null);
            Response = directories.ListDirectories(Response);
            return Response;
        }

    }
}
