﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{    
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        public Player _player;
        private float _maxViewingAngle;
        private float _maxSightDistance;
        private float _health;

        
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

        public float MaxViewingAngle
        {
            get { return _maxViewingAngle; }
            set { _maxViewingAngle = value; }
        }

        public float MaxSightDistance
        {
            get { return _maxSightDistance; }
            set { _maxSightDistance = value; }
        }
        
        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public Enemy(float x, float y, float speed, float health, float maxSightDistance, float maxViewingAngle, Player player, string name = "Actor", string path = "")
            : base(x, y,name, path)
        {
            _speed = speed;
            _player = player;
            _maxViewingAngle = maxViewingAngle;
            _maxSightDistance = maxSightDistance;
            _health = health;
        }
        public override void Update(float deltaTime)
        {
            //Inishalizes distance
            Vector2 distance = new Vector2();
            //Takes players position and eneme position to get differance
            distance = _player.LocalPosition - LocalPosition;
            distance.Normalize();
            Velocity = distance.Normalized * Speed * deltaTime;

            if(GetTargetInSight()&& GetTargetIndistance())
                LocalPosition += Velocity;

            base.Update(deltaTime);
        }

        public bool GetTargetInSight()
        {            
            Vector2 directionOfTarget = (_player.LocalPosition - LocalPosition).Normalized;                      
            
            return Math.Acos(Vector2.DotProduct(directionOfTarget, Forward)) < _maxViewingAngle;                                            
        }

        public bool GetTargetIndistance()
        { 
            return Vector2.Distance(_player.LocalPosition, LocalPosition) < _maxSightDistance;
        }

        public override void OnCollision(Actor actor)
        {            
            if (actor is Projectiles)
            {
                Health -= 1;                
            }
        
    }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}
