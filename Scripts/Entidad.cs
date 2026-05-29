using Godot;
using System;

public partial class Entidad : Node3D
{
    [Export]
    public EntidadData Data;

    public int VidaActual;

    public override void _Ready()
    {
        if (Data != null)
        {
            VidaActual = Data.Vida;

            GD.Print(Data.Nombre + " cargada.");
        }
    }

    public void EjecutarAccion()
    {
        GD.Print(Data.Nombre + " ejecuta accion");
    }
}