using Godot;
using System;

public partial class MenuPrincipal : Control
{
	private Button _btnJugar;
	private Button _btnOpciones;
    private Button _btnSalir;

    public override void _Ready()
    {
        _btnJugar = GetNode<Button>("VBoxContainer/BtnJugar");
        _btnOpciones = GetNode<Button>("VBoxContainer/BtnOpciones");
        _btnSalir = GetNode<Button>("VBoxContainer/BtnSalir");

        _btnJugar.Pressed += OnBtnJugarPressed;
        _btnOpciones.Pressed += OnBtnOpcionesPressed;
        _btnSalir.Pressed += OnBtnSalirPressed;
    }

    private void OnBtnJugarPressed()
    {
        GD.Print("Comenzaste el Juego");
        GetTree().ChangeSceneToFile("uid://co5gt2hmtwe4i");
    }

    private void OnBtnOpcionesPressed()
    {
        GD.Print("Opciones");
    }

    private void OnBtnSalirPressed()
    {
        GetTree().Quit();
    }

    public override void _Process(double delta)
	{
	}
}
