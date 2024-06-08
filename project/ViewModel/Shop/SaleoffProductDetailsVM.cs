using OganiShoppingProject.Models;

namespace OganiShoppingProject.ViewModel.Shop
{
    public class SaleoffProductDetailsVM
    {
        public SaleoffProduct? saleoffproduct { get; set; }
        public List<SaleoffProduct>? saleoffproducts { get; set; }
        public Basket? basket { get; set; }
    }
}
