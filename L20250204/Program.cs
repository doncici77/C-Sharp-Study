using System;

namespace L20250204
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "이제훈";
            string message = String.Format("{0}님 {1} 안녕하세요", name, "졸라");
            string data = "10, 20, 30, 40";

            string[] datas = data.Split(",");

            for (int i = 0; i < datas.Length; i++)
            {
                Console.WriteLine(datas[i].Trim()); // Trim 공백 제거, Split(문자) 문자 기준으로 나누기
            }

            // SubString(2) 012 뺀 배열부터 출력, ToLower 소문자로 바꿈, Replace(문자, 문자) 자리바꿈
            Console.WriteLine(message);

            // int, float, char, 등등
            char D = (char)65;
            int A = 2;
            float B = 3.5f;
            long C = 0;

            B = (float)A; // int에서 float로 형변환

            A = (int)B; // float에서 int로 형변환

            C = (long)B;

            // Parse: 번역 -> 형변환 함수, TryParse: 형변환 시도 실패시 0 반환
            // in 키워드: 값을 바꿀수 없음, out: 값을 무조건 함수내에서 초기화 해줘야 한다. ref: 그냥 참조용
            float.TryParse(datas[0], out B);

            A.ToString();

            Console.WriteLine(B);
        }
    }
    #region 강사님 버전
    /*internal class Program
    {
        enum CardType
        {
            None = -1,
            Heart,
            Diamond,
            Clover,
            Spade
        }

        static void Main(string[] args)
        {
            int[] deck = new int[52];

            Initialize(ref deck); // 덱 초기화
            Shuffle(deck); // 덱 셔플
            Print(deck); // 출력

        }

        //초기화 함수
        static void Initialize(ref int[] deck) // ref 변수: 이 값은 참조형식으로 사용하겠다.
        {
            for(int i = 0; i < deck.Length; i++)
            {
                deck[i] = i + 1;
            }
        }

        static void Shuffle(int[] deck)
        {
            Random random = new Random();

            for(int i = 0;i < deck.Length * 10;i++) // 52장번 X 10번 셔플 520번 셔플
            {
                int firstCardIndex = random.Next(0, deck.Length);
                int secondCardIndex = random.Next(0, deck.Length);

                //임의의 카드 두개 셔플
                int temp = deck[firstCardIndex];
                deck[firstCardIndex] = deck[secondCardIndex];
                deck[secondCardIndex] = temp;
            }
        }

        static void Print(int[] deck)
        {
            PrintCardList(deck);

            int computerScore = GetScore(deck[0]) + GetScore(deck[1]) + GetScore(deck[2]);
            int playerScore = GetScore(deck[3]) + GetScore(deck[4]) + GetScore(deck[5]);

            Console.WriteLine($"Computer score : {computerScore}, Player Score : {playerScore}");

            if (playerScore >= 21 && computerScore < 21)
            {
                //Computer Win
                Console.WriteLine("Computer Win");
            }
            else if(computerScore >= 21 && playerScore < 21)
            {
                //Player Win
                Console.WriteLine("Player Win");
            }
            else if(computerScore >= 21 && playerScore >= 21)
            {
                //Player Win
                Console.WriteLine("Player Win");
            }
            else if(computerScore <= playerScore)
            {
                //Player Win
                Console.WriteLine("Player Win");
            }
            else
            {
                //Computer Win
                Console.WriteLine("Computer Win");
            }

        }

        static void PrintCardList(int[] deck)
        {
            Console.WriteLine("Computer");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{deck[i]} = {CheckCardType(deck[i])} {CheckCardName(deck[i])}");
            }
            Console.WriteLine("----------------------------");

            Console.WriteLine("Player");
            for (int i = 3; i < 6; i++)
            {
                Console.WriteLine($"{deck[i]} = {CheckCardType(deck[i])} {CheckCardName(deck[i])}");
            }
        }

        static int GetScore(int cardNumber) // 점수계산 컴포넌트
        {
            int value = ((cardNumber - 1) % 13) + 1;
            return value > 10 ? 10 : value;
        }

        static CardType CheckCardType(int cardNumber) // 카드 타입 분류 enum 사용
        {
            int valueType = (cardNumber - 1) / 12; // 최적화 된 버전?

            return (CardType)valueType;
        }

        static string CheckCardName(int cardNumber) // 카트 이름과 번호 분류 swich문 사용
        {
            int cardValue = ((cardNumber -1) / 13) + 1; // case를 사용하기 위해 계산
            string cardName;

            switch(cardValue)
            {
                case 1:
                    cardName = "A";
                    break;
                case 11: 
                    cardName = "J";
                    break;
                case 12:
                    cardName = "Q";
                    break;
                case 13:
                    cardName = "K";
                    break;
                default:
                    cardName = cardValue.ToString();
                    break;

            }

            return cardName;
        }

    }*/
    #endregion

    #region 하드코딩 내 버전
    /*internal class Program
    {
        static int[] trumpCard = new int[52]; // 52까지의 숫자 배열(트럼프 카드)

        static int drawNum = 6; // 뽑을 카드 갯수

        static string[] cardData = new string[drawNum]; // 뽑은 카드 문양 정보       
        static int[] cardNumData = new int[drawNum]; // 뽑은 카드번호 정보    

        static void Main(string[] args)
        {

            // 1 - 13 -> Heart, 1 -> A, 11 -> J, 12 -> Q, 13 -> K
            // 14 - 26 -> Diamond
            // 27 - 39 -> Clover
            // 40 - 52 -> Spade

            Initialize(); // 초기화
            Shuffle(); // 섞기
            Classify(); // 8개를 뽑고 분류
            CalculateScore(); // 점수계산
        }

        static void Initialize()
        {
            for (int i = 0; i < trumpCard.Length; i++)
            {
                trumpCard[i] = i + 1;
            }
        }

        static void Shuffle()
        {
            ////시간복잡도(N^2)
            //for (int i = 0; i < outNum; i++) // 출력 값에 랜덥값 넣기
            //{
            //    val[i] = num[random.Next(1, size + 1)];

            //    for (int j = 0; j < i; j++)
            //    {
            //        if (val[i] == val[j] && i != j) // 겹치는게 있는지 확인
            //        {
            //            val[i] = num[random.Next(1, size + 1)];
            //            j = 0;
            //        }
            //    }

            //    output.AppendLine(val[i].ToString()); // 문자열에 값을 저장
            //}

            Random random = new Random(); // 임의의 숫자

            // 시간복잡도 (N)
            // Fisher-Yates shuffle (배열 섞기)
            for (int i = 0; i < trumpCard.Length * 10; ++i)
            {
                int firstCardIndex = random.Next(0, trumpCard.Length);
                int secondCardIndex = random.Next(0, trumpCard.Length);

                int temp = trumpCard[firstCardIndex];
                trumpCard[firstCardIndex] = trumpCard[secondCardIndex];
                trumpCard[secondCardIndex] = temp;
            }
        }

        static void Classify()
        {
            // 카드 종류 분류
            for (int i = 0; i < cardData.Length; i++)
            {
                // 카드 문양
                if (trumpCard[i] < 14)
                {
                    cardData[i] = "Heart";
                }
                else if (14 <= trumpCard[i] && trumpCard[i] < 27)
                {
                    cardData[i] = "Diamond";
                }
                else if (27 <= trumpCard[i] && trumpCard[i] < 40)
                {
                    cardData[i] = "Clover";
                }
                else if (40 <= trumpCard[i] && trumpCard[i] < 53)
                {
                    cardData[i] = "Spade";
                }

                // A J Q K
                if ((trumpCard[i] % 13) == 1)
                {
                    cardData[i] += " A";
                }
                else if ((trumpCard[i] % 13) == 11)
                {
                    cardData[i] += " J";
                }
                else if ((trumpCard[i] % 13) == 12)
                {
                    cardData[i] += " Q";
                }
                else if ((trumpCard[i] % 13) == 0)
                {
                    cardData[i] += " K";
                }
                else
                {
                    cardData[i] += " " + (trumpCard[i] % 13).ToString();
                }

                cardNumData[i] = trumpCard[i]; // 카드번호 정보 저장

                // 데이터 잘 저장 되었는지 확인 출력
                Console.WriteLine($"{cardNumData[i].ToString()} {cardData[i]}");
            }
        }

        static void CalculateScore()
        {
            int computerScore = 0;
            int playerScore = 0;

            for (int i = 0; i < drawNum; i++)
            {
                if (i < 3) // 3보다 작을때 상대(컴퓨터) 점수 계산
                {
                    if ((cardNumData[i] % 13) > 10) // J Q K = 10
                    {
                        computerScore += 10;
                    }
                    else if ((cardNumData[i] % 13) == 1) // A일 경우 1 혹은 11?
                    {
                        computerScore += 11;
                    }
                    else
                    {
                        computerScore += cardNumData[i] % 13;
                    }
                }
                else if (i >= 3) // 3보다 클때 플레이어 점수 게산
                {
                    if ((cardNumData[i] % 13) > 10) // J Q K = 10
                    {
                        playerScore += 10;
                    }
                    else if ((cardNumData[i] % 13) == 1) // A일 경우 1 혹은 11?
                    {
                        playerScore += 11;
                    }
                    else
                    {
                        playerScore += cardNumData[i] % 13;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"상대의 점수는: {computerScore.ToString()}");
            Console.WriteLine($"플레이어의 점수는: {playerScore.ToString()}");

            if (computerScore > 21 && playerScore > 21)
            {
                Console.WriteLine();
                Console.WriteLine("무승부!");
            }
            else if (computerScore > 21 && playerScore < 21)
            {
                Console.WriteLine();
                Console.WriteLine("플레이어 승!");
            }
            else if (computerScore < 21 && playerScore > 21)
            {
                Console.WriteLine();
                Console.WriteLine("상대 승!");
            }
            else
            {
                if (computerScore > playerScore)
                {
                    Console.WriteLine();
                    Console.WriteLine("상대 승!");
                }
                else if (computerScore < playerScore)
                {
                    Console.WriteLine();
                    Console.WriteLine("플레이어 승!");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("무승부!");
                }
            }
        }
    }*/
    #endregion
}
