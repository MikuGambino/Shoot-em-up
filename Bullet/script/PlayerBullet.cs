using Godot;
using System;

public class PlayerBullet : Bullet
{
    private Vector2 _speed = Vector2.Right * 6;
    public void Init()
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Animation = "default";
        GetNode<AnimatedSprite>("AnimatedSprite").FlipH = false;
    }
    public void Init(Vector2 speed, int deegres)
    {
        Init();
        _speed = speed;
        GetNode<AnimatedSprite>("AnimatedSprite").RotationDegrees = deegres;
    }
    
    public override void _Process(float delta)
    {
        Position += _speed;
    }

    // todo OnBulletBodyEntered
}
