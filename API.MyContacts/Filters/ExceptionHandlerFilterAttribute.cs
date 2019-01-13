using API.Instrumentation;
using API.MyContacts.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace API.MyContacts.Filters
{
    /// <summary>
    /// Custom filter generic class for handling all the exceptions
    /// </summary>
    /// <summary>
    /// Custom filter generic class for handling all the exceptions
    /// </summary>
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the CustomAPIErrorFilter class for initializing the mapping collection.
        /// </summary>
        public ExceptionHandlerFilterAttribute()
        {
        }

       

        /// <summary>
        /// Method to handle the exceptions
        /// </summary>
        /// <param name="actionExecutedContext">Context data after action is executed</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
           
            if (actionExecutedContext != null)
            {
                var exception = actionExecutedContext.Exception;
               
                while (exception.InnerException != null && !(exception is BaseException))
                {
                    exception = exception.InnerException;
                }

                if (exception is BaseException bEx)
                {
                    LogWriter.Instance.LogWarning($"Bussiness exception {bEx.ReasonPhase} {bEx.Response } ");
                    throw new HttpResponseException(new HttpResponseMessage(bEx.StatusCode)
                    {
                        Content = new StringContent(bEx.Response, System.Text.Encoding.UTF8, "application/json"),
                        ReasonPhrase = bEx.ReasonPhase
                    });
                }
                else
                {
                    LogWriter.Instance.LogError(exception);
                    var Error = new { error = "Technical error." };

                    var erroResult = JsonConvert.SerializeObject(Error, Formatting.Indented);

                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(erroResult, System.Text.Encoding.UTF8, "application/json")
                    });
                }
            }
        }
    }
}