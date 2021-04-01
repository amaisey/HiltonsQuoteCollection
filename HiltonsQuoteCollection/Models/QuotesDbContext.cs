using System;
using Microsoft.EntityFrameworkCore;

namespace HiltonsQuoteCollection.Models
{
    public class QuotesDbContext : DbContext
    {
        public QuotesDbContext(DbContextOptions<QuotesDbContext> options) : base(options)
        {

        }

        public DbSet<Quotes> Quotes { get; set; }
    }
}
