using Godot;
using Svelto.DataStructures;

public class OOPManager
{
    readonly FasterList<Node3D> objects = new FasterList<Node3D>();
    private const string PREFAB_ROOT = "res://prefabs/Sphere.tscn"; //

    internal uint ResiterEntity()
    {
        Node3D gameObject = GD.Load<PackedScene>(PREFAB_ROOT).Instantiate() as Node3D;
        if (gameObject != null)
        {
            MainContext.Instance.AddChild(gameObject);
            objects.Add(gameObject);

            return (uint)(objects.count - 1);
        }

        return 0;
    }

    internal void SetParent(uint index, in uint parent)
    {
        if (objects[index].GetParent() != null)
        {
            objects[index].GetParent().RemoveChild(objects[index]);
        }
        
        objects[parent].AddChild(objects[index]);
    }

    internal void SetPosition(uint index, in Vector3 position)
    {
        objects[index].Position = position;
    }
}