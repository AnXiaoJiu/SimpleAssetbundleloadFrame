using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Assetbundle加载器
/// </summary>
public class ABLoader : ZiyuanLoaderBase<ABLoader>
{
    protected Dictionary<string, AssetBundle> bundleMap = new Dictionary<string, AssetBundle>();  //save bundle

    /// <summary>
    /// 同步加载单个ab资源 要传入ab的路径
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    public override T LoadAsset<T>(AssetBudnleBody body)
    {
        var path = body.path;

        AssetBundle bundle = AssetBundle.LoadFromFile(path);
        if (bundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return null;
        }

        if (!bundleMap.ContainsKey(body.bundlekey)) bundleMap.Add(body.bundlekey, bundle);

        T t = bundle.LoadAsset<T>(body.name);   //assetName 大小写不区分

        //bundle.Unload(false);

        return t;
    }


    public override T[] LoadAllAssets<T>(AssetBudnleBody body)
    {
        var path = body.path;

        AssetBundle bundle = AssetBundle.LoadFromFile(path);
        if (bundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return null;
        }

        /*string assetName = System.IO.Path.GetFileNameWithoutExtension(path);*/

        if (!bundleMap.ContainsKey(body.bundlekey)) bundleMap.Add(body.bundlekey, bundle);

        T[] t = bundle.LoadAllAssets<T>();

       // bundle.Unload(false);   

        return t;
    }


    public override IEnumerator LoadAssetAsy<T>(
       AssetBudnleBody body, 
        Action<int> loaderProgressAction = null,
        Action<T> finishAction = null)
    {
        var assetPath = body.path;

        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(assetPath);

        while (!request.isDone)
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

        AssetBundle bundle = request.assetBundle;

        /*string assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);*/

        if (!bundleMap.ContainsKey(body.bundlekey)) bundleMap.Add(body.bundlekey, bundle);

        AssetBundleRequest bundleReq = bundle.LoadAssetAsync<T>(body.name);
       // bundle.Unload(false);
        while (!bundleReq.isDone)
        {
            yield return null;
        }
        T prefab = (T)bundleReq.asset;
        if (finishAction != null && prefab != null)
        {
            finishAction(prefab);
        }
    }

  
    public override IEnumerator LoadAllAssetsAsy<T>(AssetBudnleBody body, Action<UnityEngine.Object[]> finishAction = null)
    {
        var assetPath = body.path;

        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(assetPath);

        while (!request.isDone)
        {
            yield return null;
        }

        AssetBundle bundle = request.assetBundle;

        if (!bundleMap.ContainsKey(body.bundlekey)) bundleMap.Add(body.bundlekey, bundle);

        AssetBundleRequest bundleReq = bundle.LoadAllAssetsAsync<T>();
       // bundle.Unload(false);
        while (!bundleReq.isDone)
        {
            yield return null;
        }

        if (finishAction != null)
        {
            finishAction(bundleReq.allAssets);
        }
       
    }


    public override void Clear(string bundlename, bool unloadAllLoadedObjects = true)
    {
        AssetBundle bundle = null;

       bool isget = bundleMap.TryGetValue(bundlename, out bundle);

       if (!isget) return;

       //DebugSystem.Log("卸载bundle资源:" + bundlename);

        if(bundle != null)
        {
            bundle.Unload(unloadAllLoadedObjects);
        }

        bundleMap.Remove(bundlename);
    }

    public override void Clear()
    {

    }

    public void ClearAll()
    {
        foreach(KeyValuePair<string,AssetBundle> bundle in bundleMap)
        {
            if(bundle.Value != null)
            {
                bundle.Value.Unload(true);
            }
        }

        //DebugSystem.Log("卸载所有");

        bundleMap.Clear();
    }
}
