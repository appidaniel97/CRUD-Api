using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

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

        [IgnoreDataMember]
        [InverseProperty("CategoriaNavigation")]
        public virtual ICollection<Products> Products { get; set; }

    }
}
