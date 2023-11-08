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
            using var context = new MusicPrnContext();
            return context.Songs.ToList();
        }

        public Song GetSongById(int songId)
        {
            return _context.Songs.FirstOrDefault(s => s.Id == songId);
        }

        public void AddSong(Song song)
        {
            if (song != null)
            {
                using var context = new MusicPrnContext();
                context.Songs.Add(song);
                context.SaveChanges();
            }
        }

        public void UpdateSong(Song song)
        {
            try
            {
                Song s = GetSongById(song.Id);
                if (s != null)
                {
                    using var context = new MusicPrnContext();
                    context.Songs.Update(song);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Song is already exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSong(int songId)
        {
            try
            {
                Song s = GetSongById(songId);
                if (s != null)
                {
                    s.Status = "Deleted";
                    using var _context = new MusicPrnContext();
                    _context.Songs.Update(s);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Song is already exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}