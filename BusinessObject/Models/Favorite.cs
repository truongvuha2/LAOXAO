using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Favorite
{
    public string Username { get; set; } = null!;

    public int SongId { get; set; }

    public virtual Song Song { get; set; } = null!;

    public virtual Account UsernameNavigation { get; set; } = null!;
}
