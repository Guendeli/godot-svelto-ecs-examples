using Godot;
using System;
using Svelto.Context;

public partial class MainContext : GodotContext<CompositionRoot>
{
    public static Node Instance;
    public string note = "Enable this context to check how oop can be abstracted using code layers";

    public override void _Ready()
    {
        base._Ready();
        Instance = GetTree().Root;
    }
}
