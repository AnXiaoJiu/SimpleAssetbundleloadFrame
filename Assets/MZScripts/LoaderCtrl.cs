using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// 资源加载控制器
/// 负责调度资源的加载
/// </summary>
public class LoaderCtrl : UnitySingleton<LoaderCtrl> 
{
    /// <summary>
    /// 同步 单个资源加载
    /// </summary>
    /// <typeparam name="T">返回的资源类型</typeparam>
    /// <param name="path">注意：bundle路径和res下路径保持一致</param>
    /// <param name="isResload">是否resource加载</param>
    /// <returns></returns>
    public T GetAsset<T>(AssetBudnleBody body, bool isResload = false) where T : UnityEngine.Object
    {

        if (isResload)
        {
            return ResourcesLoader.Instance.LoadAsset<T>(body);
        }

         var path = body.path;

         body.path = Getroot(path);

        return ABLoader.Instance.LoadAsset<T>(body);
    }

    /// <summary>
    /// 异步加载单个资源
    /// </summary>
    /// <returns>资源内容</returns>
    /// <param name="fileName">文件名称</param>
    /// <typeparam name="T">资源类型</typeparam>
    public void GetAssetAsync<T>(AssetBudnleBody body, Action<T> finishAction, bool isResload = true) where T : UnityEngine.Object
    {

        if (isResload)
        {
            StartCoroutine(ResourcesLoader.Instance.LoadAssetAsy<T>(body, null, finishAction));

            return;
        }

        var path = body.path;

        body.path = Getroot(path);

        StartCoroutine(ABLoader.Instance.LoadAssetAsy<T>(body, null, finishAction));
    }


    public void GetAllAssetsAsync<T>(AssetBudnleBody body,Action<UnityEngine.Object[]> finishAction, bool isResload = true) where T : UnityEngine.Object
    {

        StopAllCoroutines();

        if(isResload)
        {
            StartCoroutine(ResourcesLoader.Instance.LoadAllAssetsAsy<T>(body, finishAction));

            return;
        }

        var path = body.path;

        body.path = Getroot(path);

        StartCoroutine(ABLoader.Instance.LoadAllAssetsAsy<T>(body, finishAction));

    }


    /// <summary>
    /// 获取资源到字典中
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="assetPath">路径</param>
    /// <param name="isResload">是否resource加载</param>
    /// <returns></returns>
    public Dictionary<string, T> GetAssets2Dic<T>(AssetBudnleBody body, bool isResload = true) where T : UnityEngine.Object
    {
        Dictionary<string, T> dic = new Dictionary<string, T>();

        T[] array = null;

        array = GetAssets2Arr<T>(body, isResload);

        foreach (var asset in array)
        {
            string key = asset.name;

            if (!dic.ContainsKey(key))
            {
                dic.Add(key, asset);
            }
        }

        return dic;

    }

    /// <summary>
    /// 获取资源到数组中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <param name="isResload"></param>
    /// <returns></returns>
    public T[] GetAssets2Arr<T>(AssetBudnleBody body, bool isResload = true) where T : UnityEngine.Object
    {
        T[] array = null;

        if (isResload)
        {
            return ResourcesLoader.Instance.LoadAllAssets<T>(body);
        }

        var path = body.path;

        body.path = Getroot(path);

        array = ABLoader.Instance.LoadAllAssets<T>(body);

        return array;
    }


    public void Clear(string bundlename,bool unloadAllLoadedObjects = true)
    {
        ABLoader.Instance.Clear(bundlename, unloadAllLoadedObjects);
    }

    public void Clear()
    {
        ABLoader.Instance.Clear();
    }


    private string Getroot(string path)
    {
        string r = "这里的路径是需要自己根据不同的平台进行配置";   //充当resources根路径

        r = r + "/" + path;

        return r;
    }

    void OnDestroy()
    {
        ABLoader.Instance.ClearAll();
    }
 }

/// <summary>
/// bundle体
/// 包含名字 路径 和 bundlekey
/// 负责传递资源信息
/// </summary>
public class AssetBudnleBody
{
 
    public string name;

    public string path;

    public string bundlekey;

    public AssetBudnleBody(string name,string relativepath,string bundlekey)
    {
        this.name = name;

        this.path = relativepath;

        this.bundlekey = bundlekey;
    }

    public AssetBudnleBody()
    {

    }
}
