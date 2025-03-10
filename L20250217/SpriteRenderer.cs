﻿using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    class SpriteRenderer : Component
    {
        public int orderlayer;

        public char Shape; // Mesh, Sprite
        public SDL.SDL_Color color;
        public int spriteSize = 30;

        protected bool isAnimation = false;
        protected IntPtr myTexture;
        protected IntPtr mySurface;

        protected int spriteIndexX = 0;
        protected int spriteIndexY = 0;

        protected SDL.SDL_Color colorKey;

        protected string filename;

        private float elapsedTime = 0;

        public SpriteRenderer()
        {

        }

        public SpriteRenderer(string infilename, bool inIsAnimation = false)
        {
            LoadBmp(infilename);
            isAnimation = inIsAnimation;
        }

        public override void Update()
        {

        }

        public virtual void Rander()
        {
            int X = 0;
            int Y = 0;

            // 모든 컴포넌트중에 그리는 애만 호출 해줘
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
                SDL.SDL_Surface* surface = (SDL.SDL_Surface*)(mySurface);

                SDL.SDL_Rect sourceRect; // 이미지

                if (isAnimation)
                {
                    if (elapsedTime >= 100.0f)
                    {
                        spriteIndexX++;
                        spriteIndexX = spriteIndexX % 5;
                        elapsedTime = 0;
                    }
                    else
                    {
                        elapsedTime += Time.deltaTime;
                    }

                    int cellsizeX = surface->w / 5;
                    int cellsizeY = surface->h / 5;
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
