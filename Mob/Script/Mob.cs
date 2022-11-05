using Godot;
using System;

public abstract class Mob : RigidBody2D
{
    protected int MobLives;
    private int _mobEffect = 0;

    public void Hit()
    {
        GetNode<Timer>("MobEffect").Start();
        MobLives--;
    }

    public bool IsDead()
    {
        return MobLives == 0;
    }
    
    public override void _Ready()
    {
        GetNode<Timer>("Reload").Start();
    }
    
    protected void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
    
    protected void OnTimerTimeout()
    {
        QueueFree();
    }
    
    protected void OnMobEffectTimeout()
    {
        _mobEffect++;
        GetNode<AnimatedSprite>("AnimatedSprite").Modulate = _mobEffect % 2 == 1 ? new Color(10, 10 ,10, 10) : new Color(1, 1 ,1);

        if (_mobEffect != 8) return;
        _mobEffect = 0;
        GetNode<Timer>("MobEffect").Stop();
    }
    
    protected void OnReloadTimeout()
    {
        Attack();
        var timer = GetNode<Timer>("Reload");
        timer.WaitTime = new Random().Next(2, 4);
        timer.Start();
    }

    protected abstract void Attack();
}
