using Godot;
using System;

public partial class Player : Node2D
{
	[Export] public float Speed = 300f;     // Movement speed in pixels/sec
	[Export] public PackedScene BulletScene; // Drag your Bullet scene here
	[Export] public float FireRate = 0.3f;   // Time between shots in seconds


	private Vector2 _velocity = Vector2.Zero;
	private float _fireCooldown = 0f;

	public override void _Process(double delta)
	{
		HandleMovement((float)delta);
		HandleShooting((float)delta);
	}

	private void HandleMovement(float delta)
	{
		_velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_right")) _velocity.X += 1;
		if (Input.IsActionPressed("ui_left"))  _velocity.X -= 1;
		if (Input.IsActionPressed("ui_down"))  _velocity.Y += 1;
		if (Input.IsActionPressed("ui_up"))    _velocity.Y -= 1;

		_velocity = _velocity.Normalized() * Speed * delta;
		Position += _velocity;
	}

	private void HandleShooting(float delta)
	{
		_fireCooldown -= delta;

		if (_fireCooldown > 0) return;

		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			ShootAtMouse();
			_fireCooldown = FireRate;
		}
	}

	private void ShootAtMouse()
	{
		if (BulletScene == null) return;

		var bullet = (Node2D)BulletScene.Instantiate();
		bullet.Position = GlobalPosition;

		// Calculate direction toward mouse
		Vector2 direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
		bullet.Set("Direction", direction); // We'll define Direction in Bullet.cs

		GetParent().AddChild(bullet);
	}
}
