using OganiShoppingProject.Models;

namespace OganiShoppingProject.ViewModel.Shop
{
    public class ShopVM
    {
        public PaginationVM<Product>? pagination { get; set; }
        public List<SaleoffProduct>? saleoffproducts { get; set; }
        public List<Category>? category { get; set; }
    }
}
