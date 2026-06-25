using Godot;
using System;
using System.Threading.Tasks;

public partial class Entidad : CharacterBody3D
{
    [Export]
    public EntidadData Data;

    [Export]
    public BarraDeVida barraDeVida;

    [Export]
    private InfoAccion InfoAccion;

    [Export]
    private IconCuracion InfoCuracion;

    private IconTipo _iconTipo;

    [Export]
    private Aura aura; 

    private IconTipo _iconTipo;

    private NavigationAgent3D _navigation;

    public EstadoEntidad EstadoActual = EstadoEntidad.Idle;

    public int VidaActual;

    private float _cooldownAtaque;

    private float _multiplicadorDaño = 1.0f;

    private float _tiempoBuffActual = 0.0f;

    public Entidad ObjetivoActual;

    public SpawnNode Equipo;

    public SpawnNode EquipoEnemigo;

    private float _alturaFestejo = 0.5f;

    private float _velocidadFestejo = 4.0f;

    private Vector3 _posicionFestejo;

    private float _tiempoFestejo = 0.0f;

    private ElementosManager _elementosManager;

    [Signal]
    public delegate void EntidadMurioEventHandler(
        Entidad entidad
    );
public override void _Ready()
{
    _iconTipo = GetNodeOrNull<IconTipo>("IconTipo");
    _navigation = GetNode<NavigationAgent3D>("NavigationAgent3D");
    _elementosManager = GetNode<ElementosManager>("/root/ElementosManager");

    if (Data == null)
    {
        GD.Print(Name + " no tiene EntidadData.");
        return;
    }

    VidaActual = Data.Vida;
    barraDeVida.setUpBarra(VidaActual, Data.Elemento);
    if (_iconTipo != null)
    {
        _iconTipo.Texture = _elementosManager.ObtenerTexturaElemento(Data.Elemento);
    }
    InfoAccion.Limpiar();
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

        ActualizarBuffs(delta);

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
        _navigation.TargetPosition =
            ObjetivoActual.GlobalPosition;

        Vector3 siguientePunto =
            _navigation.GetNextPathPosition();

        Vector3 direccion =
            (siguientePunto - GlobalPosition).Normalized();

        direccion.Y = 0;

        MirarObjetivo(delta);

        Velocity =
            direccion * Data.VelocidadMovimiento;

        MoveAndSlide();
    }

    private async Task Atacar(double delta)
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
                await EjecutarAsistencia();
                break;
        }
    }

    private void EjecutarAtaque()
    {
        if (ObjetivoActual == null)
            return;

        MirarObjetivo(0.1);

        int dañoFinal = _elementosManager.CalcularDañoFinal(
            Mathf.RoundToInt(Data.Daño * _multiplicadorDaño),
            Data.Elemento,
            ObjetivoActual.Data.Elemento
        );

        ObjetivoActual.RecibirDaño(dañoFinal, Data.Elemento);

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

        MirarObjetivo(0.1);

        aliado.Curar(Data.Curacion);

        GD.Print(
            Data.Nombre +
            " cura a " +
            aliado.Data.Nombre
        );
    }

    private async Task EjecutarAsistencia()
    {
        Entidad aliado = BuscarAliadoParaAsistir();

        if (aliado == null)
        {
            EjecutarAtaque();

            return;
        }

        MirarObjetivo(0.1);

        aliado.RecibirBuffDaño(
            Data.MultiplicadorAsistencia,
            Data.DuracionAsistencia
        );

        await aliado.aura.MostrarAura(ColoresAura.Buff); 

        GD.Print(
            Data.Nombre +
            " fortalece a " +
            aliado.Data.Nombre
        );
    }

    //???
    public void RecibirDaño(int daño, TipoElemento elemento)
    {
        VidaActual -= daño;

        barraDeVida.ActualizarBarra(VidaActual);

        InfoAccion.MostrarInfo(daño, elemento);

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

    public async void Curar(int cantidad)
    {
        VidaActual += cantidad;

        InfoCuracion.MostrarInfo();

        if (VidaActual > Data.Vida)
        {
            VidaActual = Data.Vida;
        }

        barraDeVida.ActualizarBarra(VidaActual);
        InfoCuracion.MostrarInfo(); 
        await aura.MostrarAura(ColoresAura.Curar);         
        barraDeVida.ActualizarBarra(VidaActual); 
    }

    public void Morir()
    {
        EstadoActual = EstadoEntidad.Muerto;

        InfoAccion.Limpiar();
        InfoAccion.Limpiar(); 
        aura.Apagar(); 

        Velocity = Vector3.Zero;

        RotateObjectLocal(
            Vector3.Right,
            Mathf.DegToRad(90)
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

    private Entidad BuscarAliadoParaAsistir()
    {
        foreach (Entidad entidad in Equipo.Entidades)
        {
            if (entidad == this)
                continue;

            if (
                entidad.EstadoActual ==
                EstadoEntidad.Muerto
            )
            {
                continue;
            }

            if (
                entidad.Data.Rol ==
                TipoRol.Atacante
            )
            {
                return entidad;
            }
        }

        return null;
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

    //Los buffs estan atados al _PhysicsProcess, funciona pero si alguien tiene tiempo de cambiarlo...
    public void RecibirBuffDaño(
        float multiplicador,
        float duracion
    )
    {
        _multiplicadorDaño = multiplicador;

        _tiempoBuffActual = duracion;

        GD.Print(
            Data.Nombre +
            " recibe buff de daño."
        );
    }

    private void ActualizarBuffs(double delta)
    {
        if (_tiempoBuffActual <= 0)
            return;

        _tiempoBuffActual -= (float)delta;

        if (_tiempoBuffActual <= 0)
        {
            _multiplicadorDaño = 1.0f;

            GD.Print(
                Data.Nombre +
                " perdió su buff."
            );
        }
    }

    private void MirarObjetivo(double delta)
    {
        if (ObjetivoActual == null)
            return;

        Vector3 direccion =
            ObjetivoActual.GlobalPosition - GlobalPosition;

        direccion.Y = 0;

        if (direccion == Vector3.Zero)
            return;

        Vector3 objetivoRotacion =
            new Vector3(
                Rotation.X,
                Mathf.Atan2(-direccion.X, -direccion.Z),
                Rotation.Z
            );

        Rotation = Rotation.Lerp(
            objetivoRotacion,
            8.0f * (float)delta
        );
    }
}