using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IFavoriteRepository
    {
        /// <summary>
        /// Thêm bài hát vào danh sách yêu thích
        /// </summary>
        void AddSongToFavorite(string username, int songId);
        /// <summary>
        /// Lấy tất cả bài hát yêu thích
        /// </summary>
        IEnumerable<Song> GetFavoriteSongs(string username);
        /// <summary>
        /// Xóa 1 bài hát ra khỏi danh sách yêu thích
        /// </summary>
        void RemoveSongFromFavorite(string username, int songId);

    }
}
