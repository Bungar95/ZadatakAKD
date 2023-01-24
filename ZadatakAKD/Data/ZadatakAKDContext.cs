using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZadatakAKD.Models;

namespace ZadatakAKD.Data
{
    public class ZadatakAKDContext : DbContext
    {
        public ZadatakAKDContext (DbContextOptions<ZadatakAKDContext> options)
            : base(options)
        {
        }

        public DbSet<ZadatakAKD.Models.Autor> Autors { get; set; } = default!;

        public DbSet<ZadatakAKD.Models.Knjiga> Knjigas { get; set; }
    }
}
