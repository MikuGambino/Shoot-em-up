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

    public void OnBulletBodyEntered(Mob body)
    {
        body.Hit();
        if (body.IsDead())
        {
            if(body.GetType().Name.Contains("red")) EmitSignal("DestroyEnemy", 166);
            if (body.GetType().Name.Contains("purple")) EmitSignal("DestroyEnemy", 166);

            var boom = (Explosion) GD.Load<PackedScene>("res://Explosion/scene/Explosion.tscn").Instance();
            boom.Position = Position;
            GetParent().GetParent().AddChild(boom);
            
            body.QueueFree();
        }
        QueueFree();
    }
    
}
