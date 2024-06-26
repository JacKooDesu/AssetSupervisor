using System;
using System.IO;
using MD5 = System.Security.Cryptography.MD5;

namespace AssetSupervisor
{
    internal class AssetDetail
    {
        AssetDetail(FileInfo fInfo)
        {
            using (var md5 = MD5.Create())
            {
                var baseUri = new Uri(Constants.BASE_FOLDER);
                Dir = baseUri.MakeRelativeUri(new(fInfo.Directory.FullName)).ToString();

                using (var stream = File.OpenRead(fInfo.FullName))
                {
                    Name = fInfo.Name;
                    MD5_Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static bool TryCreate(string fName, out AssetDetail result)
        {
            result = null;

            if (!File.Exists(fName))
                return false;

            try
            {
                result = new(new(fName));
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
                return false;
            }

            return true;
        }

        public readonly string Dir;
        public readonly string Name;
        public readonly string MD5_Hash;
    }
}