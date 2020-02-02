using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bds.Tech.Test.Model
{
    public interface ISearchEngine
    {
        Task<IList<Result>> GetResultsAsync(string searchTerm);
    }
}
