using Godot;
using System;

public partial class Ui : Control
{

	private Label labelTest; 

	public override void _Ready()
	{
		labelTest = GetNode<Label>("TestLabel");
		labelTest.Visible = false; 
		labelTest.Text = ""; 
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

}
