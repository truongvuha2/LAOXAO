using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class SongArtist
{
    public int ArtistId { get; set; }

    public int SongId { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
