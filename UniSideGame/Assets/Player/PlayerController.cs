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

    // アニメーション対応
    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    // Start is called before the first frame update
    void Start()
    {
        // Rigid2Dを取得
        rbody = this.GetComponent<Rigidbody2D>();
        // Animatorを取得
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
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
        if (onGround)
        {
            // 地上で速度0の場合、停止アニメーション
            if (axisH == 0)
            {
                nowAnime = stopAnime;
            }
            // 地上で移動中の場合、移動アニメーション
            else
            {
                nowAnime = moveAnime;
            }
        }
        else
        {
            // 空中の場合、ジャンプアニメーション
            nowAnime = jumpAnime;
        }

        // アニメーションの変更があった場合、アニメーション再生を更新
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    public void Jump()
    {
        // ジャンプ開始フラグ true
        goJump = true;
        Debug.Log("ジャンプ開始");
    }

    // 接触開始
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal(); // ゴール
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver(); // ゲームオーバー
        }
    }

    // ゴール
    public void Goal()
    {
        animator.Play(goalAnime);
    }

    // ゲームオーバー
    public void GameOver()
    {
        animator.Play(deadAnime);
    }
}
