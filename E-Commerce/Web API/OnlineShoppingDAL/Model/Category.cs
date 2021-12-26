using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnlineShoppingDAL.Model
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("Created_At", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(AppUser.CategoryCreatedByNavigations))]
        public virtual AppUser CreatedByNavigation { get; set; }
        [ForeignKey(nameof(ModifiedBy))]
        [InverseProperty(nameof(AppUser.CategoryModifiedByNavigations))]
        public virtual AppUser ModifiedByNavigation { get; set; }
        [InverseProperty(nameof(Product.Category))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
