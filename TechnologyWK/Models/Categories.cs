using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnologyWK.Models
{
    [Table("Category")]
    public class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        public int Id { get; set; }

        [Column("NameCategory")]
        [Display(Name = "Categoria")]
        public string NameCategory { get; set; }

        [InverseProperty("CategoriaNavigation")]
        public virtual ICollection<Products> Products { get; set; }

    }
}
