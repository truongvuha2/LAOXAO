using BusinessObject;

namespace DataAccess
{
    public class SongDAO
    {
        private static SongDAO instance = null;
        private static readonly object instanceLock = new object();

        public static SongDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SongDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Song> GetSongList()
        {
            var songs = new List<Song>();
            try
            {
                using var context = new MusicDbContext();
                songs = context.Songs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return songs;
        }

        public Song GetSongByID(int Id)
        {
            Song song = null;
            try
            {
                using var context = new MusicDbContext();
                song = context.Songs.SingleOrDefault(c => c.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return song;
        }

        public void Addnew(Song song)
        {
            try
            {
                Song _song = GetSongByID(song.Id);
                if (_song == null)
                {
                    using var context = new MusicDbContext();
                    context.Songs.Add(song);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The song is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Song song)
        {
            try
            {
                Song _song = GetSongByID(song.Id);
                if (_song != null)
                {
                    using var context = new MusicDbContext();
                    context.Songs.Update(song);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The song not already exis");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int Id)
        {
            try
            {
                Song song = GetSongByID(Id);
                if (song != null)
                {
                    using var context = new MusicDbContext();
                    context.Songs.Remove(song);
                    context.SaveChanges();
                }
                else
                {

                    throw new Exception("The song does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
