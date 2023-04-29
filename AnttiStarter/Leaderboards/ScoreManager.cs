using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Godot;

namespace AnttiStarter.Leaderboards;

public partial class ScoreManager : Node
{
    [Export] private string game;
    [Export] private int pageSize = 10;
    
    private HttpRequest loadRequest, saveRequest;
    
    private string loadUrl = "https://games.sahaqiel.com/leaderboards/load-scores-vball.php";
    private string saveUrl = "https://games.sahaqiel.com/leaderboards/save-score.php";

    public Action<List<LeaderBoardScore>> onLoaded;

    public override void _Ready()
    {
        loadRequest = new HttpRequest();
        saveRequest = new HttpRequest();
        
        AddChild(loadRequest);
        AddChild(saveRequest);

        // Submit("DEV", "111", 123, 321);
    }

    public void Load(int page = 0)
    {
        loadRequest.RequestCompleted += Loaded;
        loadRequest.Request($"{loadUrl}?game={game}&amt={pageSize}&p={page}");
    }
    
    public void Submit(string player, string id, long scoreSub, long levelSub, string suffix = "")
    {
        if (saveRequest == default) return;
        
        var check = (int)Secrets.GetVerificationNumber(player, scoreSub, levelSub);
			
        var parameters = "?str=";
        parameters += player;
        parameters += "," + id;
        parameters += "," + levelSub;
        parameters += "," + scoreSub;
        parameters += "," + check;
        parameters += "," + game + suffix;
        
        saveRequest.Request($"{saveUrl}{parameters}");

        saveRequest.RequestCompleted += ScoreSaved;
    }

    private void ScoreSaved(long result, long code, string[] headers, byte[] body)
    {
        saveRequest.RequestCompleted -= ScoreSaved;
    }

    private void Loaded(long result, long code, string[] headers, byte[] body)
    {
        loadRequest.RequestCompleted -= Loaded;
        var data = Json.ParseString(Encoding.UTF8.GetString(body));
        data.AsGodotDictionary().TryGetValue("scores", out var scores);
        
        onLoaded?.Invoke(scores.AsGodotArray().Select(Convert).ToList());
    }

    private static LeaderBoardScore Convert(Variant variant)
    {
        var dict = variant.AsGodotDictionary();

        return new LeaderBoardScore
        {
            name = dict.GetValueOrDefault("name", "name").AsString(),
            score = dict.GetValueOrDefault("score", "0").AsString(),
            position = dict.GetValueOrDefault("position", "0").AsInt32(),
            locale = dict.GetValueOrDefault("locale", "fi").AsString(),
            pid = dict.GetValueOrDefault("pid", "000-000-000").AsString(),
            level = dict.GetValueOrDefault("level", "0").AsString()
        };
    }
}

public struct LeaderBoardScore
{
    public string name;
    public string score;
    public string level;
    public int position;
    public string pid;
    public string locale;
}