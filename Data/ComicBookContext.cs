using Microsoft.EntityFrameworkCore;
using Zine.Data.Models;

namespace Zine.Data;

public class ComicBookContext : DbContext
{

	public DbSet<ComicBook> ComicBooks { get; set; }

	public ComicBookContext(DbContextOptions<ComicBookContext> options): base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ComicBook>().ToTable("ComicBook");
	}
}
