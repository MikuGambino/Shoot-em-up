

using Godot;

public static class EventsHolder
{
    public static readonly Events events = new Events();
}

public class Events : Node
{
    [Signal] public delegate void AddScore(int score);
    [Signal] public delegate void SetLivesUI(int lives);
    [Signal] public delegate void ResetUI();
    [Signal] public delegate void Start();
    [Signal] public delegate void GameOver();
    [Signal] public delegate void Win();

    

}