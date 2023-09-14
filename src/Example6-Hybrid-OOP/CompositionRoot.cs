using Godot;
using System;
using Svelto.Context;
using Svelto.DataStructures;
using Svelto.ECS;
using Svelto.ECS.Schedulers.Godot;

public partial class CompositionRoot : ICompositionRoot
{
	const uint NUMBER_OF_CUBES   = 5;
	const uint NUMBER_OF_SPHERES = 10;
        
	EnginesRoot _enginesRoot;
	public void OnContextInitialized<T>(T contextHolder)
	{
		Compose<T>();
		CreateStartupEntities();
	}

	private void CreateStartupEntities()
	{
		var entityFactory = _enginesRoot.GenerateEntityFactory();

		for (uint i = 0; i < NUMBER_OF_SPHERES; i++)
		{
			var sphereInit =
				entityFactory.BuildEntity<PrimitiveEntityWithParentDescriptor>(i, LayerGroups.SpherePrimitive.BuildGroup);
			sphereInit.Init(new TransformComponent(new Vector3(i + 1.5f,0,0)));
			sphereInit.Init(new ObjectIndexComponent(PrimitiveType.Cube));
		}
	}

	private void Compose<T>()
	{
		var godotEntitySubmissionScheduler = new GodotEntitySubmissionScheduler("oop-abstraction", MainContext.Instance);
		_enginesRoot = new EnginesRoot(godotEntitySubmissionScheduler);
		
		// Add Systems Below

		var moveSpheresEngine = new MoveSphereEngine();
		var tickableSystems = new FasterList<IStepEngine>(moveSpheresEngine);
		var tickingEngineGroup = new TickingEnginesGroup(tickableSystems);
		
		_enginesRoot.AddEngine(tickingEngineGroup);
		_enginesRoot.AddEngine(moveSpheresEngine);
		
		OOPCompositionRoot.Compose(_enginesRoot, tickableSystems, NUMBER_OF_SPHERES);
	}

	public void OnContextDestroyed(bool hasBeenInitialised)
	{
		if (hasBeenInitialised)
			_enginesRoot.Dispose();
	}

	public void OnContextCreated<T>(T contextHolder)
	{
		
	}
}
