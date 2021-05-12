using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadAssets : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadNorPack();
    }
    AssetBundle abPack;
    void LoadNorPack()
    {
        //这是  prefabs 标签

        abPack = AssetBundle.LoadFromFile("Assets/AssetBundles/prefabs");
        if (abPack == null)
        {
            Debug.LogError("加载失败");
            return;
        }
        Object player = abPack.LoadAsset("Player");
        //Instantiate(player);
    }

}
