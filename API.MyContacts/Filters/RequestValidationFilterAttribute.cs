using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.MyContacts.Filters
{
    /// <summary>
    /// Class for validating the request objects
    /// </summary>
    public class RequestValidationFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public RequestValidationFilterAttribute()
        { }


        /// <summary>
        /// Method that validates the data models before API action executed
        /// </summary>
        /// <param name="actionContext">Context data while action being executed</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Validate model
            if (!actionContext.ModelState.IsValid)
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(JsonConvert.SerializeObject(actionContext.ModelState, Newtonsoft.Json.Formatting.Indented), System.Text.Encoding.UTF8, "application/json")
                };

                actionContext.Response = response;
            }
        }

    }
}