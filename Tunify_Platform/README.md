# Tunify Platform

## Project Overview
Tunify Platform is a web application that allows users to manage their music library, including subscriptions, playlists, and song collections. The application integrates with a SQL Server database to handle data storage and management, as outlined in the Entity-Relationship Diagram (ERD).

## Entity-Relationship Diagram
![ERD Diagram](https://github.com/nooralbonne/Tunify-Platform1/blob/master/Tunify.png)

## Models and Relationships

- **User**: Represents a user of the platform.
  - **UserId**: Unique identifier for the user.
  - **Username**: User’s name.
  - **Email**: User’s email address.
  - **Join_Date**: Date when the user joined.
  - **SubscriptionId**: References the Subscription.
  - **Subscription**: Navigation property to the Subscription.
  - **Playlists**: Collection of Playlists created by the user.

- **Subscription**: Represents a user's subscription.
  - **SubscriptionId**: Unique identifier for the subscription.
  - **Subscription_Type**: Type of subscription.
  - **Price**: Subscription price.
  - **Users**: Collection of Users with this subscription.

- **Playlist**: Represents a music playlist created by a user.
  - **PlaylistId**: Unique identifier for the playlist.
  - **UserId**: References the User who created the playlist.
  - **User**: Navigation property to the User.
  - **Playlist_Name**: Name of the playlist.
  - **Created_Date**: Date when the playlist was created.
  - **PlaylistSongs**: Collection of PlaylistSongs associated with this playlist.

- **Song**: Represents a song in the library.
  - **SongId**: Unique identifier for the song.
  - **Title**: Song title.
  - **ArtistId**: References the Artist.
  - **Artist**: Navigation property to the Artist.
  - **AlbumId**: References the Album.
  - **Album**: Navigation property to the Album.
  - **Duration**: Length of the song.
  - **Genre**: Genre of the song.
  - **PlaylistSongs**: Collection of PlaylistSongs associated with this song.

- **Artist**: Represents a music artist.
  - **ArtistId**: Unique identifier for the artist.
  - **Name**: Artist’s name.
  - **Bio**: Artist’s biography.
  - **Songs**: Collection of Songs by this artist.
  - **Albums**: Collection of Albums by this artist.

- **Album**: Represents a music album.
  - **AlbumId**: Unique identifier for the album.
  - **Album_Name**: Name of the album.
  - **Release_Date**: Date when the album was released.
  - **ArtistId**: References the Artist.
  - **Artist**: Navigation property to the Artist.
  - **Songs**: Collection of Songs in this album.

- **PlaylistSongs**: Junction table linking playlists and songs.
  - **PlaylistSongsId**: Unique identifier for the PlaylistSongs entry.
  - **PlaylistId**: References the Playlist.
  - **Playlist**: Navigation property to the Playlist.
  - **SongId**: References the Song.
  - **Song**: Navigation property to the Song.

## Navigation and Routing (Lab 13)

### Navigation Properties

- **Playlist**: Added `ICollection<PlaylistSongs>` to manage songs in playlists.
- **Song**: Added `ICollection<PlaylistSongs>` to manage playlist associations.
- **Artist**: Added `ICollection<Song>` for songs by the artist.
- **Album**: Added `ICollection<Song>` for songs in the album.

### Routing

#### Playlist-Song Relationships

1. **Add a Song to a Playlist**

   - **Endpoint**: `POST api/playlists/{playlistId}/songs/{songId}`
   - **Controller Action**:

     ```csharp
     [HttpPost("playlists/{playlistId}/songs/{songId}")]
     public async Task<IActionResult> AddSongToPlaylist(int playlistId, int songId)
     {
         // Implementation
     }
     ```

2. **Retrieve Songs in a Playlist**

   - **Endpoint**: `GET api/playlists/{playlistId}/songs`
   - **Controller Action**:

     ```csharp
     [HttpGet("playlists/{playlistId}/songs")]
     public async Task<ActionResult<IEnumerable<Song>>> GetSongsForPlaylist(int playlistId)
     {
         // Implementation
     }
     ```

#### Artist-Song Relationships

1. **Add a Song to an Artist**

   - **Endpoint**: `POST api/artists/{artistId}/songs/{songId}`
   - **Controller Action**:

     ```csharp
     [HttpPost("artists/{artistId}/songs/{songId}")]
     public async Task<IActionResult> AddSongToArtist(int artistId, int songId)
     {
         // Implementation
     }
     ```

2. **Retrieve Songs by an Artist**

   - **Endpoint**: `GET api/artists/{artistId}/songs`
   - **Controller Action**:

     ```csharp
     [HttpGet("artists/{artistId}/songs")]
     public async Task<ActionResult<IEnumerable<Song>>> GetSongsByArtist(int artistId)
     {
         // Implementation
     }
     ```

### Relationship Management

- **PlaylistService**: Added a method to handle adding a song to a playlist.
- **ArtistService**: Added a method to handle adding a song to an artist.

### Seeding Sample Data

- Updated the `OnModelCreating` method in `TunifyDbContext` to define composite keys for join tables and seed initial data for `Playlist`, `Song`, and `Artist` models.
- Applied the necessary migrations to update the database.
- Confirmed initial data was seeded successfully.

## Repository Design Pattern

### What is the Repository Design Pattern?
The Repository Design Pattern abstracts and encapsulates data access logic, promoting better organization and separation of concerns.


### Benefits

- **Separation of Concerns**: Keeps data access code separate from business logic.
- **Testability**: Simplifies unit testing by allowing repositories to be mocked.
- **Maintainability**: Centralizes data access logic for easier updates.
- **Flexibility**: Facilitates changes to data sources or methods.
- **Consistency**: Promotes consistent data access practices.

### How to Use

1. **Define Repository Interfaces**: Create interfaces for each entity.
2. **Implement Repository Services**: Implement the interfaces in the `Repositories/Services` folder.
3. **Refactor Controllers**: Use repository services instead of direct `DbContext` access.
4. **Register Services**: Register repository services in the `ConfigureServices` method of `Program.cs`.

## Final Steps

- Ensure all repositories are correctly implemented and controllers are refactored to use them.
- Update this `README.md` to include information on the Repository Design Pattern and its benefits.

## Swagger UI Integration (Lab 14)

### Overview
In Lab 14, we have integrated Swagger UI into the Tunify Platform. Swagger UI provides an interactive interface to visualize and interact with the API's endpoints, making it easier to understand and test the API.

![SQLDatabaswAzure](https://github.com/nooralbonne/Tunify_Platform1/blob/Identity/Assets/SQLDatabaswAzure.jpg)
![SwaggerUI](https://github.com/nooralbonne/Tunify_Platform1/blob/Identity/Assets/SwaggerUI1.jpg)

### Setup and Configuration

1. **Install Swashbuckle.AspNetCore**
   - Open the NuGet Package Manager Console.
   - Run the following command to install the Swashbuckle.AspNetCore package:
     ```shell
     Install-Package Swashbuckle.AspNetCore
     ```

2. **Configure Swagger in Program.cs**
   - Add the Swagger services in `Program.cs`:
     ```csharp
     builder.Services.AddSwaggerGen(options =>
     {
         options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
         {
             Title = "Tunify API",
             Version = "v1",
             Description = "API for managing playlists, songs, and artists in the Tunify Platform"
         });
     });
     ```

   - Enable Swagger and Swagger UI in the `Program.cs` file:
     ```csharp
     app.UseSwagger(options =>
     {
         options.RouteTemplate = "api/{documentName}/swagger.json";
     });

     app.UseSwaggerUI(options =>
     {
         options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tunify API v1");
         options.RoutePrefix = "";
     });
     ```

3. **Test the Swagger UI**
   - Launch the application and navigate to the root URL to view the Swagger UI.
   - Use the Swagger UI to interact with your API endpoints, ensuring that all endpoints are documented and functional.

### Accessing and Using Swagger UI

- After starting the application, navigate to the root URL (e.g., `http://localhost:5000/`) to access the Swagger UI.
- The Swagger UI provides a user-friendly interface to interact with the API. You can test endpoints, view request/response models, and see example payloads.

### Benefits of Swagger UI

- **Interactive Documentation**: Swagger UI allows you to view and test API endpoints directly from the browser.
- **Ease of Use**: Developers can quickly understand the API structure and available operations without referring to separate documentation.
- **Auto-Generated**: Documentation is automatically generated from the API's codebase, ensuring it's always up to date.

### Conclusion

With Swagger UI integrated, the Tunify Platform now includes comprehensive, interactive API documentation that enhances the developer experience by providing a clear and accessible way to explore and test the API.

## Identity Setup

The Tunify Platform uses ASP.NET Core Identity for user authentication and management. This section provides instructions on how to use the registration, login, and logout features.

### Registration


![Register200status](https://github.com/nooralbonne/Tunify_Platform1/blob/Identity/Assets/RegisterPic.jpg)

To register a new user:

1. **Navigate to the Registration Page**: Access the registration page at `/Identity/Account/Register`.
2. **Fill Out the Registration Form**:
   - **Username**: Enter a unique username.
   - **Email**: Provide a valid email address.
   - **Password**: Create a strong password (at least 6 characters).
   - **Confirm Password**: Re-enter the password for confirmation.
3. **Submit the Form**: Click on the 'Register' button to create your account.

After successful registration, you will be redirected to the login page.

### Login

![Login200status](https://github.com/nooralbonne/Tunify_Platform1/blob/Identity/Assets/LoginPic200status.jpg)

To log in to your account:

1. **Navigate to the Login Page**: Access the login page at `/Identity/Account/Login`.
2. **Fill Out the Login Form**:
   - **Username or Email**: Enter your registered username or email.
   - **Password**: Enter your password.
3. **Submit the Form**: Click on the 'Login' button to access your account.

If the login credentials are correct, you will be redirected to the home page or the last accessed page.

### Logout

![Logout200status](https://github.com/nooralbonne/Tunify_Platform1/blob/Identity/Assets/logout.jpg)

To log out of your account:

1. **Access the Logout Feature**: You can log out by clicking on the 'Logout' button located in the navigation menu or by visiting `/Identity/Account/Logout`.
2. **Confirm Logout**: You will be logged out and redirected to the home page or login page.

For additional security, ensure to log out from shared or public computers to protect your account.


## JWT-Based Authentication and Authorization

### Setting Up JWT Authentication

1. **Install Required Package**
   - Use NuGet Package Manager to install `Microsoft.AspNetCore.Authentication.JwtBearer`.

2. **Configure JWT Authentication**
   - In `Program.cs`, configure JWT authentication:

     ```csharp
     builder.Services.AddAuthentication(
         options =>
         {
             options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         }
     ).AddJwtBearer(
         options =>
         {
             options.TokenValidationParameters = JwtTokenService.ValidateToken(builder.Configuration);
         }
     );
     ```

   - Ensure the authentication and authorization middleware is added:

     ```csharp
     app.UseAuthentication();
     app.UseAuthorization();
     ```

### Repository Interfaces and Services

1. **Extend IAccount Interface**
   - Add a method for generating JWT tokens:

     ```csharp
     Task<string> GenerateJwtToken(ApplicationUser user);
     ```

2. **Implement JwtTokenService**
   - Handle token creation, validation, and claims in `JwtTokenService`.

3. **Update IdentityAccountService**
   - Implement `GenerateJwtToken` method in `IdentityAccountService` to create JWT tokens.

### Updating AccountController

1. **Modify Login Action**
   - Generate and return a JWT token on successful authentication:

     ```csharp
     [HttpPost("Login")]
     public async Task<ActionResult<LoginDto>> Login(LoginDto loginDto)
     {
         var user = await _accountService.UserAuthentication(loginDto.Username, loginDto.Password);

         if (user == null)
         {
             return Unauthorized();
         }

         var token = await _accountService.GenerateJwtToken(user);
         user.Token = token;

         return Ok(user);
     }
     ```

2. **Include Claims in Token**
   - Ensure the JWT token includes necessary claims such as user roles and custom claims.

### Securing API Endpoints

1. **Use [Authorize] Attributes**
   - Secure endpoints using `[Authorize]` attributes:

     ```csharp
     [Authorize]
     [HttpGet("Profile")]
     public async Task<ActionResult<LoginDto>> Profile()
     {
         return await _accountService.UserProfile(User);
     }
     ```

2. **Implement Role-Based and Policy-Based Authorization**
   - Ensure that endpoints are protected based on user roles and claims as needed.

### Handling Claims and Roles

1. **Seed Roles and Users**
   - Define roles and seed initial data in `TunifyDbContext`:

     ```csharp
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<IdentityRole>().HasData(
             new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
             new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
         );

         modelBuilder.Entity<ApplicationUser>().HasData(
             new ApplicationUser
             {
                 Id = "1",
                 UserName = "admin",
                 NormalizedUserName = "ADMIN",
                 Email = "admin@tunify.com",
                 NormalizedEmail = "ADMIN@TUNIFY.COM",
                 PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "AdminPassword")
             }
         );
     }
     ```

2. **Run Migrations**
   - Ensure migrations are applied to update the database with roles and claims.


 - ## Access Admin Features

### Accessing Admin Features via Postman

1. **Set Up Postman**
   - Open Postman and create a new request.
   ![Postman Setup](path/to/postman-setup.png)

2. **Authenticate**
   - Obtain a JWT token using the `Login` endpoint.
     - **POST** `http://localhost:5046/api/Account/Login`
     - **Body (JSON):**
       ```json
       {
        "id": "79",
       "userName": "Admin",
       "password": "34827@ADFLGMjnjkn",
       "roles": [
       "Admin"
         ],
       "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjRhMjhhOTA5LWJmZWUtNGZjZC1hMjg2LTYwYTZkZjU2Y2U5YSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6IkFkbWluQGdtYWlsLmNvbSIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiVkRaUEtWNVlKV1NMN05XN1RCV0VMRFNOR0NSUjdVTFAiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTcyNDg0MTYwNH0.tmiWUqXr0ZTTbfO8A2N8QMh8OtC0mAYfW561j3DQk0E"
       }
       ```
     - Copy the `Token` from the response.
   ![Postman Login Request](path/to/postman-login-request.png)


### Accessing Admin Features via Swagger UI

1. **Open Swagger UI**
   - Navigate to `http://localhost:5046/index.html` in your browser.
   ![Swagger UI](path/to/swagger-ui.png)

2. **Authenticate**
   - Obtain a JWT token using the `Login` endpoint as described above.
   ![Swagger Auth Button](path/to/swagger-auth-button.png)

3. **Add Authorization Header**
   - In Swagger UI, click on the "Authorize" button.
   - Enter the token in the format `Bearer <your-token>` and click "Authorize".
   ![Swagger Auth Modal](path/to/swagger-auth-modal.png)

4. **Test Admin Endpoints**
   - Once authorized, test admin endpoints directly from Swagger UI.
   - Find the admin endpoints in the Swagger documentation and make requests to them with the token applied.
   ![Swagger Admin Request](path/to/swagger-admin-request.png)

## Notes

- Replace `path/to/` with the actual paths to your screenshots.
- Ensure that the JWT token used has appropriate claims and roles to access the admin features.
- Update the `your-api-url` placeholder with the actual URL of your API.

---

For further customization or advanced usage, refer to the [ASP.NET Core Identity Documentation](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity).
