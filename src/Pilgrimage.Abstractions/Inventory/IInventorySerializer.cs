using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

public interface IInventorySerializer
{
    Task<Result> Serialize(Player player);

    Task<Result<Player>> Deserialize();
}
