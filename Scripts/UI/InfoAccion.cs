using Godot;
public partial class InfoAccion : InfoVisual
{
    private Label _infoNum;
    public override void _Ready()
    {
        _infoNum = GetNode<Label>("SubViewport/Panel/InfoNum");
        Apagar();
    }
    public async void MostrarInfo(int num, TipoElemento elemento)
    {
        _infoNum.Text = "-" + num;
        await MostrarTemporal();
        Limpiar();
    }
    public void Limpiar()
    {
        _infoNum.Text = "";
    }
} 