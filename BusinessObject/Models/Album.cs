using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public string AlbumName { get; set; } = null!;

    public string AlbumStatus { get; set; } = null!;

    public string AlbumImg { get; set; } = null!;
}
