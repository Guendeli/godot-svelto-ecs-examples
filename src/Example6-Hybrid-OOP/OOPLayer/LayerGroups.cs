
using Svelto.ECS;

public static class LayerGroups
{
    public class Primitive : GroupTag<Primitive> { }
    
    public class Cube : GroupTag<Cube> { }
    public class Sphere : GroupTag<Sphere> { }

    //The specialised groups use the abstract Primitive tag so that the entities will be built in the 
    //expected groups
    public class CubePrimitive : GroupCompound<Primitive, Cube> { }
    public class SpherePrimitive : GroupCompound<Primitive, Sphere> { }
    
    public static ExclusiveGroup SphereGroup = new ExclusiveGroup();
}