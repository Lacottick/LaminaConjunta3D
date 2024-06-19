using Godot;
using System;

namespace Scripts {

	public partial class CameraMovement : Node3D
	{
		[Export]
		public float JoyStickSensitivity = 5.0f;

		[Export]
		public float MouseSensitivity = 0.1f;
		public float CurrentAngleX = 0;

		public float CurrentAngleY = 0;
		
		public Vector2 Direction = Vector2.Zero;

		public InputEvent MouseMoveEvent ;

		public float SpringArmLength {
			get {
				return GetNode<SpringArm3D>("%SpringArm").SpringLength;
			}
			set {
				GetNode<SpringArm3D>("%SpringArm").SpringLength = value;
			}
		}
		public Vector3 SpringArmPosition{
			get {
				return GetNode<SpringArm3D>("%SpringArm").Position;
			}
			set {
				GetNode<SpringArm3D>("%SpringArm").Position = value;
			}
		}
		public double Delta = 0;
		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
			Delta = delta;
			// HandleCameraMovement();
			HandleZoom();
			this.Direction = Input.GetVector("rotate_left", "rotate_right", "rotate_up", "rotate_down");
			if(this.Direction != Vector2.Zero){
				HandleCameraMovementJoystick(this.Direction);
			}
			if(MouseMoveEvent is InputEventMouseMotion){
				InputEventMouseMotion mouseMoveEvent = MouseMoveEvent as InputEventMouseMotion;
				this.Direction = new Vector2(-mouseMoveEvent.Relative.X, -mouseMoveEvent.Relative.Y);
				HandleCameraMovementMouse(this.Direction);
			}
		}

		public override void _Input(InputEvent @event)
		{
			base._Input(@event);
			if(@event is InputEventMouseMotion){
				this.MouseMoveEvent = @event;
			}
			else {
				this.MouseMoveEvent = null;
			}
		}
		public void HandleCameraMovementJoystick(Vector2 direction){
			Transform3D newTransform = Transform;
			CurrentAngleX = Mathf.Wrap(CurrentAngleX+direction.X*JoyStickSensitivity*(float)Delta, -Mathf.Pi*2, Mathf.Pi*2);
			CurrentAngleY = Mathf.Clamp(CurrentAngleY+direction.Y*JoyStickSensitivity*0.3f*(float)Delta, -Mathf.Pi/8, Mathf.Pi/12);
			Quaternion quaternionX = new(Vector3.Up, CurrentAngleX);
			Quaternion quaternionY = new(Vector3.ModelLeft, CurrentAngleY);
			newTransform.Basis = new Basis(quaternionX*quaternionY);
			using(Tween tween = CreateTween()){
					tween.SetParallel(true);
					tween.SetTrans(Tween.TransitionType.Sine);
					tween.SetEase(Tween.EaseType.Out);
					tween.TweenProperty(
						this,
						"transform",
						newTransform,
						0.5
					);
			}
		}
		public void HandleCameraMovementMouse(Vector2 direction){
			Transform3D newTransform = Transform;
			CurrentAngleX = Mathf.Wrap(CurrentAngleX+direction.X*MouseSensitivity*(float)Delta, -Mathf.Pi*2, Mathf.Pi*2);
			CurrentAngleY = Mathf.Clamp(CurrentAngleY+direction.Y*MouseSensitivity*0.3f*(float)Delta, -Mathf.Pi/8, Mathf.Pi/12);
			Quaternion quaternionX = new(Vector3.Up, CurrentAngleX);
			Quaternion quaternionY = new(Vector3.ModelLeft, CurrentAngleY);
			newTransform.Basis = new Basis(quaternionX*quaternionY);
			this.Transform = newTransform;
		}

		public void HandleZoom(){
			if(Input.IsActionJustPressed("zoom_in")){
				ZoomIn();
			}
			else if(Input.IsActionJustPressed("zoom_out")){
				ZoomOut();
			}
		}

		public void ZoomIn(){
			if(SpringArmLength>0){
				using(Tween tween = CreateTween()){
					tween.SetParallel(true);
					tween.SetTrans(Tween.TransitionType.Sine);
					tween.SetEase(Tween.EaseType.Out);
					float newLength = SpringArmLength - 1;
					tween.TweenProperty(
							this,
							"SpringArmLength",
							newLength,
							0.5
						);
					if(newLength<=0){
						tween.TweenProperty(
							this,
							"SpringArmPosition",
							new Vector3(Position.X,0.2f,Position.Z),
							0.5
						);
					}
				}
			}
		}
		

		public void ZoomOut(){
			if(SpringArmLength<4){
				using(Tween tween = CreateTween()){

					tween.SetParallel(true);
					tween.SetTrans(Tween.TransitionType.Sine);
					tween.SetEase(Tween.EaseType.Out);
					float newLength = SpringArmLength + 1;
					tween.TweenProperty(
							this,
							"SpringArmLength",
							newLength,
							0.5
						);
					if(newLength>=4){
						tween.TweenProperty(
							this,
							"SpringArmPosition",
							new Vector3(Position.X,0.2f,Position.Z),
							0.5
						);
					}
				}
			}
		}
	}

}

