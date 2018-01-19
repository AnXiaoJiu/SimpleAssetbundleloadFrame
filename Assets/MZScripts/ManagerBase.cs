using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 资源管理
/// 负责实例化资源
/// 非Load资源
/// </summary>
/// <typeparam name="U"></typeparam>
/// <typeparam name="T"></typeparam>
public class ManagerBase<U,T> : UnitySingleton<U> 
    where U : MonoBehaviour
    where T : Component
{

    protected Dictionary<string, T> dic = new Dictionary<string, T>();

    /// <summary>
    /// 添加预置体资源
    /// </summary>
    /// <param name="path">资源路径</param>
    public virtual void Addresource(AssetBudnleBody body, Transform parent, bool isResload = false)
    {
       /* string key = System.IO.Path.GetFileNameWithoutExtension(path);*/

        string key = body.name;

        if (dic.ContainsKey(key)) return;

        T go = SpawnObjectTool.Instance.SpawnGameObject(body, parent, isResload).GetComponent<T>();

        dic.Add(key, go);
    }

    /// <summary>
    /// 异步添加资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <param name="isResload"></param>
    /// <param name="finishAction"></param>
    public virtual void AddresourceAsync(AssetBudnleBody body,Transform parent,bool isResload = false,System.Action<GameObject> finishAction=null)
    {
       /* string key = System.IO.Path.GetFileNameWithoutExtension(path);*/

        string key = body.name;

        if (dic.ContainsKey(key)) return;

        SpawnObjectTool.Instance.SpawnGameObjectAsync(body, parent, isResload, go => {

            T g = go.GetComponent<T>();

            dic.Add(key, g);

            finishAction(go);
        });
    }

    /// <summary>
    /// 移除对应key的资源
    /// </summary>
    /// <param name="key"></param>
    public virtual void Removeresource(AssetBudnleBody body)
    {
        string key = body.name;

        T resource = Getresource(key);

        if (resource == null) return;

        dic.Remove(key);

        Destroy(resource.gameObject);

        LoaderCtrl.Instance.Clear(body.bundlekey);
    }
    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public T Getresource(string key)
    {
        T resource = default(T);

       if( dic.TryGetValue(key, out resource) )
       {
           return resource;
       }
       else
       {
           return null;
       }
    }

}
