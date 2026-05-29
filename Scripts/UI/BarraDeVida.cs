using Godot;
using System;

public partial class BarraDeVida : Sprite3D
{

	private ProgressBar _progressBar;

    public override void _Ready()
    {
        _progressBar = GetNode<ProgressBar>(
            "SubViewport/Panel/ProgressBar"
        );
    }

	public void setUpBarra(float maxVida)
	{
		_progressBar.MaxValue = maxVida;
		ActualizarBarra(maxVida); //La vida de los pokemones la vamos a mantener nivel por nivel??
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
