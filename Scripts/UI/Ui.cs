using Godot;
using System;

public partial class Ui : Control
{

	private Label labelTest; 
    private Label DineroLabel; 
    private PlayerManager _playerManager; 

	public override void _Ready()
	{
        _playerManager = GetNode<PlayerManager>("/root/PlayerManager");
		labelTest = GetNode<Label>("TestLabel");
        DineroLabel = GetNode<Label>("DineroLabel");
		labelTest.Visible = false; 
		labelTest.Text = ""; 

        DineroLabel.Text = "$ " + _playerManager.Dinero; 

        _playerManager.DineroCambiado += OnDineroCambiado;
	}

	public void MostrarVictoria()
    {
		GD.Print("DEBERIA MOSTRAR LA VICTORIA"); 

        labelTest.Visible = true;

        labelTest.Text = "vicTORIa";
    }

    public void MostrarDerrota()
    {
        labelTest.Visible = true;

        labelTest.Text = "deRota";
    }

    public void ActualizarDinero()
    {
        DineroLabel.Text = "$ " + _playerManager.Dinero;
    }

    private void OnDineroCambiado(
        int nuevoDinero
    )
    {
        DineroLabel.Text =
            "$ " + nuevoDinero;
    }

}
