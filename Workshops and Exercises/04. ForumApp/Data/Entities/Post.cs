using System.ComponentModel.DataAnnotations;

namespace ForumApp.Data.Entities
{
    using static ForumApp.Data.DataConstants.Post;

    public class Post
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(ContentMaxLength)]
        [Required]
        public string Content { get; set; } = null!;
    }
}
