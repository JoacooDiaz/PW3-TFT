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

		for (
			int i = 0;
			i < cantidad;
			i++
		)
		{
			int indice =
				GD.RandRange(
					0,
					_BDDEntidades.Entidades.Count - 1
				);

			_ofertasActuales.Add(
				_BDDEntidades.Entidades[indice]
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

		_playerManager.Dinero -=
			entidad.Data.Precio;

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
