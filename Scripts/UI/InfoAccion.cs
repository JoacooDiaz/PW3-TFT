using Godot;
public partial class InfoAccion : InfoVisual
{
    private Label _infoNum;
    private TextureRect _infoElemento; 
    private ElementosManager _elementosManager; 
    public override void _Ready()
    {
        _elementosManager = GetNode<ElementosManager>("/root/ElementosManager");
        _infoNum = GetNode<Label>("SubViewport/Panel/InfoNum");
        _infoElemento = GetNode<TextureRect>("SubViewport/Panel/InfoAction");
        Apagar();
    }
    public async void MostrarInfo(int num, TipoElemento elemento)
    {
        _infoNum.Text = "-" + num;
        _infoElemento.Texture = _elementosManager.ObtenerTexturaElemento(elemento); 
        await MostrarTemporal();
        Limpiar();
    }
    public void Limpiar()
    {
        _infoNum.Text = "";
    }
} 