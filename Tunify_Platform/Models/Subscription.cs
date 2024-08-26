namespace Tunify_Platform.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public string Subscription_Type { get; set; }
        public double Price { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
