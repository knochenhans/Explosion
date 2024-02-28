using Godot;

public partial class ExplosionResource : Resource
{
    [Export]
    public float MaxDuration { get; set; } = 0.0f;

    [Export]
    public SpriteFrames Frames { get; set; }

    [Export]
    public AudioStream Sound { get; set; }
}