using Godot;
using System;

public class PurpleMob : Mob
{
    public PurpleMob()
    {
        MobLives = 2;
        var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        animSprite.Animation = "purple";
        animSprite.Play();
    }

    protected override void Attack()
    {
        var bulPos = GetNode<Position2D>("BulletPosition");
        var playerBullet = GD.Load<PackedScene>("res://Bullet/scene/MobBullet.tscn");
        var bullet1 = (MobBullet) playerBullet.Instance();
        bullet1.Position = bulPos.GlobalPosition;
        bullet1.Init("purple");
        GetParent().AddChild(bullet1);
    }
}
