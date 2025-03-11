using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class CharacterController2D : Collider2D
    {
        public void Move(int addX, int addY)
        {
            int futureX = transform.X + addX;
            int futureY = transform.Y + addY;

            foreach(var choiceObject in Engine.Instance.world.GetAllGameObjects)
            {
                if(choiceObject.GetComponent<Collider2D>() != null)
                {
                    if(choiceObject.transform.X == futureX && choiceObject.transform.Y == futureY)
                    {
                        return;
                    }
                }
            }

            transform.Translate(addX, addY);
        }
    }
}
