using Moq;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

namespace TunifyPlatformTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetSongsForPlaylist_ReturnsCorrectSongs()
        {
            // Arrange //

            var playlistId = 1;
            var songs = new List<Song>
    {
        new Song { SongId = 3, Title = "Song 1", ArtistId = 1, AlbumId = 1, Duration = TimeSpan.FromMinutes(3), Genre = "Pop" },
        new Song { SongId = 4, Title = "Song 2", ArtistId = 2, AlbumId = 2, Duration = TimeSpan.FromMinutes(4), Genre = "Rock" }
    };

            var mockRepository = new Mock<IPlaylistRepository>();
            mockRepository.Setup(repo => repo.GetSongsForPlaylist(playlistId))
                          .ReturnsAsync(songs);

            // Act
            var result = await mockRepository.Object.GetSongsForPlaylist(playlistId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, s => s.Title == "Song 1");
            Assert.Contains(result, s => s.Title == "Song 2");
        }
    }
}