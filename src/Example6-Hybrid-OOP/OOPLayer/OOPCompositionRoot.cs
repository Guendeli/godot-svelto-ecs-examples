
using Svelto.DataStructures;
using Svelto.ECS;

public static class OOPCompositionRoot
{
    public static void Compose(EnginesRoot enginesRoot, FasterList<IStepEngine> tickingEnginesGroup, uint maxQuantity)
    {
        var oopManager = new OOPManager();

        var syncEngine = new SyncTransformEngine(oopManager);
        var syncHierarchyEngine = new SyncHierarchyEngine(oopManager, enginesRoot.GenerateConsumerFactory(), maxQuantity);
        
        enginesRoot.AddEngine(syncEngine);
        enginesRoot.AddEngine(syncHierarchyEngine);
        enginesRoot.AddEngine(new SyncEntityCreation(oopManager));

        tickingEnginesGroup.Add(syncEngine);
        tickingEnginesGroup.Add(syncHierarchyEngine);
    }
}