using Godot;
using System;

public class Mob : RigidBody2D
{
    private int _mobLives;
    private int _mobEffect = 0;

    public void Hit()
    {
        GetNode<Timer>("MobEffect").Start();
        _mobLives--;
    }

    public bool IsDead()
    {
        return _mobLives == 0;
    }
    
    public override void _Ready()
    {
        GetNode<Timer>("Reload").Start();
    }
    
    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
    
    public void OnTimerTimeout()
    {
        QueueFree();
    }
    
    public void OnMobEffectTimeout()
    {
        _mobEffect++;
        GetNode<AnimatedSprite>("AnimatedSprite").Modulate = _mobEffect % 2 == 1 ? new Color(10, 10 ,10, 10) : new Color(1, 1 ,1);

        if (_mobEffect != 8) return;
        _mobEffect = 0;
        GetNode<Timer>("MobEffect").Stop();
    }
    
    public void OnReloadTimeout()
    {
        Attack();
        var timer = GetNode<Timer>("Reload");
        timer.WaitTime = new Random().Next(2, 4);
        timer.Start();
    }

    public void Attack()
    {
    }
}
