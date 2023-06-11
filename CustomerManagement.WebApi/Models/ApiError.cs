using System;
namespace CustomerManagement.WebApi.Models;

/// <summary>
/// Api error
/// </summary>
public class ApiError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:CustomerManagement.WebApi.Models.ApiError"/> class.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="message"></param>
    /// <param name="code"></param>
    public ApiError(string type, string message, string code)
    {
        this.Type = type;
        this.Message = message;
        this.Code = code;
    }

    /// <summary>
    /// Gets or sets the type
    /// </summary>
    /// <value>The type</value>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the message
    /// </summary>
    /// <value>The message</value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the code
    /// </summary>
    /// <value>The code</value>
    public string Code { get; set; }
}

