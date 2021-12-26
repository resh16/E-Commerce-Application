using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnlineShoppingDAL.Model
{
    public partial class UserRole
    {
        [Key]
        public Guid UserId { get; set; }
        [Key]
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("UserRoles")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AppUser.UserRoles))]
        public virtual AppUser User { get; set; }
    }
}
