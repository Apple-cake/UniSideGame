using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Rigidbody2D変数
    Rigidbody2D rbody;
    // 水平方向の入力
    float axisH = 0.0f;
    // 移動速度
    public float speed = 3.0f;
    // ジャンプ力
    public float jump = 9.0f;
    // 着地レイヤー
    public LayerMask groundLayer;
    // ジャンプ開始フラグ
    bool goJump = false;
    // 地上判定
    bool onGround = false;

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
            Debug.Log("右移動開始");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            // 左向き
            Debug.Log("左移動開始");
            transform.localScale = new Vector2(-1, 1);
        }
        // キャラクターをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // 地上判定
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        // 地上 or プレイヤーの速度が0ではない
        if (onGround || axisH != 0)
        {
            // プレイヤーの速度を更新
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        // 地上 and ジャンプキーが押された
        if (onGround && goJump)
        {
            // ジャンプさせる
            Debug.Log("ジャンプ開始");
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            // ジャンプ開始フラグ false(= ジャンプ終了)
            goJump = false;
        }
    }

    public void Jump()
    {
        // ジャンプ開始フラグ true
        goJump = true;
        Debug.Log("ジャンプ開始");
    }
}
