using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace pract5.Labs
{
    public class Lab2
    {
        public string input { get; set; }

        int answer = 0;
        void entry(int shop, int steps)
        {
            if (steps == shop)
            {
                answer = 1;
                return;
            }
            else if (shop == steps - 1)
            {
                answer = 0;
                return;
            }
            else if ((steps - shop) % 2 != 0)
            {
                answer = 0;
                return;
            }
            else if (steps == shop + 4)
            {
                float n = shop, k = steps, a = 0;
                a += (n + 3) * (n / 2);
                answer = (int)a;
                return;
            }
            else
            {
                solve(shop, steps);
            }
        }

        void solve(int shop, int steps)
        {

            if (shop == 0)
            {
                return;
            }
            else if (shop == steps - 2)
            {
                answer += shop;
            }
            else
            {
                solve(shop - 1, steps - 1);
                solve(shop + 1, steps - 1);
            }
        }
        public List<string> start()
        {
            List<string> list = new List<string>();
            string[] inp = input.Split();
            try
            {

                int n = int.Parse(inp[0]);

                int k = int.Parse(inp[1]);

                if (n != 0 && n <= k && k <= 37)
                {
                    entry(n, k);
                    list.Add(answer.ToString());
                    return list;
                }
                else
                {
                    list.Add("parameters out of borders!");
                    return list;
                }

            }
            catch (Exception e)
            {
                list.Add(e.Message);
                return list;
            }

        }
    }
}
