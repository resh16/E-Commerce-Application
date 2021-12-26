using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnlineShoppingDAL.Model
{
    [Table("product")]
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<Image>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StockDate { get; set; }
        public int NoOfStock { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpiryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        [ForeignKey(nameof(BrandId))]
        [InverseProperty("Products")]
        public virtual Brand Brand { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(AppUser.ProductCreatedByNavigations))]
        public virtual AppUser CreatedByNavigation { get; set; }
        [ForeignKey(nameof(ModifiedBy))]
        [InverseProperty(nameof(AppUser.ProductModifiedByNavigations))]
        public virtual AppUser ModifiedByNavigation { get; set; }
        [InverseProperty(nameof(Image.Product))]
        public virtual ICollection<Image> Images { get; set; }
    }
}
