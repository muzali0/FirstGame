using Godot;
using System;

public partial class Player : Node2D
{
	[Export] public float Speed = 300f;          // Player movement speed
	[Export] public PackedScene BulletScene;     // Drag Bullet.tscn here
	[Export] public float FireRate = 0.3f;       // Time between shots

	private float _fireCooldown = 0f;

	public override void _Process(double delta)
	{
		HandleMovement((float)delta);
		HandleShooting((float)delta);
	}

	private void HandleMovement(float delta)
	{
		Vector2 velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_right")) velocity.X += 1;
		if (Input.IsActionPressed("ui_left"))  velocity.X -= 1;
		if (Input.IsActionPressed("ui_down"))  velocity.Y += 1;
		if (Input.IsActionPressed("ui_up"))    velocity.Y -= 1;

		velocity = velocity.Normalized() * Speed * delta;
		Position += velocity;
	}

	private void HandleShooting(float delta)
	{
		_fireCooldown -= (float)delta;

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

		Node2D bullet = (Node2D)BulletScene.Instantiate();
		bullet.Position = GlobalPosition;

		Vector2 direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
		bullet.Set("Direction", direction); // Bullet.cs will use this

		GetParent().AddChild(bullet);
	}
}
