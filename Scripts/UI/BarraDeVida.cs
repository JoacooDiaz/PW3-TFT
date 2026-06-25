using Godot;
using System;

public partial class BarraDeVida : Sprite3D
{
    private ProgressBar _progressBar;

    private ElementosManager _elementosManager;

    public override void _Ready()
    {
        _progressBar = GetNode<ProgressBar>(
            "SubViewport/Panel/ProgressBar"
        );

        _elementosManager = GetNode<ElementosManager>("/root/ElementosManager");

        GD.Print("[BarraDeVida] _Ready OK");
    }

    public void setUpBarra(float maxVida, TipoElemento elemento)
    {
        _progressBar.MaxValue = maxVida;
        ActualizarBarra(maxVida);

        GD.Print("[BarraDeVida] Tipo seteado: " + elemento);
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