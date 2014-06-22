using System.ComponentModel.DataAnnotations;

namespace CustomIdentitySample.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ユーザー名を入れてください")]
        [Display(Name = "ログインID")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "パスワードを入れてください")]
        [Display(Name = "パスワード")]
        public string Password { get; set; }
    }
}