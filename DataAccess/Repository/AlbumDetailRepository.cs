using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AlbumDetailRepository : IAlbumDetailRepository
    {

        public void AddSongToAlbum(int albumId, int songId)
        => AlbumDetailDAO.Instance.AddSongToAlbum(albumId, songId);

        public IEnumerable<Album> GetAlbumsForSong(int songId)
        => AlbumDetailDAO.Instance.GetAlbumsForSong(songId);

        public IEnumerable<(Album Album, List<Song> Songs)> GetAllAlbumsWithSongs()
        => AlbumDetailDAO.Instance.GetAllAlbumsWithSongs();

        public IEnumerable<(Song Song, List<Album> Albums)> GetAllSongsWithAlbums()
       => AlbumDetailDAO.Instance.GetAllSongsWithAlbums();

        public IEnumerable<Song> GetSongsInAlbum(int albumId)
         => AlbumDetailDAO.Instance.GetSongsInAlbum(albumId);

        public void RemoveSongtoAlbum(int songId, int albumId)
        => AlbumDetailDAO.Instance.RemoveSongToAlbum(songId, albumId);
    }
}
