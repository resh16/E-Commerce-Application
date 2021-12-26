using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnlineShoppingDAL.Model
{
    [Table("AppUser")]
    public partial class AppUser
    {
        public AppUser()
        {
            BrandCreatedByNavigations = new HashSet<Brand>();
            BrandModifiedByNavigations = new HashSet<Brand>();
            CategoryCreatedByNavigations = new HashSet<Category>();
            CategoryModifiedByNavigations = new HashSet<Category>();
            ProductCreatedByNavigations = new HashSet<Product>();
            ProductModifiedByNavigations = new HashSet<Product>();
            UserClaims = new HashSet<UserClaim>();
            UserLogins = new HashSet<UserLogin>();
            UserRoles = new HashSet<UserRole>();
            UserTokens = new HashSet<UserToken>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        [InverseProperty(nameof(Brand.CreatedByNavigation))]
        public virtual ICollection<Brand> BrandCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Brand.ModifiedByNavigation))]
        public virtual ICollection<Brand> BrandModifiedByNavigations { get; set; }
        [InverseProperty(nameof(Category.CreatedByNavigation))]
        public virtual ICollection<Category> CategoryCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Category.ModifiedByNavigation))]
        public virtual ICollection<Category> CategoryModifiedByNavigations { get; set; }
        [InverseProperty(nameof(Product.CreatedByNavigation))]
        public virtual ICollection<Product> ProductCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Product.ModifiedByNavigation))]
        public virtual ICollection<Product> ProductModifiedByNavigations { get; set; }
        [InverseProperty(nameof(UserClaim.User))]
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        [InverseProperty(nameof(UserLogin.User))]
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        [InverseProperty(nameof(UserRole.User))]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [InverseProperty(nameof(UserToken.User))]
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
