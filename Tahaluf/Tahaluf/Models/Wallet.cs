using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tahaluf.Models;

public partial class Wallet
{
    public decimal Id { get; set; }

    public string? Status { get; set; }


    [MaxLength(10, ErrorMessage = "Receiver Iban Is Maximum 10 Number  ")]
    

    public decimal? Iban { get; set; }

    public decimal? Balance { get; set; }

    public DateTime? Createdate { get; set; } =DateTime.Now;

    public decimal? Bankid { get; set; }

    public decimal? Useracountid { get; set; }

    public virtual Bank? Bank { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual Useracount? Useracount { get; set; }
}
