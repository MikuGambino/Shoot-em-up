using Godot;
using System;

public class MobBullet : Bullet
{
    public MobBullet()
    {
        Speed = Vector2.Left * 3;
        GetNode<AnimatedSprite>("AnimatedSprite").FlipH = true;
    }
    
    public void Init(string sprite)
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Animation = sprite;
    }
    public void Init(string sprite, Vector2 speed, int deegres)
    {
        Init(sprite);
        Speed = speed;
        GetNode<AnimatedSprite>("AnimatedSprite").RotationDegrees = deegres;
    }
    
    public override void _Process(float delta)
    {
        Position += Speed;
    }
}
