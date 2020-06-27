using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wordNote.Models;

    public class RazorPagesWordContext : DbContext
    {
        public RazorPagesWordContext (DbContextOptions<RazorPagesWordContext> options)
            : base(options)
        {
        }

        public DbSet<wordNote.Models.Word> Word { get; set; }
    }
