using Godot;
using System;

public partial class ElementosManager : Node
{

    private Texture2D NeutralTexture;
    private Texture2D FuegoTexture;
    private Texture2D AguaTexture;
    private Texture2D PlantaTexture;
    private Texture2D ElectricoTexture;
    private Texture2D OscuridadTexture;
    private Texture2D LuzTexture;

    public override void _Ready()
    {
        NeutralTexture = GD.Load<Texture2D>("res://icon.svg"); 
        FuegoTexture = GD.Load<Texture2D>("res://Assets/Img/Elementos/Agua.jpg"); 
        AguaTexture = GD.Load<Texture2D>("res://Assets/Img/Elementos/Electrio.jpg"); 
        PlantaTexture = GD.Load<Texture2D>("res://Assets/Img/Elementos/Fuego.jpg"); 
        ElectricoTexture = GD.Load<Texture2D>("res://Assets/Img/Elementos/Oscurida.jpg"); 
        OscuridadTexture = GD.Load<Texture2D>("res://Assets/Img/Elementos/Planta.png"); 
        LuzTexture = GD.Load<Texture2D>("res://Assets/Img/Elementos/Sol.jpg"); 
    }

    public Texture2D ObtenerTexturaElemento(
    	TipoElemento elemento
	)
	{
		switch (elemento)
		{
			case TipoElemento.Fuego:
				return FuegoTexture;

			case TipoElemento.Agua:
				return AguaTexture;

			case TipoElemento.Planta:
				return PlantaTexture;

			case TipoElemento.Electrico:
				return ElectricoTexture;

			case TipoElemento.Oscuridad:
				return OscuridadTexture;

			case TipoElemento.Luz:
				return LuzTexture;

			default:
				return NeutralTexture;
		}
	}
}