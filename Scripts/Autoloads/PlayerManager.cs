using Godot;
using System.Collections.Generic;

public partial class PlayerManager : Node
{

    public List<PackedScene> Aliados = new();

    public int Dinero = 50;

    private Ui _ui; 

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
        
        Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Psyduck/Psyduck.tscn")); 

        Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/ElPicachu/Picachu.tscn"));

        Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Glalie/Glalie.tscn"));
    }

    public void SetUpUi(Ui ui)
    {
        _ui = ui;

        if (IsInstanceValid(_ui))
            _ui.ActualizarDinero();
    }

    public void SumarDinero(int suma)
    {
        Dinero += suma;

        if (IsInstanceValid(_ui))
            _ui.ActualizarDinero();
    }

}