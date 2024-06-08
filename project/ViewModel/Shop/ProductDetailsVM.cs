using OganiShoppingProject.Models;

namespace OganiShoppingProject.ViewModel.Shop
{
    public class ProductDetailsVM
    {
        public Product? product { get; set; }
        public List<Product>? products { get; set; }
        public Basket? basket { get; set; }
    }
}
