using Godot;

public class main : Node
{
    public static main main_instance; // Singleton pattern
    public audio audio;
    public Node game;
    public Node level_scene;
    // public Node level_base;
    public CanvasLayer home;
    public CanvasLayer levels;
    public CanvasLayer pause;
    public CanvasLayer settings;
    public CanvasLayer credits;
    public CanvasLayer finish;
    public CanvasLayer hud;
    public int highest_level;
    public int current_level;
    public int game_count;
    public string level_name;
    public string level_path;
    public enum GAME_STATE
    {
        LOAD,
        INTRO,
        HOME,
        LEVELS,
        PLAY,
        PAUSE,
        FINISH,
        SETTINGS,
        CREDITS,
        QUIT,
        OUTRO
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
        finish = GetNode<CanvasLayer>("ui/finish_menu");
        // hud = GetNode<CanvasLayer>("ui/hud_menu");
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
        GD.Print("\nLoading level ", level_number, ".");
        level_path = $"res://levels/level_{level_number}.tscn";
        level_name = $"level_{level_number}";
        if (ResourceLoader.Exists(level_path))
        {
            level_scene = ResourceLoader.Load<PackedScene>(level_path).Instance();
            game.AddChild(level_scene);
            current_game_state = GAME_STATE.PLAY;
        }
        else
        {
            GD.Print("Error: missing level scene.");
            current_game_state = GAME_STATE.LEVELS;
        }
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

    public void LoadFinishMenu()
    {
        GD.Print("Loading finish menu.");
        current_game_state = GAME_STATE.FINISH;
        UpdateMenus();
    }

    public void UpdateMenus()
    {
        GD.Print($"Updating menus to current game state {current_game_state}.");
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
                    finish.Visible = false;
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
                    finish.Visible = false;
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
                    finish.Visible = false;
                    pause.Visible = false;
                    break;
                }
            case GAME_STATE.PLAY:
                {
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = false;
                    finish.Visible = false;
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
                    finish.Visible = false;
                    break;
                }
            case GAME_STATE.CREDITS:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = true;
                    finish.Visible = false;
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
                    finish.Visible = false;
                    pause.Visible = true;
                    break;
                }
            case GAME_STATE.FINISH:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = false;
                    finish.Visible = true;
                    pause.Visible = false;
                    break;
                }
            case GAME_STATE.QUIT:
                {
                    GetTree().Paused = true;
                    home.Visible = false;
                    levels.Visible = false;
                    settings.Visible = false;
                    credits.Visible = false;
                    finish.Visible = false;
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
                    finish.Visible = false;
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
        // if (audio.isMuted)
        // {
        //     CloseGame();
        // }
        // else
        // {
        audio.PlayQuitSFX();
        CloseGame();
        // audio.GetNode<AudioStreamPlayer>("quit_sfx").Connect("finished", this, "CloseGame");
        // }
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
