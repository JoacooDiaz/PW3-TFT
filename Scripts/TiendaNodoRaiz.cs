using Godot;
using System.Collections.Generic;

public partial class TiendaNodoRaiz : Node
{
	[Export]
	public Ui _Ui; 

	[Export]
	public Node3D SP_Comprables;
	
	[Export]
	public PackedScene UITiendaScene;

	private TiendaUi _uiTienda;

	private TiendaManager _tiendaManager;

	private List<PackedScene> _ofertasActuales = new();

	public override void _Ready()
	{
		_tiendaManager =
			GetNode<TiendaManager>(
                "/root/TiendaManager"
			);

		CargarUiTienda();
		CargarTienda();

		 
	}

	public void CargarTienda()
	{
		_ofertasActuales =
			_tiendaManager.GenerarTienda(3);

		int indice = 0;

		foreach (
			Node hijo
			in SP_Comprables.GetChildren()
		)
		{
			if (
				indice >= _ofertasActuales.Count
			)
			{
				break;
			}

			Node3D slot =
				hijo as Node3D;

			if (slot == null)
			{
				continue;
			}

			PackedScene escena =
				_ofertasActuales[indice];
				
				_uiTienda.ConfigurarTarjeta(
				indice,
   				escena
				);

			Entidad entidad =
				escena.Instantiate<Entidad>();

			slot.AddChild(entidad);

			entidad.Position =
				Vector3.Zero;

			indice++;
		}
	}

	private void CargarUiTienda()
	{
		if (UITiendaScene == null)
		{
			GD.PrintErr("UITiendaScene no asignada.");
			return;
		}

		_uiTienda = UITiendaScene.Instantiate<TiendaUi>();

		_Ui.AddChild(_uiTienda);

		_uiTienda.ContinuarPressed += OnContinuarPressed;
	}

	private void OnContinuarPressed()
	{
		LevelManager _levelManager = GetNode<LevelManager>("/root/LevelManager");
		_levelManager.IrAlSiguienteNivel(); 
	}
	
	


}
