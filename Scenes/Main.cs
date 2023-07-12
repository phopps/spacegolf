using Godot;

public class Main : Node
{
    private Audio audio;
    // private CanvasLayer sceneNext;
    private CanvasLayer sceneCurrent;
    private CanvasLayer scenePrevious;
    private static Main instance; // Singleton pattern

    public static Main GetInstance()
    {
        return instance;
    }

    public override void _Ready()
    {
        // Check for instance of Main singleton
        if (instance == null)
        {
            instance = this;
            sceneCurrent = GetNode<CanvasLayer>("Home");
        }
        else
        {
            GD.Print("Error: Duplicate 'Main' instance.");
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
        scenePrevious = sceneCurrent;
        sceneCurrent = GetNode<CanvasLayer>("Levels");
        scenePrevious.QueueFree();
        GetInstance().AddChild(sceneCurrent);
    }
}

// CHANGING SCENES
// public PackedScene simultaneousScene;

// public MyClass()
// {
//     simultaneousScene = (PackedScene)ResourceLoader.Load("res://levels/level2.tscn").instance();
// }

// public void _AddASceneManually()
// {
//     // This is like autoloading the scene, only
//     // it happens after already loading the main scene.
//     GetTree().GetRoot().AddChild(simultaneousScene);
// }