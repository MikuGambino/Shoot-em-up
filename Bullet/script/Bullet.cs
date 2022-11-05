using Godot;
using System;

public class Bullet : Area2D
{
    protected Vector2 Speed;
    public void OnTimerTimeout()
    {
        QueueFree();
    }
    
    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
    
}
