using Godot;

public class audio : Node
{
    [Export] public AudioStreamPlayer bgm;
    [Export] public AudioStreamPlayer select_sfx;
    [Export] public AudioStreamPlayer quit_sfx;
    [Export] public bool isMuted;

    public override void _Ready()
    {
        bgm = GetNode<AudioStreamPlayer>("bgm");
        select_sfx = GetNode<AudioStreamPlayer>("select_sfx");
        quit_sfx = GetNode<AudioStreamPlayer>("quit_sfx");
        isMuted = false;
        GD.Print("(audio.cs is ready)");
    }

    public void PlayBGM()
    {
        GD.Print("Playing background music.");
        bgm.Play();
    }

    public void PlaySelectSFX()
    {
        GD.Print("Playing select sound effect.");
        select_sfx.Play();
    }

    public void PlayQuitSFX()
    {
        GD.Print("Playing quit sound effect.");
        quit_sfx.Play();
    }

    public void MuteAudio()
    {
        GD.Print("Muting all audio.");
        if (isMuted)
        {
            bgm.StreamPaused = false;
            select_sfx.StreamPaused = false;
            quit_sfx.StreamPaused = false;
        }
        else
        {
            bgm.StreamPaused = true;
            select_sfx.StreamPaused = true;
            quit_sfx.StreamPaused = true;
        }
        isMuted = !isMuted;
    }
}
