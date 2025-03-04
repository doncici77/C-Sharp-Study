using SDL2;
using System;

namespace SDL_Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 엔진 초기화
            if(SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0) // 초기화
            {
                Console.WriteLine("Fail init");
            }

            // 설정 파일 읽어오기

            // 창 만들기
            IntPtr myWindow = SDL.SDL_CreateWindow(
                "Game",
                100, 100,
                640, 480,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN); // 윈도우 생성

            // 붓
            IntPtr myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);

            // 메세지 처리(사용자 처리가 추가 구조를 바꿈)
            SDL.SDL_Event myEvent;

            bool isRunning = true;

            #region 사각형 데이터 세팅
            Random random = new Random();
            int[] randomR = new int[100];
            int[] randomG = new int[100];
            int[] randomB = new int[100];
            int[] randomX = new int[100];
            int[] randomY = new int[100];
            int[] randomW = new int[100];
            int[] randomH = new int[100];

            for(int i = 0; i < 100; i++)
            {
                randomR[i] = random.Next(1, 255);
                randomG[i] = random.Next(1, 255);
                randomB[i] = random.Next(1, 255);
                randomX[i] = random.Next(1, 590); // 시작 x좌표
                randomY[i] = random.Next(1, 430); // 시작 y좌표
                randomW[i] = random.Next(1, 50);
                randomH[i] = random.Next(1, 50);
            }
            #endregion

            while (isRunning) // Event Loop, Game Loop
            {
                SDL.SDL_PollEvent(out myEvent);
                switch(myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT: // x버튼 누르면
                        isRunning = false; // 종료됨
                        break;
                }

                // cpu 명령어 설정
                SDL.SDL_SetRenderDrawColor(myRenderer, 0, 0, 0, 0);
                SDL.SDL_RenderClear(myRenderer);

                // 랜덤으로 100개 사각형 그리기
                for (int i = 0; i < 100; i++)
                {
                    SDL.SDL_Rect myRect;
                    myRect.x = randomX[i];
                    myRect.y = randomY[i];
                    myRect.w = randomW[i];
                    myRect.h = randomH[i];

                    SDL.SDL_SetRenderDrawColor(myRenderer, (byte)randomR[i], (byte)randomG[i], (byte)randomB[i], 0);
                    // SDL.SDL_RenderDrawRect(myRenderer, ref myRect);
                    // SDL.SDL_RenderFillRect(myRenderer, ref myRect);

                    int type = random.Next() % 2;
                    switch (type)
                    {
                        case 0:
                            SDL.SDL_RenderDrawRect(myRenderer, ref myRect); // 속이 빈 사각형
                            break;
                        case 1:
                            SDL.SDL_RenderFillRect(myRenderer, ref myRect); // 채운 사각형
                            break;
                    }
                }

                /*// 원그리기
                SDL.SDL_SetRenderDrawColor(myRenderer, 0, 255, 0, 0);

                int x = 300; // 중심 x좌표
                int y = 250; // 중심 y좌표
                float rad = 200; // 반지름*/

                /*#region 원그리기 각도로 구현 코드
                int oldX = (int)(MathF.Sin(MathF.PI / 180 * 0) * rad);
                int oldY = (int)(MathF.Cos(MathF.PI / 180 * 0) * rad);

                for (int i = 0; i < 360; i++)
                {
                    int x1 = oldX;
                    int y1 = oldY;
                    int x2 = (int)(MathF.Sin(MathF.PI / 180 * (i + 1)) * rad);
                    int y2 = (int)(MathF.Cos(MathF.PI / 180 * (i + 1)) * rad);

                    SDL.SDL_RenderDrawLine(myRenderer, x1 + x, y1 + y, x2 + x, y2 + y);

                    oldX = x2; 
                    oldY = y2;
                }
                #endregion*/

                /*#region 원그리기 라디안으로 구현 코드
                int oldX = (int)(MathF.Sin(0) * rad);
                int oldY = (int)(MathF.Cos(0) * rad);

                for (float i = 0; i < 2 * MathF.PI; i += 0.001f)
                {
                    int x1 = oldX;
                    int y1 = oldY;
                    int x2 = (int)(MathF.Sin(i) * rad);
                    int y2 = (int)(MathF.Cos(i) * rad);

                    SDL.SDL_RenderDrawLine(myRenderer, x1 + x, y1 + y, x2 + x, y2 + y);

                    oldX = x2;
                    oldY = y2;
                }
                #endregion*/

                // 랜덤으로 100개의 원 그리기
                for (int i = 0; i < 100; i++)
                {
                    SDL.SDL_SetRenderDrawColor(myRenderer, (byte)randomR[i], (byte)randomG[i], (byte)randomB[i], 0);
                    int oldX = (int)(MathF.Sin(0) * randomW[i]);
                    int oldY = (int)(MathF.Cos(0) * randomW[i]);

                    for (float j = 0; j < 2 * MathF.PI; j += 0.001f)
                    {
                        int x1 = oldX;
                        int y1 = oldY;
                        int x2 = (int)(MathF.Sin(j) * randomW[i]);
                        int y2 = (int)(MathF.Cos(j) * randomW[i]);

                        SDL.SDL_RenderDrawLine(myRenderer, x1 + randomX[i], y1 + randomY[i], x2 + randomX[i], y2 + randomY[i]);

                        oldX = x2;
                        oldY = y2;
                    }
                }

                // GPU 호출
                SDL.SDL_RenderPresent(myRenderer);

            }

            // 종료
            SDL.SDL_DestroyWindow(myWindow);

            SDL.SDL_Quit();
        }
    }
}
