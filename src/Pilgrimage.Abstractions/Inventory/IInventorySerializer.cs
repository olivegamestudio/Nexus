using System.IO;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

/// <summary>
/// Interface for serializing and deserializing player inventory data.
/// </summary>
public interface IInventorySerializer
{
    /// <summary>
    /// Serializes the player's inventory data to the provided stream.
    /// </summary>
    /// <param name="s">The stream to which the data will be serialized.</param>
    /// <param name="player">The player whose inventory data is to be serialized.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result"/> indicating the success or failure of the operation.</returns>
    Task<Result> Serialize(Stream s, PilgrimPlayer player);

    /// <summary>
    /// Deserializes the player's inventory data from the provided stream.
    /// </summary>
    /// <param name="s">The stream from which the data will be deserialized.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Player}"/> with the deserialized player data.</returns>
    Task<Result<PilgrimPlayer>> Deserialize(Stream s);
}
