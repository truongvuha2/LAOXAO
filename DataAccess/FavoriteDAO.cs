using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FavoriteDAO
    {

        public static FavoriteDAO instance = null;
        private readonly MusicPrnContext _context = new MusicPrnContext();
        public static FavoriteDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FavoriteDAO();
                }
                return instance;
            }
        }
        public void AddSongToFavorite(string username, int songId)
        {
            var favorite = new Favorite
            {
                Username = username,
                SongId = songId
            };

            _context.Favorites.Add(favorite);
            _context.SaveChanges();
        }

        // Lấy tất cả bài hát trong danh sách yêu thích của một người dùng (Username)
        public IEnumerable<Song> GetFavoriteSongs(string username)
        {
            return _context.Favorites
                .Where(f => f.Username == username)
                .Select(f => f.Song)
                .ToList();
        }

        public void RemoveSongFromFavorite(string username, int songId)
        {
            var favorite = _context.Favorites
                .FirstOrDefault(f => f.Username == username && f.SongId == songId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
            }
        }
    }
}
