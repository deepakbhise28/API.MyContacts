using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace API.MyContacts.Exceptions
{
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Response { get; }

        /// <summary>
        /// 
        /// </summary>
        public string ReasonPhase { get; set; }
    }

    /// <summary>
    /// Generic APIResonse with Data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessException<T> : BaseException
    {
        /// <summary>
        /// 
        /// </summary>
        public T Content { get; }

        /// <summary>
        /// Json Response
        /// </summary>
        public override string Response
        {
            get
            {
                return JsonConvert.SerializeObject(Content, Formatting.Indented);
            }
        }

        /// <summary>
        /// Reason Phase
        /// </summary>
        public override string Message
        {
            get
            {
                return ReasonPhase;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="result"></param>
        /// <param name="reasonPhase"></param>
        public BusinessException(HttpStatusCode statusCode, T content, string reasonPhase = null)
      : base()
        {
            StatusCode = statusCode;
            ReasonPhase = reasonPhase;
            Content = content;
        }

        /// <summary>
        /// 422; //for when the requested information is okay, but invalid.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="reasonPhase"></param>
        public BusinessException(T content, string reasonPhase = null)
        : base()
        {
            StatusCode = (HttpStatusCode)422; //for when the requested information is okay, but invalid.
            ReasonPhase = reasonPhase;
            Content = content;
        }
    }

}