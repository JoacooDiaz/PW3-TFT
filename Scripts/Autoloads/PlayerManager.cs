using Godot;
using System.Collections.Generic;

public partial class PlayerManager : Node
{

    public List<PackedScene> Aliados = new();

    private int _dinero = 50;

    [Signal]
    public delegate void DineroCambiadoEventHandler(int nuevoDinero);

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

        Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Blissey/Blissey.tscn"));

        Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Audino/Audino.tscn"));

        Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/ElPicachu/Picachu.tscn"));
    }

    public int Dinero
    {
        get => _dinero;

        set
        {
            _dinero = value;

            EmitSignal(
                SignalName.DineroCambiado,
                _dinero
            );
        }
    }
}