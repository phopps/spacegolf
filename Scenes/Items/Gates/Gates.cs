using Godot;

public class Gates : Items
{
    [Export] public int gateID;

    public override void _Ready()
    {
        GD.Print("Gates are ready.");
    }
}
