using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dejtinghemsida.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Kön")]
        public Gender Kön { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} måste innehålla minst {2} tecken.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [Compare("Password", ErrorMessage = "Kontrollera lösenord. Lösenord måste matcha!")]
        public string ConfirmPassword { get; set; }


        [Required]
        [RegularExpression("[a-öA-Ö]+", ErrorMessage = "Ange Förnamn med bokstäver från A-Ö och måste börja med stor bokstav")]
        [Display(Name = "Förnamn")]
        public string Förnamn { get; set; }

        [Required]
        [RegularExpression("[a-öA-Ö]+", ErrorMessage = "Ange Efternamn med bokstäver från A-Ö och måste börja med stor bokstav")]
        [Display(Name = "Efternamn")]
        public string Efternamn { get; set; }

        [Required]
        [Display(Name = "Ålder")]
        public int Ålder { get; set; }

        [Required]
        [RegularExpression("[a-öA-Ö]+", ErrorMessage = "Ange sysselsättning med bokstäver från A-Ö och måste börja med stor bokstav")]
        [Display(Name = "Sysselsättning")]
        public string Sysselsättning { get; set; }

        [Display(Name = "Profilbild")]
        public byte[] UserPhoto { get; set; }


        public enum Gender
        {
            Man,
            Kvinna,
            Annat
        }
    }
}
