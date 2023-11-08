using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        void IArtistRepository.AddArtist(Artist artist)
        => ArtistDAO.Instance.AddArtist(artist);

        void IArtistRepository.DeleteArtist(int artistId)
        => ArtistDAO.Instance.DeleteArtist(artistId);

        IEnumerable<Artist> IArtistRepository.GetAllArtists()
        => ArtistDAO.Instance.GetAllArtists();

        Artist IArtistRepository.GetArtistById(int artistId)
       => ArtistDAO.Instance.GetArtistById(artistId);

        void IArtistRepository.UpdateArtist(Artist artist)
        => ArtistDAO.Instance.UpdateArtist(artist);
    }
}
