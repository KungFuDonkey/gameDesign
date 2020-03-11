using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace GameDesign
{
    static class SaveSystem
    {
        static string baseFolder, saveFile;

        public static void SaveGame()
        {
            baseFolder = GameValues.appDataFilePath + "\\" + GameValues.gameName + "";
            CreateBaseFolders();
            BinaryFormatter formatter = new BinaryFormatter();
            saveFile = baseFolder + "\\SavedGames\\game.University";
            FileStream stream = new FileStream(saveFile, FileMode.Create);

            GameData data = new GameData();

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static GameData LoadGame()
        {
            if (File.Exists(baseFolder))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(saveFile, FileMode.Open);

                GameData data = formatter.Deserialize(stream) as GameData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.WriteLine("Save file not found at " + saveFile);
                return null;
            }
        }

        public static void CreateBaseFolders()
        {
            CreateFolder(baseFolder);
            CreateFolder(baseFolder + "\\SavedGames");
        }

        public static void CreateFolder(string s)
        {
            DirectoryInfo CreateSiteDirectory = new DirectoryInfo(s);
            if (!CreateSiteDirectory.Exists)
            {
                CreateSiteDirectory.Create();
            }
        }
    }
}
