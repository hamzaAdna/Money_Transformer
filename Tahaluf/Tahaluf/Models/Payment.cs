using System;
using System.Collections.Generic;

namespace Tahaluf.Models;

public partial class Payment
{
    public decimal Id { get; set; }

    public DateTime? Paymentdate { get; set; }

    public decimal? Useracountid { get; set; }

    public decimal? Walletid { get; set; }

    public virtual Useracount? Useracount { get; set; }

    public virtual Wallet? Wallet { get; set; }
}
