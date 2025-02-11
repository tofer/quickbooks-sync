﻿#pragma warning disable IDE1006
namespace QbSync.QbXml.Objects
{
    /// <summary>
    /// An interface indicating if the class is a response.
    /// </summary>
    public interface IQbResponse
    {
        /// <summary>
        /// The request ID.
        /// </summary>
        string requestID { get; set; }

        /// <summary>
        /// The Status Code. 0 on success. 1 when nothing is found. Another positive number on error.
        /// </summary>
        string statusCode { get; set; }

        /// <summary>
        /// The Status Message.
        /// </summary>
        string statusMessage { get; set; }

        /// <summary>
        /// The Severity. (Info, Error)
        /// </summary>
        string statusSeverity { get; set; }
    }
}
#pragma warning restore IDE1006
