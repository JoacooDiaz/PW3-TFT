using Godot;
using System;

public partial class MenuPrincipal : Control
{
	private Button _btnJugar;
	private Button _btnSalir;
	public override void _Ready()
	{
		_btnJugar = GetNode<Button>("VBoxContainer/Label/BtnJugar");
		_btnSalir = GetNode<Button>("VBoxContainer/Label/BtnSalir");

		_btnJugar.Pressed += OnJugarPressed;
		_btnSalir.Pressed += OnSalirPressed;
	}

	private void OnSalirPressed()
	{
		GetTree().Quit();
	}

	private void OnJugarPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/UI/PantallaCarga.tscn");
	}

	public override void _Process(double delta)
	{
	}
}
