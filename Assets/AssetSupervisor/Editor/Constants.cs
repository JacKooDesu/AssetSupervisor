using System.Reflection;

namespace AssetSupervisor
{
    internal class Constants
    {
        public const string CONFIG_PATH = "Assets/AssetSupervisor/Setting.asset";
        public const string DB_PATH = "/AssetSupervisor/FileDetails.db";

        public static string BASE_FOLDER = UnityEngine.Application.dataPath;
    }
}