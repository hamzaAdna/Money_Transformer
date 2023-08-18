using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tahaluf.Models;

public partial class Home
{
    public decimal Id { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? Logopath { get; set; }
    [NotMapped]
    public IFormFile? LogoFile { get; set; }
    public string? Bankname { get; set; }

    public string? Paragraph { get; set; }

    public string? Websitephone { get; set; }

    public string? Wensiteemail { get; set; }
}
