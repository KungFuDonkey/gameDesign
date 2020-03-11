using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameDesign
{
    class Save
    {
        string gameName, baseFolder;
        
        public Save()
        {
            gameName = GameValues.gameName;

            baseFolder = GameValues.appDataFilePath + "\\" + gameName + "";
            CreateBaseFolders();
        }


        public void CreateBaseFolders()
        {
            CreateFolder(baseFolder);
            CreateFolder(baseFolder + "\\XML");
            CreateFolder(baseFolder + "\\XML\\SavedGames");
        }

        public void CreateFolder(string s)
        {
            DirectoryInfo CreateSiteDirectory = new DirectoryInfo(s);
            if (!CreateSiteDirectory.Exists)
            {
                CreateSiteDirectory.Create();
            }
        }

        public bool FileExists(string PATH)
        {
            return File.Exists(GameValues.appDataFilePath + "\\" + gameName + "\\" + PATH);
        }

        public void DeleteFile(string PATH)
        {
            File.Delete(PATH);
        }

        public XDocument GetFile(string FILE)
        {
            if (FileExists(FILE))
            {
                return XDocument.Load(GameValues.appDataFilePath + "\\" + gameName + "\\" + FILE);
            }

            return null;
        }

        public virtual void HandleSaveFormates(XDocument xml, string PATH)
        {
            xml.Save(GameValues.appDataFilePath + "\\" + gameName + "\\XML\\" + PATH);
        }
    }
}
