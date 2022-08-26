using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Error
{
    public class ApiExeption
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public ApiExeption(int statusCode, string message=null, string details=null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
