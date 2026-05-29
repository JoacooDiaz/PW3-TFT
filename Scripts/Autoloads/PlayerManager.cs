using Godot;
using System.Collections.Generic;

public partial class PlayerManager : Node
{

    public List<PackedScene> Aliados = new();

    public override void _Ready()
    {

        /*
        
            Detalle a tener en cuenta:
                Los enemigos eligen a quien atacar segun distancia.
                SpawnNode se encarga de poner a las entidades y quienes esten primeros en la lista
                mas cerca estaran del equipo enemigo.

            Tenemos que ver si hacemos que esto sea una funcionalidad o si vamos a "Corregirlo"
            para controlarlo nosotros mismos...

        */

        Aliados.Add(
            GD.Load<PackedScene>("res://Assets/Pokemones/Blissey/Blissey.tscn")
        );

        Aliados.Add(
            GD.Load<PackedScene>("res://Assets/Pokemones/Audino/Audino.tscn")
        );

        Aliados.Add(
            GD.Load<PackedScene>("res://Assets/Pokemones/ElPicachu/Picachu.tscn")
        );

    }
}