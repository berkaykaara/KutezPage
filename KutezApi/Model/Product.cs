namespace KutezApi.Model
{
    public class Product
    {
        public string Name { get; set; }
        public double PopularityScore { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public ProductImage Images { get; set; }
    }
}
