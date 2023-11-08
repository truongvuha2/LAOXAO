using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        public void AddAlbum(Album album)
        => AlbumDAO.Instance.AddAlbum(album);

        public void DeleteAlbum(int albumId)
       => AlbumDAO.Instance.DeleteAlbum(albumId);

        public Album GetAlbumById(int albumId)
        => AlbumDAO.Instance.GetAlbumById(albumId);

        public IEnumerable<Album> GetAllAlbums()
       => AlbumDAO.Instance.GetAllAlbums();
        public void UpdateAlbum(Album album)
       => AlbumDAO.Instance.UpdateAlbum(album);
    }
}
