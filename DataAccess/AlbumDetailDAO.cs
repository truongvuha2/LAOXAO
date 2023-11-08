using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class AlbumDetailDAO
    {
        public static AlbumDetailDAO instance = null;
        private readonly MusicPrnContext _context = new MusicPrnContext();
        public static AlbumDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AlbumDetailDAO();
                }
                return instance;
            }
        }

        public IEnumerable<Song> GetSongsInAlbum(int albumId)
        {
            return _context.AlbumDetails
                .Where(ad => ad.AlbumId == albumId)
                .Select(ad => ad.Song)
                .ToList();
        }

        public IEnumerable<Album> GetAlbumsForSong(int songId)
        {
            return _context.AlbumDetails
                .Where(ad => ad.SongId == songId)
                .Select(ad => ad.Album)
                .ToList();
        }
        public IEnumerable<(Song Song, List<Album> Albums)> GetAllSongsWithAlbums()
        {
            var albumDetails = _context.AlbumDetails
                .Include(ad => ad.Song)
                .Include(ad => ad.Album)
                .ToList();

            var albumDetailGroups = albumDetails.GroupBy(ad => ad.Song);

            var result = new List<(Song Song, List<Album> Albums)>();

            foreach (var group in albumDetailGroups)
            {
                var song = group.Key;
                var albums = group.Select(ad => ad.Album).ToList();
                result.Add((song, albums));
            }

            return result;
        }

        // Đưa ra tất cả các album và danh sách bài hát thuộc về mỗi album
        public IEnumerable<(Album Album, List<Song> Songs)> GetAllAlbumsWithSongs()
        {
            var albumDetails = _context.AlbumDetails
                .Include(ad => ad.Album)
                .Include(ad => ad.Song)
                .ToList();

            var albumGroups = albumDetails.GroupBy(ad => ad.Album);

            var result = new List<(Album Album, List<Song> Songs)>();

            foreach (var group in albumGroups)
            {
                var album = group.Key;
                var songs = group.Select(ad => ad.Song).ToList();
                result.Add((album, songs));
            }

            return result;
        }

        public void RemoveSongToAlbum(int albumId, int songId)
        {
            var albumDetail = new AlbumDetail
            {
                AlbumId = albumId,
                SongId = songId
            };

            _context.AlbumDetails.Remove(albumDetail);
            _context.SaveChanges();
        }


        public void AddSongToAlbum(int albumId, int songId)
        {
            var albumDetail = new AlbumDetail
            {
                AlbumId = albumId,
                SongId = songId
            };

            _context.AlbumDetails.Add(albumDetail);
            _context.SaveChanges();
        }
    }
}
