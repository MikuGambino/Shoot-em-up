using Godot;
using System;

public class MainMenu : Control
{
    public void OnStartPressed()
    {
        EventsHolder.events.EmitSignal("Start");
        Hide();
    }

    public void OnExitPressed()
    {
        GetTree().Quit();
    }
}
