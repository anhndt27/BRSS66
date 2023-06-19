namespace BRSS66.Web.MVC.Models;

public class DataTableAjax
{
    public string? Draw { get; set; }
    public string? SortColumn { get; set; }
    public string? SortColumnDirection { get; set; }
    public string? SearchValue { get; set; }
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int TotalRecord { get; set; }
    public int FilterRecord { get; set; }
}