using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace GameDesign
{
    public static class SaveBuilding
    {
        public static void save(char[,] save, int size, Type type)
        {
            string directory = Directory.GetCurrentDirectory();
            directory = directory.Remove(directory.Length - 22);
            string amountpath = Path.Combine(directory, "Rooms//amount.txt");
            int amount = int.Parse(File.ReadAllLines(amountpath)[0]);
            string path = Path.Combine(directory, "Rooms//room" + amount.ToString() + ".txt");
            amount++;
            List<string> Save = new List<string>();
            for(int y = 0; y < size; y++)
            {
                string line = "";
                for(int x = 0; x < size; x++)
                {
                    line += save[x, y];
                }
                Save.Add(line);
            }
            File.WriteAllLines(amountpath, new string[]{ amount.ToString()});
            File.WriteAllLines(path, Save.ToArray());
        }
    }
}
