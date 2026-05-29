using Godot;
using System.Collections.Generic;

public partial class NodoRaizScript : Node
{
	[Export]
	public Node3D Aliados;

	[Export]
	public Node3D Enemigos;

	private CombateManager _combateManager;
	
	private PlayerManager _playerManager; 

	private LevelManager _levelManager;

	public override void _Ready()
	{
		_combateManager = GetNode<CombateManager>("/root/CombateManager");
		_playerManager = GetNode<PlayerManager>("/root/PlayerManager"); 
		_levelManager = GetNode<LevelManager>("/root/LevelManager");

		SpawnNode aliadosSpawn = Aliados as SpawnNode;
		SpawnNode enemigosSpawn = Enemigos as SpawnNode;

		foreach (PackedScene aliadoScene in _playerManager.Aliados)
		{
			Entidad entidad = aliadoScene.Instantiate<Entidad>();

			aliadosSpawn.AgregarEntidad(entidad);
		}

		foreach (PackedScene enemigoScene in _levelManager.GetEnemigos())
		{
			Entidad entidad = enemigoScene.Instantiate<Entidad>();

			enemigosSpawn.AgregarEntidad(entidad);
		}

		_combateManager.SetupPelea(Aliados, Enemigos);

		_combateManager.ComenzarPelea();
	}
}
