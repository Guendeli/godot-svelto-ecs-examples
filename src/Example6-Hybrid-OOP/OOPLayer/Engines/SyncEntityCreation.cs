
using Svelto.ECS;

public class SyncEntityCreation : IReactOnAddEx<ObjectIndexComponent>
{
    readonly OOPManager _oopManager;

    public SyncEntityCreation(OOPManager oopManager)
    {
        _oopManager = oopManager;
    }

    
    public void Add((uint start, uint end) rangeOfEntities, in EntityCollection<ObjectIndexComponent> entities, ExclusiveGroupStruct groupID)
    {
        var (objectentities, entityIDs, _) = entities;
        for (uint i = rangeOfEntities.start; i < rangeOfEntities.end; i++)
        {
            objectentities[i].Index = _oopManager.ResiterEntity();
        }
    }
}