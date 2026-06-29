using Godot;
using System.Collections.Generic;
using System.Linq;

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
		
		foreach (
	Node hijo
	in SP_Comprables.GetChildren()
)
{
	foreach (
		Node nieto
		in hijo.GetChildren()
	)
	{
		nieto.QueueFree();
	}
}

		_ofertasActuales =
			_tiendaManager.GenerarTienda(3);

		int indice = 0;

		Camera3D cam = GetNode<Camera3D>("Vista/SubViewport/Camera3D");
		Transform3D camInv = cam.GlobalTransform.AffineInverse();

		var slots = SP_Comprables.GetChildren()
			.Cast<Node3D>()
			.OrderBy(s => (camInv * s.GlobalPosition).X)
			.ToList();

		foreach (Node3D slot in slots)
		{
			if (indice >= _ofertasActuales.Count)
			{
				break;
			}

			PackedScene escena = _ofertasActuales[indice];

			_uiTienda.ConfigurarTarjeta(indice, escena);

			Entidad entidad = escena.Instantiate<Entidad>();
			slot.AddChild(entidad);
			entidad.Position = Vector3.Zero;

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
	
	_uiTienda.RefrescarPressed +=
	OnRefrescarPressed;
	
	}

	private void OnContinuarPressed()
	{
		LevelManager _levelManager = GetNode<LevelManager>("/root/LevelManager");
		_levelManager.IrAlSiguienteNivel(); 
	}
	
	private void OnRefrescarPressed()
{
	PlayerManager player =
		GetNode<PlayerManager>(
            "/root/PlayerManager"
		);

	if (player.Dinero < 2)
	{
		return;
	}

	player.RestarDinero(2);
	_uiTienda.ResetearBotones();
	CargarTienda();

}
	
	


}
