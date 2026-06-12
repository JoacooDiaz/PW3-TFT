using Godot;

public partial class IconTipo : Sprite3D
{
    public void SetupTipo(TipoElemento elemento, Texture2D textura)
    {
        GD.Print("[IconTipo] SetupTipo llamado con: " + elemento + " textura: " + (textura != null ? "OK" : "NULL"));

        if (textura == null)
        {
            Visible = false;
            return;
        }

        Texture = textura;
        Visible = true;
    }
}