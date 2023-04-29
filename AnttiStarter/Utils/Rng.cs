using Godot;

namespace AnttiStarter.Utils;

public static class Rng
{
    public static float Value => new RandomNumberGenerator().Randf();
    public static bool Half => Value < 0.5f;
    public static float PlusMinusOne => Half ? 1f : -1f;

    public static float Range(float limit)
    {
        return Range(-limit, limit);
    }
    
    public static float Range(float min, float max)
    {
        return new RandomNumberGenerator().RandfRange(min, max);
    }
    
    public static int Range(int limit)
    {
        return Range(-limit, limit);
    }
    
    public static int Range(int min, int max)
    {
        return new RandomNumberGenerator().RandiRange(min, max);
    }
}