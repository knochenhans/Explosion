using Godot;

public partial class VisualEffect : Node2D
{
    [Export] public AnimatedSprite2D AnimatedSprite;
    [Export] public TimedAudioStreamPlayer2D AudioStreamPlayer;
    [Export] public Timer Timer;
    [Export] public PointLight2D PointLight;

    [Export] public VisualEffectResource VisualEffectResource;

    [Export] public float Force = 1f;

    int pendingOperations = 0;

    public override void _Ready()
    {
        if (VisualEffectResource == null)
        {
            Logger.LogError("VisualEffectResource is not set.", "VisualEffect", Logger.LogTypeEnum.World);
            return;
        }

        if (VisualEffectResource.Frames != null)
        {
            AnimatedSprite.SpriteFrames = VisualEffectResource.Frames;
            AnimatedSprite.AnimationFinished += () =>
            {
                pendingOperations--;
                CheckFinished();
            };
            AnimatedSprite.Modulate = VisualEffectResource.Modulate;
            AnimatedSprite.Play();
        }

        AudioStreamPlayer.ApplySoundSet(VisualEffectResource.SoundSet);

        AudioStreamPlayer.Finished += OnAudioStreamPlayerFinished;
        AudioStreamPlayer.Play();

        if (VisualEffectResource.LightEnergy > 0f)
        {
            PointLight.Color = VisualEffectResource.LightColor;
            PointLight.Energy = VisualEffectResource.LightEnergy * Force;
            PointLight.Visible = true;
        }

        if (VisualEffectResource.MaxDuration > 0.0f)
        {
            Timer.WaitTime = VisualEffectResource.MaxDuration;
            Timer.Timeout += OnTimerTimeout;
            Timer.Start();
        }
    }

    public VisualEffect WithData(VisualEffectResource visualEffectResource, Vector2 position, float force = 1f)
    {
        VisualEffectResource = visualEffectResource;
        Position = position;
        Force = force;

        return this;
    }

    public void OnTimerTimeout() => QueueFree();

    public void OnAudioStreamPlayerFinished()
    {
        pendingOperations--;
        CheckFinished();
    }

    public void CheckFinished()
    {
        if (pendingOperations <= 0)
            QueueFree();
    }
}
