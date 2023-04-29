using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Utils;
using Godot;

namespace AnttiStarter.Grid;

public class Grid<T> where T : Node, IGridTile
{
    private readonly Dictionary<string, Cell> items = new();

    public Cell Get(Vector2I p)
    {
        return Get(p.X, p.Y);
    }

    public Cell Get(int x, int y)
    {
        var key = GetKey(x, y);
        return new Cell(x, y, items.ContainsKey(key) ? items[key].Value : default);
    }

    public void Set(int x, int y, T value)
    {
        var prev = items.Values.FirstOrDefault(i => i.Value == value);
        if (prev != default)
        {
            prev.Value = default;
        }
        var key = GetKey(x, y);
        if (items.ContainsKey(key)) items.Remove(key);
        items.Add(GetKey(x, y), new Cell(x, y, value));
    }

    private static string GetKey(int x, int y)
    {
        return $"{x},{y}";
    }

    public bool IsTile(int x, int y)
    {
        return items.ContainsKey(GetKey(x, y));
    }

    public Vector2 GetCenter()
    {
        var positions = items.Values.Select(a => a.Position).ToList();
        var x = (positions.Max(p => p.X) + positions.Min(p => p.X)) * 0.5f;
        var y = (positions.Max(p => p.Y) + positions.Min(p => p.Y)) * 0.5f;
        return new Vector2(x, y);
    }

    public Vector2I GetSize()
    {
        var positions = items.Values.Select(a => a.Position).ToList();
        var x = Mathf.Abs(positions.Max(p => p.X) + 0.5f) + Mathf.Abs(positions.Min(p => p.X) - 0.5f);
        var y = Mathf.Abs(positions.Max(p => p.Y) + 0.5f) + Mathf.Abs(positions.Min(p => p.Y) - 0.5f);
        return new Vector2I(Mathf.CeilToInt(x), Mathf.CeilToInt(y));
    }

    public Cell RandomFree()
    {
        return items.Values.Where(a => a.IsEmpty).MinBy(_ => Rng.Value);
    }

    public Cell GetRandom()
    {
        return items.Values.Where(a => !a.IsUndefined).MinBy(_ => Rng.Value);
    }

    public Cell GetClosest(Vector2 pos)
    {
        return items.Values
            .Where(v => v.IsEmpty && IsOnEdge(v.Position.X, v.Position.Y))
            .MinBy(v => pos.DirectionTo(v.AsVector2));
    }
    
    public Cell GetClosestEdge(Vector2 pos)
    {
        return GetEdges()
            .OrderBy(v => v.AsVector2.DistanceTo(pos))
            .First();
    }

    public IEnumerable<Cell> GetNeighbours(int x, int y)
    {
        return new []
        {
            Get(x + 1, y),
            Get(x - 1, y),
            Get(x, y + 1),
            Get(x, y - 1)
        };
    }
    
    public IEnumerable<Cell> GetNeighboursWithDiagonals(int x, int y, int reach = 1)
    {
        var spots = new List<Cell>();
        
        for (var i = -reach; i <= reach; i++)
        {
            for (var j = -reach; j <= reach; j++)
            {
                spots.Add(Get(x + i, y + j));
            }   
        }

        return spots;
    }

    public IEnumerable<Cell> GetEdgeNeighbours(int x, int y)
    {
        return GetNeighbours(x, y).Where(v => v.IsUndefined);
    }
    
    public IEnumerable<Cell> GetEdgeNeighboursWithDiagonals(int x, int y)
    {
        return GetNeighboursWithDiagonals(x, y).Where(v => v.IsUndefined);
    }

    public IEnumerable<Cell> GetEdges()
    {
        var all = items.SelectMany(v =>
        {
            var p = GetPosition(v.Key);
            return GetEdgeNeighboursWithDiagonals(p.X, p.Y);
        }).GroupBy(a => GetKey(a.Position.X, a.Position.Y)).Select(g => g.First());

        return all;
    }

    public bool IsOnEdge(int x, int y)
    {
        return GetNeighbours(x, y).Any(v => v.IsUndefined);
    }

    public Vector2I GetPosition(string key)
    {
        var parts = key.Split(",");
        return new Vector2I(int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public List<Cell> GetAll()
    {
        return items.Values.ToList();
    }
    
    public List<Cell> GetAllOccupied()
    {
        return items.Values.Where(i => i.IsOccupied).ToList();
    }

    public int GetEmptyCount()
    {
        return items.Values.Count(a => a.IsEmpty);
    }
    
    public class Cell
    {
        public Vector2I Position;
        public T Value;

        public bool IsUndefined => Value == default;
        public bool IsEmpty => Value == default || Value.IsEmpty();
        public bool IsOccupied => Value != default && !Value.IsEmpty();

        public Vector2 AsVector2 => new(Position.X, Position.Y);

        public Cell(int x, int y, T val)
        {
            Position = new Vector2I(x, y);
            Value = val;
        }
        
        public Cell(Vector2I pos)
        {
            Position = pos;
            Value = default;
        }
    }

    public IEnumerable<Cell> GetColumn(int column)
    {
        return items.Values.Where(v => v.IsOccupied && v.Position.X == column);
    }
    
    public IEnumerable<Cell> GetRow(int row)
    {
        return items.Values.Where(v => v.IsOccupied && v.Position.Y == row);
    }
}