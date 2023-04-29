using Godot;

namespace Scripts;

public partial class GameState : Node2D
{
    public int Level { get; set; } = 1;
    public string Word { get; set; }
    public int Score { get; set; }
    public int Lives { get; set; } = 5;
}