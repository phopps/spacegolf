using Godot;

public class pause_menu : CanvasLayer
{
    private Button pause_button;
    private Button resume_button;
    private Button fullscreen_button;
    private Button home_button;
    private Button quit_button;
    private Button debug_button;

    public override void _Ready()
    {
        pause_button = GetNode<Button>("button_container/pause_button");
        resume_button = GetNode<Button>("button_container/resume_button");
        fullscreen_button = GetNode<Button>("button_container/fullscreen_button");
        home_button = GetNode<Button>("button_container/home_button");
        quit_button = GetNode<Button>("button_container/quit_button");
        debug_button = GetNode<Button>("button_container/debug_button");
        GD.Print("(pause_menu.cs is ready)");
    }

    public void _on_pause_button_pressed()
    {
        GD.Print("Pause button pressed.");
        main.GetInstance().current_game_state = main.GAME_STATE.PAUSE;
        main.GetInstance().UpdateMenus();
        resume_button.Visible = true;
        pause_button.Visible = false;
        fullscreen_button.Visible = true;
        home_button.Visible = true;
        quit_button.Visible = true;
        debug_button.Visible = true;
    }

    public void _on_resume_button_pressed()
    {
        GD.Print("Resume button pressed.");
        main.GetInstance().current_game_state = main.GAME_STATE.PLAY;
        main.GetInstance().UpdateMenus();
        pause_button.Visible = true;
        resume_button.Visible = false;
        fullscreen_button.Visible = false;
        home_button.Visible = false;
        quit_button.Visible = false;
        debug_button.Visible = false;
    }

    public void _on_home_button_pressed()
    {
        GD.Print("Home button pressed.");

        // TODO: remove game nodes when going back to menu, from any screen (credits, pause, settings, etc.)
        // GetNodeOrNull<Node2D>("../../game/level_1").QueueFree();
        Visible = false;
        main.GetInstance().current_game_state = main.GAME_STATE.HOME;
        main.GetInstance().UpdateMenus();
    }

    public void _on_fullscreen_button_pressed()
    {
        GD.Print("Fullscreen button pressed.");
        OS.WindowFullscreen = !OS.WindowFullscreen;
    }

    public void _on_quit_button_pressed()
    {
        GD.Print("Quit button pressed.");
        main.GetInstance().current_game_state = main.GAME_STATE.QUIT;
        main.GetInstance().QuitGame();
    }

    public void _on_debug_button_pressed()
    {
        GD.Print("Debug button pressed.");
        // Toggle debug menu visibility
    }

    public void _on_mute_button_pressed()
    {
        GD.Print("Mute button pressed.");
        // Toggle audio volume muting
        main.GetInstance().audio.MuteAudio();
    }
}