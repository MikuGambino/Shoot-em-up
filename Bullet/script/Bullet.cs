using Godot;
using System;

public class Bullet : Area2D
{
    public void OnTimerTimeout()
    {
        QueueFree();
    }
    
    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
    
}
