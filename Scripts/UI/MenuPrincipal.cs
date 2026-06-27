using Godot;
using System;

public partial class MenuPrincipal : Control
{
    private TextureButton _btnJugar;
    private TextureButton _btnSalir;

    public override void _Ready()
    {
        _btnJugar = GetNode<TextureButton>("VBoxContainer/BtnJugar");
        _btnSalir = GetNode<TextureButton>("VBoxContainer/BtnSalir");

        _btnJugar.Pressed += OnBtnJugarPressed;
        _btnSalir.Pressed += OnBtnSalirPressed;
        GD.Print("SOY EL SCRIPT NUEVO");
    }

    private void OnBtnJugarPressed()
    {
        GD.Print("Iniciaste la partida");
    }

    private void OnBtnSalirPressed()
    {
        GetTree().Quit();
    }

    public override void _Process(double delta)
	{
	}
}
