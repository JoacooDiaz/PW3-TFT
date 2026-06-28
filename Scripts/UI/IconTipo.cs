using Godot;

public partial class IconTipo : InfoVisual
{

    private ElementosManager _elementosManager; 
    private TextureRect elementoIcon; 
    public override void _Ready()
    {
        _elementosManager = GetNode<ElementosManager>("/root/ElementosManager");
        elementoIcon = GetNode<TextureRect>("SubViewport/Panel/TipoIcon");
    }

    public void SetupTipo(TipoElemento elemento)
    {
        elementoIcon.Texture = _elementosManager.ObtenerTexturaElemento(elemento); 
    }
}