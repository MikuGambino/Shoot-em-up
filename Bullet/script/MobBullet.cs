using Godot;
using System;

public class MobBullet : Bullet
{
    
    private Vector2 _speed = Vector2.Left * 3;

    public void Init(string sprite)
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Animation = sprite;
        GetNode<AnimatedSprite>("AnimatedSprite").FlipH = true;
    }
    public void Init(string sprite, Vector2 speed, int deegres)
    {
        Init(sprite);
        _speed = speed;
        GetNode<AnimatedSprite>("AnimatedSprite").RotationDegrees = deegres;
    }
    
    public override void _Process(float delta)
    {
        Position += _speed;
    }
}
