namespace L20250204
{
    internal class Program
    {
        static int[] trumpCard = new int[52]; // 52까지의 숫자 배열(트럼프 카드)
        static Random random = new Random(); // 임의의 숫자
        static string[] cardData = new string[8]; // 뽑은 카드 문양 정보       
        static int[] cardNumData = new int[8]; // 뽑은 카드번호 정보

        static void Main(string[] args)
        {

            // 1 - 13 -> Heart, 1 -> A, 11 -> J, 12 -> Q, 13 -> K
            // 14 - 26 -> Diamond
            // 27 - 39 -> Clover
            // 40 - 52 -> Spade

            Initialize(); // 초기화
            Shuffle(); // 섞기
            Classify(); // 8개를 뽑고 분류
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
            /*//시간복잡도(N^2)
            for (int i = 0; i < outNum; i++) // 출력 값에 랜덥값 넣기
            {
                val[i] = num[random.Next(1, size + 1)];

                for (int j = 0; j < i; j++)
                {
                    if (val[i] == val[j] && i != j) // 겹치는게 있는지 확인
                    {
                        val[i] = num[random.Next(1, size + 1)];
                        j = 0;
                    }
                }

                output.AppendLine(val[i].ToString()); // 문자열에 값을 저장
            }*/

            // 시간복잡도 (N)
            // Fisher-Yates shuffle (배열 섞기)
            for (int i = trumpCard.Length - 1; i > 0; i--)
            {
                // 랜덤 인덱스 선택
                int j = random.Next(i + 1);

                // 현재 인덱스와 랜덤 인덱스 값을 교환
                int temp = trumpCard[i];
                trumpCard[i] = trumpCard[j];
                trumpCard[j] = temp;
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

                cardNumData[i] = trumpCard[i]; // 카드번호 정보 저장

                // 데이터 잘 저장 되었는지 확인 출력
                Console.WriteLine(cardNumData[i].ToString() + " " + cardData[i]);
            }
        }
    }
}
