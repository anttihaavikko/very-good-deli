using AnttiStarter.Utils;
using Godot;

namespace AnttiStarter.Extensions;

public static class AudioExtension
{
    public static void PlayWithVariation(this AudioStreamPlayer2D audio)
    {
        audio.Stop();
        audio.PitchScale = Rng.Range(0.9f, 1.1f);
        audio.Play();
    }
}