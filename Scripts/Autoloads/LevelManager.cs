using Godot;
using System.Collections.Generic;

public partial class LevelManager : Node
{

    private Ui _ui;

    private CombateManager _combateManager;

    public override void _Ready() 
    {
        _combateManager = GetNode<CombateManager>("/root/CombateManager");

        _combateManager.Victoria += OnVictoria;
        _combateManager.Derrota += OnDerrota;
    }

    public void RegistrarUI(Ui ui)
    {
        _ui = ui;
    }

    public List<PackedScene> GetEnemigos()
    {
        List<PackedScene> enemigos = new();

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Entidades/Entidad.tscn")
        );

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Entidades/Entidad.tscn")
        );

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Entidades/Entidad.tscn")
        );

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Entidades/Entidad.tscn")
        );

        return enemigos;
    }

    private void OnVictoria()
    {
        GD.Print("Jugador ganó.");

        _ui.MostrarVictoria();
    }

    private void OnDerrota()
    {
        GD.Print("Jugador perdió.");

        _ui.MostrarDerrota();
    }
}