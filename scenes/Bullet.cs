using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public float Speed = 600f;
	[Export] public float Lifetime = 2f;

	public Vector2 Direction = Vector2.Zero;
	private float _lifeTimer;

	public override void _Ready()
	{
		_lifeTimer = Lifetime;
		BodyEntered += OnBodyEntered; // Connect collision signal
	}

	public override void _Process(double delta)
	{
		Position += Direction * Speed * (float)delta;

		_lifeTimer -= (float)delta;
		if (_lifeTimer <= 0)
			QueueFree();
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Enemy enemy)
		{
			enemy.Hit();                       // Destroy enemy
			QueueFree();                       // Destroy bullet
			KillCounter.Instance?.AddKill();   // Increment kill count
		}
	}
}
