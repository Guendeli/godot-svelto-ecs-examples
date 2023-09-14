#if GODOT
using System;
using Godot;

namespace Svelto.ECS.Schedulers.Godot
{

    public class GodotEntitySubmissionScheduler : EntitiesSubmissionScheduler
    {
        readonly NodeScheduler        _scheduler;
        EnginesRoot.EntitiesSubmitter _onTick;

        public GodotEntitySubmissionScheduler(string name, Node parent)
        {
            
            _scheduler = new NodeScheduler();
            _scheduler.onTick = SubmitEntities;
            parent.AddChild(_scheduler);
        }

        private void SubmitEntities()
        {
            try
            {
                _onTick.SubmitEntities();
            }
            catch (Exception e)
            {
                paused = true;
                
                Svelto.Console.LogException(e);

                throw;
            }
        }

        protected internal override EnginesRoot.EntitiesSubmitter onTick
        {
            set => _onTick = value;
        }

        public override void Dispose()
        {
            if (_scheduler != null)
            {
                _scheduler.QueueFree();
            }
        }
    }

}
#endif