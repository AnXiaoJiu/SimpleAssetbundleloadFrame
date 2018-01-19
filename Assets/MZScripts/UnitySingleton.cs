using UnityEngine;
using System.Collections;
public class UnitySingleton<T> : MonoBehaviour where T : Component
{

    private static T instance = default(T);

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Object.FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject t_go = new GameObject();
                    t_go.name = typeof(T).Name;   //类名
                    instance = t_go.AddComponent<T>();
                }
            }

            return instance;
          
        }
    }

    void OnDestroy()
    {
        System.GC.Collect();
    }

}
