using Godot;
using System;
using System.Collections.Generic;

public partial class SpawnNode : Node3D
{
    public List<Entidad> Entidades = new();

    public override void _Ready()
    {
        ActualizarListaEntidades();
    }

    public void ActualizarListaEntidades()
    {
        Entidades.Clear();

        foreach (Entidad entidad in GetChildren())
        {
            Entidades.Add(entidad);
        }

        GD.Print(Name + " tiene " + Entidades.Count + " entidades.");
    }

    public void AgregarEntidad(Entidad entidad)
    {
        AddChild(entidad);

        Entidades.Add(entidad);
    }

	public void SpawnearEntidades()
	{
		
	}

    public void AsignarAcciones()
    {
        foreach (Entidad entidad in Entidades)
        {
            entidad.EjecutarAccion();
        }
    }
}