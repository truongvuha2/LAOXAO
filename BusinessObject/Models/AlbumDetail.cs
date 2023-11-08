using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class AlbumDetail
{
    public int AlbumId { get; set; }

    public int SongId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
