using Godot;

namespace Scripts;

public partial class Music : Node2D
{
    [Export] private AudioStreamPlayer2D music;

    public void Lowpass(bool state)
    {
        music.Bus = state ? "Lowpassed" : "Master";
    }
}