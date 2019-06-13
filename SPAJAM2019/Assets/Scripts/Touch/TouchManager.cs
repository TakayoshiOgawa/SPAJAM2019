using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : SingletonMonoBehaviour<TouchManager>
{
    const int TOUCH_COUNT = 5;
    private TouchInfo[] touches = new TouchInfo[TOUCH_COUNT];

    public int TouchCount { get { return Input.touchCount; } }

    /// <summary>
    /// 生成時に実行
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        for(int index = 0; index < TOUCH_COUNT; index++)
        {
            touches[index] = new TouchInfo(index);
        }
    }

    /// <summary>
    /// タップ判定の取得
    /// マルチタッチの時はこちらを使う
    /// </summary>
    public bool Tap(int index)
    {
        return (index < TOUCH_COUNT) ? touches[index].tap : false;
    }

    /// <summary>
    /// ホールド判定の取得
    /// マルチタッチの時はこちらを使う
    /// </summary>
    public bool Hold(int index)
    {
        return (index < TOUCH_COUNT) ? touches[index].hold : false;
    }

    /// <summary>
    /// リリース判定の取得
    /// マルチタッチの時はこちらを使う
    /// </summary>
    public bool Release(int index)
    {
        return (index < TOUCH_COUNT) ? touches[index].release : false;
    }

    /// <summary>
    /// タッチ座標の取得
    /// </summary>
    public Vector3 Position(int index = 0)
    {
        return (index <TOUCH_COUNT) ? touches[index].position : Vector3.zero;
    }

    /// <summary>
    /// タッチ判定の取得
    /// シングルタッチの時はこちらを使う
    /// </summary>
    public bool Tap()
    {
        var result = false;
        foreach(var touch in touches)
        {
            result |= touch.tap;
        }
        return result;
    }

    /// <summary>
    /// ホールド判定の取得
    /// シングルタッチの時はこちらを使う
    /// </summary>
    public bool Hold()
    {
        var result = false;
        foreach (var touch in touches)
        {
            result |= touch.hold;
        }
        return result;
    }

    /// <summary>
    /// リリース判定の取得
    /// シングルタッチの時はこちらを使う
    /// </summary>
    public bool Release()
    {
        var result = false;
        foreach (var touch in touches)
        {
            result |= touch.release;
        }
        return result;
    }

    private bool doubletapFlag = false;
    private float doubletapTimer = 0F;
    /// <summary>
    /// ダブルタップの判定
    /// </summary>
    public bool DoubleTap()
    {
        if (!doubletapFlag)
        {
            // 一回目のタップを計測
            if (Tap())
            {
                doubletapFlag = true;
                doubletapTimer = 0F;
            }
        }
        else
        {
            // 一定時間経過後タップされたら成功
            doubletapTimer += Time.deltaTime;
            if (Tap() && doubletapTimer <= 0.5F)
            {
                doubletapFlag = false;
                return true;
            }
            else if(doubletapTimer >= 1F)
            {
                doubletapFlag = false;
            }
        }
        return false;
    }

    private float longtapTimer = 0F;
    /// <summary>
    /// ロングタップの判定
    /// </summary>
    public bool LongTap()
    {
        if (Hold())
        {
            longtapTimer = Mathf.Clamp01(longtapTimer + Time.deltaTime);
            return longtapTimer >= 1F;
        }
        else if(Release())
        {
            longtapTimer = 0F;
        }
        return false;
    }
    
    private Vector3 startPos = Vector3.zero;
    /// <summary>
    /// 移動量の取得
    /// </summary>
    public Vector3 Move(int index = 0)
    {
        if (Tap(index))
        {
            // 始点を取得
            startPos = Position(index);
        }
        else if (Hold(index))
        {
            // 差分を返す
            return startPos - Position(index);
        }
        return Vector3.zero;
    }

    private float startPinchScale = 0F;
    /// <summary>
    /// ピンチの拡大率を取得
    /// </summary>
    public float Pinch()
    {
        if (Hold(0))
        {
            if(Tap(1))
            {
                startPinchScale = Vector3.Distance(Position(0), Position(1));
            }
            else if(Hold(1))
            {
                return startPinchScale - Vector3.Distance(Position(0), Position(1));
            }
        }
        return 0F;
    }
}
