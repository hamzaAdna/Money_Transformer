using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tahaluf.Models;

public partial class Visacard
{
    public decimal Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "CVV is required")]
    [MaxLength(3, ErrorMessage = "Cvv Iban Is Maximum 10 Number ")]
    public string? Cvv { get; set; }

   [Required(ErrorMessage = "Card number is required")]
    [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be a 16-digit number")]
    public string? Cardnumber { get; set; }

    [Required(ErrorMessage = "Expiration date is required")]
    [FutureDate(ErrorMessage = "Expiration date must be in the future")]
    public DateTime? Expiredate { get; set; }

    public string? Goodthrow { get; set; }

    public decimal? Balance { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    public decimal? Useracount { get; set; }

    public virtual Useracount? UseracountNavigation { get; set; }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }
            return false;
        }
    }


}
