using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace lab_2
{
    class Keys
    {
        public int GenerateP()
        {
            Random rnd = new Random();
            int p = 4;
            while (!IsPrime(p))
            {
                p = rnd.Next(10000, 1000000);
            }
            
            return p;
        }

        private bool IsPrime(int number)
        {
            if (number > 1)
            {
                int sqrt = (int)Math.Ceiling(Math.Sqrt((double)number));
                for (int i = 2; i <= sqrt; i++)
                {
                    if (number % i == 0)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public int GenerateG(int p)
        {
            for (int i = 0; i < p; i++)
                if (IsPrimitiveRoot(p, i))
                    return i;
            return 0;
        }

        public bool IsPrimitiveRoot(int p, int a)
        {
            if (a == 0 || a == 1)
                return false;
            int last = 1;
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < p - 1; i++)
            {
                last = (last * a) % p;
                if (set.Contains(last))
                    return false;
                set.Add(last);
            }
            return true;
        }

        public int EulerFunction(int number)
        {
            int result = number;
            int sqrt = (int)Math.Ceiling(Math.Sqrt(number));
            for (int i = 2; i <= sqrt; i++)
            {
                if (number % i == 0)
                {
                    while (number % i == 0)
                    {
                        number /= i;
                    }
                    result -= (result / i);
                }
            }
            if (number > 1)
            {
                result -= (result / number);
            }
            return result;
        }

        public int GenerateX(int p)
        {
            Random rnd = new Random();
            int x = rnd.Next(2, p - 1);
            return x;
        }

        public int FastExponentiation(BigInteger mod, BigInteger num, BigInteger deg)
        {
            BigInteger y = 1;
            while (deg != 0)
            {
                while (deg % 2 == 0)
                {
                    deg /= 2;
                    num = (num * num) % mod; 
                }
                deg--;
                y = (y * num) % mod;
            }
            return (int)y;
        }

        public bool IsCoprime(int num1, int num2)
        {
            if (num1 == num2)
            {
                return num1 == 1;
            }
            else if (num1 > num2)
            {
                return IsCoprime(num1 - num2, num2);
            }
            else
            {
                return IsCoprime(num2 - num1, num1);
            }  
        }

        public int Mul(int a, int b, int n) // a*b mod n - умножение a на b по модулю n
        {
            var sum = 0;
            for (var i = 0; i < b; i++)
            {
                sum += a;
                if (sum >= n)
                {
                    sum -= n;
                }
            }

            return sum;
        }
    }
}
