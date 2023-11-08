using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaoXao.Controllers
{
    public class ManagersController : Controller
    {
        ISongRepository songRepository = new SongRepository();
        ISongArtistRepository songArtistRepository = new SongArtistRepository();

        public ActionResult Index(string? name)
        {
            var songList = songRepository.GetAllSongs();

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
        public ActionResult Details(int? id)
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

        // GET: SongsController/Create
        public ActionResult Create() => View();

        // POST: SongsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Song song)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    songRepository.UpdateSong(song);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(song);
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
        public ActionResult Edit(int id, Song song)
        {
            try
            {
                if (id != song.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    songRepository.UpdateSong(song);
                }
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
