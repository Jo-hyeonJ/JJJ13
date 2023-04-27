using System.Security.Authentication;
using System.Collections.Generic;
using System.Collections;

namespace JJJ13
{
    internal class Program
    {
        static int Sum(int a, int b) 
        {
            return a + b;
        }

        static float Sum(float a, float b)
        {
            return a + b;
        }

        static void TypePrint<T>(T value)
        {
            Console.WriteLine($"Type : {typeof(T).FullName}");
        }

        // 연산자 오버로딩
        class Vector2
        {
            private float x;
            private float y;
            public Vector2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"{x},{y}";
            }

            // 연산자 오버로딩
            public static Vector2 operator +(Vector2 p1, Vector2 p2)
            {
                Vector2 vec = new Vector2(p1.x + p2.x, p1.y + p2.y);
                return vec;
            }
            public static Vector2 operator -(Vector2 p1, Vector2 p2)
            {
                Vector2 vec = new Vector2(p1.x - p2.x, p1.y - p2.y);
                return vec;
            }
            // 다른 자료형끼리의 연산도 가능하다.
            public static Vector2 operator *(Vector2 p1, float x)
            {
                Vector2 vec = new Vector2(p1.x * x, p1.y * x);
                return vec;
            }
        }


        // T : 형식 매개변수, 실행 전까지는 자료형이 정해지지 않는다. 런타임 때 값이 넘어오면 그때서야 자료형이 정해진다.
        // 일반 타입 클래스, Generic Type Class라고도 한다.

        // where : 어떤 자료형이든 수용이 가능한 T 자료형에 들어갈 수 있는 자료형을 제한하는 제한자. 함수는 못붙임

   /*     static T Sum2<T>(T a value); 
        {
            return a + b;
        }
*/
        
        class Box<T, T2> // T는 복수 수용 가능하다.
            where T : struct
            where T2 : struct
        {
            public T2 count;
            public string name;
            public T number;

        }

        class GiftBox : IEnumerator, IEnumerable<string>
        {
            string[] gifts;
            int capacity;       // 배열의 총 용량
            int size;           // 속성(=실제 값)의 길이
            int last;           // 마지막 배열의 index가 어디인가

            public object Current => throw new NotImplementedException();

            public GiftBox()
            {
                gifts = new string[4];
                capacity = 4;
                size = 0;
                last = -1;
            }

            public void Add(string str)
            {
                last += 1;
                if (last == capacity)
                {
                    capacity *=2;
                    Array.Resize<string>(ref gifts, capacity);
                }

                gifts[last] = str;

            }

            public string this[int index]
            {
                get
                {
                    return gifts[index];
                }
            }


            public IEnumerator GetEnumerator() // giftbox 내부에 있는 get
            {
                for (int i = 0; i <= last; i++)
                {
                    yield return gifts[i];
                }


            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            IEnumerator<string> IEnumerable<string>.GetEnumerator() // IEnumerable. 안에 있는 get
            {
                for (int i = 0; i <= last; i++)
                {
                    yield return gifts[i];
                }

            }
        }
        static void Main(string[] args)
        {
            /*
            // 일반화
            // 공통적인 부분을 묶고 비공통적인 부분에 대해 처리하는 방법

            // 오버로딩을 이용 할 경우
            float ab1 = 140.5f;
            float ab2 = 288.4f;
            Sum(ab1, ab2);


            // 일반화를 이용한 방법
            TypePrint(ab1); // float = system.single

            double ab3 = 403.1;
            TypePrint(ab3); // system.double

            Vector2 position = new Vector2(10, 20);
            Console.WriteLine(position);
            Vector2 position2 = new Vector2(3, 5);
            
            // 기존의 존재하지 않았던 벡터 더하기 함수 사용
            Console.WriteLine($"p1 : {position} + p2:{position2} = {position + position2}");
            Console.WriteLine($"p1 : {position} - p2:{position2} = {position - position2}");
            Console.WriteLine($"p1 : {position} - x:{2} = {position * 2}");

            Box<int> b1 = new Box<int>();
             Box<double> b2 = new Box<double>();
            // Box<string> b3 = new Box<string>; => struct만 수용이 가능하기에 오류뜸
            */
            /*
                        Console.WriteLine($"b1's number : {b1.number.GetType().name}"); 
                        Console.WriteLine($"b2's number : {b2.number.GetType().name}");
            */


            // Generic Collection
            // [컬렉션들끼리는 자신의 값을 자유롭게 상호교환이 가능하다. 인터페이스 시스템이 이를 가능케한다.]
            //                                                          ↑ 모든 컬렉션이 IEnumerable을 기반으로 만들어졌기 때문이다.
            // List     : Linked List와 다름. 개수 제한이 없는 Generic 자료형
            // Queue    : FIFO형태의 Generic 자료형
            // Stack    : LIFO형태의 Generic 자료형
            // Dictionary   : Hashtable의 Generic형태





            // 한가지 자료형만 수용 가능한 리스트. object형태로 받는 것이 아니기 때문에 형변환의 필요성이 없다.

            string[] strs = { "AA", "BB", "CC", "DD" };

            // List가 생성자를 호출 시 IEnumerable<T> 타입의 값을 받을 수 있는데
            // 현재 배열은 컬렉션의 일종이고 해당 인터페이스를 구현하고 있기 때문이다.
            // 기존 배열도 IEnumerable를 상속 중



            GiftBox giftbox = new GiftBox();
            giftbox.Add("랜덤선물");
            giftbox.Add("랜덤선물1");
            giftbox.Add("랜덤선물2");
            giftbox.Add("랜덤선물3");

            // foreach는 제네릭 수용하지 못한다. 그래서 일반 타입으로 만들어줘야함
            foreach (string f in giftbox)
            {
                Console.WriteLine(f);
            }


            //                                          ↓ 같은 인터페이스를 공유하기에 삽입 가능  
            List<string> personList = new List<string>(strs);
            personList.Add("abcd");
            personList.Add("Monday");
            personList.Add("Minsu");

            string str = personList[0];
            Console.WriteLine(str);

            for (int i = 0; i < strs.Length; i++)
            {
                personList.Add(strs[i]);
            }

            List<string> personList2 = new List<string>(giftbox); //제네릭으로 만듬

            // Queue
            Queue<string> queue = new Queue<string>(giftbox);

            Console.WriteLine("queue " + queue.Peek());

            // Stack
            Stack<string> stack = new Stack<string>(giftbox);
            Console.WriteLine("stack " + stack.Peek());



            // null : 참조 값이 없다. 값이 없다. 참조하고 있는 메모리가 없다.
            int? a = null;          // nullable형식의 자료형. ( 값형 타입이 null을 포함할 수 있다.)

            // 예로 스킬포인트가 있다. 초보자는 sp가 해금되지 않는다.
            int sp = 0;
            int? newbie = null;



            // Dictionary
            
            
            Dictionary<int, string> locker = new Dictionary<int, string>();
            locker.Add(3, "여행용 가방");
            locker.Add(7, "서류 가방");

            foreach(int num in locker.Keys)
            {
                Console.WriteLine($"대여 중인 사물함은 : {num}");
            }

            // 컬렉션에 한해 모든 요소를 (특정 문자 기준) 문자열로 변환해주는 함수
            Console.WriteLine(string.Join(",", locker.Keys));

            int select = 0;
            Console.Write("사물함의 번호를 입력하세요 : ");
            // TryParse는 제시 받은 자료형이 아닌 값이 들어갈 경우 무시 되고 기본값인 0으로 초기화된다. (확인바람)
            int.TryParse(Console.ReadLine(), out select);

            if(select == 0 || !locker.ContainsKey(select))
            {
            Console.WriteLine("잘못된 번호입니다.");
            }
            else
            {
                Console.WriteLine("사물함이 열렸다." + locker[select]);
            }

            // out : ref처럼 참조형식으로 매개 변수를 받는다.(기본적으로 ref를 내포함) 이 변수에게 무조건 함수가 끝나기 전까지 값을 대입하겠다.
            // return으로 하나의 값을 주는 것이 아닌 out을 활용해 2가지 값을 출력할 수 있다. = ref도 가능은 하다.

        }
    }
}