using Godot;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 200f;
	[Export] public PackedScene BulletScene;
	[Export] public int MaxHealth = 5;

	private int currentHealth;
	private float shootCooldown = 0.2f;
	private float shootTimer = 0f;

	public override void _Ready()
	{
		currentHealth = MaxHealth;
	}

	public override void _Process(double delta)
	{
		HandleMovement(delta);
		HandleShooting((float)delta);
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_right"))
			velocity.X += 1;
		if (Input.IsActionPressed("ui_left"))
			velocity.X -= 1;
		if (Input.IsActionPressed("ui_down"))
			velocity.Y += 1;
		if (Input.IsActionPressed("ui_up"))
			velocity.Y -= 1;

		Velocity = velocity.Normalized() * Speed;
		MoveAndSlide();
	}

	private void HandleShooting(float delta)
	{
		shootTimer -= delta;

		if (Input.IsActionPressed("shoot") && shootTimer <= 0)
		{
			shootTimer = shootCooldown;

			Bullet bullet = BulletScene.Instantiate<Bullet>();
			GetParent().AddChild(bullet);

			Vector2 mousePos = GetGlobalMousePosition();
			Vector2 dir = (mousePos - GlobalPosition).Normalized();

			bullet.Position = GlobalPosition;
			bullet.Direction = dir;
		}
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		GD.Print("Player HP: " + currentHealth);

		if (currentHealth <= 0)
		{
			GD.Print("Player died!");
			QueueFree();
		}
	}
}
