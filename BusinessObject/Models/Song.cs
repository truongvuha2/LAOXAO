using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Song
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string ImgUrl { get; set; } = null!;

    public string Status { get; set; } = null!;
}
