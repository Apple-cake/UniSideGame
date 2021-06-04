using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // 発生させるPrefabデータ
    public GameObject objPrefab;
    // 遅延時間
    public float delayTime = 3.0f;
    // X方向の発射速度
    public float fireSpeedX = -4.0f;
    // Y方向の発射速度
    public float fireSpeedY = 0.0f;

    // プレイヤー
    GameObject player;
    // 発射口
    GameObject gateObj;
    // 経過時間
    float passTimes = 0;
    // Start is called before the first frame update
    void Start()
    {
        // 発射口オブジェクトを取得
        Transform tr = transform.Find("gate");
        gateObj = tr.gameObject;
        // プレイヤー
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
