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

namespace AssetSupervisor
{
    internal partial class Handler : AssetPostprocessor
    {
        static void Entry() { }

        public static void OnPostprocessAllAssets(
            string[] imported,
            string[] deleted,
            string[] moved, string[] movedFrom,
            bool didDomainReload)
        { }
    }
}

#endif