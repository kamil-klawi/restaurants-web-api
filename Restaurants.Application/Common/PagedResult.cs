namespace Restaurants.Application.Common;

public class PagedResult<T>(IEnumerable<T> items, int totalItemsCount, int pageSize, int pageNumber)
{
    public IEnumerable<T> Items { get; set; } = items;
    public int TotalPages { get; set; } = (int) Math.Ceiling(totalItemsCount / (double) pageSize);
    public int TotalItemsCount { get; set; } = totalItemsCount;
    public int ItemsFrom { get; set; } = pageSize * (pageNumber - 1) + 1;
    public int ItemsTo { get; set; } = pageSize * (pageNumber - 1) + pageSize;
}