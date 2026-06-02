using Godot;
using System;
using System.Threading.Tasks;

public partial class InfoAccion : Sprite3D
{
	
	private Label InfoNum; 

	private TextureRect InfoAccionTexture; 

	//??? 
	[Export] private Texture2D NeutralTexture;
	[Export] private Texture2D FuegoTexture;
	[Export] private Texture2D AguaTexture;
	[Export] private Texture2D PlantaTexture;
	[Export] private Texture2D ElectricoTexture;
	[Export] private Texture2D OscuridadTexture;
	[Export] private Texture2D LuzTexture;

	public override void _Ready()
	{
		InfoNum = GetNode<Label>("SubViewport/Panel/InfoNum");
		InfoAccionTexture = GetNode<TextureRect>("SubViewport/Panel/InfoAction");
	}

	public async void MostrarInfo(
		int num,
		TipoElemento elemento
	)
	{
		InfoNum.Text = "-" + num;

		InfoAccionTexture.Texture =
			ObtenerTexturaElemento(elemento);

		InfoAccionTexture.Visible = true;

		await ToSignal(
			GetTree().CreateTimer(0.5),
			SceneTreeTimer.SignalName.Timeout
		);

		if (!IsInsideTree())
			return;

		OcultarInfo();
	}

	private Texture2D ObtenerTexturaElemento(
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

	public void OcultarInfo()
	{
		InfoNum.Text = ""; 
		InfoAccionTexture.Visible = false; 
	}

}
