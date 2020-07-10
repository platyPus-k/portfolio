using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wordNote.Models;

    public class RazorPagesUserContext : DbContext
    {
        public RazorPagesUserContext (DbContextOptions<RazorPagesUserContext> options)
            : base(options)
        {
        }

        public DbSet<wordNote.Models.User> User { get; set; }
    }
