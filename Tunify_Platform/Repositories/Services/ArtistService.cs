using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tunify_Platform.Repositories.Services
{
    public class ArtistService : IArtistRepository
    {
        private readonly TunifyDbContext _context;

        public ArtistService(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task AddArtistAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArtistAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetArtistByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task UpdateArtistAsync(Artist artist)
        {
            var existingArtist = await _context.Artists.FindAsync(artist.ArtistId);
            if (existingArtist != null)
            {
                _context.Entry(existingArtist).CurrentValues.SetValues(artist);
                await _context.SaveChangesAsync();
            }
        }

       public async Task<Song> AddSongToArtist(int artistId, int songId)
        {
            var song = await _context.Songs.FindAsync(songId);
            if (song != null)
            {
                song.ArtistId = artistId;
                _context.Entry(song).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            var result = _context.Songs.Where(a => a.ArtistId == artistId).FirstOrDefault();
            return result;

        }

        public async Task<List<Song>> GetSongsForArtists(int ArtistsId)
        {
            var ArtistsSongsVar = await _context.Songs
                .Where(pl => pl.ArtistId == ArtistsId).ToListAsync();
            return ArtistsSongsVar;
        }
    }
    //
}
