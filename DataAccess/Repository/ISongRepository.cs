using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISongRepository
    {
        IEnumerable<Song> GetSongs();
        Song GetSongByID(int Id);
        void InsertSong(Song song);
        void DeleteSong(int Id);
        void UpdateSong(Song song);
    }
}
