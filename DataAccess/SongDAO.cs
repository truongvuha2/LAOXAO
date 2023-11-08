using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SongDAO
    {
        public static SongDAO instance = null;
        private readonly MusicPrnContext _context = new MusicPrnContext();
        public static SongDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SongDAO();
                }
                return instance;
            }
        }
        public IEnumerable<Song> GetAllSongs()
        {
            return _context.Songs.ToList();
        }

        public Song GetSongById(int songId)
        {
            return _context.Songs.FirstOrDefault(s => s.Id == songId);
        }

        public void AddSong(Song song)
        {
            if (song != null)
            {
                _context.Songs.Add(song);
                _context.SaveChanges();
            }
        }

        public void UpdateSong(Song song)
        {
            if (song != null)
            {
                _context.Entry(song).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteSong(int songId)
        {
            var song = _context.Songs.FirstOrDefault(s => s.Id == songId);
            if (song != null)
            {
                // Thay đổi trạng thái của bản ghi thành "Deleted"
                song.Status = "Deleted";
                _context.SaveChanges();
            }
        }
    }
}
