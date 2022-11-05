using Godot;
using System;

public abstract class Bullet : Area2D
{
    protected Vector2 Speed;
    protected void OnTimerTimeout()
    {
        QueueFree();
    }
    
    protected void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
    
}
