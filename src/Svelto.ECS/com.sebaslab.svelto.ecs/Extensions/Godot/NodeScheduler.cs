
using Godot;

namespace Svelto.ECS.Schedulers.Godot
{
    public partial class NodeScheduler : Node
    {   
        internal System.Action onTick;

        public override void _Process(double delta)
        {
            onTick();
        }

        // public async void Routine()
        // {
        //     while (true)
        //     {
        //         await ToSignal(GetTree(), "process_frame");
        //
        //         onTick();
        //         
        //     }
        // }
    }
}