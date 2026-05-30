using Godot;
using System;

public partial class GameManager : Node
{

    public void IrAEscena(PackedScene scene)
    {
        if (scene == null)
        {
            GD.PrintErr("Escena nula.");
            return;
        }

        GetTree().ChangeSceneToPacked(scene);
    }

}
