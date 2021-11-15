using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class EnemyProjectiles : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Player _player;
        public int _xdirection;
        public int _ydirection;
        public Scene _scene;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public EnemyProjectiles(float x, float y, float speed, int xdirection, int ydirection, Scene scene, string name = "Actor", string path = "")
            : base(x, y, name, path)
        {
            _speed = speed;
            _xdirection = xdirection;
            _ydirection = ydirection;
            _scene = scene;
        }

        public override void Update(float deltaTime)
        {
            //Creat a vector that stores the move input            
            Vector2 moveDirection = new Vector2(_xdirection, _ydirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            //Uses velocity with current Position
            LocalPosition += Velocity;

            base.Update(deltaTime);
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
        /// Uses Local Position to set a Boundry witch if reached will delete the actor.
        /// </summary>
        public virtual void BuletOutOfBounds()
        {
            if (LocalPosition.X < 23 || LocalPosition.X > 780 || LocalPosition.Y < 23 || LocalPosition.Y > 980)
            {
                _scene.RemoveActor(this);
            }
        }
    }
}

