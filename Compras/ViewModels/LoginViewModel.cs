using System.ComponentModel.DataAnnotations;

namespace Compras.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
