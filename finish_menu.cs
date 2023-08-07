using Godot;

public class finish_menu : CanvasLayer
{
    public override void _Ready()
    {
        GD.Print("(finish_menu.cs is ready)");
    }

    public void _on_next_button_pressed()
    {
        GD.Print("Next button pressed.");
        main.GetInstance().RemovePreviousLevel();
        main.GetInstance().LoadNextLevel();
    }

    public void _on_replay_button_pressed()
    {
        GD.Print("Replay button pressed.");
        main.GetInstance().RemovePreviousLevel();
        main.GetInstance().ReplayLevel();
    }

    public void _on_home_button_pressed()
    {
        GD.Print("Home button pressed.");
        Visible = false;
        main.GetInstance().RemovePreviousLevel();
        main.GetInstance().current_game_state = main.GAME_STATE.HOME;
        main.GetInstance().UpdateMenus();
    }

    public void _on_quit_button_pressed()
    {
        GD.Print("Quit button pressed.");
        main.GetInstance().RemovePreviousLevel();
        main.GetInstance().current_game_state = main.GAME_STATE.QUIT;
        main.GetInstance().QuitGame();
    }
}
