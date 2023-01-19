using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class VotingContext : DbContext
{
    public VotingContext(DbContextOptions<VotingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Election> Election { get; set; }
    public virtual DbSet<Party> Party { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Vote> Vote { get; set; }
}