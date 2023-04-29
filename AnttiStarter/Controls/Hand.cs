using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Extensions;
using Godot;

namespace AnttiStarter.Controls;

public partial class Hand : Control
{
    [Export] private float cardSize = 100f;
    [Export] private float gap = 10f;

    private readonly List<Draggable> cards = new();
    
    private int lastIndex = -1;

    public void Add(Draggable card)
    {
        cards.Add(card);
        PositionCards();
    }

    public void Remove(Draggable card)
    {
        cards.Remove(card);
        PositionCards();
    }

    public void PositionCards(Draggable preview = null)
    {
        var ordered = cards.OrderBy(c => c.GlobalPosition.X).ToList();

        if (preview != default && ordered.IndexOf(preview) == lastIndex) return;
        
        lastIndex = ordered.IndexOf(preview);
        
        var start = this.GetGlobalCenter() + Vector2.Left * cardSize * 0.5f * cards.Count + Vector2.Up * cardSize * 0.5f;
        var i = 0;

        ordered.ForEach(c =>
        {
            if (c != preview)
            {
                c.MoveToGlobal(start + Vector2.Right * (cardSize + gap) * i, 0.2f, Tween.TransitionType.Bounce);
            }
            i++;
        });
    }
}