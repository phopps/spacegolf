using Godot;

public class Home : CanvasLayer
{
    public override void _Ready()
    {
        GD.Print("Home.cs is ready.");
    }

    public void _on_HomePlayButton_pressed()
    {
        GD.Print("Play button pressed on Home. Starting game. Loading highest unlocked level.");
    }

    public void _on_HomeLevelsButton_pressed()
    {
        GD.Print("Levels button pressed on Home. Loading level select menu.");
        Main.GetInstance().LoadLevelsMenu();
    }

    public void _on_HomeSettingsButton_pressed()
    {
        GD.Print("Settings button pressed on Home. Loading settings menu.");
    }

    public void _on_HomeCreditsButton_pressed()
    {
        GD.Print("Credits button pressed on Home. Loading credits screen.");
    }

    public void _on_HomeQuitButton_pressed()
    {
        GD.Print("Quit button pressed on Home. Quitting game.");
        Main.GetInstance().QuitGame();
    }
}
