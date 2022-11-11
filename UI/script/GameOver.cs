using Godot;
using System;

public class GameOver : Node2D
{
    
    public void OnButtonYesMouseEntered()
    {
        GetNode<AnimatedSprite>("TryAgain").Animation = "yes";
    }
    
    public void OnButtonNoMouseEntered()
    {
        GetNode<AnimatedSprite>("TryAgain").Animation = "no";
    }

    public void OnButtonYesPressed()
    {
        EventsHolder.events.EmitSignal("Start");
    }

    public void OnButtonNoPressed()
    {
        GetTree().Quit();
    }
}
