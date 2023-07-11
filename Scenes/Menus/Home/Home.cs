using Godot;

public class Home : CanvasLayer
{
    public override void _Ready()
    {
        GD.Print("Home.cs is ready.");
    }

    public void _on_Play_pressed()
    {
        GD.Print("Play button pressed on Home. Starting game.");
    }

    public void _on_Quit_pressed()
    {
        GD.Print("Quit button pressed on Home. Quitting game.");
        Main.GetInstance().QuitGame();
    }
}
