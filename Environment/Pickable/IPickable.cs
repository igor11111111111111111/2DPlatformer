namespace Platformer2D
{
    public interface IPickable
    {
        int Id { get; set; }
        PrefabSaveData PrefabData { get; set; }
    }
}
