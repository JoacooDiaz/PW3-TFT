using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class LevelManager : Node
{

    private Ui _ui;

    private CombateManager _combateManager;

    private GameManager _gameManager; 

    private int NivelActual;

    private readonly PackedScene _nivelScene = GD.Load<PackedScene>("res://Scenes/nivel_test.tscn");
    private readonly PackedScene _tiendaScene = GD.Load<PackedScene>("res://Scenes/tienda.tscn");

    public override void _Ready() 
    {
        _combateManager = GetNode<CombateManager>("/root/CombateManager");

        _combateManager.Victoria += OnVictoria;
        _combateManager.Derrota += OnDerrota;
    
        _gameManager = GetNode<GameManager>("/root/GameManager"); 

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

    private void OnDerrota()
    {
        _ui.MostrarDerrota();
    }

    private void IrATienda()
    {
        _gameManager.IrAEscena(_tiendaScene);
    }

    public void IrAlSiguienteNivel()
    {
        _gameManager.IrAEscena(_nivelScene);
    }
}