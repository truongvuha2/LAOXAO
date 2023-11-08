using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaoXao.Controllers
{
    public class ManagersController : Controller
    {
        IAlbumDetailRepository albumDetailRepository = new AlbumDetailRepository();
        ISongRepository songRepository = new SongRepository();
        IArtistRepository artistRepository = new ArtistRepository();
        ISongArtistRepository songArtistRepository = new SongArtistRepository();

        public ActionResult Index(string? name)
        {
            var songList = songRepository.GetAllSongs().Where(x => x.Status.Equals("Active"));

            if (name != null)
            {

                ViewBag.SearchName = name;
                ViewBag.SearchResults = songList.Where(f => f.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                ViewBag.SearchResults = null;
            }

            return View(songList);
        }

        // GET: SongsController/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var song = songRepository.GetSongById(id.Value);
        //    if (song == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(song);
        //}

        // GET: SongsController/Create
        public ActionResult Create() => View();

        // POST: SongsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Title, string FilePath, string ImgUrl, string ArtistName)
        {
            try
            {
                Song song = new Song()
                {
                    Title = Title,
                    FilePath = FilePath,
                    ImgUrl = ImgUrl,
                    Status = "Active",
                };

                Artist artist = new Artist()
                {
                    ArtistCountry = "Viet Nam",
                    ArtistImg = "a",
                    ArtistName = ArtistName,
                    ArtistStatus = "Active"
                };

                songRepository.AddSong(song);
                artistRepository.AddArtist(artist);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: SongsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var song = songRepository.GetSongById(id.Value);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: SongsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, string Title, string FilePath, string ImgUrl, string ArtistName)
        {
            try
            {
                Song song = new Song()
                {
                    Id = Id,
                    Title = Title,
                    FilePath = FilePath,
                    ImgUrl = ImgUrl,
                    Status = "Active",
                };
                songRepository.UpdateSong(song);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: SongsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var song = songRepository.GetSongById(id.Value);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: SongsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                songRepository.DeleteSong(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}