using Godot;
using System;

public class MainMenu : Control
{
    [Signal] public delegate void Start();
    
    public void _on_Start_pressed()
    {
        EmitSignal(nameof(Start));
        Hide();
    }

    public void _on_Exit_pressed()
    {
        GetTree().Quit();
    }
}
