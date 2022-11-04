using Godot;
using System;

public class Player : Area2D
{
    private int Speed = 100;
    public bool CanPlay = false;
    public bool Upgrade = false;
    public int PlayerEffect = 0;
    private Vector2 ScreenSize;

    public override void _Ready()
    {
        // Hide(); //todo откоментрировать
        CanPlay = true; //todo убрать
        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        animatedSprite.Animation = "default";
        animatedSprite.Play();
        ScreenSize = GetViewportRect().Size;
    }
    
    public override void _Process(float delta)
    {
        if(!CanPlay) return;
        var velocity = Vector2.Zero;
        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        
        // OnAttack(); //TODO откоментировать
        
        var flag = false;
        
        Console.WriteLine("Dasdas");
        
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
            x: Mathf.Clamp(Position.x, 9, ScreenSize.x - 11),
            y: Mathf.Clamp(Position.y, 3, ScreenSize.y - 3)
        );
    }
    
    // TODO переписать
    // private void OnAttack()
    // {
    //     if (!Input.IsActionJustPressed("fire") || !CanPlay) return;
    //     var bInstance = (Bullet) Bullet.Instance();
    //     bInstance.Connect("DestroyEnemy", this, nameof(AddScore));
    //     var bulPos = GetNode<Position2D>("BulletPosition");
    //     bInstance.Position = bulPos.GlobalPosition;
    //     GetParent().AddChild(bInstance);
    //
    //     if (Upgrade)
    //     {
    //         for (int i = 1; i < 3; i++)
    //         {
    //             bInstance = (Bullet) Bullet.Instance();
    //             bInstance.Connect("DestroyEnemy", this, nameof(AddScore));
    //             bInstance.SetType("default", i, true);
    //             bInstance.Position = bulPos.GlobalPosition;
    //             GetParent().AddChild(bInstance);
    //         }
    //     }
    // }
    
    // public void AddScore(int score)
    // {
    //     EmitSignal("Score", score);
    // }
    
    // TODO переименовать в годоте
    // public void OnPlayerAreaEntered(Area2D area)
    // {
    //     if (area.GetType().Name.Contains("Booster"))
    //     {
    //         var booster = (Booster)area;
    //         
    //         if (booster.Type == "hp")
    //         {
    //             EmitSignal(nameof(HP));
    //         }
    //         else if (booster.Type == "gun")
    //         {
    //             Upgrade = true;
    //         }
    //         area.QueueFree();
    //         return;
    //     }
    //     if (area.Name.GetType().Name.Contains("Bullet"))
    //     {
    //         var bullet = (Bullet)area;
    //         if(bullet.Type == "default") return;
    //         area.QueueFree();
    //         EmitSignal(nameof(Hit));
    //         Upgrade = false;
    //         if (CanPlay)
    //         {
    //             GetNode<Timer>("PlayerEffect").Start();
    //             GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    //         }
    //     }
    //     if (area.GetType().Name.Contains("Boss"))
    //     {
    //         EmitSignal(nameof(CrashBoss));
    //     }
    // }
    
    //TODO переименовать в годоте
    //TODO настроить таймер
    //TODO настроить позицию пули
    // public void OnPlayerBodyEntered(RigidBody2D body)
    // {
    //     var boom = (Explosion)Explosion.Instance();
    //     boom.Position = Position;
    //     GetParent().AddChild(boom);
    //     
    //     body.QueueFree();
    //     EmitSignal("Hit");
    //     
    //     if (CanPlay)
    //     {
    //         GetNode<Timer>("PlayerEffect").Start();
    //         GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    //     }
    // }
    
    //TODO проверить
public void OnPlayerEffectTimeout()
    {
        PlayerEffect++;
        Visible = !Visible;
        if (PlayerEffect == 20)
        {
            PlayerEffect = 0;
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
