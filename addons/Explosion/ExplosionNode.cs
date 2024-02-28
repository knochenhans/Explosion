using Godot;

public partial class ExplosionNode : Node2D
{
	[Export]
	public ExplosionResource ExplosionResource { get; set; }

	public AnimatedSprite2D AnimatedSpriteNode { get; private set; }
	public AudioStreamPlayer2D AudioStreamPlayerNode { get; private set; }
	public Timer TimerNode { get; private set; }

	public bool AnimationFinished { get; private set; }
	public bool SoundFinished { get; private set; }

	public override void _Ready()
	{
		if (ExplosionResource == null)
		{
			GD.PrintErr("ExplosionResource is not set.");
			return;
		}

		AnimatedSpriteNode = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		AnimatedSpriteNode.SpriteFrames = ExplosionResource.Frames;
		AnimatedSpriteNode.AnimationFinished += _OnAnimatedSpriteNodeAnimationFinished;
		AnimatedSpriteNode.Play();

		AudioStreamPlayerNode = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		AudioStreamPlayerNode.Stream = ExplosionResource.Sound;
		AudioStreamPlayerNode.Finished += _OnAudioStreamPlayerFinished;
		AudioStreamPlayerNode.Play();

		TimerNode = GetNode<Timer>("Timer");
		if (ExplosionResource.MaxDuration > 0.0f)
		{
			TimerNode.WaitTime = ExplosionResource.MaxDuration;
			TimerNode.Timeout += _OnTimerTimeout;
			TimerNode.Start();
		}
	}

	public void _OnTimerTimeout() => QueueFree();

	public void _OnAnimatedSpriteNodeAnimationFinished()
	{
		AnimationFinished = true;
		Visible = false;
		CheckQueueFree();
	}

	public void _OnAudioStreamPlayerFinished()
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
