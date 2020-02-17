namespace ShopCart.Models {

    public class Gender
    {
        public static string M = "M";
        public static string F = "M";
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public Address Address { get; set; }
    }
}