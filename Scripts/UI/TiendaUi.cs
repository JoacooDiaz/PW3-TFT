using Godot;

public partial class TiendaUi : Control
{
    [Signal]
    public delegate void ContinuarPressedEventHandler();

    private Button _continuarButton;

    public override void _Ready()
    {
        _continuarButton = GetNode<Button>("ContinuarButton");

        _continuarButton.Pressed += OnContinuarPressed;
    }

    private void OnContinuarPressed()
    {
        EmitSignal(SignalName.ContinuarPressed);
    }
}