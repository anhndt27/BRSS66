namespace BRSS66.ApplicationCore.Models;

public class JqueryDatatableParam
{
    public string? Draw { get; set; }
    public string? SortColumn { get; set; }
    public string? SortColumnDirection { get; set; }
    public string? SearchValue { get; set; }
    public int PageSize { get; set; }
    public int Skip { get; set; }
}