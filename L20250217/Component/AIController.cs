using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class AIController : Component
    {
        public Random random = new Random();

        private float elapsedTime = 0;

        public SpriteRenderer spriteRenderer;
        public CharacterController2D characterController2D;

        public override void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            characterController2D = GetComponent<CharacterController2D>();
        }

        public override void Update()
        {
            Move();
        }

        public void Move()
        {
            if (elapsedTime > 500f)
            {
                int count = random.Next(0, 4);

                switch (count)
                {
                    case 0:
                        characterController2D.Move(-1, 0);
                        break;

                    case 1:
                        characterController2D.Move(1, 0);
                        break;

                    case 2:
                        characterController2D.Move(0, 1);
                        break;

                    case 3:
                        characterController2D.Move(0, -1);
                        break;

                    default:
                        break;
                }
                elapsedTime = 0;
            }
            elapsedTime += Time.deltaTime;
        }
    }
}
