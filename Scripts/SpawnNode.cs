using Godot;
using System.Collections.Generic;

public partial class SpawnNode : Node3D
{
    public List<Entidad> Entidades = new();

    [Export]
    private float _distanciaEntreEntidades = 1.8f;

    [Export]
    private int _entidadesPorFila = 5;

    public void SpawnearEntidades(
        List<PackedScene> entidadesASpawnear
    )
    {
        if (entidadesASpawnear == null) return;
        for (int i = 0; i < entidadesASpawnear.Count; i++)
        {
            PackedScene escenaEntidad =
                entidadesASpawnear[i];

            if (escenaEntidad == null)
            {
                GD.PrintErr("[SpawnNode] Escena de entidad nula encontrada en el índice: " + i);
                continue;
            }

            Entidad entidad =
                escenaEntidad.Instantiate<Entidad>();

            AddChild(entidad);

            PosicionarEntidad(entidad, i);

            Entidades.Add(entidad);
        }
    }

    private void PosicionarEntidad(
        Entidad entidad,
        int indice
    )
    {
        int fila =
            indice / _entidadesPorFila;

        int columna =
            indice % _entidadesPorFila;

        Vector3 offset = new Vector3(
            columna * _distanciaEntreEntidades,
            0,
            fila * _distanciaEntreEntidades
        );

        entidad.Position = offset;
    }

    public void CambiarEstadoEntidades(
        EstadoEntidad nuevoEstado
    )
    {
        foreach (Entidad entidad in Entidades)
        {
            entidad.CambiarEstado(nuevoEstado);
        }
    }
}