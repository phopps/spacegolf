using Godot;

public class Audio : Node
{
    public AudioStreamPlayer AudioBGM;
    public AudioStreamPlayer AudioSelect;
    public AudioStreamPlayer AudioQuit;

    public override void _Ready()
    {
        AudioBGM = GetNode<AudioStreamPlayer>("AudioBGM");
        AudioSelect = GetNode<AudioStreamPlayer>("AudioSelect");
        AudioQuit = GetNode<AudioStreamPlayer>("AudioQuit");
        GD.Print("Audio.cs is ready.");
    }

    public void PlayAudioBGM()
    {
        GD.Print("Playing background music.");
        AudioBGM.Play();
    }

    public void PlayAudioSelect()
    {
        GD.Print("Playing select sound effect.");
        AudioSelect.Play();
    }

    public void PlayAudioQuit()
    {
        GD.Print("Playing quit sound effect.");
        AudioQuit.Play();
    }
}
