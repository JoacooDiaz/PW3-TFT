using Godot;
using System.Collections.Generic;

public partial class TiendaManager : Node
{
	private BDDEntidades _BDDEntidades;

	private PlayerManager _playerManager;

	private List<PackedScene> _ofertasActuales =
		new();

	public override void _Ready()
	{
		_BDDEntidades =
			GetNode<BDDEntidades>(
                "/root/BDDEntidades"
			);

		_playerManager =
			GetNode<PlayerManager>(
                "/root/PlayerManager"
			);
	}

	public List<PackedScene> GenerarTienda(
		int cantidad
	)
	{
		_ofertasActuales.Clear();

		if (
			_BDDEntidades.Entidades.Count == 0
		)
		{
			GD.PrintErr(
                "La base de datos está vacía."
			);

			return _ofertasActuales;
		}

		var disponibles = new List<PackedScene>(
			_BDDEntidades.Entidades
		);

		for (int i = disponibles.Count - 1; i > 0; i--)
		{
			int j = GD.RandRange(0, i);
			(disponibles[i], disponibles[j]) =
				(disponibles[j], disponibles[i]);
		}

		int tomar = Mathf.Min(
			cantidad, disponibles.Count
		);

		for (int i = 0; i < tomar; i++)
		{
			_ofertasActuales.Add(
				disponibles[i]
			);
		}

		return _ofertasActuales;
	}

	public bool ComprarPokemon(
		PackedScene escena
	)
	{
		Entidad entidad =
			escena.Instantiate<Entidad>();

		if (
			entidad.Data.Precio >
			_playerManager.Dinero
		)
		{
			GD.Print(
                "No hay dinero suficiente."
			);

			return false;
		}

		/*_playerManager.Dinero -=
			entidad.Data.Precio;*/
			
			_playerManager.RestarDinero(
			entidad.Data.Precio
			);

		_playerManager.Aliados.Add(
			escena
		);

		GD.Print(
			"Comprado: " +
			entidad.Data.Nombre
		);

		return true;
	}

	public void VenderPokemon(
		PackedScene escena
	)
	{
		Entidad entidad =
			escena.Instantiate<Entidad>();

		_playerManager.Dinero +=
			entidad.Data.Recompensa;

		_playerManager.Aliados.Remove(
			escena
		);

		GD.Print(
			"Vendido: " +
			entidad.Data.Nombre
		);
	}
}