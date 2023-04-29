using AnttiStarter.Animations;
using Godot;

namespace AnttiStarter.SceneChanger;

public partial class SceneChanger : Node
{
    [Export] private NodePath blinderLeft, blinderRight;

    private Appearer left, right;
    private Timer timer = new();
    
    public override void _Ready()
    {
        left = GetNode<Appearer>(blinderLeft);
        right = GetNode<Appearer>(blinderRight);
        
        AddChild(timer);
        timer.OneShot = true;
        
        OpenAfter(0.25f);
    }

    public override void _Input(InputEvent @event)
    {
        if (!OS.IsDebugBuild()) return;
        if (@event is InputEventKey { Pressed: false, Keycode: Key.R })
        {
            GetTree().ReloadCurrentScene();
        }
    }

    private void OpenAfter(float delay)
    {
        timer.Timeout += OpenBlinders;
        timer.Stop();
        timer.Start(delay);
    }

    private void OpenBlinders()
    {
        timer.Timeout -= OpenBlinders;
        left.Toggle(false);
        right.Toggle(false);
    }
    
    private void CloseBlinders()
    {
        left.Toggle(true);
        right.Toggle(true);
    }

    public void ChangeScene(string scene)
    {
        CloseBlinders();
        timer.Timeout += () => GetTree().ChangeSceneToFile(scene);
        timer.Stop();
        timer.Start(1f);
    }
}