using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tahaluf.Models;

public partial class Useracount
{
    public decimal Id { get; set; }


    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public decimal? Roleid { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role1? Role { get; set; }
    public virtual ICollection<Visacard> Visacards { get; set; } = new List<Visacard>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
