using Godot;
using Scripts.Interfaces;

namespace Scripts {

	public partial class Player : CharacterBody3D, IEntityWithHitbox
	{
		public const float SPEED = 5.0f;
		public const float JUMP_VELOCITY = 4.5f;

		public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

		public override void _PhysicsProcess(double delta)
		{
			Vector3 velocity = Velocity;

			if (!IsOnFloor())
				velocity.Y -= gravity * (float)delta;

			if (Input.IsActionJustPressed("jump") && IsOnFloor())
				velocity.Y = JUMP_VELOCITY;

			Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_foward", "move_back");
			Vector3 moveDirection = (GetNode<Node3D>("VerticalPivot").Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
			Vector3 animationDirection =new Vector3(inputDir.X, 0, inputDir.Y).Normalized();
			HandleAnimation(animationDirection);
			if (moveDirection != Vector3.Zero)
			{
				velocity.X = moveDirection.X * SPEED;
				velocity.Z = moveDirection.Z * SPEED;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, SPEED);
				velocity.Z = Mathf.MoveToward(Velocity.Z, 0, SPEED);
			}
			Velocity = velocity;
			MoveAndSlide();
		}

		public void HandleAnimation(Vector3 direction)
		{
			if (direction.Equals(Vector3.Zero))
			{
				GetNode<AnimatedSprite3D>("Sprite").Play("idle");
			}
			else if (direction.Z == 1)
			{
				GetNode<AnimatedSprite3D>("Sprite").Play("walkBack");
			}
			else if (direction.Z == -1) {
				GetNode<AnimatedSprite3D>("Sprite").Play("walkFoward");
			}
			else if (direction.X == -1){
				GetNode<AnimatedSprite3D>("Sprite").FlipH = true;
				GetNode<AnimatedSprite3D>("Sprite").Play("walkSide");
			}
			else if (direction.X == 1){
				GetNode<AnimatedSprite3D>("Sprite").FlipH = false;
				GetNode<AnimatedSprite3D>("Sprite").Play("walkSide");
			}

		}

		public void HandleBodyEntered(Node3D body)
		{
			throw new System.NotImplementedException();
		}

	}
}
