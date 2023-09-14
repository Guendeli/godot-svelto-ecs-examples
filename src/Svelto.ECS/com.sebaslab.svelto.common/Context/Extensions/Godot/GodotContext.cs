#if GODOT
using Godot;

namespace Svelto.Context
{
    public abstract partial class GodotContext : Node
    {

    }

    public partial class GodotContext<T> : GodotContext where T : class, ICompositionRoot, new()
    {
        public override void _EnterTree()
        {
            _applicationRoot = new T();

            _applicationRoot.OnContextCreated(this);
        }


        public override void _ExitTree()
        {
            _applicationRoot?.OnContextDestroyed(_hasBeenInitialised);
        }

        public override void _Ready()
        {
            WaitForFrameInit();
        }


        private async void WaitForFrameInit()
        {
            //let's wait until the end of the frame, so we are sure that all the awake and starts are called
            await ToSignal(GetTree(), "process_frame");

            _hasBeenInitialised = true;

            _applicationRoot.OnContextInitialized(this);
        }

        T _applicationRoot;
        bool _hasBeenInitialised;
    }
}
#endif