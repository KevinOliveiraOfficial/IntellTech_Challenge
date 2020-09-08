using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntellTech_challenge.Models
{
	public class ErrorModel
	{
        public string Error { get; set; }
        public ErrorModel()
        {
        }

        public ErrorModel(string Error)
        {
            this.Error = Error;
        }
    }
}