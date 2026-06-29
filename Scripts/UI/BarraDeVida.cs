using Godot;
using System;

public partial class BarraDeVida : Sprite3D
{
    private ProgressBar _progressBar;

    private ElementosManager _elementosManager;

    private TextureRect _iconoElemento; 

    public override void _Ready()
    {
        _progressBar = GetNode<ProgressBar>("SubViewport/PanelBarra/ProgressBar");

        _elementosManager = GetNode<ElementosManager>("/root/ElementosManager");

        _iconoElemento = GetNode<TextureRect>("SubViewport/PanelElemento/IconElemento");
    }

    public void setUpBarra(float maxVida, TipoElemento elemento, string equipo)
    {
        _progressBar.MaxValue = maxVida;
        _progressBar.Value = maxVida;

        StyleBox fill;
        StyleBox background = GD.Load<StyleBox>("res://Scripts/UI/Themes/PBFondo.tres");

        if (equipo == "Enemigos")
        {
            fill = GD.Load<StyleBox>("res://Scripts/UI/Themes/PBFillRojo.tres");
        }
        else
        {
            fill = GD.Load<StyleBox>("res://Scripts/UI/Themes/PBFillVerde.tres");
        }

        _progressBar.AddThemeStyleboxOverride("fill", fill);
        _progressBar.AddThemeStyleboxOverride("background", background);

        _iconoElemento.Texture = _elementosManager.ObtenerTexturaElemento(elemento);
    }

    public void ActualizarBarra(float value)
    {
        _progressBar.Value = value;
    }

    public void OcultarBarra()
    {
        Hide();
    }
}