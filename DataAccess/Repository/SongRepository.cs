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
        public Song GetSongByID(int Id) => SongDAO.Instance.GetSongByID(Id);
        public IEnumerable<Song> GetSongs() => SongDAO.Instance.GetSongList();
        public void InsertSong(Song song) => SongDAO.Instance.Addnew(song);
        public void DeleteSong(int Id) => SongDAO.Instance.Remove(Id);
        public void UpdateSong(Song song) => SongDAO.Instance.Update(song);
    }
}
