using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tunify_Platform.Migrations
{
    /// <inheritdoc />
    public partial class addedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "Bio", "Name" },
                values: new object[,]
                {
                    { 1, "American singer-songwriter, known for narrative songs about her personal life.", "Taylor Swift" },
                    { 2, "English singer-songwriter, known for his hit singles and acoustic performances.", "Ed Sheeran" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "SubscriptionId", "Price", "Subscription_Type" },
                values: new object[,]
                {
                    { 1, 9.9900000000000002, "Basic" },
                    { 2, 19.989999999999998, "Premium" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "AlbumId", "Album_Name", "ArtistId", "Release_Date" },
                values: new object[,]
                {
                    { 1, "1989", 1, new DateTime(2014, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Divide", 2, new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Join_Date", "SubscriptionId", "Username" },
                values: new object[,]
                {
                    { 1, "hamadanjo@gmail.com", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Moayad" },
                    { 2, "aya@gmail.com", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Aya" }
                });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "PlaylistId", "Created_Date", "Playlist_Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Morning Motivation", 1 },
                    { 2, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Evening Relaxation", 2 },
                    { 3, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workout Hits", 1 },
                    { 4, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Road Trip", 2 }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "SongId", "AlbumId", "ArtistId", "Duration", "Genre", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, new TimeSpan(0, 0, 3, 30, 0), "Pop", "Blank Space" },
                    { 2, 2, 2, new TimeSpan(0, 0, 4, 0, 0), "Pop", "Shape of You" },
                    { 3, 1, 1, new TimeSpan(0, 0, 3, 30, 0), "Pop", "Style" },
                    { 4, 2, 2, new TimeSpan(0, 0, 4, 0, 0), "Rock", "Castle on the Hill" }
                });

            migrationBuilder.InsertData(
                table: "PlaylistSongs",
                columns: new[] { "PlaylistSongId", "PlaylistId", "SongId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlaylistSongs",
                keyColumn: "PlaylistSongId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlaylistSongs",
                keyColumn: "PlaylistSongId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlaylistSongs",
                keyColumn: "PlaylistSongId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PlaylistSongs",
                keyColumn: "PlaylistSongId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Playlists",
                keyColumn: "PlaylistId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Playlists",
                keyColumn: "PlaylistId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Playlists",
                keyColumn: "PlaylistId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Playlists",
                keyColumn: "PlaylistId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "SongId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "AlbumId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "AlbumId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "ArtistId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "ArtistId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 2);
        }
    }
}
