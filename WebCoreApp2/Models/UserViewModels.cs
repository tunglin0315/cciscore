using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CCISWebCore.Models
{
    public class CreateModel
    {
        [Required(ErrorMessage = "請輸入帳號")]
        [Display(Name = "帳號名稱")]
        public string Name { get; set; }
        [Required(ErrorMessage = "請輸入E-Mail")]
        [Display(Name = "電子信箱")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "請輸入密碼")]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "{0}至少需要{2}字元", MinimumLength = 6)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "密碼必須至少包含8個字符，並且包含以下4個字符中的3個：大寫（A-Z），小寫（a-z），數字（0-9）和特殊字符（例如！@＃$ %% ^＆*）")]
        public string Password { get; set; }
        [Required(ErrorMessage = "請輸入確認密碼")]
        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "與輸入的密碼不一致!")]
        public string ConfirmPassword { get; set; }
    }

    public class EditModel
    {
        public string Id { get; set; }
        [Display(Name = "帳號名稱")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "請輸入E-Mail")]
        [Display(Name = "電子信箱")]
        public string Email { get; set; }
        [Required(ErrorMessage = "請輸入密碼")]
        [Display(Name = "更改密碼")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "{0}至少需要{2}字元", MinimumLength = 6)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "密碼必須至少包含8個字符，並且包含以下4個字符中的3個：大寫（A-Z），小寫（a-z），數字（0-9）和特殊字符（例如！@＃$ %% ^＆*）")]
        public string Password { get; set; }
        [Required(ErrorMessage = "請輸入確認密碼")]
        [Display(Name = "確認更改密碼")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "與輸入的密碼不一致!")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "密碼")]
        public string IsAPwd { get; set; }
    }

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        [Display(Name = "角色名稱")]
        public string RoleName { get; set; }
        [Display(Name = "角色編號!!")]
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}
