using Godot;
using System.Collections.Generic;

public partial class SpawnNode : Node3D
{
    public List<Entidad> Entidades = new();

    public void AgregarEntidad(Entidad entidad)
    {
        AddChild(entidad);

        Entidades.Add(entidad);
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