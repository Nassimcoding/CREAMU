using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FinalProject.ViewModel
{
    public class EmployeesViewModelEdit
    {
        public int EmployeeId { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "請輸入名稱!")]
        [RegularExpression(@"^[a-zA-Z0-9\u4E00-\u9FA5 .,-]+$", ErrorMessage = "姓名只能包含中文、英文、數字、空格和特殊字符 !")]
        [StringLength(10, ErrorMessage = "姓名長度不能超過10個字符。")]
        public string? Name { get; set; }

        [Display(Name = "電話")]
        [Required(ErrorMessage = "請輸入電話!")]
        [RegularExpression(@"^[0-9-]+$", ErrorMessage = "電話只能包含數字和橫槓（-）!")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "電話長度需在8到15位之間 !")]
        public string? Telephone { get; set; }

        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入密碼!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "請輸入8~20位數的密碼 !")]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*]+$", ErrorMessage = "密碼只能包含字母、數字和特殊字符 !")]
        public string? Password { get; set; }

        public int? EmailId { get; set; }

        [Display(Name = "地址")]
        [Required(ErrorMessage = "請輸入地址!")]
        [RegularExpression(@"^[a-zA-Z0-9\u4E00-\u9FA5 ]+$", ErrorMessage = "地址只能包含中文、英文、數字和空格 !")]
        public string? Address { get; set; }

        public string? Image { get; set; }

        [Display(Name = "生日")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "請輸入生日!")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "職稱")]
        [Required(ErrorMessage = "請填寫職稱!")]
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime? JoinDate { get; set; }


        [Required(ErrorMessage = "Email 是必填的。")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "請輸入有效的 Email 地址。")]
        public string Email { get; set; }

        public string? Notes { get; set; }
    }
}
