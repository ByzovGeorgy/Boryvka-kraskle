using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boryvka_kraskle
{
    class Program
    {
        public static List<List<KeyValuePair<int, int>>> edges = new List<List<KeyValuePair<int, int>>>();
        public static int[] name;
        public static int[] next;
        public static int[] size;
        public static int count;
        public static List<List<KeyValuePair<int, int>>> Ostov;
        public static List<KeyValuePair<int, KeyValuePair<int, int>>> SortedEdges = new List<KeyValuePair<int, KeyValuePair<int, int>>>();
        static void Main(string[] args)
        {
            Algoritm();
            for(int i=0;i<Ostov.Count;i++)
            {
                for(int j=0;j<Ostov[i].Count;j++)
                {
                    for (int k = j+1; k < Ostov[i].Count; k++)
                    {
                        if (Ostov[i][j].Key > Ostov[i][k].Key)
                        {
                            KeyValuePair<int, int> t = Ostov[i][j];
                            Ostov[i][j] = Ostov[i][k];
                            Ostov[i][k] = t;
                        }
                    }
                }
            }
            StreamWriter writer = new StreamWriter("out.txt");
            int sum = 0;
            for (int i = 0; i < Ostov.Count; i++)
            {
                for (int j = 0; j < Ostov[i].Count; j++)
                {
                    writer.Write((Ostov[i][j].Key+1) + " ");
                    sum += Ostov[i][j].Value;
                }
                writer.WriteLine();
            }
            sum /= 2;
            writer.Write(sum);
            writer.Close();
        }
        public static int mark = 0;
        private static void Algoritm()
        {
            Reader();
            SortE();
            for(int i=0;i<count;i++)
            {
                name[i] = i;
                next[i] = i;
                size[i] = 1;
            }
            Ostov= new List<List<KeyValuePair<int, int>>>();
            for(int i=0;i<count;i++)
            {
                Ostov.Add( new List<KeyValuePair<int, int>>());
            }
            int p, q, sum = 0 ;
            KeyValuePair<int, int> edge;
            while (sum<count-1)
            {
                edge = SortedEdges[mark].Value;
                p = name[edge.Key];
                q = name[edge.Value];
                if(p!=q)
                {
                    if (p > q)
                        Merge(edge.Value, edge.Key, q, p);
                    else
                        Merge(edge.Key, edge.Value, p, q);
                    Ostov[edge.Key].Add(new KeyValuePair<int, int>(edge.Value, SortedEdges[mark].Key));
                    Ostov[edge.Value].Add(new KeyValuePair<int, int>(edge.Key, SortedEdges[mark].Key));
                    sum++;
                }
                mark++;
            }
        }
        private static void Merge(int v,int w,int p,int q)
        {
            name[w] = p;
            int u = next[w];
            while(name[u]!=p)
            {
                name[u] = p;
                u = next[u];
            }
            size[p] += size[q];
            int x = next[v];
            next[v] = next[w];
            next[w] = x;
        }
        private static bool Exist(int i,int j)
        {
            for (int k = 0; k < SortedEdges.Count; k++)
            {
                if (SortedEdges[k].Value.Key == j && SortedEdges[k].Value.Value == i)
                    return true;
            }
                return false;
        }
        private static void SortE()
        {
            for(int i=0;i<edges.Count;i++)
            {
                for(int j=0;j<edges[i].Count;j++)
                {
                    if(!Exist(i, edges[i][j].Key))
                        SortedEdges.Add(new KeyValuePair<int,KeyValuePair<int,int>> (edges[i][j].Value, new KeyValuePair<int, int>(i,edges[i][j].Key)));
                }
            }
            for (int i = 0; i < SortedEdges.Count; i++)
            {
                for (int j = i+1; j < SortedEdges.Count; j++)
                {
                    if(SortedEdges[i].Key>SortedEdges[j].Key)
                    {
                        KeyValuePair<int, KeyValuePair<int, int>> t = SortedEdges[i];
                        SortedEdges[i] = SortedEdges[j];
                        SortedEdges[j] = t;
                    }
                }
            }
        }
        private static void Reader()
        {
            StreamReader reader = new StreamReader("in.txt");
            count = int.Parse(reader.ReadLine());
            name = new int[count];
            next = new int[count];
            size = new int[count];
            int[] s;
            for (int i = 0; i < count; i++)
            {
                s = reader.ReadLine().Split(new char[] {' '} ,StringSplitOptions.RemoveEmptyEntries).Select(a=>int.Parse(a)).ToArray();
                edges.Add(new List<KeyValuePair<int, int>>());
                for (int j = 0; j < s.Length; j+=2)
                {
                    if (s[j]!=0)
                        edges[i].Add(new KeyValuePair<int, int>(s[j] - 1, s[j+1]));

                }
            }
        }
    }
}
