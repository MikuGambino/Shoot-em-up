using Godot;
using System;

public class Player : Area2D
{
    private int Speed = 100;
    public bool CanPlay;
    private int _lives = 3;
    private bool _upgrade;
    private int _playerEffect;
    private Vector2 _screenSize;

    public override void _Ready()
    {
        // Hide(); //todo откоментрировать
        CanPlay = true; //todo убрать
        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        animatedSprite.Animation = "default";
        animatedSprite.Play();
        _screenSize = GetViewportRect().Size;
    }
    
    public override void _Process(float delta)
    {
        if(!CanPlay) return;
        var velocity = Vector2.Zero;
        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        
        OnAttack(); 
        
        var flag = false;
        
        if (Input.IsActionPressed("ui_right"))
        {
            velocity.x += 1;
        }
        if (Input.IsActionPressed("ui_left"))
        {
            velocity.x -= 1;
        }
        if (Input.IsActionPressed("ui_down"))
        {
            velocity.y += 1;
            flag = true;
            if (animatedSprite.Animation != "down") 
                animatedSprite.Animation = "down";
        }
        if (Input.IsActionPressed("ui_up"))
        {
            velocity.y -= 1;
            flag = true;
            if (animatedSprite.Animation != "up") 
                animatedSprite.Animation = "up";
        }
        if(!flag) animatedSprite.Animation = "default";
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
        }
        Position += velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 9, _screenSize.x - 11),
            y: Mathf.Clamp(Position.y, 3, _screenSize.y - 3)
        );
    }
    
    private void OnAttack()
    {
        if (!Input.IsActionJustPressed("fire") || !CanPlay) return;
        var bulPos = GetNode<Position2D>("BulletPosition");
        var playerBullet = GD.Load<PackedScene>("res://Bullet/scene/PlayerBullet.tscn");
        var bullet1 = (PlayerBullet) playerBullet.Instance();
        bullet1.Position = bulPos.GlobalPosition;
        GetParent().AddChild(bullet1);
        
        if (_upgrade)
        {
            var bullet2 = (PlayerBullet) playerBullet.Instance();
            bullet2.Init(Vector2.Right * 4 + Vector2.Up, -8);
            bullet2.Position = bulPos.GlobalPosition;
            GetParent().AddChild(bullet2);
            
            var bullet3 = (PlayerBullet) playerBullet.Instance();
            bullet3.Init(Vector2.Right * 4 + Vector2.Down, 8);
            bullet3.Position = bulPos.GlobalPosition;
            GetParent().AddChild(bullet3);
        }
    }

    private void Hit()
    {
        _lives--;
        // todo уменьшить количество жизней в UI via signal        
        if (_lives < 0)
        {
            Console.WriteLine(_lives);
            var boom = (Explosion) GD.Load<PackedScene>("res://Explosion/scene/Explosion.tscn").Instance();
            boom.Position = Position;
            GetParent().AddChild(boom);
            // todo сигнал GameOver
        }
    }
    private void OnPlayerAreaEntered(Area2D area)
    {
        // if (area.GetType().Name.Contains("Booster"))
        // {
        //     var booster = (Booster)area;
        //     
        //     if (booster.Type == "hp")
        //     {
        //         EmitSignal(nameof(HP));
        //     }
        //     else if (booster.Type == "gun")
        //     {
        //         Upgrade = true;
        //     }
        //     area.QueueFree();
        //     return;
        // }
        if (area.GetType().Name.Contains("MobBullet"))
        {
            area.QueueFree();
            // todo отнять жизнь
            _upgrade = false;
            if (CanPlay)
            {
                GetNode<Timer>("PlayerEffect").Start();
            }
        }
        if (area.GetType().Name.Contains("Boss"))
        {
            _lives = 0;
            Hit();
        }
    }
    
    private void OnPlayerBodyEntered(RigidBody2D body)
    {
        var boom = (Explosion) GD.Load<PackedScene>("res://Explosion/scene/Explosion.tscn").Instance();
        boom.Position = Position;
        GetParent().AddChild(boom);
        
        body.QueueFree();
        Hit();
        
        if (CanPlay)
        {
            GetNode<Timer>("PlayerEffect").Start();
        }
    }
    
    private void OnPlayerEffectTimeout()
    {
        if(_playerEffect == 0) GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
        _playerEffect++;
        Visible = !Visible;
        if (_playerEffect == 20)
        {
            _playerEffect = 0;
            Visible = true;
            GetNode<Timer>("PlayerEffect").Stop();
            GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
        }
    }
    
    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }
}
