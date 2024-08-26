namespace Tunify_Platform.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Album_Name { get; set; }
        public DateTime Release_Date { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
