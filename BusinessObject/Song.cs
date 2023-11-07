﻿using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Song
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Artist { get; set; }

    public string FilePath { get; set; } = null!;

    public string ImgUrl { get; set; } = null!;
}