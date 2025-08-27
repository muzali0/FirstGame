using Godot;

public partial class Bullet : Area2D
{
	[Export] public float Speed = 600f;
	public Vector2 Direction;

	public override void _Process(double delta)
	{
		Position += Direction * Speed * (float)delta;

		// Destroy if off-screen
		var viewport = GetViewportRect().Size;
		if (Position.X < 0 || Position.Y < 0 || Position.X > viewport.X || Position.Y > viewport.Y)
		{
			QueueFree();
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Enemy enemy)
		{
			enemy.TakeDamage(1);
			QueueFree();
		}
	}
}
