

using Godot;

public static class EventsHolder
{
    public static readonly Events events = new Events();
}

public class Events : Node
{
    [Signal] public delegate void DestroyEnemy(int score);

}