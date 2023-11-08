using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace DataAccess;

public class ArtistDAO
{
    private ArtistDAO() { }

    public static ArtistDAO instance = null;
    private readonly MusicPrnContext _context = new MusicPrnContext();
    public static ArtistDAO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ArtistDAO();
            }
            return instance;
        }
    }

    public IEnumerable<Artist> GetAllArtists()
    {
        return _context.Artists.ToList();
    }

    public Artist GetArtistById(int artistId)
    {
        return _context.Artists.FirstOrDefault(a => a.ArtistId == artistId);
    }

    public void AddArtist(Artist artist)
    {
        if (artist != null)
        {
            _context.Artists.Add(artist);
            _context.SaveChanges();
        }
    }

    public void UpdateArtist(Artist artist)
    {
        if (artist != null)
        {
            _context.Entry(artist).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public void DeleteArtist(int artistId)
    {
        var artist = _context.Artists.FirstOrDefault(a => a.ArtistId == artistId);
        if (artist != null)
        {
            // Thay đổi trạng thái của bản ghi thành "Deleted"
            artist.ArtistStatus = "Deleted";
            _context.SaveChanges();
        }
    }
}
