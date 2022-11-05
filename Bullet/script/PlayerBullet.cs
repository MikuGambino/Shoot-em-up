using Godot;
using System;

public class PlayerBullet : Bullet
{
    public PlayerBullet()
    {
        Speed = Vector2.Right * 6;
        GetNode<AnimatedSprite>("AnimatedSprite").Animation = "default";
        GetNode<AnimatedSprite>("AnimatedSprite").FlipH = false;
    }
    public void Init(Vector2 speed, int deegres)
    {
        Speed = speed;
        GetNode<AnimatedSprite>("AnimatedSprite").RotationDegrees = deegres;
    }
    
    public override void _Process(float delta)
    {
        Position += Speed;
    }

    // todo OnBulletBodyEntered
}
