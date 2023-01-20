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
    public virtual DbSet<NationalInsurance> NationalInsurance { get; set; }
    public virtual DbSet<Party> Party { get; set; }
    public virtual DbSet<Passport> Passport { get; set; }
    public virtual DbSet<Token> Token { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Vote> Vote { get; set; }
}