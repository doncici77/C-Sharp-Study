using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Player : GameObject
    {
        public Player(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
            orderlayer = 4;
            isTrigger = true;

            color.r = 0;
            color.g = 0;
            color.b = 255;

            colorKey.r = 255;
            colorKey.g = 0;
            colorKey.b = 255;
            colorKey.a = 0;

            isAnimation = true;

            LoadBmp("data/player.bmp");
        }

        public override void Update()
        {
            if(Input.GetKeyDown(SDL.SDL_Keycode.SDLK_a) || Input.GetKeyDown(SDL.SDL_Keycode.SDLK_LEFT)) // a를 눌렀을때
            {
                if(!PredictCollision(X - 1, Y))
                {
                    X--;
                    spriteIndexY = 0;
                }

            }
            else if(Input.GetKeyDown(SDL.SDL_Keycode.SDLK_d) || Input.GetKeyDown(SDL.SDL_Keycode.SDLK_RIGHT))
            {
                if (!PredictCollision(X + 1, Y))
                {
                    X++;
                    spriteIndexY = 1;
                }
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_w) || Input.GetKeyDown(SDL.SDL_Keycode.SDLK_UP))
            {
                if (!PredictCollision(X, Y - 1))
                {
                    Y--;
                    spriteIndexY = 2;
                }
            }
            else if (Input.GetKeyDown(SDL.SDL_Keycode.SDLK_s) || Input.GetKeyDown(SDL.SDL_Keycode.SDLK_DOWN))
            {
                if (!PredictCollision(X, Y + 1))
                {
                    Y++;
                    spriteIndexY = 3;
                }
            }
        }

    }
}
