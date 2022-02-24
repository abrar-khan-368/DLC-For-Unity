using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetBundleCreator
{
#if UNITY_EDITOR
    public class CreateAssetBundle
    {
        [MenuItem("Assets/ Build Asset Bundles")]
        static void BuildAllAssetBundles()
        {
            BuildPipeline.BuildAssetBundles("Assets/MultiplayerMayhemDLC/Resources", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
    }
#endif
}
