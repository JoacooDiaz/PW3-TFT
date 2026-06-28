using Godot;
using System;
using System.Collections.Generic;

public partial class ElementosManager : Node
{
    private Dictionary<TipoElemento, Texture2D> _texturas = new();

	//Esta seria la logica de la tabla de debilidades
    // Usamos un dictionary que es como un clave valor en vez de if
    // x2.0 = ventaja, x0.5 = desventaja, x1.0 = neutral (no se guarda, es el default)
    private static readonly Dictionary<TipoElemento, Dictionary<TipoElemento, float>> _tablaEfectividad = new()
    {
        [TipoElemento.Fuego] = new()
        {
            [TipoElemento.Planta] = 2.0f,
            [TipoElemento.Acero]  = 2.0f,
            [TipoElemento.Agua]   = 0.5f,
            [TipoElemento.Fuego]  = 0.5f,
            [TipoElemento.Tierra] = 0.5f,
        },
        [TipoElemento.Agua] = new()
        {
            [TipoElemento.Fuego]    = 2.0f,
            [TipoElemento.Tierra]   = 2.0f,
            [TipoElemento.Planta]   = 0.5f,
            [TipoElemento.Agua]     = 0.5f,
            [TipoElemento.Electrico]= 0.5f,
        },
        [TipoElemento.Planta] = new()
        {
            [TipoElemento.Agua]   = 2.0f,
            [TipoElemento.Tierra] = 2.0f,
            [TipoElemento.Fuego]  = 0.5f,
            [TipoElemento.Planta] = 0.5f,
            [TipoElemento.Acero]  = 0.5f,
        },
        [TipoElemento.Electrico] = new()
        {
            [TipoElemento.Agua]     = 2.0f,
            [TipoElemento.Tierra]   = 0.5f,
            [TipoElemento.Electrico]= 0.5f,
        },
        [TipoElemento.Hada] = new()
        {
            [TipoElemento.Lucha]  = 2.0f,
            [TipoElemento.Acero]  = 0.5f,
            [TipoElemento.Fuego]  = 0.5f,
            [TipoElemento.Dragon]  = 2.0f,
        },
        [TipoElemento.Normal] = new()
        {
            [TipoElemento.Lucha]  = 0.5f,
            [TipoElemento.Acero]  = 0.5f,
        },
        [TipoElemento.Lucha] = new()
        {
            [TipoElemento.Normal] = 2.0f,
            [TipoElemento.Acero]  = 2.0f,
            [TipoElemento.Hada]   = 0.5f,
            [TipoElemento.Hielo]   = 2.0f,
            [TipoElemento.Dragon]   = 2.0f,
            
        },
        [TipoElemento.Acero] = new()
        {
            [TipoElemento.Hada]   = 2.0f,
            [TipoElemento.Fuego]  = 0.5f,
            [TipoElemento.Lucha]  = 0.5f,
            [TipoElemento.Tierra] = 0.5f,
            [TipoElemento.Acero]  = 0.5f,
        },
        [TipoElemento.Tierra] = new()
        {
            [TipoElemento.Fuego]    = 2.0f,
            [TipoElemento.Electrico]= 2.0f,
            [TipoElemento.Acero]    = 2.0f,
            [TipoElemento.Agua]     = 0.5f,
            [TipoElemento.Planta]   = 0.5f,
        },
          [TipoElemento.Hielo] = new()
        {
            [TipoElemento.Dragon]    = 2.0f,
            [TipoElemento.Planta]   = 0.5f,
            [TipoElemento.Tierra]     = 2.0f,
        },
           [TipoElemento.Dragon] = new()
        {
            [TipoElemento.Dragon]    = 2.0f,
            [TipoElemento.Fuego]   = 0.5f,
            [TipoElemento.Agua]   = 0.5f,
            [TipoElemento.Electrico]     = 0.5f,
            [TipoElemento.Planta]     = 0.5f,
        },
    };

    public override void _Ready()
    {
        CargarTextura(TipoElemento.Normal,    "res://Assets/Img/Elementos/Normal.png");
        CargarTextura(TipoElemento.Fuego,     "res://Assets/Img/Elementos/fuego.png");
        CargarTextura(TipoElemento.Agua,      "res://Assets/Img/Elementos/agua.png");
        CargarTextura(TipoElemento.Planta,    "res://Assets/Img/Elementos/Planta.png");
        CargarTextura(TipoElemento.Electrico, "res://Assets/Img/Elementos/electrico.png");
        CargarTextura(TipoElemento.Hada,      "res://Assets/Img/Elementos/hada.png");
        CargarTextura(TipoElemento.Lucha,     "res://Assets/Img/Elementos/lucha.png");
        CargarTextura(TipoElemento.Acero,     "res://Assets/Img/Elementos/acero.png");
        CargarTextura(TipoElemento.Tierra,    "res://Assets/Img/Elementos/tierra.png");
        CargarTextura(TipoElemento.Hielo,    "res://Assets/Img/Elementos/hielo.png");
        CargarTextura(TipoElemento.Dragon,    "res://Assets/Img/Elementos/dragon.png");
    }

    private void CargarTextura(TipoElemento tipo, string path)
    {
        Texture2D textura = GD.Load<Texture2D>(path);
        _texturas[tipo] = textura;
    }

    public Texture2D ObtenerTexturaElemento(TipoElemento elemento)
    {
        if (_texturas.TryGetValue(elemento, out Texture2D textura))
        {
            return textura;
        }
        return null;
    }

    public float ObtenerMultiplicador(TipoElemento atacante, TipoElemento defensor)
    {
        if (_tablaEfectividad.TryGetValue(atacante, out var filaAtacante))
        {
            if (filaAtacante.TryGetValue(defensor, out float multiplicador))
            {
                return multiplicador;
            }
        }

        return 1.0f;
    }
	public int CalcularDañoFinal(int dañoBase, TipoElemento atacante, TipoElemento defensor)
    {
        float multiplicador = ObtenerMultiplicador(atacante, defensor);
        return Mathf.RoundToInt(dañoBase * multiplicador);
    }
}