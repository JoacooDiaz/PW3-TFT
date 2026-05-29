using Godot;
using System;

[GlobalClass]
public partial class EntidadData : Resource
{
    [Export]
    public string Nombre = "Entidad";

    [Export]
    public int Vida = 100;

    [Export]
    public int Daño = 10;

    [Export]
    public float TiempoEntreAtaques = 1.0f;
}