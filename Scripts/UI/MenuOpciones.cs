using Godot;
using System;

public partial class MenuOpciones : Control
{
	private Button _btnVovler;


	public override void _Ready()
	{
		_btnVovler = GetNode<Button>("VBoxContainer/BtnVolver");
		_btnVovler.Pressed += OnBtnVolverPressed;
	}

    private void OnBtnVolverPressed()
    {
        GetTree().ChangeSceneToFile("uid://pkl1ufm5eaih");
    }
}
