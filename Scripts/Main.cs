using Godot;
using System;
namespace Scripts {
	public partial class Main : Node3D
	{
		public override void _Ready()
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}

		public override void _Process(double delta)
		{
		}
	}
}

