using Godot;
using System;

public class RedMob : Mob
{
    public RedMob()
    {
        MobLives = 3;
        var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        animSprite.Animation = "red";
        animSprite.Play();
    }

    protected override void Attack()
    {
        var bulPos = GetNode<Position2D>("BulletPosition");
        var playerBullet = GD.Load<PackedScene>("res://Bullet/scene/MobBullet.tscn");
        var bullet1 = (MobBullet) playerBullet.Instance();
        bullet1.Position = bulPos.GlobalPosition;
        bullet1.Init("red");
        GetParent().AddChild(bullet1);
        
        var bullet2 = (MobBullet) playerBullet.Instance();
        bullet2.Position = bulPos.GlobalPosition;
        bullet2.Init("red", Vector2.Left * 2 + Vector2.Up * 0.5f, 8);
        GetParent().AddChild(bullet2);
        
        var bullet3 = (MobBullet) playerBullet.Instance();
        bullet3.Position = bulPos.GlobalPosition;
        bullet3.Init("red", Vector2.Left * 2 + Vector2.Down * 0.5f, -8);
        GetParent().AddChild(bullet3);
    }
}
