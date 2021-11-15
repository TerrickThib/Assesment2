using System;
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
        public Scene _scene;

        
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

        public Enemy(float x, float y, float speed, float health, float maxSightDistance, float maxViewingAngle, Player player,Scene scene, string name = "Actor", string path = "")
            : base(x, y,name, path)
        {
            _speed = speed;
            _player = player;
            _maxViewingAngle = maxViewingAngle;
            _maxSightDistance = maxSightDistance;
            _health = health;
            _scene = scene;
        }
        public override void Update(float deltaTime)
        {
            HealthNoneDead();
            IfHealthLowUpgrade();
            //Inishalizes distance
            Vector2 distance = new Vector2();
            //Takes players position and eneme position to get differance
            distance = _player.LocalPosition - LocalPosition;
            distance.Normalize();
            Velocity = distance.Normalized * Speed * deltaTime;

            if(GetTargetInSight()&& GetTargetIndistance())
                LocalPosition += Velocity;

            base.Update(deltaTime);
            LookAt(_player.WorldPosition);
        }

        /// <summary>
        /// Gets players position relative to youres and normlizes it
        /// </summary>
        /// <returns>Moves Enemy if players positiob is less than the max viewing angle
        public bool GetTargetInSight()
        {            
            Vector2 directionOfTarget = (_player.LocalPosition - LocalPosition).Normalized;                      
            
            return Math.Acos(Vector2.DotProduct(directionOfTarget, Forward)) <= 360;                                            
        }

        /// <summary>
        /// Gets the distance from player and sees if its with in sight distance
        /// </summary>
        /// <returns>Enemys movement if the difrence is less than max sight distance</returns>
        public bool GetTargetIndistance()
        { 
            return Vector2.Distance(_player.LocalPosition, LocalPosition) <= _maxSightDistance;
        }

        public override void OnCollision(Actor actor)
        {
          
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        /// <summary>
        /// Removes actor if there health is 1 scale it up and replenish some health
        /// </summary>
        public void IfHealthLowUpgrade()
        {
            if (Health == 5)
            {
                SetScale(300, 300);
            }
        }

        /// <summary>
        /// If enemys Health reaches 0 Remove the actor
        /// </summary>
        public void HealthNoneDead()
        {
            if(Health <= 0)
            {
                _scene.RemoveActor(this);                
                Engine.CloseApplication();
            }
        }
    }
}
