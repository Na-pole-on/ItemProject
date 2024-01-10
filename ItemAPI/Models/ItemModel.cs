namespace ItemAPI.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
        public string PhotoUrl { get; set; }
    }
}
