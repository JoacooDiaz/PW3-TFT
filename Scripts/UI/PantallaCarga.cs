using Godot;
using System;
using System.Threading.Tasks;

public partial class PantallaCarga : Control
{
	private ProgressBar _bar;
	public override async void _Ready()
	{
		_bar = GetNode<ProgressBar>("ProgressBar");

		for (int i = 0; i <= 100; i++) 
		{
			_bar.Value = i;

			await ToSignal(GetTree().CreateTimer(0.03), SceneTreeTimer.SignalName.Timeout);
		} 
		 GetTree().ChangeSceneToFile("res://Scenes/Niveles/nivel_1.tscn");
	}

	public override void _Process(double delta)
	{
	}
}
