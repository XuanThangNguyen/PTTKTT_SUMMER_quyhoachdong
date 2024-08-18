using System;

namespace ConsoleApp1
{
    class TinhSoToHop
    {
        static int TinhToHop_DP_CaiTien(int n, int k)
        {
            int[] V = new int[n + 1];
            int p1, p2;

            // Khởi tạo giá trị cho trường hợp cơ sở
            V[0] = 1;
            V[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                p1 = V[0];

                for (int j = 1; j < i; j++)
                {
                    p2 = V[j];
                    V[j] = p1 + p2;
                    p1 = p2;
                }

                V[i] = 1;
            }

            return V[k];
        }

        static void Main()
        {
            int n = 5;
            int k = 2;
            int result = TinhToHop_DP_CaiTien(n, k);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"Tổ hợp chập {k} của {n} là: {result}");
        }
    }
}
