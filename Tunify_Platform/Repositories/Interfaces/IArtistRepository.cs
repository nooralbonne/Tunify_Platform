using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAllArtistsAsync();
        Task<Artist> GetArtistByIdAsync(int id);
        Task AddArtistAsync(Artist artist);
        Task UpdateArtistAsync(Artist artist);
        Task DeleteArtistAsync(int id);
        Task<Song> AddSongToArtist(int artistId, int songId);
        Task<List<Song>> GetSongsForArtists(int ArtistsId);
    }
}
