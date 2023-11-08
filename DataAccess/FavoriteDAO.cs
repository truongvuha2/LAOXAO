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
            try
            {
                using var context = new MusicPrnContext();
                var newFavorite = new Favorite
                    {
                        Username = username,
                        SongId = songId
                    };


                    context.Set<Favorite>().AddRange(new List<Favorite> { newFavorite });
                    context.SaveChanges();
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }   

        }

        // Lấy tất cả bài hát trong danh sách yêu thích của một người dùng (Username)
        public IEnumerable<Song> GetFavoriteSongs(string username)
        {
            using var context = new MusicPrnContext();
            return context.Favorites
                .Where(f => f.Username == username)
                .Select(f => f.Song)
                .ToList();
            
        }

        public void RemoveSongFromFavorite(string username, int songId)
        {
            try
            {
                using var context = new MusicPrnContext();

                // Tìm bản ghi Favorite có Username và SongId cụ thể
                var existingFavorite = context.Favorites.FirstOrDefault(f => f.Username.Equals(username) && f.SongId == songId);

                if (existingFavorite != null)
                {
                    context.Favorites.Remove(existingFavorite);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
