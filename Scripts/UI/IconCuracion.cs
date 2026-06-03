using Godot;
using System.Threading.Tasks;

public partial class IconCuracion : InfoVisual
{
    [Export]
    private Texture2D Imagen;

    private TextureRect _icono;

    public override void _Ready()
    {
        _icono = GetNode<TextureRect>("SubViewport/Panel/VidaIcon");

        Apagar();
    }

    public async void MostrarInfo()
    {
        _icono.Texture = Imagen;

        await MostrarTemporal();

        _icono.Texture = null;
    }

    public override void Prender()
    {
        base.Prender();

        _icono.Visible = true;
    }

    public override void Apagar()
    {
        base.Apagar();

        if (_icono != null)
            _icono.Visible = false;
    }
}