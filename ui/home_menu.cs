using Godot;

public class home_menu : CanvasLayer
{
    public override void _Ready()
    {
        GD.Print("(home_menu.cs is ready)");
    }

    public void _on_play_button_pressed()
    {
        GD.Print("Play button pressed.");
        main.GetInstance().LoadHighestLevel();
    }

    public void _on_levels_button_pressed()
    {
        GD.Print("Levels button pressed.");
        main.GetInstance().LoadLevelsMenu();
    }

    public void _on_settings_button_pressed()
    {
        GD.Print("Settings button pressed.");
        main.GetInstance().LoadSettingsMenu();
    }

    public void _on_credits_button_pressed()
    {
        GD.Print("Credits button pressed.");
        main.GetInstance().LoadCreditsMenu();
    }

    public void _on_quit_button_pressed()
    {
        GD.Print("Quit button pressed.");
        main.GetInstance().QuitGame();
    }
}
