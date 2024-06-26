using SQLite4Unity3d;

namespace AssetSupervisor.DB
{
    public class AssetDetailData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; private set; }
        public int DirId { get; set; }
        public string Name { get; set; }
        public string MD5 { get; set; }
    }
}