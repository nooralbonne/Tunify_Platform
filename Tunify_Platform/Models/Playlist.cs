namespace Tunify_Platform.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Playlist_Name { get; set; }
        public DateTime Created_Date { get; set; }

        public ICollection<PlaylistSong> playlistSongs { get; set; }
    }
}
