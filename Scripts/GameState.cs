using Godot;

namespace Scripts;

public partial class GameState : Node2D
{
    public int Level { get; set; } = 1;
    public string Word { get; set; }
    public int Score { get; set; }
    public int Lives { get; set; } = 5;
    public string PlayerName { get; set; } = "Anonymous";
    public string PlayerId { get; set; } = "123123123";
    public bool NameSaved { get; set; }

    public void Reset()
    {
        Level = 1;
        Word = null;
        Score = 0;
        Lives = 5;
    }
}