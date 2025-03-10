using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class GameObject
    {
        public int X; // X좌표
        public int Y; // Y좌표
        public char Shape; // Mesh, Sprite
        public int orderlayer;
        public bool isTrigger = false;
        public bool isCollide = false;

        public SDL.SDL_Color color;
        public int spriteSize = 30;

        protected bool isAnimation = false;
        protected IntPtr myTexture;
        protected IntPtr mySurface;

        protected SDL.SDL_Color colorKey;

        public GameObject()
        {
            colorKey.r = 255;
            colorKey.g = 255;
            colorKey.b = 255;
            colorKey.a = 255;
        }

        public bool PredictCollision(int newX, int newY)
        {
            for (int i = 0; i < Engine.Instance.world.GetAllGameObjects.Count; i++)
            {
                if (Engine.Instance.world.GetAllGameObjects[i].isCollide == true &&
                   Engine.Instance.world.GetAllGameObjects[i].X == newX &&
                   Engine.Instance.world.GetAllGameObjects[i].Y == newY)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void Update()
        {

        }
        public virtual void Rander() 
        {
            // X,Y 위치에 Shape 출력
            //Console.SetCursorPosition(X, Y);
            //Console.WriteLine(Shape);

            Engine.backBuffer[Y, X] = Shape; // 백버퍼의 좌표값에 모양을 저장 // 그리는것은 아님.

            // SDL.SDL_SetRenderDrawColor(Engine.Instance.myRenderer, color.r, color.g, color.b, color.a);
            // SDL.SDL_RenderDrawPoint(Engine.Instance.myRenderer, X, Y);
            

            SDL.SDL_Rect myRect;
            myRect.x = X * spriteSize;
            myRect.y = Y * spriteSize;
            myRect.w = spriteSize;
            myRect.h = spriteSize;

            // SDL.SDL_RenderFillRect(Engine.Instance.myRenderer, ref myRect);
            unsafe
            {
                // 이미지 정보 가져와서 할일이 있음.
                SDL.SDL_Surface* suface = (SDL.SDL_Surface*)(mySurface);

                SDL.SDL_Rect sourceRect;

                if (isAnimation)
                {
                    sourceRect.x = 0;
                    sourceRect.y = 0;
                    sourceRect.w = suface->w / 5;
                    sourceRect.h = suface->h / 5;
                }
                else
                {
                    sourceRect.x = 0;
                    sourceRect.y = 0;
                    sourceRect.w = suface->w;
                    sourceRect.h = suface->h;
                }

                SDL.SDL_RenderCopy(Engine.Instance.myRenderer, myTexture, ref sourceRect, ref myRect);
            }
        }

        public void LoadBmp(string filename)
        {
            // SDL C, 접근 할 수 있는게 없어거 unsafe 사용
            mySurface = SDL.SDL_LoadBMP(filename);

            unsafe
            {
                // 이미지 정보 가져와서 할일이 있음.
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)(mySurface);
                SDL.SDL_SetColorKey(mySurface, 1, SDL.SDL_MapRGB(surface->format, colorKey.r, colorKey.g, colorKey.b));
            }

            myTexture = SDL.SDL_CreateTextureFromSurface(Engine.Instance.myRenderer, mySurface);
        }

    }
}
