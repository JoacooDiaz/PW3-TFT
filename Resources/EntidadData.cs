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
    public int Curacion = 15;

    [Export]
    public float MultiplicadorAsistencia = 1.25f;

    [Export]
    public float DuracionAsistencia = 5.0f;

    [Export]
    public float TiempoEntreAtaques = 1.0f;

    [Export]
    public float VelocidadMovimiento = 4.0f;

    [Export]
    public float DistanciaAtaque = 2.0f;

    [Export]
    public TipoRol Rol = TipoRol.Atacante;

    [Export]
    public TipoElemento Elemento =
        TipoElemento.Neutral;

    [Export]
    public int Precio = 3;

    [Export]
    public int Recompensa = 1;

    /*
        Recomendacion: No hacer la distancia de ataque menor a 2.0, sino la distancia puede ser menor al
        tamaño de la entidad perse 
    */
}