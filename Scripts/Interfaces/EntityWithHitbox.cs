

using Godot;

namespace Scripts.Interfaces {

    public interface IEntityWithHitbox {

        public void HandleBodyEntered(Node3D body);
    }

}