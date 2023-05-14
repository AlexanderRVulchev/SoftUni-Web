using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities
{
    using static Data.DataConstants.Category;

    public class Category
    {
        public Category()
        {
            Books = new List<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        
        public ICollection<Book> Books { get; set; } = null!;
    }
}
