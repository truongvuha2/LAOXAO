using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SongRepository : ISongRepository
    {
        public void AddSong(Song song)
        => SongDAO.Instance.AddSong(song);

        public void DeleteSong(int songId)
           => SongDAO.Instance.DeleteSong(songId);

        public IEnumerable<Song> GetAllSongs()
           => SongDAO.Instance.GetAllSongs();

        public Song GetSongById(int songId)
           => SongDAO.Instance.GetSongById(songId);

        public void UpdateSong(Song song)
           => SongDAO.Instance.UpdateSong(song);
    }
}
