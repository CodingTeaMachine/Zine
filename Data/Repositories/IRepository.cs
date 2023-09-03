using Zine.Data.Models;

namespace Zine.Data.Repositories;

public interface IRepository<out T>
{
	IEnumerable<T> GetAll();
}
