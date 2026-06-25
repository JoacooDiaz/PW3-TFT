using Godot;
using System.Threading.Tasks;

public enum ColoresAura
{
	Curar,
	Buff
}

public partial class Aura : Node3D
{
    [Export]
    public float Duracion = 0.5f;

    [Export]
	public Color ColorCuracion = new Color(1f, 1f, 1f, 0.6f);

	[Export]
	public Color ColorBuff = new Color(1f, 1f, 1f, 0.6f);
	

    private CsgTorus3D auraMesh;
    private StandardMaterial3D materialAura;

    public override void _Ready()
    {
        auraMesh = GetNode<CsgTorus3D>("auraMesh");

        materialAura = new StandardMaterial3D
        {
            AlbedoColor = ColorCuracion
        };

        auraMesh.Material = materialAura;

        Apagar();
    }

    public virtual void Prender()
    {
        Visible = true;
    }

    public virtual void Apagar()
    {
        Visible = false;
    }

    public async Task MostrarAura(ColoresAura color)
    {
        CambiarColorAura(color);

        Prender();

        await ToSignal(
            GetTree().CreateTimer(Duracion),
            SceneTreeTimer.SignalName.Timeout
        );

        if (!IsInsideTree())
            return;

        Apagar();
    }

    public void CambiarColorAura(ColoresAura color)
    {
        if (materialAura == null)
            return;

		switch (color)
		{
			case ColoresAura.Curar:
				materialAura.AlbedoColor = ColorCuracion;
			break;

			case ColoresAura.Buff: 
				materialAura.AlbedoColor = ColorBuff; 
			break; 
		}
    }
}