﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISongRepository
    {
        /// <summary>
        /// Lấy tất cả các bài hát
        /// </summary>
        IEnumerable<Song> GetAllSongs();
        /// <summary>
        /// Lấy bài hát theo Id
        /// </summary>
        Song GetSongById(int songId);
        /// <summary>
        ///Thêm một bài hát
        /// </summary>
        void AddSong(Song song);
        /// <summary>
        /// Chỉnh sửa bài hát
        /// </summary>
        void UpdateSong(Song song);
        /// <summary>
        /// Xóa bài hát
        /// </summary>
        void DeleteSong(int songId);
    }
}
