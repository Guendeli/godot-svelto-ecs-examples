
using System;
using Svelto.ECS;

public class SyncTransformEngine : IStepEngine, IQueryingEntitiesEngine
{
    private readonly OOPManager _oopManager;

    public SyncTransformEngine(OOPManager oopManager)
    {
        _oopManager = oopManager;
    }
    public void Step()
    {
        foreach (var  ((transforms, indices, count), _) in entitiesDB.QueryEntities<TransformComponent, ObjectIndexComponent>(LayerGroups.Primitive.Groups))
        {
            for(int i = 0; i < count; i++)
            {
                _oopManager.SetPosition(indices[i].Index, transforms[i].position);
            }
        }
    }

    public string name => nameof(SyncTransformEngine);
    public void Ready() { }

    public EntitiesDB entitiesDB { get; set; }
    
}