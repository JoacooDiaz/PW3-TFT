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

		CargarTienda();

		CargarUiTienda(); 
	}

	public void CargarTienda()
	{
		_ofertasActuales =
			_tiendaManager.GenerarTienda(5);

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

			if (escena == null)
			{
				GD.PrintErr("[TiendaNodoRaiz] Escena en el índice " + indice + " es nula.");
				indice++;
				continue;
			}

			Entidad entidad =
				escena.Instantiate<Entidad>();

			if (entidad == null)
			{
				GD.PrintErr("[TiendaNodoRaiz] No se pudo instanciar la entidad para la escena en el índice " + indice);
				indice++;
				continue;
			}

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
