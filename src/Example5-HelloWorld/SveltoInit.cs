using Godot;
using System;
using Svelto.ECS;
using Svelto.ECS.Schedulers;

public static class ExclusiveGroups
{
	public static ExclusiveGroup group0 = new ExclusiveGroup();
	public static ExclusiveGroup group1 = new ExclusiveGroup();
}

public partial class SveltoInit : Node
{
	private EnginesRoot _enginesRoot;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var simpleEntitiesSubmissionScheduler = new SimpleEntitiesSubmissionScheduler();
		_enginesRoot = new EnginesRoot(simpleEntitiesSubmissionScheduler);

		var entityFactory = _enginesRoot.GenerateEntityFactory();
		var entityFunctions = _enginesRoot.GenerateEntityFunctions();
		
		// Add System 
		var system = new BehaviourForEntityEngine(entityFunctions);
		_enginesRoot.AddEngine(system);
		
		// Create Entity
		entityFactory.BuildEntity<EntityDescriptor>(new EGID(0, ExclusiveGroups.group0));
		
		simpleEntitiesSubmissionScheduler.SubmitEntities();
		
		// update system
		system.Update();
		
		GD.Print("Done");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

public struct EntityComponent : IEntityComponent
{
	public int Counter;
}

class EntityDescriptor : GenericEntityDescriptor<EntityComponent>{}

public class BehaviourForEntityEngine : IReactOnAddEx<EntityComponent>, IReactOnSwapEx<EntityComponent>, IQueryingEntitiesEngine
{
	private readonly IEntityFunctions _entityFunctions;

	public BehaviourForEntityEngine(IEntityFunctions entityFunctions)
	{
		_entityFunctions = entityFunctions;
	}
	public void Add((uint start, uint end) rangeOfEntities, in EntityCollection<EntityComponent> entities, ExclusiveGroupStruct groupID)
	{
		var (_, entityIDs, _) = entities;
		
		for(uint i = rangeOfEntities.start; i < rangeOfEntities.end; i++)
		{
			_entityFunctions.SwapEntityGroup<EntityDescriptor>(new EGID(entityIDs[i], groupID),ExclusiveGroups.group1);
		}
	}

	public void MovedTo((uint start, uint end) rangeOfEntities, in EntityCollection<EntityComponent> entities, ExclusiveGroupStruct fromGroup,
		ExclusiveGroupStruct toGroup)
	{
		for (uint i = rangeOfEntities.start; i < rangeOfEntities.end; i++)
		{
			GD.Print("Swap happened");
		}
	}

	public void Ready()
	{
	}

	public void Update()
	{
		var (components, entityIDs, count) = entitiesDB.QueryEntities<EntityComponent>(ExclusiveGroups.group1);
		uint entityID;
		
		for (int i = 0; i < count; i++)
		{
			components[i].Counter++;
			entityID = entityIDs[i];
		}
		
		GD.Print("Entity struct engine executed");
		
	}

	public EntitiesDB entitiesDB { get; set; }
}