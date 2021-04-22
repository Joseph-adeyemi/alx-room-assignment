using System;
using System.Collections.Generic;
using System.Text;

namespace Promo.Core.Models.APIResponses
{
    public class GenericAPIResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string Token { get; set; }

        public GenericAPIResponse()
        { }

        public GenericAPIResponse(string responseCode, string responseDescription, string token)
        {
            ResponseCode = responseCode;
            ResponseDescription = responseDescription;
            Token = token;
        }
    }

    public class GenericAPIResponse<T> : GenericAPIResponse
    {
        public T Data { get; set; }

        public GenericAPIResponse(string responseCode, string responseDescription, string token, T data)
            : base(responseCode, responseDescription, token)
        {
            Data = data;
        }

        public GenericAPIResponse()
        { }
    }
}
