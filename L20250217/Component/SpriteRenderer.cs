using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class SpriteRenderer : Component
    {
        public int orderlayer;

        public char Shape; // Mesh, Sprite
        public SDL.SDL_Color color;
        public int spriteSize = 30;

        protected bool isAnimation = false;
        protected IntPtr myTexture;
        protected IntPtr mySurface;

        public int spriteIndexX = 0;
        public int spriteIndexY = 0;

        public SDL.SDL_Color colorKey;

        protected string filename;

        private float elapsedTime = 0;
        public float preocessTime = 100.0f;

        public int maxCellCountX = 5;
        public int maxCellCountY = 5;

        SDL.SDL_Rect sourceRect; // 원본 이미지
        SDL.SDL_Rect destinationRect;

        public SpriteRenderer()
        {

        }

        public override void Update()
        {
            int X = gameObject.transform.X;
            int Y = gameObject.transform.Y;

            destinationRect.x = X * spriteSize;
            destinationRect.y = Y * spriteSize;
            destinationRect.w = spriteSize;
            destinationRect.h = spriteSize;

            unsafe
            {
                // 이미지 정보 가져와서 할일이 있음.
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)(mySurface);

                if (isAnimation)
                {
                    if (elapsedTime >= preocessTime)
                    {
                        spriteIndexX++;
                        spriteIndexX = spriteIndexX % maxCellCountX;
                        elapsedTime = 0;
                    }
                    else
                    {
                        elapsedTime += Time.deltaTime;
                    }

                    int cellsizeX = surface->w / maxCellCountX;
                    int cellsizeY = surface->h / maxCellCountY;
                    sourceRect.x = cellsizeX * spriteIndexX;
                    sourceRect.y = cellsizeY * spriteIndexY;
                    sourceRect.w = cellsizeX;
                    sourceRect.h = cellsizeY;
                }
                else
                {
                    sourceRect.x = 0;
                    sourceRect.y = 0;
                    sourceRect.w = surface->w;
                    sourceRect.h = surface->h;
                }
            }
        }

        public virtual void Rander()
        {
            int X = gameObject.transform.X;
            int Y = gameObject.transform.Y;

            Engine.backBuffer[Y, X] = Shape; // 백버퍼의 좌표값에 모양을 저장 // 그리는것은 아님.

            unsafe
            {
                SDL.SDL_RenderCopy(Engine.Instance.myRenderer, myTexture, ref sourceRect, ref destinationRect);
            }
        }

        public void LoadBmp(string filename, bool inIsAnimation = false)
        {
            string projectFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            isAnimation = inIsAnimation;

            // SDL C, 접근 할 수 있는게 없어거 unsafe 사용
            mySurface = SDL.SDL_LoadBMP(projectFolder + "/data/" + filename);

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
