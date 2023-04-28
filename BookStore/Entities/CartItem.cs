namespace BookStore.Entities
{
    public class CartItem
    {
        public int ID { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
