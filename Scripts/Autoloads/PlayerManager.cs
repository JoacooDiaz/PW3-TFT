using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerManager : Node
{

	public List<PackedScene> Aliados = new();

	public int Dinero;

	private Ui _ui; 

	public override void _Ready()
	{
		Dinero = 0; 
		CargarEquipoInicial();
	}

	public void CargarEquipoInicial()
	{
		Aliados.Clear();
		Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Popplio/Popplio.tscn"));
		Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Bulbasaur/Bulbasaur.tscn")); 
		Aliados.Add(GD.Load<PackedScene>("res://Assets/Entidades/Scorbunny/Scorbunny.tscn"));
	}

	public void ResetearProgreso()
	{
		Dinero = 0;
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
		
	public void EliminarDeEquipo(string nombre)
	{
		for (int i = Aliados.Count - 1; i >= 0; i--)
		{
			Entidad entidad = Aliados[i].Instantiate<Entidad>();

			if (entidad.Data.Nombre == nombre)
			{
				Aliados.RemoveAt(i);
				break; 
			}
		}
	}

}
