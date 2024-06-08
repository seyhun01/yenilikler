using OganiShoppingProject.Models;

namespace OganiShoppingProject.ViewModel.Home
{
    public class FavoriteProductDetailsVM
    {
        public FavoriteProduct? favoriteproduct { get; set; }
        public List<FavoriteProduct>? favoriteproducts { get; set; }
        public Basket? basket { get; set; }
    }
}
