using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace II
{
    class Program
    {

        static void Main(string[] args)
        {

            string est_path, topic_path;
            int groups_qty;

            //Si argumentos no son pasados en ejecucion por consola, se piden por pantalla.
            if (args.Length == 0)
            {
                Console.WriteLine("Ingrese el path del archivo de estudiantes: ");
                est_path = Console.ReadLine();
                Console.WriteLine("Ingrese el path del archivo de temas: ");
                topic_path = Console.ReadLine();
                Console.WriteLine("Ingrese la cantidad de grupos");
                groups_qty = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                est_path = args[0];
                topic_path = args[1];
                groups_qty = Convert.ToInt32(args[2]);
            }

            //Se convierte el contenido de los archivos en array.
            string[] est = File.ReadAllLines(est_path);
            string[] topic = File.ReadAllLines(topic_path);

            //Funcion random para aleatorizar estudiantes y temas.
            Random(est);
            Random(topic);


            //Numero de personas por grupo.
            List<int> grp_div = DistributeInteger(est.Length, groups_qty).ToList();

            int g = 0, f = grp_div[0];
            List<string> groups = new List<string>();
            List<string> group = new List<string>();
            //Loop para crear una colleccion de strings con los grupos.
            for(int i = 1; i <= est.Length; i++)
            {
                group.Add(est[i-1]);
                if(f == i)
                {
                    if(g < (grp_div.ToArray().Length - 1))
                    {
                        g++;
                    }
                    string a = string.Join(" , ", group);
                    groups.Add(a);
                    f += grp_div[g];
                    group = new List<string>();
                }

            }

            string[] random_groups = groups.ToArray();
            Random(random_groups);

            //Loop para mostrar por pantalla los grupos con sus temas.
            for(int i = 0; i < groups_qty; i++)
            {
                Console.WriteLine("Grupo {0}:\n " +
                    "Tema: {2} \n " +
                    "Integrantes: {1} \n" +
                    "====================================" +
                    "\n", (i+1), random_groups[i], topic[i]);
            }
        }

        public static void Random<arr>(arr[] items)
        {
            Random rand = new Random();

            for (int i = 0; i < items.Length - 1; i++)
            {

                int j = rand.Next(i, items.Length);
                arr temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }

        public static IEnumerable<int> DistributeInteger(int total, int divider)
        {
            if (divider == 0)
            {
                yield return 0;
            }
            else
            {
                int rest = total % divider;
                double result = total / (double)divider;

                for (int i = 0; i < divider; i++)
                {
                    if (rest-- > 0)
                        yield return (int)Math.Ceiling(result);
                    else
                        yield return (int)Math.Floor(result);
                }
            }
        }
    }
}
