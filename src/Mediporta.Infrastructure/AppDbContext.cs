using Mediporta.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mediporta.Infrastructure;

public class AppDbContext : DbContext
{
    public virtual DbSet<Tag> Tags { get; set; }
}