using Godot;
using Svelto.ECS;

public class MoveSphereEngine : IStepEngine, IQueryingEntitiesEngine
{
    public void Step()
    {
        GroupsEnumerable<TransformComponent> group = entitiesDB.QueryEntities<TransformComponent>(LayerGroups.SpherePrimitive.Groups);
        foreach (var ((buffer,count), _) in group)
        {
            for (int i = 0; i < count; i++)
            {
                Oscillate(ref buffer[i].position, i);
            }
        }
    }

    public string name => nameof(MoveSphereEngine);
    public void Ready() { }

    public EntitiesDB entitiesDB { get; set; }

    private void Oscillate(ref Vector3 transformPosition, int i)
    {
        float timeInSec = (Time.GetTicksMsec() / 1000f);

        float xVariance = Mathf.Cos(timeInSec * 3 / Mathf.Pi);

        transformPosition.X = xVariance + i * 1.5f;
    }
}