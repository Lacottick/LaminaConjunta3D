using Godot;

[GlobalClass]
public partial class HitboxComponent : Area3D {
    [Export]
    public HpComponent HpComponent;

    public override void _Ready()
    {
        base._Ready();
        this.BodyEntered += OnBodyEntered;
    }
    public void Damage(int amount){
        this.HpComponent?.Damage(amount);
    }

    public void OnBodyEntered(Node3D body){
        if(GetParent() is EntityWithHitbox){
            GetParent<EntityWithHitbox>().BodyEntered(body);
        }
    }

}
