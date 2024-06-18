using Godot;

public partial class Enemy : RigidBody3D, EntityWithHitbox
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    void EntityWithHitbox.BodyEntered(Node3D body)
    {
        throw new System.NotImplementedException();
    }

}
