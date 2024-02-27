using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions options) : base(options) { }
        DbSet<DemoEntity> DemoVersionTable { get; set; }
        public DbSet<User> UserTable { get; set; }
        public DbSet<NoteEntity> NoteTable { get; set; }
    }
}
