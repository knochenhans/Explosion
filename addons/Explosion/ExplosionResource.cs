using System.Reflection;
using Godot;

public partial class ExplosionResource : Resource
{

    [Export]
    // Leave duration at 0.0f to use the duration of the animation or sound
    public float MaxDuration { get; set; } = 0.0f;

    [Export]
    public SpriteFrames Frames { get; set; }

    [Export]
    public AudioStream Sound { get; set; }
}