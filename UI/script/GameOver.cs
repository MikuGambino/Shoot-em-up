using Godot;
using System;

public class GameOver : Node2D
{
    
    [Signal] public delegate void YesPressed();
    public void _on_ButtonYes_mouse_entered()
    {
        GetNode<AnimatedSprite>("TryAgain").Animation = "yes";
    }
    
    public void _on_ButtonNo_mouse_entered()
    {
        GetNode<AnimatedSprite>("TryAgain").Animation = "no";
    }

    public void _on_ButtonYes_pressed()
    {
        EmitSignal(nameof(YesPressed));
    }

    public void _on_ButtonNo_pressed()
    {
        GetTree().Quit();
    }
}
