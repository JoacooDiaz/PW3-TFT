using Godot;
using System;

public partial class MenuPrincipal : Control
{
	private TextureButton _btnJugar;
	private TextureButton _btnSalir;
	public override void _Ready()
	{
        GD.Print("MenuPrincipal ready");
        _btnJugar = GetNode<TextureButton>("VBoxContainer/Label/BtnJugar");
		_btnSalir = GetNode<TextureButton>("VBoxContainer/Label/BtnSalir");

        GD.Print(_btnJugar);
        GD.Print(_btnSalir);

        _btnJugar.Pressed += OnJugarPressed;
        _btnSalir.Pressed += OnSalirPressed;
    }

    private void OnSalirPressed()
    {
        GetTree().Quit();
    }

    private void OnJugarPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/PantallaInicio3d.tscn");
    }

    public override void _Process(double delta)
	{
	}
}
