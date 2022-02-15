using UnityEngine;

namespace Platformer2D
{
    public interface IItem
    {
        string Name { get; }
        Sprite Sprite { get; }
        GameObject Prefab { get; }
        int Id { get; }
    }
}
