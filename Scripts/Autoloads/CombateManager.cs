using Godot;

public partial class CombateManager : Node
{
    private SpawnNode _aliados;

    private SpawnNode _enemigos;

    [Signal]
    public delegate void VictoriaEventHandler();

    [Signal]
    public delegate void DerrotaEventHandler();

    public void SetupPelea(
        Node3D aliados,
        Node3D enemigos
    )
    {
        _aliados = aliados as SpawnNode;

        _enemigos = enemigos as SpawnNode;

        ConfigurarRelaciones();

        foreach (Entidad aliado in _aliados.Entidades)
        {
            aliado.EntidadMurio += OnEntidadMurio;
        }

        foreach (Entidad enemigo in _enemigos.Entidades)
        {
            enemigo.EntidadMurio += OnEntidadMurio;
        }

        GD.Print("Pelea configurada.");
    }

    private void ConfigurarRelaciones()
    {
        foreach (Entidad aliado in _aliados.Entidades)
        {
            aliado.Equipo = _aliados;

            aliado.EquipoEnemigo = _enemigos;
        }

        foreach (Entidad enemigo in _enemigos.Entidades)
        {
            enemigo.Equipo = _enemigos;

            enemigo.EquipoEnemigo = _aliados;
        }
    }

    public void ComenzarPelea()
    {
        _aliados.CambiarEstadoEntidades(
            EstadoEntidad.Pelea
        );

        _enemigos.CambiarEstadoEntidades(
            EstadoEntidad.Pelea
        );
    }

    public void TerminarPelea(
        bool victoriaJugador
    )
    {
        if (victoriaJugador)
        {
            _aliados.CambiarEstadoEntidades(
                EstadoEntidad.Festejo
            );

            EmitSignal(
                SignalName.Victoria
            );
        }
        else
        {

            _enemigos.CambiarEstadoEntidades(
                EstadoEntidad.Festejo
            ); 

            EmitSignal(
                SignalName.Derrota
            );
        }
    }

    private void OnEntidadMurio(
        Entidad entidad
    )
    {
        GD.Print(
            entidad.Data.Nombre +
            " notificó su muerte."
        );

        VerificarFinPelea();
    }

    private void VerificarFinPelea()
    {
        bool aliadosVivos = false;

        bool enemigosVivos = false;

        foreach (Entidad aliado in _aliados.Entidades)
        {
            if (
                IsInstanceValid(aliado) &&
                aliado.EstadoActual != EstadoEntidad.Muerto
            )
            {
                aliadosVivos = true;

                break;
            }
        }

        foreach (Entidad enemigo in _enemigos.Entidades)
        {
            if (
                IsInstanceValid(enemigo) &&
                enemigo.EstadoActual != EstadoEntidad.Muerto
            )
            {
                enemigosVivos = true;

                break;
            }
        }

        if (!aliadosVivos)
        {
            GD.Print("Derrota.");

            TerminarPelea(false);
        }

        if (!enemigosVivos)
        {
            GD.Print("Victoria.");

            TerminarPelea(true);
        }
    }
}