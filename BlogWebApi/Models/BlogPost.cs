using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.Models
{
    public class BlogPost
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string BlogHtml { get; set; }

    }
}
