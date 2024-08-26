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
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistsController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetAllArtists()
        {
            return Ok(await _artistRepository.GetAllArtistsAsync());
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtistById(int id)
        {
            var artist = await _artistRepository.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return artist;
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            await _artistRepository.UpdateArtistAsync(artist);
            return NoContent();
        }

        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artist>> AddArtist(Artist artist)
        {
            await _artistRepository.AddArtistAsync(artist);
            return CreatedAtAction(nameof(GetArtistById), new { id = artist.ArtistId }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _artistRepository.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            await _artistRepository.DeleteArtistAsync(id);
            return NoContent();
        }

        [HttpPost("artists/{artistId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToArtist(int artistId, int songId)
        {
            await _artistRepository.AddSongToArtist(artistId, songId);
            return Ok();
        }

        [HttpPost("artists/{artistId}")]
        public async Task<IActionResult> GetSongsForArtists(int ArtistsId)
        {
            await _artistRepository.GetSongsForArtists(ArtistsId);
            return Ok();
        }
    }
}