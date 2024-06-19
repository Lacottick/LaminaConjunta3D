using System;
using Godot;

namespace Scripts {

	public partial class EnemySpawner : Node {

        public Path3D Path;

        public PathFollow3D PathFollow;
        
        public Timer SpawnTimer;

        public PackedScene EnemyScene;

        public override void _Ready() { 
            this.Path = GetNode<Path3D>("Path3D");
            this.PathFollow = GetNode<PathFollow3D>("PathFollow3D");
            this.SpawnTimer = new Timer();
            this.EnemyScene = GD.Load<PackedScene>("res://Scenes/Enemy.tscn");
            this.Start();
        }

        private void Start() {
            this.PathFollow.ProgressRatio = GD.Randf();
            Enemy enemy = EnemyScene.Instantiate<Enemy>();
            this.AddChild(enemy);
            double direction = 0;
            enemy.Position = this.PathFollow.Position;
            double newDirection = GD.RandRange(-Mathf.Pi/4, Mathf.Pi/4);
            direction += newDirection;
            
        }

    }

}