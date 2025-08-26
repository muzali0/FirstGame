using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene EnemyScene;   // Drag Enemy.tscn here
	[Export] public float SpawnRate = 1.0f;    // Seconds between spawns

	private float _spawnTimer = 0f;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	public override void _Process(double delta)
	{
		_spawnTimer -= (float)delta;
		if (_spawnTimer <= 0)
		{
			SpawnEnemy();
			_spawnTimer = SpawnRate;
		}
	}

	private void SpawnEnemy()
	{
		if (EnemyScene == null) return;

		var enemy = (Node2D)EnemyScene.Instantiate();

		// Random X position inside screen width (example: 0-800)
		_rng.Randomize();
		float x = _rng.RandfRange(50, 750);
		enemy.Position = new Vector2(x, -50); // spawn above screen

		AddChild(enemy);
	}
}
