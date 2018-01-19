using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// 资源加载器基类
/// 派生出 : resource加载器 and ab加载器
/// </summary>
public abstract class ZiyuanLoaderBase<U> : Singleton<U>  where U:class,new()
{
    /// <summary>
    /// 同步 load single resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="body"></param>
    /// <returns></returns>
     public abstract T LoadAsset<T>(AssetBudnleBody body) where T : UnityEngine.Object;

    /// <summary>
    /// load all resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <returns></returns>
     public abstract T[] LoadAllAssets<T>(AssetBudnleBody body) where T : UnityEngine.Object;

     public abstract IEnumerator LoadAllAssetsAsy<T>(AssetBudnleBody body,
         Action<UnityEngine.Object[]> finishAction = null) where T : UnityEngine.Object;

    /// <summary>
    /// 异步 load single resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <param name="loaderProgressAction"></param>
    /// <param name="finishAction"></param>
    /// <returns></returns>
     public abstract IEnumerator LoadAssetAsy<T>(AssetBudnleBody body,
         Action<int> loaderProgressAction = null,
         Action<T> finishAction = null) where T : UnityEngine.Object;

    /// <summary>
    /// clear resource by assetname
    /// </summary>
    /// <param name="bundlekey"></param>
     public abstract void Clear(string bundlekey, bool unloadAllLoadedObjects = true);

    /// <summary>
    /// clear resouce
    /// </summary>
     public abstract void Clear();
}
