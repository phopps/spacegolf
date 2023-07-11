using Godot;

public class Main : Node
{
    private Audio audio;
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
        }
        else
        {
            GD.Print("Error: Duplicate 'Main' instance.");
            QueueFree();
        }

        audio = GetNode<Audio>("Audio");
        audio.PlayBGM();
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
            audio.PlaySelect();
        }
    }

    public async void QuitGame()
    {
        audio.PlayQuit();
        await ToSignal(audio.GetNode<AudioStreamPlayer>("Quit"), "finished");
        GetTree().Quit();
    }
}
