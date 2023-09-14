
using System;
using Svelto.DataStructures;
using Svelto.ECS;
using Godot;

public class TickingEnginesGroup : UnsortedEnginesGroup<IStepEngine>
{
    public TickingEnginesGroup(FasterList<IStepEngine> engines) : base(engines)
    {
        var tickingSystem = new UpdateMe(Step);
        MainContext.Instance.AddChild(tickingSystem);
    }    
}

public partial class UpdateMe : Node
{
    internal Action update;

    public UpdateMe(Action update)
    {
        this.update = update;
    }

    public override void _Process(double delta)
    {
        update();
    }
}