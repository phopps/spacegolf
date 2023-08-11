using Godot;

public class portal : Area2D
{
    public override void _Ready()
    {
        GD.Print("(portal.cs is ready)");
    }

    public void _on_portal_body_entered(Node body)
    {
        GD.Print(body.Name, " entered portal.");
        main.GetInstance().LoadFinishMenu();
    }
}
