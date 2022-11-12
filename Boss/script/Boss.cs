using Godot;
using System;

public class Boss : Area2D
{
    private enum Direction
    {
        Left,
        Up,
        Down,
        Stop
    }
    
    private int _lives = 55;
    private int _bossEffect;

    private Direction _dir = Direction.Stop;
    
    public override void _Ready()
    {
        Hide();
        GetNode<AnimatedSprite>("AnimatedSprite").Play();
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }

    public void StartBoss(Vector2 pos)
    {
        _dir = Direction.Left;
        Position = pos;
        Show();
    }
    private void GoUp()
    {
        _dir = Direction.Up;
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    private void OnReloadTimeout()
    {
        Attack();
    }

    private void Attack()
    {
        var bulPos = GetNode<Position2D>("BulletPosition");
        var mobBullet = GD.Load<PackedScene>("res://Bullet/scene/MobBullet.tscn");
        var bullet1 = (MobBullet) mobBullet.Instance();
        bullet1.Position = bulPos.GlobalPosition;
        bullet1.Init("boss");
        GetParent().AddChild(bullet1);
        
        var bullet2 = (MobBullet) mobBullet.Instance();
        bullet2.Position = bulPos.GlobalPosition;
        bullet2.Init("boss", Vector2.Left * 2 + Vector2.Up * 0.5f, 8);
        GetParent().AddChild(bullet2);
        
        var bullet3 = (MobBullet) mobBullet.Instance();
        bullet3.Position = bulPos.GlobalPosition;
        bullet3.Init("boss", Vector2.Left * 2 + Vector2.Down * 0.5f, -8);
        GetParent().AddChild(bullet3);
    }
    
    public override void _Process(float delta)
    {
        if (_dir == Direction.Left)
        {
            Position += Vector2.Left * 0.5f;
            if (Position.x < 210)
            {
                _dir = Direction.Stop;
                var timer = new Timer();
                timer.Connect("timeout", this, nameof(GoUp));
                timer.WaitTime = 1.5f;
                timer.OneShot = true;
                AddChild(timer);
                timer.Start();
                GetNode<Timer>("Reload").Start();
            }
        }

        if (_dir == Direction.Up)
        {
            Position += Vector2.Up * 0.5f;
            if (Position.y < 25) _dir = Direction.Down;
        }

        if (_dir == Direction.Down)
        {
            Position += Vector2.Down * 0.5f;
            if (Position.y > 120) _dir = Direction.Up;
        }
    }
    
    public void OnBossEffectTimeout()
    {
        _bossEffect++;
        if (_bossEffect % 2 == 1)
        {
            GetNode<AnimatedSprite>("AnimatedSprite").Modulate = new Color(10, 10 ,10, 10);
        }
        else GetNode<AnimatedSprite>("AnimatedSprite").Modulate = new Color(1, 1 ,1);

        if (_bossEffect == 8)
        {
            _bossEffect = 0;
            GetNode<Timer>("BossEffect").Stop();
        }
    }

    public void OnBossAreaEntered(PlayerBullet area)
    {
        if (area.Name.Contains("Mob")) return;
        area.QueueFree();
        _lives -= 1;
        if (_lives < 0)
        {
            var boom = (Explosion) GD.Load<PackedScene>("res://Explosion/scene/Explosion.tscn").Instance();
            boom.Scale = new Vector2(3, 3);
            boom.Position = Position;
            GetParent().AddChild(boom);
            
            GetNode<Timer>("Reload").Stop();
            EventsHolder.events.EmitSignal("Win");
            _dir = Direction.Stop;
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
            Hide();
        }
        else GetNode<Timer>("BossEffect").Start();
    }
}
