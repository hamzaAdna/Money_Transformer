using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tahaluf.Models;

public partial class Transaction
{
    public decimal Id { get; set; }

    public decimal? Transvlaue { get; set; }


    public decimal? Senderiban { get; set; }




    public decimal? Receiveriban { get; set; }

    public decimal? Commission { get; set; }

    public DateTime? Transdate { get; set; }

    public DateTime? Enddate { get; set; }

    public decimal? Walletid { get; set; }

    public virtual Wallet? Wallet { get; set; }
}
