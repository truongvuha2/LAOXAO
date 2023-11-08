using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AlbumDAO
    {
        private static AlbumDAO instance = null;
        private static readonly object instanceLock = new object();
        private readonly MusicPrnContext _context = new MusicPrnContext();
        private AlbumDAO() { }
        public static AlbumDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AlbumDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Album> GetAllAlbums()
        {
            return _context.Albums.ToList();
        }

        public Album GetAlbumById(int albumId)
        {
            return _context.Albums.FirstOrDefault(a => a.AlbumId == albumId);
        }

        public void AddAlbum(Album album)
        {
            if (album != null)
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
            }
        }

        public void UpdateAlbum(Album album)
        {
            if (album != null)
            {
                _context.Entry(album).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteAlbum(int albumId)
        {
            var album = _context.Albums.FirstOrDefault(a => a.AlbumId == albumId);
            if (album != null)
            {
                album.AlbumStatus = "Deleted";
                _context.SaveChanges();
            }
        }
    }
}
