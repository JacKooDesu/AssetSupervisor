using SQLite4Unity3d;

namespace AssetSupervisor.DB
{
    public class DirectoryData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; private set; }
        public string Path { get; set; }
        public int Count { get; set; }
    }
}