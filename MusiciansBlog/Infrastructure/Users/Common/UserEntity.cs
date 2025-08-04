using MusiciansBlog.API.Infrastructure.Blogs.Common;
using MusiciansBlog.API.Infrastructure.Comments.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansBlog.API.Infrastructure.Users.Common
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Table("Users", Schema = "MusiciansBlog")]
    public sealed class UserEntity
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        [Column("id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        [Column("created")]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        [Column("email")]
        public required string Email { get; set; }

        /// <summary>
        /// Хэш пароля пользователя.
        /// </summary>
        [Column("passwordHash")]
        public required string PasswordHash { get; set; }

        /// <summary>
        /// Никнейм пользователя.
        /// </summary>
        [Column("username")]
        public required string Username { get; set; }

        /// <summary>
        /// Токен обновления.
        /// </summary>
        [Column("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Дата истечения токена обновления.
        /// </summary>
        [Column("refreshTokenExpiry")]
        public DateTime RefreshTokenExpiry { get; set; }

        /// <summary>
        /// Тип аутентификации.
        /// </summary>
        [Column("authType")]
        public AuthType AuthType { get; set; }

        public List<BlogEntity> Blogs { get; set; } = new();

        public List<CommentEntity> Comments { get; set; } = new();
    }
}
