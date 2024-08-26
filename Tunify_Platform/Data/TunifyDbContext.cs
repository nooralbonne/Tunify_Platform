using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Models;

namespace Tunify_Platform.Data
{
    public class TunifyDbContext : IdentityDbContext<ApplicationUser>

    {
        public TunifyDbContext(DbContextOptions<TunifyDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PlaylistSong>().HasKey(pk => new { pk.SongId, pk.PlaylistId });

            // PlaylistSongs and Playlist: Many-to-One
            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.playlistSongs)
                .HasForeignKey(ps => ps.PlaylistId)
                .OnDelete(DeleteBehavior.Restrict);

            // PlaylistSongs and Songs: Many-to-One
            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.playlistSongs)
                .HasForeignKey(ps => ps.SongId)
                .OnDelete(DeleteBehavior.Restrict);

            // Users and Subscriptions: One-to-Many
            modelBuilder.Entity<User>()
                .HasOne(u => u.Subscription)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Users and Playlists: One-to-Many
            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.User)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Songs and Artists: Many-to-One
            modelBuilder.Entity<Song>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);

            // Songs and Albums: Many-to-One
            modelBuilder.Entity<Song>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            // Albums and Artists: Many-to-One
            modelBuilder.Entity<Album>()
                .HasOne(a => a.Artist)
                .WithMany(ar => ar.Albums)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
            //----------------------------------
            // Seeding Subscription data
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription { SubscriptionId = 1, Subscription_Type = "Basic", Price = 9.99 },
                new Subscription { SubscriptionId = 2, Subscription_Type = "Premium", Price = 19.99 }
            );

            // Seeding Artist data
            modelBuilder.Entity<Artist>().HasData(
                new Artist { ArtistId = 1, Name = "Taylor Swift", Bio = "American singer-songwriter, known for narrative songs about her personal life." },
                new Artist { ArtistId = 2, Name = "Ed Sheeran", Bio = "English singer-songwriter, known for his hit singles and acoustic performances." }
            );

            // Seeding Album data
            modelBuilder.Entity<Album>().HasData(
                new Album { AlbumId = 1, Album_Name = "1989", Release_Date = new DateTime(2014, 10, 27), ArtistId = 1 },
                new Album { AlbumId = 2, Album_Name = "Divide", Release_Date = new DateTime(2017, 3, 3), ArtistId = 2 }
            );

            // Seeding Song data
            modelBuilder.Entity<Song>().HasData(
                new Song { SongId = 1, Title = "Blank Space", ArtistId = 1, AlbumId = 1, Duration = TimeSpan.FromMinutes(3.5), Genre = "Pop" },
                new Song { SongId = 2, Title = "Shape of You", ArtistId = 2, AlbumId = 2, Duration = TimeSpan.FromMinutes(4), Genre = "Pop" },
                new Song { SongId = 3, Title = "Style", ArtistId = 1, AlbumId = 1, Duration = TimeSpan.FromMinutes(3.5), Genre = "Pop" },
                new Song { SongId = 4, Title = "Castle on the Hill", ArtistId = 2, AlbumId = 2, Duration = TimeSpan.FromMinutes(4), Genre = "Rock" }
            );

            // Seeding User data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "Moayad", Email = "hamadanjo@gmail.com", Join_Date = new DateTime(2024, 1, 15), SubscriptionId = 1 },
                new User { UserId = 2, Username = "Aya", Email = "aya@gmail.com", Join_Date = new DateTime(2024, 2, 10), SubscriptionId = 2 }
            );

            // Seeding Playlist data
            modelBuilder.Entity<Playlist>().HasData(
                new Playlist { PlaylistId = 1, UserId = 1, Playlist_Name = "Morning Motivation", Created_Date = new DateTime(2024, 3, 1) },
                new Playlist { PlaylistId = 2, UserId = 2, Playlist_Name = "Evening Relaxation", Created_Date = new DateTime(2024, 3, 5) },
                new Playlist { PlaylistId = 3, UserId = 1, Playlist_Name = "Workout Hits", Created_Date = new DateTime(2024, 3, 10) },
                new Playlist { PlaylistId = 4, UserId = 2, Playlist_Name = "Road Trip", Created_Date = new DateTime(2024, 3, 15) }
            );

            // Seeding PlaylistSongs data
            modelBuilder.Entity<PlaylistSong>().HasData(
                new PlaylistSong { PlaylistSongId = 1, PlaylistId = 1, SongId = 1 },
                new PlaylistSong { PlaylistSongId = 2, PlaylistId = 2, SongId = 2 },
                new PlaylistSong { PlaylistSongId = 3, PlaylistId = 3, SongId = 3 },
                new PlaylistSong { PlaylistSongId = 4, PlaylistId = 4, SongId = 4 }
            );
        }
    }
}
