using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class LevelManager : Node
{

    private Ui _ui;

    private CombateManager _combateManager;

    private GameManager _gameManager; 

    private int NivelActual;

    //CREO que cargar escenas asi es malo para el performance, despues vemos 
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

    //Quiza cada nivel tenga su lista de enemigos, veremos 
    public List<PackedScene> GetEnemigos()
    {
        List<PackedScene> enemigos = new();

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Entidades/Entidad.tscn")
        );

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Entidades/Entidad.tscn")
        );

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Entidades/Entidad.tscn")
        );

        enemigos.Add(
            GD.Load<PackedScene>("res://Assets/Entidades/Entidad.tscn")
        );

        return enemigos;
    }

    private async void OnVictoria()
    {
        _ui.MostrarVictoria();

        NivelActual++; 

        //Se podra poner en otro hilo? 
        await Task.Delay(2000);

        IrATienda();
    }

    private void OnDerrota()
    {
        GD.Print("Jugador perdió.");

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