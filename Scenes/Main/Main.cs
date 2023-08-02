using Godot;

public class Main : Node
{
    public Node sceneHome;
    public Node sceneLevels;
    public Node scenePrevious;
    public Node sceneCurrent;
    public Node sceneNext;
    private static Main instance; // Singleton pattern
    private Audio audio;

    public static Main GetInstance()
    {
        return instance;
    }

    public override void _Ready()
    {
        sceneHome = ResourceLoader.Load<PackedScene>("res://Scenes/Menus/Home/Home.tscn").Instance();
        sceneLevels = ResourceLoader.Load<PackedScene>("res://Scenes/Menus/Levels/Levels.tscn").Instance();
        // sceneCurrent = sceneHome;
        // scenePrevious = null;
        // sceneNext = null;

        // Check for instance of Main singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GD.Print("Error: Duplicate 'Main' instance.");
            // sceneCurrent = null;
            QueueFree();
        }

        audio = GetNode<Audio>("Audio");
        audio.PlayAudioBGM();
        GD.Print("Main.cs is ready.");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if ((@event is InputEventKey) && @event.IsActionPressed("Quit"))
        {
            GD.Print("Quit key pressed. Quitting game.");
            QuitGame();
        }
        if ((@event is InputEventKey) && @event.IsActionPressed("Select"))
        {
            GD.Print("Select key pressed.");
            audio.PlayAudioSelect();
        }
    }

    public void QuitGame()
    {
        audio.PlayAudioQuit();
        audio.GetNode<AudioStreamPlayer>("AudioQuit").Connect("finished", this, "CloseGame");
    }

    public void CloseGame()
    {
        GetTree().Quit();
    }

    public void PlayGame()
    {
        // Load highest unlocked level by default
        // Level contains map terrain, gates, portal, player

    }

    public void LoadLevelsMenu()
    {
        // sceneNext = sceneLevels;
        // scenePrevious = sceneCurrent;
        // sceneCurrent = sceneNext;
        // GetTree().QueueDelete(scenePrevious);
        // instance.AddChild(sceneCurrent);
        // GetNode<Node>("UI").AddChild(sceneLevels);
        // sceneNext = null;


        // Make "Levels" scene visible
        GetNode<CanvasLayer>("UI/LevelMenu").Visible = true;
        GetNode<CanvasLayer>("UI/Home").Visible = false;
    }

    public void LoadLevel(int levelNumber)
    {
        if (levelNumber == 1)
        {
            GetNode<Node>("Level").AddChild(ResourceLoader.Load<PackedScene>("res://Level1.tscn").Instance());
        }
        GetNode<CanvasLayer>("UI/LevelMenu").Visible = false;
        GetNode<CanvasLayer>("UI/Home").Visible = false;
    }
}