using Godot;

public partial class Enemy : CharacterBody2D
{
	[Export] public int Health = 3;
	[Export] public float Speed = 100f;

	// Reference to KillCounter; will be assigned by EnemySpawner
	public KillCounter KillCounter;

	private Player _player;

	public override void _Ready()
	{
		// Dynamically find the Player in the scene
		_player = GetTree().Root.GetNode<Player>("World/Player"); // Adjust path to your main scene

		if (_player == null)
			GD.PrintErr("Player node not found!");

		// Connect enemy hitbox to damage the player
		Area2D hitbox = GetNode<Area2D>("Hitbox");
		if (hitbox != null)
			hitbox.BodyEntered += OnHitboxBodyEntered;
		else
			GD.PrintErr("Enemy hitbox not found!");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_player == null) return;

		// Move toward the player
		Vector2 direction = (_player.GlobalPosition - GlobalPosition).Normalized();
		Velocity = direction * Speed;
		MoveAndSlide();
	}

	// Called by bullets when hit
	public void TakeDamage(int amount)
	{
		Health -= amount;

		if (Health <= 0)
		{
			KillCounter?.AddKill(); // Safely increment kill counter if assigned
			QueueFree();
		}
	}

	// Called when enemy hitbox touches player
	private void OnHitboxBodyEntered(Node body)
	{
		if (body is Player player)
		{
			player.TakeDamage(1);
		}
	}
}
