using Godot;

public partial class IconTipo : Sprite3D
{
    public void SetupTipo(TipoElemento elemento, Texture2D textura)
    {
        if (textura == null)
        {
            Visible = false;
            return;
        }

        Texture = textura;
        Visible = true;
    }
}