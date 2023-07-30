using System.ComponentModel.DataAnnotations;
namespace FinalProject.Data.DTO
{
    public class ProductDetailToCart
    {
        [Required(ErrorMessage = "請先登入會員")]
        public int? MemberId { get; set; }
        public int? ProductId { get; set; }
        public int? Qty { get; set; }
    }
}
