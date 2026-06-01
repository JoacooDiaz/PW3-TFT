using Godot;
using System.Collections.Generic;

public partial class EnemigosSpawnNode : SpawnNode
{
    [Export]
    public int CantidadMinima = 1;

    [Export]
    public int CantidadMaxima = 3;

    [Export]
    public Godot.Collections.Array<PackedScene> EnemigosPosibles = new();

    public void SpawnearEnemigosAleatorios()
    {
        List<PackedScene> seleccionados = new();

        RandomNumberGenerator rng = new();
        rng.Randomize();

        int cantidad = rng.RandiRange(CantidadMinima, CantidadMaxima);

        for (int i = 0; i < cantidad; i++)
        {
            int index = rng.RandiRange(0, EnemigosPosibles.Count - 1);

            seleccionados.Add(
                EnemigosPosibles[index]
            );
        }

        SpawnearEntidades(seleccionados);
    }
}