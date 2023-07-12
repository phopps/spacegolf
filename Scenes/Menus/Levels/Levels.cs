using Godot;

public class Levels : CanvasLayer
{
    public override void _Ready()
    {
        GD.Print("Levels.cs is ready.");
    }

    public void _on_LevelsHomeButton_pressed()
    {
        GD.Print("Home button pressed on Levels. Loading home menu.");
    }

    public void _on_LevelsQuitButton_pressed()
    {
        GD.Print("Quit button pressed on Levels. Quitting game.");
        Main.GetInstance().QuitGame();
    }
}
