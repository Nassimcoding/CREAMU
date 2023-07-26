namespace FinalProject.Data.DTO
{
    public class OrderDetailsDTO
    {
        public int OrderDetailId { get; set; }

        public int? ProductId { get; set; }

        public int? Qty { get; set; }

        public int? UnitPrice { get; set; }

        public float? Discount { get; set; }

        public int? Subtotal { get; set; }

        public string? Notes { get; set; }

        public string? Type { get; set; }
    }
}
