using Godot;
using System.Threading.Tasks;

public partial class InfoAccion : InfoVisual
{
    private Label _infoNum;

    private TextureRect _infoTexture;

    private ElementosManager _elementosManager;

    public override void _Ready()
    {
        _infoNum = GetNode<Label>("SubViewport/Panel/InfoNum");

        _infoTexture = GetNode<TextureRect>("SubViewport/Panel/InfoAction");

        _elementosManager = GetNode<ElementosManager>("/root/ElementosManager");

        Apagar();
    }

    public async void MostrarInfo(
        int num,
        TipoElemento elemento
    )
    {
        _infoNum.Text = "-" + num;

        _infoTexture.Texture = _elementosManager.ObtenerTexturaElemento(elemento);

        await MostrarTemporal();

        Limpiar();
    }

    public void Limpiar()
    {
        _infoNum.Text = "";
        _infoTexture.Texture = null;
    }

    public override void Prender()
    {
        base.Prender();
        _infoTexture.Visible = true;
    }

    public override void Apagar()
    {
        base.Apagar();
        _infoTexture.Visible = false;
    }
}