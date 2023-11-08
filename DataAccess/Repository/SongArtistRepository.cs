using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SongArtistRepository : ISongArtistRepository
    {
        public void AddArtistToSong(int songId, int artistId)
        => SongArtistDAO.Instance.AddArtistToSong(songId, artistId);

        public IEnumerable<(Artist Artist, List<Song> Songs)> GetAllArtistsWithSongs()
        => SongArtistDAO.Instance.GetAllArtistsWithSongs();

        public IEnumerable<(Song Song, List<Artist> Artists)> GetAllSongsWithArtists()
        => SongArtistDAO.Instance.GetAllSongsWithArtists();

        public IEnumerable<Artist> GetArtistsBySongId(int songId)
        => SongArtistDAO.Instance.GetArtistsBySongId(songId);

        public IEnumerable<Song> GetSongsByArtistId(int artistId)
        => SongArtistDAO.Instance.GetSongsByArtistId(artistId);

        public void RemoveArtistToSong(int songId, int artistId)
        => SongArtistDAO.Instance.RemoveArtistToSong(songId, artistId);
    }
}
