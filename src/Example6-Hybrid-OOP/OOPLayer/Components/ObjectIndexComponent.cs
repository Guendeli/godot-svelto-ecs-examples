
using Godot;
using Svelto.ECS;

public enum PrimitiveType
{
    Cube
}

public struct  ObjectIndexComponent : IEntityComponent
{
    public uint Index;
    public PrimitiveType Type;

    public ObjectIndexComponent(PrimitiveType type) : this()
    {
        this.Type = type;
    }
}

public struct ObjectParentComponent : IEntityComponent
{
    public uint parentIndex;
}

public class PrimitiveEntityDescriptor : GenericEntityDescriptor<TransformComponent, ObjectIndexComponent> { }

public class PrimitiveEntityWithParentDescriptor : ExtendibleEntityDescriptor<PrimitiveEntityDescriptor>
{
    public PrimitiveEntityWithParentDescriptor() : base(new IComponentBuilder[]
    {
        new ComponentBuilder<ObjectParentComponent>()
    }) {}
}

public struct TransformComponent : IEntityComponent
{
    public Vector3 position;

    public TransformComponent(Vector3 vector3) { position = vector3; }
}
