using Godot;
using static System.Net.Mime.MediaTypeNames;

public partial class TiendaUi : Control
{
	[Signal]
	public delegate void ContinuarPressedEventHandler();
	

	private Button _continuarButton;

	private Label _nombre1;
	private Label _precio1;
	private Label _rol1; 
	private TextureRect _imagen1;

	private Label _nombre2;
	private Label _precio2;
	private Label _rol2; 
	private TextureRect _imagen2;

	private Label _nombre3;
	private Label _precio3;
	private Label _rol3; 
	private TextureRect _imagen3;
	
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

		_rol1 = GetNode<Label>("HBoxContainer/Tarjeta1/Rol");

		_imagen1 =
			GetNode<TextureRect>(
                "HBoxContainer/Tarjeta1/Imagen"
			);

		_nombre2 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta2/Nombre"
			);

		_precio2 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta2/Precio"
			);

		_rol2 = GetNode<Label>("HBoxContainer/Tarjeta2/Rol");

		_imagen2 =
			GetNode<TextureRect>(
                "HBoxContainer/Tarjeta2/Imagen"
			);

		_nombre3 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta3/Nombre"
			);

		_precio3 =
			GetNode<Label>(
                "HBoxContainer/Tarjeta3/Precio"
			);

		_rol3 = GetNode<Label>("HBoxContainer/Tarjeta3/Rol");

		_imagen3 =
			GetNode<TextureRect>(
                "HBoxContainer/Tarjeta3/Imagen"
			);
			
	_comprar1 = GetNode<Button>("HBoxContainer/Tarjeta1/Comprar");
	_comprar2 = GetNode<Button>("HBoxContainer/Tarjeta2/Comprar");
	_comprar3 = GetNode<Button>("HBoxContainer/Tarjeta3/Comprar");	
	_botonRefrescar = GetNode<Button>("BotonRefrescar");
	


	_comprar1.Pressed += OnComprar1;
	_comprar2.Pressed += OnComprar2;
	_comprar3.Pressed += OnComprar3;
	_botonRefrescar.Pressed += OnRefrescarPressed;
	
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
	Entidad entidad =
		escena.Instantiate<Entidad>();

	string auxTextoRol = ""; 

	if (indice == 0)
	{
		_pokemon1 = escena;

		_nombre1.Text =
			entidad.Data.Nombre;

		_precio1.Text = "$" + entidad.Data.Precio;

		_rol1.Text = "Rol: " + entidad.Data.Rol;

		if (!_tiendaManager.PuedeComprarPokemon(escena))
		{
			_comprar1.Disabled = true; 
		} else _comprar1.Disabled = false;
	}

	if (indice == 1)
	{
		_pokemon2 = escena;

		_nombre2.Text =
			entidad.Data.Nombre;

		_precio2.Text =
			"$" + entidad.Data.Precio;

		_rol2.Text = "Rol: " + entidad.Data.Rol; 

		if (!_tiendaManager.PuedeComprarPokemon(escena))
		{
			_comprar2.Disabled = true; 
		} else _comprar2.Disabled = false;

        }

	if (indice == 2)
	{
		_pokemon3 = escena;

		_nombre3.Text =
			entidad.Data.Nombre;

		_precio3.Text =
			"$" + entidad.Data.Precio;

		_rol3.Text = "Rol: " + entidad.Data.Rol; 
	
		if (!_tiendaManager.PuedeComprarPokemon(escena))
		{
			_comprar3.Disabled = true; 
		} else _comprar3.Disabled = false;
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
