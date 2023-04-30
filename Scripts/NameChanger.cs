using Godot;

namespace Scripts;

public partial class NameChanger : Label
{
    [Export] private WordDictionary adjectives;

    public override void _Ready()
    {
        Text = adjectives.GetRandomWord();
    }
}