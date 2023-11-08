using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISongArtistRepository
    {
        /// <summary>
        /// Lấy tất cả nghệ sĩ của 1 bài hát
        /// </summary>
        IEnumerable<Artist> GetArtistsBySongId(int songId);
        /// <summary>
        /// Lấy tát cả các bài hát của nghệ sĩ
        /// </summary>
        IEnumerable<Song> GetSongsByArtistId(int artistId);
        /// <summary>
        /// Lấy tất cả các bài hát và nghệ sĩ có mặt trong bài hát đó
        /// </summary>
        IEnumerable<(Song Song, List<Artist> Artists)> GetAllSongsWithArtists();
        /// <summary>
        /// lấy tất cả nghệ sĩ và các bài hát của những nghệ sĩ đó
        /// </summary>
        IEnumerable<(Artist Artist, List<Song> Songs)> GetAllArtistsWithSongs();
        /// <summary>
        /// Thêm nghệ sĩ vào 1 bài hát
        /// </summary>
        void AddArtistToSong(int songId, int artistId);
        /// <summary>
        /// Xóa nghệ sĩ vào 1 bài hát
        /// </summary>
        void RemoveArtistToSong(int songId, int artistId);
    }
}
