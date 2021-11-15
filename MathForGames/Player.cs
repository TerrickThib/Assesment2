﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        public Scene _scene;
        private float _cooldowntimer = 0.5f;
        private float _timesincelastshot = 0;
        private float _health = 0;        

        //Allows us to give _ speed a value
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        //Allows us to give Velocity a value
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }
          
        public Player( float x, float y, float health, float speed,Scene scene, string name = "Actor", string path = "") 
            : base(x, y, name, path)
        {
            _speed = speed;
            _scene = scene;
            _health = health;
        }

        public override void Update(float deltaTime)
        {
            //Checks if Health is 0 every update
            DeadEndGame();            

            //Sets time for cooldown timer
            _timesincelastshot += deltaTime;
            
            //GEts the player input direction
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));            
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //To shot bullet
            int xDirectionofBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int yDirectionofBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            if (_timesincelastshot > _cooldowntimer)
            {
                if (xDirectionofBullet != 0 || yDirectionofBullet != 0)
                {
                    //Sets what a bullet is, sets scale sets bullet size, sets bullet hit box them adds bullet to scene, and resets time
                    Projectiles bullet = new Projectiles(LocalPosition.X, LocalPosition.Y, 200, xDirectionofBullet, yDirectionofBullet, _scene, "Bullet", "Images/bullet.png");
                    bullet.SetScale(25, 25);
                    CircleCollider bulletCircleCollider = new CircleCollider(10, bullet);
                    bullet.Collider = bulletCircleCollider;
                    _scene.AddActor(bullet);
                    _timesincelastshot = 0;
                }
            }

            //Creat a vector that stores the move input            
            Vector2 moveDirection = new Vector2(xDirection, yDirection);
            Vector2 bulletDirection = new Vector2(xDirectionofBullet, yDirectionofBullet);
                                             
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            //Moves players direction to direction your moving
            if (Velocity.Magnitude > 0)
            {
                Forward = Velocity.Normalized; 
            }

            //Changes players direction to where player is shooting
            if (bulletDirection.Magnitude > 0)
            {
                Forward = bulletDirection.Normalized;
            }

            //Uses velocity with current Position
            LocalPosition += Velocity;

            //Clamps your position between two points and then sets your LOcal position to be resultX and resultY
            float resultX = Math.Clamp(LocalPosition.X, 23, 780);
            float resultY = Math.Clamp(LocalPosition.Y, 23, 980);
            LocalPosition = new Vector2(resultX, resultY);

            base.Update(deltaTime);               
        }
        //If collision happens with these actors decreas health
        public override void OnCollision(Actor actor)
        {
            if (actor is EnemyProjectiles || actor is Enemy || actor is SmashBros )
            {
                Health -= 1;                
            }
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        /// <summary>
        /// If Health is 0 Close Application
        /// </summary>
        public void DeadEndGame()
        {
            if (Health <= 0)
            {                
                Engine.CloseApplication();                
            }            
        }
    }
}
