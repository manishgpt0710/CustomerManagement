using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CustomerManagement.WebApi.Models;

/// <summary>
/// Api response
/// </summary>
[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ApiResponse
{
    private int? _page;
    private int? _pageSize;
    private int? _total;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CustomerManagement.WebApi.Models.ApiResponse"/> class.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="errors"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="total"></param>
    public ApiResponse(object data = null, IEnumerable<ApiError> errors = null, int? page = null, int? pageSize = null, int? total = null)
    {
        this.Data = data;
        this.Page = page;
        this.PageSize = pageSize;
        this.Total = total;
        this.Errors = errors;
    }

    /// <summary>
    /// Gets or sets the data
    /// </summary>
    /// <value>The data</value>
    public object Data { get; set; }

    /// <summary>
    /// Gets or sets the page number
    /// </summary>
    /// <value>The page number</value>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? Page
    {
        get => this.Data == null ? null : _page;
        set => _page = value;
    }

    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    /// <value>The page size</value>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? PageSize
    {
        get => this.Data == null ? null : _pageSize;
        set => _pageSize = value;
    }

    /// <summary>
    /// Gets or sets the total number of data
    /// </summary>
    /// <value>The total number of data</value>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? Total
    {
        get => this.Data == null ? null : _total;
        set => _total = value;
    }

    /// <summary>
    /// Gets or sets the errors
    /// </summary>
    /// <value>The errors</value>
    public IEnumerable<ApiError> Errors { get; set; }
}

