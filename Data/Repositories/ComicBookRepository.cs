using Microsoft.AspNetCore.Components;
using Zine.Data.Models;

namespace Zine.Data.Repositories;

public class ComicBookRepository : IRepository<ComicBook>
{
	private readonly ComicBookContext _context;

	public ComicBookRepository(ComicBookContext context)
	{
		_context = context;
	}

	public IEnumerable<ComicBook> GetAll()
	{
		return _context.ComicBooks.OrderBy(comic => comic.Title).ToList();
	}
}
