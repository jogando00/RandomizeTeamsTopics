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

            //If args aren't passed at start, file paths are required through screen.
            if (args.Length == 0)
            {
                Console.WriteLine("Input estudents file path: ");
                est_path = Console.ReadLine();
                Console.WriteLine("Input topics file path: ");
                topic_path = Console.ReadLine();
                Console.WriteLine("Inpit groups quantity");
                groups_qty = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                est_path = args[0];
                topic_path = args[1];
                groups_qty = Convert.ToInt32(args[2]);
            }

            //Convert the files content into arrays.
            string[] est = File.ReadAllLines(est_path);
            string[] topic = File.ReadAllLines(topic_path);

            //Randomize students and topics arrays.
            Random(est);
            Random(topic);

            //List of quantity of people per groups.
            List<int> grp_div = DistributeInteger(est.Length, groups_qty).ToList();

            //List of quantity of topics per groups.
            List<int> topic_div = DistributeInteger(topic.Length, groups_qty).ToList();



            int g = 0, f = grp_div[0], v = 0, h = topic_div[0];
            List<string> groups = new List<string>();
            List<string> group = new List<string>();
            List<string> topics = new List<string>();
            List<string> topic_temp = new List<string>();

            //Loop to create groups with students.
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

            for(int i = 1; i <= topic.Length; i++)
            {
                topic_temp.Add(topic[i - 1]);
                if (h == i)
                {
                    if (v < (topic_div.ToArray().Length - 1))
                    {
                        v++;
                    }
                    string a = string.Join(", ", topic_temp);
                    topics.Add(a);
                    h += topic_div[v];
                    topic_temp = new List<string>();
                }
            }

            string[] random_groups = groups.ToArray();
            Random(random_groups);
            string[] random_topics = topics.ToArray();
            Random(random_topics);

            //Loop to show groups, students and topics.
            for(int i = 0; i < groups_qty; i++)
            {
                Console.WriteLine("Group #{0}: (Members Qty: {3}, Topics Qty: {4})\n " +
                    "Topics: {2} \n " +
                    "Members: {1} \n" +
                    "====================================" +
                    "\n", (i+1), random_groups[i], random_topics[i], GetIndexesOnString(random_groups[i]), GetIndexesOnString(random_topics[i]));
            }
        }

        public static int GetIndexesOnString(string strArray)
        {
            int count = strArray.Where(c => c == ',').Count();

            return count + 1;
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
                double result = total / Convert.ToDouble(divider); 

                for (int i = 0; i < divider; i++) 
                {
                    if (rest-- > 0) 
                        yield return Convert.ToInt32(Math.Ceiling(result)); 
                    else
                        yield return Convert.ToInt32(Math.Floor(result)); 
                }
            }
        }
    }
}
