#if UNITY_EDITOR

using System;
using System.Linq;

using UnityEngine;
using UnityEditor;

namespace AssetSupervisor
{
    internal class Handler : AssetPostprocessor
    {
        static void Entry()
        {
            
        }

        public static void OnPostprocessAllAssets(
            string[] imported,
            string[] deleted,
            string[] moved, string[] movedFrom,
            bool didDomainReload)
        {
            Entry();

            // foreach (var x in imported)
            // {
            //     Debug.Log(x);
            // }
            // Debug.Log(deleted.Length);
            // Debug.Log(moved.Length);
            // Debug.Log(movedFrom.Length);
            // Debug.Log(didDomainReload);
        }
    }
}

#endif