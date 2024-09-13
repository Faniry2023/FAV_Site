using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAV_Site.Helper
{
    public class GenerateRef
    {
        public string a()
        {
            char[] a = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Random rand = new Random();
            int num = rand.Next(0, a.Length);

            return a[num].ToString();
        }
        public string A()
        {
            char[] A = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            Random rand = new Random();
            int num = rand.Next(0, A.Length);

            return A[num].ToString();
        }
        public  string Chiffre()
        {
            Random rand = new Random();
            int num = rand.Next(0, 10);
            return num + "";
        }
        public string CodeReferenceLogicie()
        {
            string code = string.Empty;
            for (int i = 1; i <= 9; i++)
            {
                if (i == 3)
                {
                    code += "-";
                }
                if (i == 7)
                {
                    code += "-";
                }
                Random rand = new Random();
                int choix = rand.Next(1, 4);
                if (choix == 1 && i != 3 && i != 7)
                {
                    code += a();
                }
                if (choix == 2 && i != 3 && i != 7)
                {
                    code += A();
                }
                if (choix == 3 && i != 3 && i != 7)
                {
                    code += Chiffre();
                }
            }
            return code;
        }
    }
}
