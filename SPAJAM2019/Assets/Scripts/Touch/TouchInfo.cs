using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInfo
{
    // 共通のタッチ状態
    public enum Phase
    {
        None,        // なし
        Began,       // タッチ開始
        Canceled,    // キャンセル
        Ended,       // タッチ終了
        Moved,       // スワイプ
        Stationary,  // ホールド
    }

    public bool touched { get { return Input.touchCount > ID; } }
    private Touch main { get { return Input.GetTouch(ID); } }

    // タッチ番号
    public int ID { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id">タッチ番号</param>
    public TouchInfo(int id)
    {
        ID = id;
    }

    /// <summary>
    /// タッチ座標
    /// </summary>
    public Vector3 position
    {
        get
        {
            if (MobileAppUtility.AppRuntime())
            {
                if (touched)
                    return main.position;
            }
            else if (Application.isEditor)
            {
                return Input.mousePosition;
            }
            return Vector3.zero;
        }
    }

    /// <summary>
    /// タップ判定
    /// </summary>
    public bool tap
    {
        get
        {
            return GetTouch().Equals(Phase.Began);
        }
    }

    /// <summary>
    /// ホールド判定
    /// </summary>
    public bool hold
    {
        get
        {
            return GetTouch().Equals(Phase.Moved) || GetTouch().Equals(Phase.Stationary);
        }
    }

    /// <summary>
    /// リリース判定
    /// </summary>
    public bool release
    {
        get
        {
            return GetTouch().Equals(Phase.Canceled | Phase.Ended);
        }
    }

    /// <summary>
    /// Editorと共通のタッチ状態
    /// </summary>
    /// <returns>タッチ状態</returns>
    private Phase GetTouch()
    {
        if (MobileAppUtility.AppRuntime())
        {
            // スマホのタッチ状態
            if (touched)
            {
                return (Phase)((int)main.phase);
            }
        }
        else
        {
            // Editorのタッチ状態
            if (Input.GetMouseButtonDown(0)) return Phase.Began;
            if (Input.GetMouseButton(0))     return Phase.Stationary;
            if (Input.GetMouseButtonUp(0))   return Phase.Ended;
        }
        return Phase.None;
    }
}
