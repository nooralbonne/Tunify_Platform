using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

namespace Tunify_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistsController(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetAllPlaylists()
        {
            return Ok(await _playlistRepository.GetAllPlaylistsAsync());
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylistById(int id)
        {
            var playlist = await _playlistRepository.GetPlaylistByIdAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return playlist;
        }

        // PUT: api/Playlists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, Playlist playlist)
        {
            if (id != playlist.PlaylistId)
            {
                return BadRequest();
            }

            await _playlistRepository.UpdatePlaylistAsync(playlist);
            return NoContent();
        }

        // POST: api/Playlists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Playlist>> AddPlaylist(Playlist playlist)
        {
            await _playlistRepository.AddPlaylistAsync(playlist);
            return CreatedAtAction(nameof(GetPlaylistById), new { id = playlist.PlaylistId }, playlist);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlist = await _playlistRepository.GetPlaylistByIdAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            await _playlistRepository.DeletePlaylistAsync(id);
            return NoContent();
        }

        [HttpPost("playlists/{playlistId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToPlaylist(int playlistId, int songId)
        {
            await _playlistRepository.AddSongToPlaylist(playlistId, songId);
            return Ok();
        }

        [HttpPost("playlists/{playlistId}")]
        public async Task<IActionResult> GetSongsForPlaylist(int playlistID)
        {
            await _playlistRepository.GetSongsForPlaylist(playlistID);
            return Ok();
        }
    }
}