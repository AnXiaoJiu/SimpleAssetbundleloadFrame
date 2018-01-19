using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Resources本地资源加载器
/// </summary>
public class ResourcesLoader : ZiyuanLoaderBase<ResourcesLoader>
{
    public override T[] LoadAllAssets<T>(AssetBudnleBody body)
    {
        var path = body.path;

        return Resources.LoadAll<T>(path);
    }

    public override T LoadAsset<T>(AssetBudnleBody body)
	{
         var path = body.path;

        return Resources.Load<T>(path);
	}

    public override IEnumerator LoadAssetAsy<T>(AssetBudnleBody body,
        Action<int> loaderProgressAction = null, 
        Action<T> finishAction = null)
	{
         var path = body.path;

        ResourceRequest request = Resources.LoadAsync<T>(path);

        while(!request.isDone)
        {
            float p = request.progress;
            int _p = ((int)(p * 100));
            if (_p >= 99)
            {
                _p = 100;
            }
            if (loaderProgressAction != null) loaderProgressAction(_p);
            yield return null;
        }


        T prefab = request.asset as T;

        if(prefab != null && finishAction != null)
        {
            finishAction(prefab);
        }
	}

    public override void Clear(string assetname, bool unloadAllLoadedObjects = true)
    {
        
    }

    public override void Clear()
    {
        Resources.UnloadUnusedAssets();
    }


    public override IEnumerator LoadAllAssetsAsy<T>(AssetBudnleBody body,
        Action<UnityEngine.Object[]> finishAction = null)
    {
        yield return null;
    }
}

