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

    [Export]
    public float VelocidadMovimiento = 4.0f;

    [Export]
    public float DistanciaAtaque = 2.0f;

    [Export]
    public TipoRol Rol = TipoRol.Atacante;

    [Export]
    public TipoElemento Elemento = TipoElemento.Neutral;

    /*
    
    IMPORTANTE: 
        Hay un par de valores que evitan un buen funcionamiento, 
        EJ.: DistanciaAtaque = 1.0f
            La distancia es menor al tamaño de la entidad por ende nunca ataca 
        Asi que ojo con que valores meten!
    */

}