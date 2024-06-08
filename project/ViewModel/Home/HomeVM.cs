using OganiShoppingProject.Models;

namespace OganiShoppingProject.ViewModel.Home
{
    public class HomeVM
    {
        public List<FavoriteProduct>? favproduct { get; set; }
        public Slide? slide { get; set; }
        public List<Category>? category { get; set; }
    }
}
