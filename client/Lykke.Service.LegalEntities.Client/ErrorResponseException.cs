using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Lykke.Common.Api.Contract.Responses;
using Refit;

namespace Lykke.Service.LegalEntities.Client
{
    /// <summary>
    /// Represents error response from the Legal entity API service
    /// </summary>
    public class ErrorResponseException : Exception
    {
        /// <summary>
        /// Gets a response error details.
        /// </summary>
        public ErrorResponse Error { get; }

        /// <summary>
        /// Gets a http response status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorResponseException"/> with response error details and API excepiton.
        /// </summary>
        /// <param name="error">The response error details</param>
        /// <param name="inner">The exception occurred during calling service API.</param>
        public ErrorResponseException(ErrorResponse error, ApiException inner) :
            base(error.GetSummaryMessage() ?? string.Empty, inner)
        {
            Error = error;
            StatusCode = inner.StatusCode;
        }
    }
}
