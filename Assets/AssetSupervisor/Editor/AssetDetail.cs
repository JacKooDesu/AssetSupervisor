using System;
using System.IO;
using MD5 = System.Security.Cryptography.MD5;

namespace AssetSupervisor
{
    internal class AssetDetail
    {
        AssetDetail(string fName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fName))
                {
                    Name = stream.Name;
                    MD5_Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public bool TryCreate(string fName, out AssetDetail result)
        {
            result = null;

            if (!File.Exists(fName))
                return false;

            try
            {
                result = new(fName);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public readonly string Name;
        public readonly string MD5_Hash;
    }
}