using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTest : MonoBehaviour 
{

	// Use this for initialization
	void Start ()
    {
        //资源名称
        string resname = "";
        //资源相对于各个平台的路径
        string relpath = "";
        //bundle名称
        string bundlename = "";
        AssetBudnleBody abbody = new AssetBudnleBody(resname,relpath,bundlename);
        //加载一个3d物体
        ModelManager.Instance.Addresource(abbody, null, false);
        //获取该模型
        GameObject go  = ModelManager.Instance.Getresource(resname).gameObject;
        //销毁该模型
        ModelManager.Instance.Removeresource(abbody);
	}
	

}
