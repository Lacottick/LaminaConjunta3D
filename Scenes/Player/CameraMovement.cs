using Godot;
using System;

public partial class CameraMovement : Node3D
{
	[Export]
	public float MouseSens = 3.0f;

	[Export]
	public float CurrentAngle = 0;

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
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if(@event is InputEventMouseMotion){
			InputEventMouseMotion mouseMoveEvent = @event as InputEventMouseMotion;
			HandleCameraMovement(mouseMoveEvent.Relative.X*0.3f, mouseMoveEvent.Relative.Y*0.3f);
		}
    }
	
    public void HandleCameraMovement(InputEventMouseMotion @event){
		RotateY(
			(float)Mathf.DegToRad(@event.Relative.Y*0.3f)
			);
		float verticalAngle = -@event.Relative.X*0.3f;
		if(verticalAngle > -45 && verticalAngle < 45){
			RotateX(Mathf.DegToRad(verticalAngle));
		}
	}

	public void HandleCameraMovement(float xStrength, float yStrenght){
		float input = -xStrength+yStrenght;
		CurrentAngle = Mathf.Wrap(CurrentAngle+input*MouseSens*(float)Delta, -Mathf.Pi, Mathf.Pi);
		Transform3D newTransform = Transform;
		newTransform.Basis = new Basis(new Quaternion(Vector3.Up, CurrentAngle));
		Transform = newTransform;
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
