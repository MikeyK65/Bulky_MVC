using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }             // If name is Id then EF will auto assume it is the PK
        [Required]
        [AllowNull]
        [MaxLength(100)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order Must be less than 100")]
        public int DisplayOrder { get; set; }

    }
}
