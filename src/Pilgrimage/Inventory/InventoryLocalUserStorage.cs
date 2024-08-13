using System;

namespace Pilgrimage;

public class InventoryLocalUserStorage : IInventoryFileStorage
{
    public string Path => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "battleforce-savegame.json");
}
