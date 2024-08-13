using System;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

public interface IItemService
{
    Task<Result> LoadItems();

    Task<Result<Item>> GetItem(int id);
    
    bool HasLoaded { get; set; }

    event EventHandler ItemsLoaded;
}
