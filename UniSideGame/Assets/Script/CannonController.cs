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
    public float length = 8.0f;

    // プレイヤー
    GameObject player;
    // 発射口
    GameObject gateObj;
    // 経過時間
    float passedTimes = 0;
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
        // 発射時間判定
        passedTimes += Time.deltaTime;
        // 距離チェック
        if (CheckLength(player.transform.position))
        {
            if (passedTimes > delayTime)
            {
                // 発射
                passedTimes = 0;
            }
        }
    }

    // 距離チェック
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        // 発射口とプレイヤーの距離を計測
        float d = Vector2.Distance(transform.position, targetPos);
        // 指定の距離以下になった場合に発射
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }
}
