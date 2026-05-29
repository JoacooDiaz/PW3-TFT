using Godot;
using System.Collections.Generic;

public partial class LevelManager : Node
{
    public List<PackedScene> GetEnemigos()
    {
        List<PackedScene> enemigos = new();

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Pokemones/Entidad.tscn")
        );

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Pokemones/Entidad.tscn")
        );

        return enemigos;
    }
}