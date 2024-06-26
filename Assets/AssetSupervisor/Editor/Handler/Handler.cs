// Reference:
// https://docs.unity3d.com/ScriptReference/AssetPostprocessor.html
//
// Post Funcs do things with argument
// Post-Func:
// OnAssignMaterialModel(Material,Renderer)
// OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
// OnPostprocessAnimation(GameObject, AnimationClip)
// OnPostprocessAssetbundleNameChanged(string, string, string)
// OnPostprocessAudio(AudioClip)
// OnPostprocessCubemap(Cubemap)
// OnPostprocessGameObjectWithAnimatedUserProperties(GameObject, EditorCurveBinding[])
// OnPostprocessGameObjectWithUserProperties(GameObject,string[],object[])
// OnPostprocessMaterial(Material)
// OnPostprocessMeshHierarchy(GameObject)
// OnPostprocessModel(GameObject)
// OnPostprocessPrefab(GameObject root)
// OnPostprocessSpeedTree(GameObject)
// OnPostprocessSprites(Texture2D, Sprite[])
// OnPostprocessTexture(Texture2D)
// OnPostprocessTexture2DArray(Texture2DArray)
// OnPostprocessTexture3D(Texture3D)
//
//
// Pre Funcs usually do thins with `assetImporter`, ex:
// var texImporter = assetImporter as TextureImporter;
// if(texImporter is not null)
//     texImporter.filterMode = FilterMode.Point;
//
// Pre-Func:
// OnPreprocessAnimation
// OnPreprocessAsset
// OnPreprocessAudio
// OnPreprocessCameraDescription
// OnPreprocessLightDescription
// OnPreprocessMaterialDescription
// OnPreprocessModel
// OnPreprocessSpeedTree
// OnPreprocessTexture

#if UNITY_EDITOR

using System;
using System.Linq;

using UnityEngine;
using UnityEditor;
using SQLite4Unity3d;
using System.Threading;

namespace AssetSupervisor
{
    using System.IO;
    using System.Threading.Tasks;
    using DB;

    internal partial class Handler : AssetPostprocessor
    {
        static SQLiteConnection _sqlConnection;
        static Config _config;

        static void Entry()
        {
            _config = Config.Instance;
            _sqlConnection = new(
                Application.dataPath + Constants.DB_PATH,
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

            SetupDB();
            Task.Run(BackgroundWorking);
        }

        static void SetupDB()
        {
            _sqlConnection.CreateTable<DirectoryData>();
            _sqlConnection.CreateTable<AssetDetailData>();
        }

        static void BackgroundWorking()
        {
            var iter = 0;
            while (true)
            {
                Debug.Log("Checking...");
                if (_config.Paths.Count == iter)
                {
                    Thread.Sleep(5000);
                    continue;
                }

                for (int i = iter; i < _config.Paths.Count; iter = ++i)
                {
                    Debug.Log("Updating...");
                    var path = _config.Paths[i];
                    var files = Directory.GetFiles(path);
                    if (files.Length ==
                        (_sqlConnection.Table<DirectoryData>()
                            .FirstOrDefault(x => x.Path == path)?.Count ?? 0))
                        continue;

                    foreach (var file in files)
                    {
                        if (AssetDetail.TryCreate(file, out var detail))
                        {
                            Debug.Log($"Creating asset detail `{file}`!");
                            SaveAssetDetail(detail);
                        }

                        Thread.Yield();
                    }
                }

                Thread.Sleep(10000);
            }
        }

        public static void OnPostprocessAllAssets(
            string[] imported,
            string[] deleted,
            string[] moved, string[] movedFrom,
            bool didDomainReload)
        { }

        static bool SaveAssetDetail(AssetDetail detail)
        {
            lock (_sqlConnection)
            {
                DirectoryData dirData;
                AssetDetailData assetDetailData = new()
                {
                    Name = detail.Name,
                    MD5 = detail.MD5_Hash
                };

                if ((dirData = _sqlConnection.Table<DirectoryData>().FirstOrDefault(x => x.Path == detail.Dir)) is null)
                    assetDetailData.DirId = _sqlConnection.Insert(dirData = new() { Path = detail.Dir });
                else
                    assetDetailData.DirId = dirData.Id;

                _sqlConnection.Insert(assetDetailData);

                dirData.Count++;
                _sqlConnection.Update(dirData);
            }
            Debug.Log($"Added `{detail.Name}` ({detail.MD5_Hash})");
            return true;
        }

        [InitializeOnLoad]
        static class _Loader
        {
            static _Loader()
            {
                Entry();
            }
        }
    }
}

#endif