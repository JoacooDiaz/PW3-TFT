using Godot;
using System;

public partial class Entidad : CharacterBody3D
{
    [Export]
    public EntidadData Data;

    [Export]
    public BarraDeVida barraDeVida;

    private NavigationAgent3D _navigation;

    public EstadoEntidad EstadoActual = EstadoEntidad.Idle;

    public int VidaActual;

    private float _cooldownAtaque;

    public Entidad ObjetivoActual;

    public SpawnNode Equipo;

    public SpawnNode EquipoEnemigo;

    private float _alturaFestejo = 0.5f;

    private float _velocidadFestejo = 4.0f;

    private Vector3 _posicionFestejo;

    private float _tiempoFestejo = 0.0f;

    [Signal]
    public delegate void EntidadMurioEventHandler(
        Entidad entidad
    );

    public override void _Ready()
    {
        _navigation = GetNode<NavigationAgent3D>("NavigationAgent3D");

        if (Data == null)
        {
            GD.Print(Name + " no tiene EntidadData.");
            return;
        }

        VidaActual = Data.Vida;
        barraDeVida.setUpBarra(VidaActual); 
    }

    public override void _PhysicsProcess(double delta)
    {
        if (EstadoActual == EstadoEntidad.Festejo)
        {
            FestejarVictoria();

            return;
        }

        if (EstadoActual != EstadoEntidad.Pelea)
        {
            return;
        }

        if (ObjetivoActual == null)
        {
            BuscarObjetivo();

            return;
        }

        if (!IsInstanceValid(ObjetivoActual))
        {
            BuscarObjetivo();

            return;
        }

        if (ObjetivoActual.EstadoActual == EstadoEntidad.Muerto)
        {
            BuscarObjetivo();

            return;
        }

        float distancia =
            GlobalPosition.DistanceTo(ObjetivoActual.GlobalPosition);

        if (distancia > Data.DistanciaAtaque)
        {
            MoverHaciaObjetivo(delta);
        }
        else
        {
            Atacar(delta);
        }
    }

    public void CambiarEstado(EstadoEntidad nuevoEstado)
    {
        EstadoActual = nuevoEstado;

        switch (EstadoActual)
        {
            case EstadoEntidad.Festejo:
                Velocity = Vector3.Zero;
                _posicionFestejo = GlobalPosition;
                _tiempoFestejo = 0.0f;
                break;

            case EstadoEntidad.Pelea:
                GD.Print(Data.Nombre + " Entra a la pelea");
                BuscarObjetivo();
                break;
        }
    }

    private void BuscarObjetivo()
    {
        if (EquipoEnemigo == null)
        {
            GD.Print(Data.Nombre + " No encontro un enemigo");
            return;
        }

        float mejorDistancia = Mathf.Inf;

        Entidad mejorObjetivo = null;

        foreach (Entidad entidad in EquipoEnemigo.Entidades)
        {
            if (entidad.EstadoActual == EstadoEntidad.Muerto)
                continue;

            float distancia =
                GlobalPosition.DistanceTo(entidad.GlobalPosition);

            if (distancia < mejorDistancia)
            {
                mejorDistancia = distancia;

                mejorObjetivo = entidad;
            }
        }

        ObjetivoActual = mejorObjetivo;
    }

    private void MoverHaciaObjetivo(double delta)
    {
        _navigation.TargetPosition = ObjetivoActual.GlobalPosition;

        Vector3 siguientePunto =
            _navigation.GetNextPathPosition();

        Vector3 direccion =
            (siguientePunto - GlobalPosition).Normalized();

        Velocity =
            direccion * Data.VelocidadMovimiento;

        MoveAndSlide();
    }

    private void Atacar(double delta)
    {
        _cooldownAtaque -= (float)delta;

        if (_cooldownAtaque > 0)
            return;

        _cooldownAtaque = Data.TiempoEntreAtaques;

        switch (Data.Rol)
        {
            case TipoRol.Atacante:
                EjecutarAtaque();
                break;

            case TipoRol.Curador:
                EjecutarCuracion();
                break;

            case TipoRol.Asistente:
                EjecutarAsistencia();
                break;
        }
    }

    private void EjecutarAtaque()
    {
        if (ObjetivoActual == null)
            return;

        int dañoFinal = CalcularDañoFinal(
            Data.Daño,
            Data.Elemento,
            ObjetivoActual.Data.Elemento
        );

        ObjetivoActual.RecibirDaño(dañoFinal);

        GD.Print(
            Data.Nombre +
            " ataca a " +
            ObjetivoActual.Data.Nombre +
            " por " +
            dañoFinal
        );
    }

    private void EjecutarCuracion()
    {
        Entidad aliado = BuscarAliadoHerido();

        if (aliado == null)
        {
            EjecutarAtaque();

            return;
        }

        aliado.Curar(Data.Daño);

        GD.Print(
            Data.Nombre +
            " cura a " +
            aliado.Data.Nombre
        );
    }

    private void EjecutarAsistencia()
    {
        Entidad aliado = BuscarAliado();

        if (aliado == null)
        {
            EjecutarAtaque();

            return;
        }

        GD.Print(
            Data.Nombre +
            " fortalece a " +
            aliado.Data.Nombre
        );
    }

    public void RecibirDaño(int daño)
    {
        VidaActual -= daño;

        barraDeVida.ActualizarBarra(VidaActual); 

        GD.Print(
            Data.Nombre +
            " recibe " +
            daño +
            " daño."
        );

        if (VidaActual <= 0)
        {
            Morir();
        }
    }

    public void Curar(int cantidad)
    {
        VidaActual += cantidad;

        if (VidaActual > Data.Vida)
        {
            VidaActual = Data.Vida;
        }
        
        barraDeVida.ActualizarBarra(VidaActual); 
    }

    private void Morir()
    {
        EstadoActual = EstadoEntidad.Muerto;

        Velocity = Vector3.Zero;

        RotationDegrees = new Vector3(
            90,
            RotationDegrees.Y,
            RotationDegrees.Z
        );

        barraDeVida.OcultarBarra(); 

        EmitSignal(
            SignalName.EntidadMurio,
            this
        );
    }

    private Entidad BuscarAliadoHerido()
    {
        foreach (Entidad entidad in Equipo.Entidades)
        {
            if (entidad == this)
                continue;

            if (entidad.EstadoActual == EstadoEntidad.Muerto)
                continue;

            if (entidad.VidaActual < entidad.Data.Vida)
            {
                return entidad;
            }
        }

        return null;
    }

    private Entidad BuscarAliado()
    {
        foreach (Entidad entidad in Equipo.Entidades)
        {
            if (entidad == this)
                continue;

            if (entidad.EstadoActual == EstadoEntidad.Muerto)
                continue;

            return entidad;
        }

        return null;
    }

    private int CalcularDañoFinal(
        int dañoBase,
        TipoElemento atacante,
        TipoElemento defensor
    )
    {
        float multiplicador = 1.0f;

        if (
            atacante == TipoElemento.Fuego &&
            defensor == TipoElemento.Planta
        )
        {
            multiplicador = 1.5f;
        }

        if (
            atacante == TipoElemento.Agua &&
            defensor == TipoElemento.Fuego
        )
        {
            multiplicador = 1.5f;
        }

        if (
            atacante == TipoElemento.Planta &&
            defensor == TipoElemento.Agua
        )
        {
            multiplicador = 1.5f;
        }

        return Mathf.RoundToInt(dañoBase * multiplicador);
    }

    public void FestejarVictoria()
    {
        Velocity = Vector3.Zero;

        _tiempoFestejo += (float)GetPhysicsProcessDeltaTime();

        float salto =
            (Mathf.Sin(
                _tiempoFestejo *
                _velocidadFestejo
            ) + 1.0f) * 0.5f;

        Vector3 posicionObjetivo =
            _posicionFestejo;

        posicionObjetivo.Y +=
            salto * _alturaFestejo;

        GlobalPosition = posicionObjetivo;
    }
}