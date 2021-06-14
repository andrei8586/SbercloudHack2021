namespace DataAccess.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DataAccess.Entities;

    using Microsoft.EntityFrameworkCore;

    internal class AppDbContext : DbContext
    {
        public DbSet<ChatEntity> Chats { get; set; }

        public DbSet<ChatMemberEntity> ChatMembers { get; set; }

        public DbSet<ChatMembersGroup> ChatMembersGroups { get; set; }
    }
}
