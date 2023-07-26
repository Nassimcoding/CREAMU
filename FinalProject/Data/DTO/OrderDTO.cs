namespace FinalProject.Data.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? TotalAmount { get; set; }

        public string? OrderStatus { get; set; }

        public string? PaymentStatus { get; set; }

    }
}
