using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    class Test
    {
        public static string processInput(string input)
        {
            int? qtd_Entradas = Convert.ToInt32(input.Substring(0,1));
            int tamanho_senha = Convert.ToInt32(input.Substring(1,1));
            var senha = input.Substring(2);
            var palavras = new List<string>();

            while (senha.Length >= tamanho_senha)
            {
                var palavra = "";
                for (int i = 0; i < tamanho_senha; i++)
                {
                    palavra += senha[i];
                }
                palavras.Add(palavra);

                senha = senha.Substring(1);
            }
            
            
            //Aqui é onde você deve desenvolver o seu algoritmo que irá processar a entrada e então retorna-la.
        return input;
        }

        //Esta função geralmente não precisa ser alterada, a não ser que você julgue ser necessário.
        public static void Main()
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                processInput("13onetwoone");
                //Console.WriteLine(processInput(line));
            }
        }

    }
    



}
