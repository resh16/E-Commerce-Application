using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnlineShoppingDAL.Model
{
    public partial class Role
    {
        public Role()
        {
            RoleClaims = new HashSet<RoleClaim>();
            UserRoles = new HashSet<UserRole>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        [InverseProperty(nameof(RoleClaim.Role))]
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        [InverseProperty(nameof(UserRole.Role))]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
