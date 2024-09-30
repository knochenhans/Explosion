using Godot;

public partial class ExplosionNode : Node2D
{
	[Export]
	public ExplosionResource ExplosionResource { get; set; }

	public AnimatedSprite2D AnimatedSpriteNode => GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	public AudioStreamPlayer2D AudioStreamPlayerNode => GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	public Timer TimerNode => GetNode<Timer>("Timer");

	public bool AnimationFinished { get; private set; }
	public bool SoundFinished { get; private set; }

	public override void _Ready()
	{
		if (ExplosionResource == null)
		{
			GD.PrintErr("ExplosionResource is not set.");
			return;
		}

		AnimatedSpriteNode.SpriteFrames = ExplosionResource.Frames;
		AnimatedSpriteNode.AnimationFinished += OnAnimatedSpriteNodeAnimationFinished;
		AnimatedSpriteNode.Play();

		foreach (AudioStream audioStream in ExplosionResource.Sounds)
			(AudioStreamPlayerNode.Stream as AudioStreamRandomizer).AddStream(-1, audioStream);

		AudioStreamPlayerNode.Finished += OnAudioStreamPlayerFinished;
		AudioStreamPlayerNode.Play();

		if (ExplosionResource.MaxDuration > 0.0f)
		{
			TimerNode.WaitTime = ExplosionResource.MaxDuration;
			TimerNode.Timeout += OnTimerTimeout;
			TimerNode.Start();
		}
	}

	public void OnTimerTimeout() => QueueFree();

	public void OnAnimatedSpriteNodeAnimationFinished()
	{
		AnimationFinished = true;
		Visible = false;
		CheckQueueFree();
	}

	public void OnAudioStreamPlayerFinished()
	{
		SoundFinished = true;
		CheckQueueFree();
	}

	public void CheckQueueFree()
	{
		if (AnimationFinished && SoundFinished)
			QueueFree();
	}
}
