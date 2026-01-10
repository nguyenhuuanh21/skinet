namespace skinet.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        public string Tyoe { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }
    }
}
