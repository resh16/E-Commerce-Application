using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace OnlineShoppingDAL.Model
{
    public partial class UserLogin
    {
        [Key]
        public Guid LoginProvider { get; set; }
        [Key]
        public Guid ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AppUser.UserLogins))]
        public virtual AppUser User { get; set; }
    }
}
