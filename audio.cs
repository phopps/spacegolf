using Godot;

public class audio : Node
{
    public AudioStreamPlayer bgm;
    public AudioStreamPlayer select_sfx;
    public AudioStreamPlayer quit_sfx;

    public override void _Ready()
    {
        bgm = GetNode<AudioStreamPlayer>("bgm");
        select_sfx = GetNode<AudioStreamPlayer>("select_sfx");
        quit_sfx = GetNode<AudioStreamPlayer>("quit_sfx");
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
}
