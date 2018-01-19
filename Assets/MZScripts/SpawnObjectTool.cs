using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 物体生成工具类
/// </summary>
public class SpawnObjectTool : Singleton<SpawnObjectTool>
{
    public GameObject SpawnGameObject(AssetBudnleBody body,Transform require_parent = null,bool isResload = false)
    {
        GameObject prefab = LoaderCtrl.Instance.GetAsset<GameObject>(body, isResload);

        GameObject go = GameObject.Instantiate<GameObject>(prefab);
        go.name = prefab.name;
        go.transform.position = prefab.transform.position;
        go.transform.rotation = prefab.transform.rotation;

        if (require_parent != null)
        {
            go.transform.SetParent(require_parent, false);
        }

        return go; 
    }

    public void SpawnGameObjectAsync(AssetBudnleBody body, Transform require_parent = null, bool isResload = false, System.Action<GameObject> finishAction = null)
    {
        LoaderCtrl.Instance.GetAssetAsync<GameObject>(body, prefab =>
        {

            GameObject go = GameObject.Instantiate<GameObject>(prefab);
            go.name = prefab.name;
            go.transform.position = prefab.transform.position;
            go.transform.rotation = prefab.transform.rotation;

            if (require_parent != null)
            {
                go.transform.SetParent(require_parent, false);
            }

            if(finishAction != null && go != null)
            {
                finishAction(go);
            }

        }, isResload);
    }

    /// <summary>
    /// 预加载 并非实例化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T PrestrainGameobject<T>(string path) where T : Object
    {
        T prefab = default(T);

       prefab = Resources.Load<T>(path);

       return prefab;
    }

}
