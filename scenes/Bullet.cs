using Godot;
using System;

public partial class Bullet : Node2D
{
	[Export] public float Speed = 600f;        // Bullet speed
	[Export] public float Lifetime = 2f;       // Bullet lifetime in seconds

	public Vector2 Direction = Vector2.Zero;    // Set by Player.cs

	private float _lifeTimer;

	public override void _Ready()
	{
		_lifeTimer = Lifetime;
	}

	public override void _Process(double delta)
	{
		float d = (float)delta;
		Position += Direction * Speed * d;

		_lifeTimer -= d;
		if (_lifeTimer <= 0) QueueFree();
	}
}
