using Godot;

public partial class NivelNodoRaiz : Node
{
    [Export]
    public Node3D Aliados;

    [Export]
    public Node3D Enemigos;

    [Export]
    public Ui Ui;

    private CombateManager _combateManager;

    private PlayerManager _playerManager;

    private LevelManager _levelManager;

    public override void _Ready()
    {
        _combateManager =
            GetNode<CombateManager>(
                "/root/CombateManager"
            );

        _playerManager =
            GetNode<PlayerManager>(
                "/root/PlayerManager"
            );

        _levelManager =
            GetNode<LevelManager>(
                "/root/LevelManager"
            );

        _levelManager.RegistrarUI(Ui);

        SpawnNode aliadosSpawn =
            Aliados as SpawnNode;

        EnemigosSpawnNode enemigosSpawn =
            Enemigos as EnemigosSpawnNode;

        aliadosSpawn.SpawnearEntidades(
            _playerManager.Aliados
        );

        enemigosSpawn.SpawnearEnemigosAleatorios();

        _playerManager.SetUpUi(Ui); 

        _combateManager.SetupPelea(
            Aliados,
            Enemigos
        );

        _combateManager.ComenzarPelea();
    }
}