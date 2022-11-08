using Godot;
using System;

public class Explosion : Node2D
{
    public override void _Ready()
    {
        GetNode<AnimationPlayer>("Animation").Play("Boom");
    }

    public void OnAnimationFinished(string animName)
    {
        QueueFree();
    }
}
