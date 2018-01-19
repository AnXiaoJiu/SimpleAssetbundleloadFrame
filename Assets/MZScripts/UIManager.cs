using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// 场景UI管理器
/// </summary>
public class UIManager : ManagerBase<UIManager,Component>
{

    public override void Addresource(AssetBudnleBody body, Transform parent, bool isResload = false)
    {
        base.Addresource(body, parent, isResload);
    }

    public override void AddresourceAsync(AssetBudnleBody body, Transform parent, bool isResload = false, Action<GameObject> finishAction = null)
    {
        base.AddresourceAsync(body, parent, isResload, finishAction);
    }

    public override void Removeresource(AssetBudnleBody body)
    {
        base.Removeresource(body);
    }

}
