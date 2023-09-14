
using System;
using Svelto.ECS;

public class SyncHierarchyEngine : IStepEngine, IQueryingEntitiesEngine, IDisposable
{
    private readonly OOPManager _oopManager;
    private Consumer<ObjectParentComponent> _consumer;
    
    public SyncHierarchyEngine(OOPManager oopManager, IEntityStreamConsumerFactory generateConsumerFactory, uint maxQuantity)
    {
        _oopManager = oopManager;
        _consumer = generateConsumerFactory.GenerateConsumer<ObjectParentComponent>("SyncHierarchyEngine", maxQuantity);
    }
    public void Step()
    {
        while (_consumer.TryDequeue(out var entity, out var id))
        {
            _oopManager.SetParent(entitiesDB.QueryEntity<ObjectIndexComponent>(id).Index, entity.parentIndex);
        }

    }

    public string name => nameof(SyncTransformEngine);
    public void Ready()
    {
    }

    public void Dispose()
    {
        _consumer.Dispose();
    }

    public EntitiesDB entitiesDB { get; set; }
}