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

    public void setUpBarra(float maxVida, TipoElemento elemento)
    {
        _progressBar.MaxValue = maxVida;
        ActualizarBarra(maxVida);
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