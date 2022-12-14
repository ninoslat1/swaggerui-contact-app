using ContactApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
