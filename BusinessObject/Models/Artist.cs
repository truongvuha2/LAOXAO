using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string ArtistName { get; set; } = null!;

    public string ArtistCountry { get; set; } = null!;

    public string ArtistStatus { get; set; } = null!;

    public string ArtistImg { get; set; } = null!;
}
