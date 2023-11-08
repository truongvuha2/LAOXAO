using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IAlbumRepository
    {
        /// <summary>
        /// Lấy danh sách album
        /// </summary>
        IEnumerable<Album> GetAllAlbums();
        /// <summary>
        /// Thêm 1 album bằng id
        /// </summary>
        Album GetAlbumById(int albumId);
        /// <summary>
        /// Thêm album
        /// </summary>
        void AddAlbum(Album album);
        /// <summary>
        /// Chỉnh sửa album
        /// </summary>
        void UpdateAlbum(Album album);
        /// <summary>
        /// Xóa album
        /// </summary>
        void DeleteAlbum(int albumId);
    }
}
