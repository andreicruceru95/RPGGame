using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class AStar
    {
        private Vector2 origin;
        private Vector2 goal;
        private Dictionary<string, Vector2> nodes = new Dictionary<string, Vector2>();//each node has a position and cost
        private Dictionary<Vector2, Vector2> open = new Dictionary<Vector2, Vector2>();//open will store the current possible nodes
        private Dictionary<Vector2, Vector2> close = new Dictionary<Vector2, Vector2>();//close will store the used nodes

        //replace vectors with actors once the inheritance is added
        public void Initialize(Map map, Vector2 hunterPos, Vector2 targetPos)
        {
            origin = hunterPos;
            goal = targetPos;
        }        
    }
}
