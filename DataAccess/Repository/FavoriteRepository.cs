using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        public void AddSongToFavorite(string username, int songId)
        => FavoriteDAO.Instance.AddSongToFavorite(username, songId);

        public IEnumerable<Song> GetFavoriteSongs(string username)
        => FavoriteDAO.Instance.GetFavoriteSongs(username);

        public void RemoveSongFromFavorite(string username, int songId)
        => FavoriteDAO.Instance.RemoveSongFromFavorite(username, songId);
    }
}
