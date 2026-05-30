using Godot;

public partial class SiguienteNivelButton : Node3D
{
    [Export]
    private Camera3D Camera;

    private Area3D _area;

    public override void _Ready()
    {
        _area = GetNode<Area3D>("Area3D");

        GD.Print("Button ready");

        if (Camera == null)
        {
            GD.PrintErr(
                "No Camera assigned to SiguienteNivelButton."
            );
        }
    }

    public override void _Input(
        InputEvent @event
    )
    {
        if (
            @event is not InputEventMouseButton mouse
        )
        {
            return;
        }

        if (
            !mouse.Pressed ||
            mouse.ButtonIndex != MouseButton.Left
        )
        {
            return;
        }

        VerificarClick(mouse.Position);
    }

    private void VerificarClick(
        Vector2 mousePosition
    )
    {
        if (Camera == null)
        {
            return;
        }

        Vector3 rayOrigin =
            Camera.ProjectRayOrigin(
                mousePosition
            );

        Vector3 rayDirection =
            Camera.ProjectRayNormal(
                mousePosition
            );

        PhysicsRayQueryParameters3D query =
            PhysicsRayQueryParameters3D.Create(
                rayOrigin,
                rayOrigin + rayDirection * 1000f
            );

        var result =
            GetWorld3D()
            .DirectSpaceState
            .IntersectRay(query);

        if (result.Count == 0)
        {
            GD.Print("Nothing hit");

            return;
        }

        Node collider =
            result["collider"].As<Node>();

        GD.Print(
            "Hit: ",
            collider.Name
        );

        if (
            collider == _area ||
            collider.GetParent() == _area
        )
        {
            OnContinueButtonClicked();
        }
    }

    private void OnContinueButtonClicked()
    {
        GD.Print("Continue!");
		/* 
        GameManager gameManager =
            GetNode<GameManager>(
                "/root/GameManager"
            );

        gameManager.GoToNextLevel(); */
    }
}