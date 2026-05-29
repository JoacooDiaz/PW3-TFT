using Godot;
using System;

public partial class CombateManager : Node
{
    private SpawnNode _aliados;
    private SpawnNode _enemigos;

    public void SetupPelea(Node3D aliados, Node3D enemigos)
    {
        _aliados = aliados as SpawnNode;
        _enemigos = enemigos as SpawnNode;

        _aliados.SpawnearEntidades();
        _enemigos.SpawnearEntidades();
    }

    public void ComenzarPelea()
    {
        _aliados.AsignarAcciones();
        _enemigos.AsignarAcciones();
    }
}