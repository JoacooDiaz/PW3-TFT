using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class LevelManager : Node
{

	private Ui _ui;

	private CombateManager _combateManager;

	private GameManager _gameManager; 

	private int NivelActual;

	private List<PackedScene> _niveles = new();

	private readonly PackedScene _tiendaScene = GD.Load<PackedScene>("res://Scenes/tienda.tscn");

	public override void _Ready() 
	{
		_combateManager = GetNode<CombateManager>("/root/CombateManager");

		_combateManager.Victoria += OnVictoria;
		_combateManager.Derrota += OnDerrota;
	
		_gameManager = GetNode<GameManager>("/root/GameManager"); 

		CargarNiveles(); 

		NivelActual = 0; 
	}

	public void RegistrarUI(Ui ui)
	{
		_ui = ui;
	}

	private async void OnVictoria()
	{
		_ui.MostrarVictoria();

		NivelActual++; 

		await Task.Delay(2000);

		IrATienda();
	}

	private async void OnDerrota()
	{
		_ui.MostrarDerrota();

		// Reiniciamos nivel y progreso del jugador
		NivelActual = 0;
		var playerManager = GetNode<PlayerManager>("/root/PlayerManager");
		playerManager.ResetearProgreso();

		// Esperamos 3 segundos para que el jugador vea el mensaje de derrota
		await Task.Delay(3000);

		// De vuelta a la tienda
		IrATienda();
	}

	private void IrATienda()
	{
		_gameManager.IrAEscena(_tiendaScene);
	}

	public void IrAlSiguienteNivel()
	{
		if (NivelActual >= _niveles.Count)
		{
		   NivelActual = 0; 
		}

		_gameManager.IrAEscena(_niveles[NivelActual]);
	}

	private void CargarNiveles()
	{
		_niveles.Add(GD.Load<PackedScene>("res://Scenes/Niveles/nivel_1.tscn")); 
		_niveles.Add(GD.Load<PackedScene>("res://Scenes/Niveles/nivel_2.tscn")); 
		_niveles.Add(GD.Load<PackedScene>("res://Scenes/Niveles/nivel_3.tscn")); 
		_niveles.Add(GD.Load<PackedScene>("res://Scenes/Niveles/nivel_4.tscn")); 
		_niveles.Add(GD.Load<PackedScene>("res://Scenes/Niveles/nivel_5.tscn")); 
		_niveles.Add(GD.Load<PackedScene>("res://Scenes/Niveles/nivel_jefe.tscn")); 
	}
}
