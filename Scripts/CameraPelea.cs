using Godot;
using System;

public partial class CameraPelea : Camera3D
{
    [Export]
    private float _intensidadRespiracion = 0.08f;

    [Export]
    private float _velocidadRespiracion = 1.2f;

    private Vector3 _posicionInicial;

    private float _tiempo = 0.0f;

    public override void _Ready()
    {
        _posicionInicial = Position;
    }

    public override void _Process(double delta)
    {
        _tiempo += (float)delta;

        float offsetY =
            Mathf.Sin(
                _tiempo *
                _velocidadRespiracion
            ) * _intensidadRespiracion;

        float offsetZ =
            Mathf.Cos(
                _tiempo *
                (_velocidadRespiracion * 0.5f)
            ) * (_intensidadRespiracion * 0.5f);

        Position = new Vector3(
            _posicionInicial.X,
            _posicionInicial.Y + offsetY,
            _posicionInicial.Z + offsetZ
        );
    }
}