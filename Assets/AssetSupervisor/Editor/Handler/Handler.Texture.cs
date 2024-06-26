using UnityEditor;
using UnityEngine;

namespace AssetSupervisor
{
    internal partial class Handler : AssetPostprocessor
    {
        void OnPostprocessTexture(Texture2D tex2d)
        {
            if (Config.Instance.Tracking(Config.ETrackType.Texture))
                Debug.Log(tex2d);
        }

        void OnPreprocessTexture()
        {
            if (!Config.Instance.Tracking(Config.ETrackType.Texture))
                return;

            var texImporter = assetImporter as TextureImporter;
            if (texImporter is null)
                return;

            Debug.Log(texImporter.assetPath);
        }
    }
}