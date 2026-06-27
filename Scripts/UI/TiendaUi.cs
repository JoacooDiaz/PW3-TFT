using Godot;

public partial class TiendaUi : Control
{
	[Signal]
	public delegate void ContinuarPressedEventHandler();
	

	private Button _continuarButton;

	private Label _nombre1;
	private Label _precio1;
	private Node3D _preview1;

	private Label _nombre2;
	private Label _precio2;
	private Node3D _preview2;

	private Label _nombre3;
	private Label _precio3;
	private Node3D _preview3;
	
	private Button _comprar1;
	private Button _comprar2;
	private Button _comprar3;
	private Button _botonRefrescar;

	private PackedScene _pokemon1;
	private PackedScene _pokemon2;
	private PackedScene _pokemon3;
	
	private TiendaManager _tiendaManager;
	
	[Signal]
public delegate void RefrescarPressedEventHandler();


	public override void _Ready()
	{
		
		if (GetParent() is SubViewport)
{
	SetPhysicsProcess(false);
}
		
		_tiendaManager =
	GetNode<TiendaManager>(
        "/root/TiendaManager"
	);
	
		_continuarButton =
			GetNode<Button>("ContinuarButton");

		_continuarButton.Pressed +=
			OnContinuarPressed;

		_nombre1 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta1/Nombre"
			);

		_precio1 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta1/Precio"
			);

		_preview1 =
			GetNode<Node3D>(
				"HBoxContainer/Tarjeta1/SubViewportContainer/SubViewport/Node3D"
			);

		_nombre2 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta2/Nombre"
			);

		_precio2 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta2/Precio"
			);

		_preview2 =
			GetNode<Node3D>(
				"HBoxContainer/Tarjeta2/SubViewportContainer/SubViewport/Node3D"
			);

		_nombre3 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta3/Nombre"
			);

		_precio3 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta3/Precio"
			);

		_preview3 =
			GetNode<Node3D>(
				"HBoxContainer/Tarjeta3/SubViewportContainer/SubViewport/Node3D"
			);
			
			_comprar1 =
	GetNode<Button>(
        "HBoxContainer/Tarjeta1/Comprar"
	);

_comprar2 =
	GetNode<Button>(
        "HBoxContainer/Tarjeta2/Comprar"
	);

_comprar3 =
	GetNode<Button>(
        "HBoxContainer/Tarjeta3/Comprar"
	);
	
	_comprar1.Pressed += OnComprar1;
_comprar2.Pressed += OnComprar2;
_comprar3.Pressed += OnComprar3;


_botonRefrescar =
	GetNode<Button>("BotonRefrescar");
	
	_botonRefrescar.Pressed +=
	OnRefrescarPressed;
	
	}

	private void OnContinuarPressed()
	{
		EmitSignal(
			SignalName.ContinuarPressed
		);
	}
	
private void OnComprar1()
{
	if (_tiendaManager.ComprarPokemon(_pokemon1))
	{
		_comprar1.Disabled = true;
		_comprar1.Text = "Comprado";
	}
}
private void OnComprar2()
{
	if (_tiendaManager.ComprarPokemon(_pokemon2))
	{
		_comprar2.Disabled = true;
		_comprar2.Text = "Comprado";
	}
}

private void OnComprar3()
{
	if (_tiendaManager.ComprarPokemon(_pokemon3))
	{
		_comprar3.Disabled = true;
		_comprar3.Text = "Comprado";
	}
}

private void OnRefrescarPressed()
{
	EmitSignal(
		SignalName.RefrescarPressed
	);
}
	
	public void ConfigurarTarjeta(
	int indice,
	PackedScene escena
)
{
	
	GD.Print(preview.GetParent().Name);
	Entidad entidad =
		escena.Instantiate<Entidad>();
		
		

	if (indice == 0)
	{
		GD.Print(indice);
		_pokemon1 = escena;

		_nombre1.Text =
			entidad.Data.Nombre;

		_precio1.Text =
			"$" + entidad.Data.Precio;

		foreach (Node hijo in _preview1.GetChildren())
{
	if (hijo is Entidad)
		hijo.QueueFree();
}

Entidad preview =
escena.Instantiate<Entidad>();

preview.EstadoActual =
EstadoEntidad.Idle;

preview.GetNode<Sprite3D>("BarraDeVida").Visible = false;
preview.GetNode<Sprite3D>("InfoAccion").Visible = false;
preview.GetNode<Sprite3D>("IconCuracio").Visible = false;
preview.GetNode<Sprite3D>("IconTipo").Visible = false;
preview.GetNode<Node3D>("Aura").Visible = false;

_preview1.AddChild(preview);
GD.Print(preview.GetParent().Name);
preview.Position = Vector3.Zero;
preview.GlobalPosition = Vector3.Zero;
/*Node3D cuerpo = preview.GetNode<Node3D>("Cuerpo");
cuerpo.Position = Vector3.Zero;
cuerpo.Rotation = Vector3.Zero;
cuerpo.Scale = Vector3.One;*/

preview.Rotation = Vector3.Zero;
preview.Scale = Vector3.One * 0.7f;
	}

	if (indice == 1)
	{
		_pokemon2 = escena;

		_nombre2.Text =
			entidad.Data.Nombre;

		_precio2.Text =
			"$" + entidad.Data.Precio;

		foreach (Node hijo in _preview2.GetChildren())
{
	if (hijo is Entidad)
		hijo.QueueFree();
}

Entidad preview =
	escena.Instantiate<Entidad>();

preview.EstadoActual =
	EstadoEntidad.Idle;
	
preview.GetNode<Sprite3D>("BarraDeVida").Visible = false;
preview.GetNode<Sprite3D>("InfoAccion").Visible = false;
preview.GetNode<Sprite3D>("IconCuracio").Visible = false;
preview.GetNode<Sprite3D>("IconTipo").Visible = false;
preview.GetNode<Node3D>("Aura").Visible = false;

_preview2.AddChild(preview);
GD.Print(preview.GetParent().Name);
preview.Position = Vector3.Zero;
preview.GlobalPosition = Vector3.Zero;
/*Node3D cuerpo = preview.GetNode<Node3D>("Cuerpo");
cuerpo.Position = Vector3.Zero;
cuerpo.Rotation = Vector3.Zero;
cuerpo.Scale = Vector3.One;*/

preview.Rotation = Vector3.Zero;
preview.Scale = Vector3.One * 0.7f;
	}

	if (indice == 2)
	{
		_pokemon3 = escena;

		_nombre3.Text =
			entidad.Data.Nombre;

		_precio3.Text =
			"$" + entidad.Data.Precio;

		foreach (Node hijo in _preview3.GetChildren())
{
	if (hijo is Entidad)
		hijo.QueueFree();
}

Entidad preview =
	escena.Instantiate<Entidad>();

preview.EstadoActual =
	EstadoEntidad.Idle;
	
preview.GetNode<Sprite3D>("BarraDeVida").Visible = false;
preview.GetNode<Sprite3D>("InfoAccion").Visible = false;
preview.GetNode<Sprite3D>("IconCuracio").Visible = false;
preview.GetNode<Sprite3D>("IconTipo").Visible = false;
preview.GetNode<Node3D>("Aura").Visible = false;

_preview3.AddChild(preview);
GD.Print(preview.GetParent().Name);
preview.Position = Vector3.Zero;
preview.GlobalPosition = Vector3.Zero;
/*Node3D cuerpo = preview.GetNode<Node3D>("Cuerpo");
cuerpo.Position = Vector3.Zero;
cuerpo.Rotation = Vector3.Zero;
cuerpo.Scale = Vector3.One;*/

preview.Rotation = Vector3.Zero;
preview.Scale = Vector3.One * 0.7f;
	}
}

public void ResetearBotones()
{
	_comprar1.Disabled = false;
	_comprar1.Text = "Comprar";

	_comprar2.Disabled = false;
	_comprar2.Text = "Comprar";

	_comprar3.Disabled = false;
	_comprar3.Text = "Comprar";
}

}
