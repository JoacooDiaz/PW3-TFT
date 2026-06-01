using Godot;
using System;

public partial class GameManager : Node
{

    public async void IrAEscena(PackedScene scene)
    {
        await ToSignal(
            GetTree(),
            SceneTree.SignalName.ProcessFrame
        );

        GetTree().ChangeSceneToPacked(scene);
    }

}
