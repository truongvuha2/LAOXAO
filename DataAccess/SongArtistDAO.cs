using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SongArtistDAO
    {

        public static SongArtistDAO instance = null;
        private readonly MusicPrnContext _context = new MusicPrnContext();
        public static SongArtistDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SongArtistDAO();
                }
                return instance;
            }
        }
        public IEnumerable<Artist> GetArtistsBySongId(int songId)
        {
            return _context.SongArtists
                .Where(sa => sa.SongId == songId)
                .Select(sa => sa.Artist)
                .ToList();
        }

        public IEnumerable<Song> GetSongsByArtistId(int artistId)
        {
            return _context.SongArtists
                .Where(sa => sa.ArtistId == artistId)
                .Select(sa => sa.Song)
                .ToList();
        }



        public IEnumerable<(Song Song, List<Artist> Artists)> GetAllSongsWithArtists()
        {
            var songArtists = _context.SongArtists
                .Include(sa => sa.Song)
                .Include(sa => sa.Artist)
                .ToList();

            var songArtistGroups = songArtists.GroupBy(sa => sa.Song);

            var result = new List<(Song Song, List<Artist> Artists)>();

            foreach (var group in songArtistGroups)
            {
                var song = group.Key;
                var artists = group.Select(sa => sa.Artist).ToList();
                result.Add((song, artists));
            }

            return result;
        }

        // Đưa ra tất cả các nghệ sĩ và danh sách bài hát họ tham gia
        public IEnumerable<(Artist Artist, List<Song> Songs)> GetAllArtistsWithSongs()
        {
            var songArtists = _context.SongArtists
                .Include(sa => sa.Song)
                .Include(sa => sa.Artist)
                .ToList();

            var artistGroups = songArtists.GroupBy(sa => sa.Artist);

            var result = new List<(Artist Artist, List<Song> Songs)>();

            foreach (var group in artistGroups)
            {
                var artist = group.Key;
                var songs = group.Select(sa => sa.Song).ToList();
                result.Add((artist, songs));
            }

            return result;
        }

        public void AddArtistToSong(int songId, int artistId)
        {
            var songArtist = new SongArtist
            {
                SongId = songId,
                ArtistId = artistId
            };

            _context.SongArtists.Add(songArtist);
            _context.SaveChanges();
        }
        public void RemoveArtistToSong(int songId, int artistId)
        {
            var songArtist = new SongArtist
            {
                SongId = songId,
                ArtistId = artistId
            };

            _context.SongArtists.Remove(songArtist);
            _context.SaveChanges();
        }

    }

}
