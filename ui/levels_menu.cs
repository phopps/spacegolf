using Godot;

public class levels_menu : CanvasLayer
{
    private CanvasLayer pause_menu;

    public override void _Ready()
    {
        pause_menu = GetNode<CanvasLayer>("../pause_menu");
        GD.Print("(levels_menu.cs is ready)");
    }

    public void _on_home_button_pressed()
    {
        GD.Print("Home button pressed.");
        main.GetInstance().current_game_state = main.GAME_STATE.HOME;
        main.GetInstance().UpdateMenus();
    }

    public void _on_quit_button_pressed()
    {
        GD.Print("Quit button pressed.");
        // Quit game
        main.GetInstance().QuitGame();
    }

    public void _on_level_01_button_pressed()
    {
        GD.Print("Level 1 button pressed.");
        main.GetInstance().LoadLevel(1);
        main.GetInstance().current_level = 1;
        pause_menu.Visible = true;
    }

    public void _on_level_02_button_pressed()
    {
        GD.Print("Level 2 button pressed.");
        main.GetInstance().LoadLevel(2);
        main.GetInstance().current_level = 2;
        pause_menu.Visible = true;
    }

    public void _on_level_03_button_pressed()
    {
        GD.Print("Level 3 button pressed.");
        main.GetInstance().LoadLevel(3);
        main.GetInstance().current_level = 3;
        pause_menu.Visible = true;
    }




}
