using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MobileAppUtility
{
    /// <summary>
    /// スマートフォンで実行しているか判定
    /// Editorを検知する場合は"Application.isEditor"を使う
    /// </summary>
    public static bool AppRuntime()
    {
        return (Application.platform.Equals(RuntimePlatform.Android)
             || Application.platform.Equals(RuntimePlatform.IPhonePlayer));
    }
}
