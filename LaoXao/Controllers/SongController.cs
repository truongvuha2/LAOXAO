using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repository;
using BusinessObject.Models;
using System.Collections.Generic;

namespace LaoXao.Controllers
{
    public class SongsController : Controller
    {
        ISongRepository songRepository = new SongRepository();
        ISongArtistRepository songArtistRepository = new SongArtistRepository();
        IAlbumRepository albumRepository = new AlbumRepository();
        IFavoriteRepository favoriteRepository = new FavoriteRepository();

        // GET: SongsController
        [Route("Index")]
        public ActionResult Index(string? name)
        {
            string username = Request.Cookies["Username"];
            var songList = songRepository.GetAllSongs().Where(x=>x.Status.Equals("Active"));
            var songArtist = songArtistRepository.GetAllSongsWithArtists().Where(x => x.Song.Status.Equals("Active"));
            var albumList = albumRepository.GetAllAlbums().Where(x => x.AlbumStatus.Equals("Active")); ;
            var favoriteList = favoriteRepository.GetFavoriteSongs(username).Where(x=>x.Status.Equals("Active"));
            IEnumerable<(Song, List<Artist>)> favoriteArtistList = songArtist
     .Where(sa => favoriteList.Any(f => f.Id == sa.Song.Id))
     .ToList();



            if (name != null)
            {
                ViewBag.SearchName = name;
                ViewBag.SearchResults = songList.Where(f => f.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                ViewBag.SearchResults = null;
            }

            ViewBag.Username = username;
            ViewBag.ArtistList = songArtist;
            ViewBag.AlbumList = albumList;
            ViewBag.FavoriteList = favoriteArtistList;
            return View(songList);
        }
        [Route("Manager")]
        public ActionResult Manager(string? name)
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

        public ActionResult GetSongs()
        {
            var songList = songRepository.GetAllSongs();
            return Json(songList);
        }




        public ActionResult Manager()
        {
            var songList = songRepository.GetAllSongs();
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
                return RedirectToAction(nameof(Manager));
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
                return RedirectToAction(nameof(Manager));
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
                return RedirectToAction(nameof(Manager));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public ActionResult AddFavorite(int id)
        {
            string username = Request.Cookies["Username"];
            favoriteRepository.AddSongToFavorite(username, id);
            return Json(new { success = true });
        }

        public ActionResult RemoveFavorite(int id)
        {
            string username = Request.Cookies["Username"];
            string user = username;
            favoriteRepository.RemoveSongFromFavorite(user, id);
            return Json(new { success = true });
        }

        public ActionResult IsFavorite(int id)
        {
            MusicPrnContext context = new MusicPrnContext();
            string username = Request.Cookies["Username"];

            IEnumerable<Song> favorite = favoriteRepository.GetFavoriteSongs(username);
            Song song = favorite.FirstOrDefault(x => x.Id == id);
            if (song != null)
            {
                return Json(new { success = true });
            }
            else
            {
                return BadRequest("An error occurred.");
            }
        }
    }
}
