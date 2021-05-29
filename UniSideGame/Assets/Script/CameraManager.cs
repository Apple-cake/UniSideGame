using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // 左スクロールリミット
    public float leftLimit = 0.0f;
    // 右スクロールリミット
    public float rightLimit = 0.0f;
    // 上スクロールリミット
    public float topLimit = 0.0f;
    // 下スクロールリミット
    public float bottomLimit = 0.0f;
    // サブスクリーン
    public GameObject subScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // カメラの座標を更新
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;
            // 横同期させる
            // 両端に移動制限をつける
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }
            // 縦同期させる
            // 上下に移動制限をつける
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }
            // カメラ位置のVector3
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;
            // サブスクリーンスクロール
            if (subScreen != null)
            {
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                Vector3 v = new Vector3(x / 2.0f, y, z);
                subScreen.transform.position = v;
            }
        }
    }
}
