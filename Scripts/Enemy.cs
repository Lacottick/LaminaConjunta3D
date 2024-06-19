using Godot;
using Scripts.Interfaces;

namespace Scripts {
	public partial class Enemy : RigidBody3D, IEntityWithHitbox
	{
		
		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
		}

		public void HandleBodyEntered(Node3D body)
		{
			throw new System.NotImplementedException();
		}

	}
}
