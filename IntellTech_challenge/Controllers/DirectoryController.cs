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
    [Route("directory")]
    public class DirectoryController : ApiController
    {
        private static List<DirectoryModel> Response = new List<DirectoryModel>();
        private static List<ErrorModel> Error = new List<ErrorModel>();


        [HttpPost]
        public dynamic create_directory([FromBody] JObject data)
		{
            Error.Clear();
            if (data != null)
            {
                string directory_name = data["directory_name"].ToString();
                if (directory_name == null || directory_name == "")
                {
                    ErrorModel error = new ErrorModel("Invalid_Directory_Name");
                    Error.Add(error);
                    return Error;
                }
                Response.Clear();
                DirectoryModel directory = new DirectoryModel(null, directory_name);
                directory.CreateDirectory();
                Response.Add(directory);
                return Response;
            }
            else
			{
                ErrorModel error = new ErrorModel("Invalid_JSON_Request");
                Error.Add(error);
                return Error;
            }
        }
        // GET api/directory/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
        //[HttpGet("{directory_guid}")]
        [HttpGet]
        public List<DirectoryModel> ListDirectories()
        {
            Response.Clear();
            DirectoryModel directories = new DirectoryModel(null, null);
            Response = directories.ListDirectories(Response);
            return Response;
        }

    }
}
