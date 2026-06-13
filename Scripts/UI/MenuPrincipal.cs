using Godot;
using System;

public partial class MenuPrincipal : Control
{
	private Button _btnJugar;
	private Button _btnOpciones;
    private Button _btnSalir;

    public override void _Ready()
    {
        GD.Print("Menu Principal listo");
    }

    private void OnBtnJugarPressed()
    {
        GD.Print("READY EJECUTADO");
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
