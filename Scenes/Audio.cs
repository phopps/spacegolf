using Godot;

public class Audio : Node
{
    public AudioStreamPlayer BGM;
    public AudioStreamPlayer Select;
    public AudioStreamPlayer Quit;

    public override void _Ready()
    {
        BGM = GetNode<AudioStreamPlayer>("BGM");
        Select = GetNode<AudioStreamPlayer>("Select");
        Quit = GetNode<AudioStreamPlayer>("Quit");
        GD.Print("Audio.cs is ready.");
    }

    public void PlayBGM()
    {
        GD.Print("Playing background music.");
        BGM.Play();
    }

    public void PlaySelect()
    {
        GD.Print("Playing select sound effect.");
        Select.Play();
    }

    public void PlayQuit()
    {
        GD.Print("Playing quit sound effect.");
        Quit.Play();
    }
}
