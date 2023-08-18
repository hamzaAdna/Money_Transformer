using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tahaluf.Models;

public partial class Bank
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Imagepath { get; set; }



    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
