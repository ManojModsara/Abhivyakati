using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
namespace QuizGame.API.Models
{

    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            Code = HttpStatusCode.OK;
        }
        public bool Status { get; set; }      
        public string Message { get; set; }
        public int UserId { get; set; }
        public T Data { get; set; }
        public HttpStatusCode Code { get; set; }    
        public string[] Errors { get; set; }
    }
  
}