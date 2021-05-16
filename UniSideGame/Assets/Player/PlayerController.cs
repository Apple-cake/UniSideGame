using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 水平方向のキー入力を取得
        axisH = Input.GetAxisRaw("Horizontal");
        // プレイヤーの向きをキー入力に合わせる
        if (axisH > 0.0f)
        {
            // 右向き
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            // 左向き
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void FixedUpdate()
    {
        // プレイヤーの速度
        rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
    }
}
