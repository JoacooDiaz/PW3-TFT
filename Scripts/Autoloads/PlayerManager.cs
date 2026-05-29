using Godot;
using System.Collections.Generic;

public partial class PlayerManager : Node
{

    public List<PackedScene> Aliados = new();

    public override void _Ready()
    {
        Aliados.Add(
            GD.Load<PackedScene>("res://Assets/Pokemones/ElPicachu/Picachu.tscn")
        );

        //PUEDE SER Que tener mas de una entidad en un equipo sea un problema...

    }
}