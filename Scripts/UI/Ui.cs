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

        ActualizarDinero(); 

        OcultarMensaje();
	}

	public void MostrarVictoria()
    {
        labelTest.Visible = true;
        labelTest.Text = "vicTORIa";
    }

    public void MostrarDerrota()
    {
        labelTest.Visible = true;
        labelTest.Text = "deRota";
    }

    public void OcultarMensaje()
    {
        labelTest.Visible = false;
        labelTest.Text = "";
    }

    public void ActualizarDinero()
    {
        DineroLabel.Text = "$ " + _playerManager.Dinero;
    }

    public override void _ExitTree()
    {
        if (_playerManager != null)
            _playerManager.SetUpUi(null);
    }
}
