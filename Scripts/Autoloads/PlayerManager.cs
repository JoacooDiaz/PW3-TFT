using Godot;
using System.Collections.Generic;

public partial class PlayerManager : Node
{

	public List<PackedScene> Aliados = new();

	public int Dinero = 50;

	private Ui _ui; 

	public override void _Ready()
	{

		/*
		
			Detalle a tener en cuenta:
				Los enemigos eligen a quien atacar segun distancia.
				SpawnNode se encarga de poner a las entidades y quienes esten primeros en la lista
				mas cerca estaran del equipo enemigo.

			Tenemos que ver si hacemos que esto sea una funcionalidad o si vamos a "Corregirlo"
			para controlarlo nosotros mismos...
		*/
		
		CargarEquipoInicial();
	}

	private void CargarEquipoInicial()
	{
		Aliados.Clear();
		Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Blissey/Blissey.tscn"));
		Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Audino/Audino.tscn")); 
		Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/ElPicachu/Picachu.tscn"));
	}

	public void ResetearProgreso()
	{
		Dinero = 50;
		CargarEquipoInicial();
		if (IsInstanceValid(_ui))
			_ui.ActualizarDinero();
	}

	public void SetUpUi(Ui ui)
	{
		_ui = ui;

		if (IsInstanceValid(_ui))
			_ui.ActualizarDinero();
	}

	public void SumarDinero(int suma)
	{
		Dinero += suma;

		if (IsInstanceValid(_ui))
			_ui.ActualizarDinero();
	}
	
	public void RestarDinero(int cantidad)
{
	Dinero -= cantidad;

	if (IsInstanceValid(_ui))
		_ui.ActualizarDinero();
}

}
