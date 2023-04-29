using System.Collections.Generic;
using Godot;

namespace AnttiStarter.Leaderboards;

public partial class Leaderboards : Node
{
    [Export] private PackedScene rowPrefab;
    [Export] private ScoreManager scoreManager;

    public override void _Ready()
    {
        if (scoreManager == default) return;
        
        scoreManager.onLoaded += UpdateBoard;
        scoreManager.Load();
    }

    private void UpdateBoard(List<LeaderBoardScore> entries)
    {
        entries.ForEach(entry =>
        {
            var row = rowPrefab.Instantiate();
            AddChild(row);
            (row as ScoreRow)?.Init(entry); 
        });
    }
}