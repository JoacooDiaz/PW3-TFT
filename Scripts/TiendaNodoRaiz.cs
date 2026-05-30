using Godot;
using System.Collections.Generic;

public partial class TiendaNodoRaiz : Node
{
    [Export]
    public Node3D SP_Comprables;

    private TiendaManager _tiendaManager;

    private List<PackedScene> _ofertasActuales =
        new();

    public override void _Ready()
    {
        _tiendaManager =
            GetNode<TiendaManager>(
                "/root/TiendaManager"
            );

        CargarTienda();
    }

    public void CargarTienda()
    {
        _ofertasActuales =
            _tiendaManager.GenerarTienda(5);

        int indice = 0;

        foreach (
            Node hijo
            in SP_Comprables.GetChildren()
        )
        {
            if (
                indice >= _ofertasActuales.Count
            )
            {
                break;
            }

            Node3D slot =
                hijo as Node3D;

            if (slot == null)
            {
                continue;
            }

            PackedScene escena =
                _ofertasActuales[indice];

            Entidad entidad =
                escena.Instantiate<Entidad>();

            slot.AddChild(entidad);

            entidad.Position =
                Vector3.Zero;

            indice++;
        }
    }
}