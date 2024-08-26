using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tunify_Platform.Repositories.Services
{
    public class PlaylistService : IPlaylistRepository
    {
        private readonly TunifyDbContext _context;

        public PlaylistService(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task AddPlaylistAsync(Playlist playlist)
        {
            await _context.Playlists.AddAsync(playlist);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlaylistAsync(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylistsAsync()
        {
            return await _context.Playlists.ToListAsync();
        }

        public async Task<Playlist> GetPlaylistByIdAsync(int id)
        {
            return await _context.Playlists.FindAsync(id);
        }

        public async Task UpdatePlaylistAsync(Playlist playlist)
        {
            var existingPlaylist = await _context.Playlists.FindAsync(playlist.PlaylistId);
            if (existingPlaylist != null)
            {
                _context.Entry(existingPlaylist).CurrentValues.SetValues(playlist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Playlist> AddSongToPlaylist(int playlistId, int songId)
        {
            var playlistSong = new PlaylistSong
            {
                PlaylistId = playlistId,
                SongId = songId
            };
            _context.PlaylistSongs.Add(playlistSong);
            await _context.SaveChangesAsync();
            var playlist = _context.Playlists.FirstOrDefault(p => p.PlaylistId == playlistId);
            return playlist;
        }

        public async Task<List<Song>> GetSongsForPlaylist(int playlistID)
        {
            var playlistsSongsVar = await _context.PlaylistSongs
                .Where(pl => pl.PlaylistId == playlistID)
                .Select(pl => pl.Song)
                .ToListAsync();
            return playlistsSongsVar;
        }
    }
}
