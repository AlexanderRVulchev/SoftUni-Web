using System.ComponentModel.DataAnnotations;

namespace TextSplitterApp.Models
{
    public class TextViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Text field is required")]
        [MinLength(2)]
        [MaxLength(30)]
        public string Text { get; set; } = null!;

        public string SplitText { get; set; } = null!;
    }
}
