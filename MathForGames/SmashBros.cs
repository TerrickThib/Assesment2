using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class SmashBros : Actor
    {
        public Player _player;
        public Scene _scene;

        public SmashBros(float x, float y, Player player, string name = "Actor", string path = "")
            : base(x, y, name, path)
        {}

        /// <summary>
        /// Calls everetime the game runs changes rotation and updates using delta timed
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime)
        {
            this.Rotate(0.2f);

            base.Update(deltaTime);
        }
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
        public override void OnCollision(Actor actor)
        {
            
        }
    }
}
