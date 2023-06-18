using FinalProject.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FinalProject.ViewModel
{
    public class EmployeesViewModel
    {
        public int EmployeeId { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "請輸入名稱!")]
        public string? Name { get; set; }

        [Display(Name = "電話")]
        [Required(ErrorMessage = "請輸入電話!")]
        public string? Telephone { get; set; }

        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入密碼!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "請輸入8~20位數的密碼 !")]
        public string? Password { get; set; }

        [Display(Name = "再次確認密碼")]
        [Required(ErrorMessage = "請再次輸入密碼!")]
        [Compare("Password", ErrorMessage = "密碼不符合 !")]
        public string? CheckPassword { get; set; }

        public int? EmailId { get; set; }

        [Display(Name = "地址")]
        [Required(ErrorMessage = "請輸入地址!")]
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

       
        [Required(ErrorMessage = "請輸入Email !")]
        [EmailAddress(ErrorMessage = "請填寫正確Email !")]
        public string Email { get; set; }

        public string? Notes { get; set; }

    }
}
