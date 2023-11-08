using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IAlbumDetailRepository
    {
        /// <summary>
        /// Lấy tất cả các bài hát từ 1 album
        /// </summary>
        IEnumerable<Song> GetSongsInAlbum(int albumId);

        /// <summary>
        /// Lấy tất cả các album có bài hát này
        /// </summary>
        IEnumerable<Album> GetAlbumsForSong(int songId);

        /// <summary>
        /// Lấy tất cả các bài hát và album chứa bài hát đó
        /// </summary>
        IEnumerable<(Song Song, List<Album> Albums)> GetAllSongsWithAlbums();
        /// <summary>
        /// Lấy tất cả các album và bài hát có torng album
        /// </summary>
        IEnumerable<(Album Album, List<Song> Songs)> GetAllAlbumsWithSongs();
        /// <summary>
        /// Thêm bài hát cho 1 album
        /// </summary>
        void AddSongToAlbum(int songId, int albumId);
        /// <summary>
        /// Xóa bài hát cho 1 album
        /// </summary>
        void RemoveSongtoAlbum(int songId, int albumId);
    }
}
