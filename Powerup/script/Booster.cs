using Godot;
using System;

public class Booster : Area2D
{
    public string Type;
    public override void _Process(float delta)
    {
        Position += Vector2.Down * 0.4f;
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }

    public void SetType(string type)
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Animation = type;
        Type = type;
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
