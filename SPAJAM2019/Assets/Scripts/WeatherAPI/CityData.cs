using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityData
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CityData(string i_id, string i_name)
    {
        ID = i_id;
        Name = i_name;
    }

    // プロパティ
    public string ID { get; private set; }
    public string Name { get; private set; }
}
