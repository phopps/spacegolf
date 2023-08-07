using Godot;

public class main : Node
{
    public static main main_instance; // Singleton pattern
    public audio audio;
    public Node game;
    public CanvasLayer home;
    public CanvasLayer levels;
    public CanvasLayer pause;
    public CanvasLayer settings;
    public CanvasLayer credits;
    public int highest_level;
    public int current_level;
    public int game_count;
    public string level_name;
    public enum GAME_STATE
    {
        LOAD,
        HOME,
        LEVELS,
        PLAY,
        PAUSE,
        SETTINGS,
        CREDITS,
        QUIT
    }
    public GAME_STATE current_game_state;

    public static main GetInstance()
    {
        return main_instance;
    }

    public override void _Ready()
    {
        // Check for instance of main singleton
        if (main_instance == null)
        {
            main_instance = this;
        }
        else
        {
            GD.Print("Error: Duplicate main instance.");
            QueueFree();
        }

        current_game_state = GAME_STATE.LOAD;
        audio = GetNode<audio>("audio");
        game = GetNode<Node>("game");
        home = GetNode<CanvasLayer>("ui/home_menu");
        levels = GetNode<CanvasLayer>("ui/levels_menu");
        pause = GetNode<CanvasLayer>("ui/pause_menu");
        settings = GetNode<CanvasLayer>("ui/settings_menu");
        credits = GetNode<CanvasLayer>("ui/credits_menu");
        current_level = 1;
        highest_level = 1; // Set default highest level unlocked
        level_name = "";
        audio.PlayBGM();
        current_game_state = GAME_STATE.HOME;
        UpdateMenus();
        GD.Print("(main.cs is ready)");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if ((@event is InputEventKey) && @event.IsActionPressed("Quit"))
        {
            GD.Print("Quit key pressed.");
            current_game_state = GAME_STATE.QUIT;
            QuitGame();
        }
        if ((@event is InputEventKey) && @event.IsActionPressed("Select"))
        {
            GD.Print("Select key pressed.");
            audio.PlaySelectSFX();
        }
    }

    public void LoadHighestLevel()
    {
        GD.Print("Loading highest unlocked level.");
        LoadLevel(highest_level); // Load highest unlocked level
    }

    public void LoadLevel(int level_number)
    {
        // Level contains map terrain, gates, portal, player
        GD.Print("Loading level ", level_number, ".");
        level_name = $"res://levels/level_{level_number}.tscn";
        GD.Print("Level name: ", level_name);
        game.AddChild(ResourceLoader.Load<PackedScene>(level_name).Instance());
        current_game_state = GAME_STATE.PLAY;
        UpdateMenus();
    }

    public void ReplayLevel()
    {
        GD.Print("Replaying current level.");
        LoadLevel(current_level);
    }

    public void LoadNextLevel()
    {
        GD.Print("Loading next level.");
        current_level += 1;
        if (current_level > highest_level)
        {
            highest_level = current_level;
        }
        LoadLevel(current_level);
    }

    public void LoadLevelsMenu()
    {
        GD.Print("Loading levels menu.");
        current_game_state = GAME_STATE.LEVELS;
        UpdateMenus();
    }

    public void PauseGame()
    {
        GD.Print("Pausing game.");
        current_game_state = GAME_STATE.PAUSE;
        UpdateMenus();
        GetTree().Paused = true; // Pause game physics
    }

    public void ResumeGame()
    {
        GD.Print("Resuming game.");
        current_game_state = GAME_STATE.PLAY;
        UpdateMenus();
        GetTree().Paused = false; // Unpause game physics
    }

    public void LoadSettingsMenu()
    {
        GD.Print("Loading settings menu.");
        current_game_state = GAME_STATE.SETTINGS;
        UpdateMenus();
    }

    public void LoadCreditsMenu()
    {
        GD.Print("Loading credits menu.");
        current_game_state = GAME_STATE.CREDITS;
        UpdateMenus();
    }

    public void UpdateMenus()
    {
        GD.Print($"Updating menus to {current_game_state}.");
        // Set menu visibilities based on current game state
        switch (current_game_state)
        {
            case GAME_STATE.LOAD:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = false;
                    pause.Visible = false;
                    break;
                }
            case GAME_STATE.HOME:
                {
                    GetTree().Paused = true;
                    home.Visible = true;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = false;
                    pause.Visible = false;
                    pause.GetNode<Button>("button_container/pause_button").Pressed = false;
                    break;
                }
            case GAME_STATE.LEVELS:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = true;
                    settings.Visible = false;
                    credits.Visible = false;
                    pause.Visible = false;
                    break;
                }
            case GAME_STATE.PLAY:
                {
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = false;
                    pause.Visible = true;
                    GetTree().Paused = false;
                    pause.GetNodeOrNull<Button>("button_container/pause_button").Pressed = false;
                    break;
                }
            case GAME_STATE.PAUSE:
                {
                    GetTree().Paused = true;
                    pause.Visible = true;
                    // pause.GetNodeOrNull<Button>("ui/pause_menu/pause_button").Pressed = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = true;
                    credits.Visible = false;
                    break;
                }
            case GAME_STATE.CREDITS:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = true;
                    pause.Visible = true;
                    break;
                }
            case GAME_STATE.SETTINGS:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = true;
                    credits.Visible = false;
                    pause.Visible = true;
                    break;
                }
            case GAME_STATE.QUIT:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = false;
                    pause.Visible = false;
                    break;
                }
            default:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = false;
                    pause.Visible = false;
                    break;
                }
        }
    }

    public void QuitGame()
    {
        GD.Print("Quitting game.");
        current_game_state = GAME_STATE.QUIT;
        // UpdateMenus();
        if (audio.isMuted)
        {
            CloseGame();
        }
        else
        {
            audio.PlayQuitSFX();
            audio.GetNode<AudioStreamPlayer>("quit_sfx").Connect("finished", this, "CloseGame");
        }
    }

    public void CloseGame()
    {
        GD.Print("Closing game.");
        GetTree().Quit();
    }

    public void RemovePreviousLevel()
    {
        GD.Print("Removing previous level.");
        game_count = game.GetChildCount();
        if (game_count > 0)
        {
            foreach (Node child in game.GetChildren())
            {
                child.QueueFree();
            }
        }
    }
}
