using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnlineShoppingDAL.Model
{
    [Table("Image")]
    public partial class Image
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Version { get; set; }
        [Required]
        [Column("Image")]
        [StringLength(300)]
        public string Image1 { get; set; }
        [Required]
        [StringLength(500)]
        public string UniqueImageName { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Images")]
        public virtual Product Product { get; set; }
    }
}
