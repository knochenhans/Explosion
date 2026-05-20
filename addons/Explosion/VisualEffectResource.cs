using Godot;

[GlobalClass]
public partial class VisualEffectResource : Resource
{
    // Leave duration at 0.0f to use the duration of the animation or sound
    [Export] public float MaxDuration = 0.0f;
    [Export] public SpriteFrames Frames;
    [Export] public SoundSetResource SoundSet = new();
    [Export] public Color Modulate = Colors.White;
    [Export] public Color LightColor = Colors.White;
    [Export] public float LightEnergy = 0.0f;
}