using Godot;
using System.Threading.Tasks;

public abstract partial class InfoVisual : Sprite3D
{
	[Export]
	public float Duracion = 0.5f;

	public virtual void Prender()
	{
		Visible = true;
	}

	public virtual void Apagar()
	{
		Visible = false;
	}
	
	protected async Task MostrarTemporal()
	{
		Prender();

		await ToSignal(
			GetTree().CreateTimer(Duracion),
			SceneTreeTimer.SignalName.Timeout
		);

		if (!IsInsideTree())
			return;

		Apagar();
	}
}
