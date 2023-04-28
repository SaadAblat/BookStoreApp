namespace BookStore.Entities
{
    public class Cart
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }

}
