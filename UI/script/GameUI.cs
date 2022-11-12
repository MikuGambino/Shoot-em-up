using Godot;
using System;

public class GameUI : Node2D
{
    public int Score;
    
    public override void _Ready()
    {
        GetNode<AnimatedSprite>("Win/Pepe").Play();
        EventsHolder.events.Connect("SetLivesUI", this, nameof(SetLifeUI));
        EventsHolder.events.Connect("AddScore", this, nameof(AddScore));
        EventsHolder.events.Connect("ResetUI", this, nameof(Reset));
    }
    public void SetLifeUI(int lives)
    {
        if (lives < 0) return;
        GetNode<AnimatedSprite>("HP").Animation = lives.ToString();
    }

    public void AddScore(int score)
    {
        Score += score;
        for (int i = 1, j = 10000; i <= 5; i++, j /= 10)
        {
            GetNode<AnimatedSprite>("Score/num" + i).Animation = (Score / j % 10).ToString();
        }
    }

    public void Reset()
    {
        SetLifeUI(3);
        Score = 0;
        AddScore(0);
    }
}
