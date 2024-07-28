using Microsoft.EntityFrameworkCore;

namespace StudenManagementBackEnd.Models
{
    public class StudenContext: DbContext
    {
        // short cut ctor of contructor
        public StudenContext(DbContextOptions<StudenContext> options) : base(options) // theory is not done yet
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
