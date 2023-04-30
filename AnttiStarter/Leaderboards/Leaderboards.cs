using System.Collections.Generic;
using Godot;

namespace AnttiStarter.Leaderboards;

public partial class Leaderboards : Node
{
    [Export] private PackedScene rowPrefab;
    [Export] private ScoreManager scoreManager;

    private int page;
    private List<Node> rows = new();

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
            rows.Add(row);
            (row as ScoreRow)?.Init(entry); 
        });
    }

    private void Clear()
    {
        rows.ForEach(RemoveChild);
        rows.Clear();
    }

    public void ChangePage(int dir)
    {
        Clear();
        page = Mathf.Max(0, page + dir);
        scoreManager.Load(page);
    }
}