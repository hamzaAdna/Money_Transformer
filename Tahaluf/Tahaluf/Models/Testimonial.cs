using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tahaluf.Models;

public partial class Testimonial
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? Message { get; set; }

    public string? Status { get; set; }

    public decimal? Useracountid { get; set; }

    public virtual Useracount? Useracount { get; set; }
}
