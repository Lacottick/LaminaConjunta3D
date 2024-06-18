using Godot;

[GlobalClass]
public partial class HpComponent : Node {
    [Export]
    public int MaxHealth = 3;

    public int CurrentHealth = 0;

    public override void _Ready(){
        CurrentHealth = MaxHealth;
    }

    public void Damage(int amount){
        CurrentHealth -= amount;
        if(CurrentHealth == 0){
            GetParent().QueueFree();
            return;
        }
        if(CurrentHealth > MaxHealth){
            CurrentHealth = MaxHealth;
        }
    }


}