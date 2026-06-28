using Godot;
public partial class InfoAccion : InfoVisual
{
    private Label _infoNum;
    private ElementosManager _elementosManager; 
    private TextureRect _iconoElemento; 
    public override void _Ready()
    {
        _infoNum = GetNode<Label>("SubViewport/Panel/InfoNum");
        _iconoElemento = GetNode<TextureRect>("SubViewport/Panel/IconoElemento");
        _elementosManager = GetNode<ElementosManager>("/root/ElementosManager");
        Apagar();
    }
    public async void MostrarInfo(int num, TipoElemento elemento)
    {
        _infoNum.Text = "-" + num;
        _iconoElemento.Texture = _elementosManager.ObtenerTexturaElemento(elemento); 
        await MostrarTemporal();
        Limpiar();
    }
    public void Limpiar()
    {
        _infoNum.Text = "";
    }
} 