namespace OganiShoppingProject.ViewModel
{
    public class PaginationVM<T>
    {
        public decimal CurrentPage { get; set; }
        public decimal TotalPage { get; set; }
        public List<T>? Items { get; set; }
    }
}
