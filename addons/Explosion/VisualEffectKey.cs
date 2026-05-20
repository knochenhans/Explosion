using Godot;

[GlobalClass, Tool]
public partial class VisualEffectKey : Resource
{
    [Export] public VisualEffectType Type;
    [Export] public MaterialType Material;
}