using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<IEnumerable<Playlist>> GetAllPlaylistsAsync();
        Task<Playlist> GetPlaylistByIdAsync(int id);
        Task AddPlaylistAsync(Playlist playlist);
        Task UpdatePlaylistAsync(Playlist playlist);
        Task DeletePlaylistAsync(int id);
        Task<Playlist> AddSongToPlaylist(int playlistId, int songId);
        Task<List<Song>> GetSongsForPlaylist(int playlistID);
    }
}
