using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene EnemyScene;
	[Export] public float SpawnInterval = 2f;
	[Export] public int MaxEnemies = 10;
	[Export] public Vector2 SpawnAreaSize = new Vector2(800, 450);
	[Export] public KillCounter KillCounter; // assign the KillCounter node here

	private float _spawnTimer = 0f;

	public override void _Process(double delta)
	{
		_spawnTimer -= (float)delta;

		if (_spawnTimer <= 0 && GetChildCount() < MaxEnemies)
		{
			SpawnEnemy();
			_spawnTimer = SpawnInterval;
		}
	}

	private void SpawnEnemy()
	{
		if (EnemyScene == null)
		{
			GD.PrintErr("EnemyScene not assigned!");
			return;
		}

		Enemy enemy = EnemyScene.Instantiate<Enemy>();

		// Random position inside spawn area
		Vector2 spawnPos = new Vector2(
			(float)GD.RandRange(0, SpawnAreaSize.X),
			(float)GD.RandRange(0, SpawnAreaSize.Y)
		);
		enemy.Position = spawnPos;

		// Assign KillCounter
		enemy.KillCounter = KillCounter;

		AddChild(enemy);
	}
}
