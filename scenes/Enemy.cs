using Godot;
using System;

public partial class Enemy : Node2D
{
	[Export] public float Speed = 100f;

	public override void _Process(double delta)
	{
		// Simple movement: move down slowly
		Position += Vector2.Down * Speed * (float)delta;

		// If enemy goes off screen, remove it
		if (Position.Y > 1000) QueueFree();
	}

	public void Hit()
	{
		// Call this when hit by bullet
		QueueFree();
	}
}
