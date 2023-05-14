using MessagePack.Formatters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities
{
    using static Data.DataConstants.Book;

    public class Book
    {
        public Book()
        {
            ApplicationUsersBooks = new List<ApplicationUserBook>();
        }
        
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;
        
        [Required]
        [StringLength(AuthorMaxLegnth)]
        public string Author { get; set; } = null!;
        
        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;
        
        [Required]
        public string ImageUrl { get; set; } = null!;
        
        [Required]
        public decimal Rating { get; set; }
        
        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        
        public ICollection<ApplicationUserBook> ApplicationUsersBooks { get; set; } = null!;
    }
}
