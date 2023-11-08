using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IArtistRepository
    {
        /// <summary>
        /// Lấy tất cả các nghệ sĩ
        /// </summary>
        IEnumerable<Artist> GetAllArtists();
        /// <summary>
        /// Lấy thông tin 1 nghệ sĩ bằng id
        /// </summary>
        Artist GetArtistById(int artistId);
        /// <summary>
        /// Thêm 1 nghệ sĩ
        /// </summary>
        void AddArtist(Artist artist);
        /// <summary>
        /// Cập nhật nghệ sĩ
        /// </summary>
        void UpdateArtist(Artist artist);
        /// <summary>
        /// Xoá nghệ sĩ bằng cách đổi status = "Deleted"
        /// </summary>
        void DeleteArtist(int artistId);
    }
}
