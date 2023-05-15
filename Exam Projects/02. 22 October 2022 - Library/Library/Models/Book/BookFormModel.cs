using System.ComponentModel.DataAnnotations;

namespace Library.Models.Book
{
    using static Data.DataConstants.Book;

    public class BookFormModel
    {
        public BookFormModel()
        {
            Categories = new List<CategoryItemModel>();
        }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AuthorMaxLegnth, MinimumLength = AuthorMinLegnth)]
        public string Author { get; set; } = null!;

        [Range(typeof(decimal), "0.0", "10.0")]
        public decimal Rating { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public IEnumerable<CategoryItemModel> Categories { get; set; } = null!;
    }
}
