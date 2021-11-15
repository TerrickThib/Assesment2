using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Projectiles : Actor
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

        public Projectiles(float x, float y, float speed, int xdirection, int ydirection, Scene scene, string name = "Actor", string path = "")
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

            BuletOutOfBounds();
            base.Update(deltaTime);
        }

        /// <summary>
        /// On colision with a enemy or enemyProjectiles decrment health by 1 and set actors health to be enemy actor then remove the projectile.
        /// </summary>
        /// <param name="actor"></param>
        public override void OnCollision(Actor actor)
        {
            if (actor is Enemy || actor is EnemyProjectiles)
            {
                //Create a enemy Actor that takes in the actors valuethen decrement there health then set it to be the current actor.
                Enemy enemyActor = (Enemy)actor;
                enemyActor.Health--;
                actor = enemyActor;

                _scene.RemoveActor(this);                
            }
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }        

        /// <summary>
        /// Uses Local Position to set a Boundry which if reached will delete the actor.
        /// </summary>
        public virtual void BuletOutOfBounds()
        {
            if (LocalPosition.X < 23 || LocalPosition.X > 780 ||LocalPosition.Y < 23 || LocalPosition.Y > 980 )
            {
                _scene.RemoveActor(this);
            }
        }       
    }
}
