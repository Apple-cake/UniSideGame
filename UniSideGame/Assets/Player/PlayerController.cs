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
    public float speed = 4.0f;
    // ジャンプ力
    public float jump = 9.0f;
    // 着地レイヤー
    public LayerMask groundLayer;
    // ジャンプ開始フラグ
    bool goJump = false;
    // 地上判定
    bool onGround = false;
    // スコア
    public int score = 0;

    // アニメーション対応
    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    // ゲームの状態
    public static string gameState = "playing";

    // Start is called before the first frame update
    void Start()
    {
        // Rigid2Dを取得
        rbody = this.GetComponent<Rigidbody2D>();
        // Animatorを取得
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        // 開始時、ゲーム中にする
        gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム中ではない場合、処理中断
        if (gameState != "playing")
        {
            return;
        }
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
        // ゲーム中ではない場合、処理中断
        if (gameState != "playing")
        {
            return;
        }
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
        // ゴール
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        // ゲームオーバー
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        // スコアアイテム獲得
        else if (collision.gameObject.tag == "ScoreItem")
        {
            // アイテム
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            // アイテムのスコア
            score = item.value;
            // アイテムの削除
            Destroy(collision.gameObject);
        }
    }

    // ゴール
    public void Goal()
    {
        animator.Play(goalAnime);
        // ゲームクリアにする
        gameState = "gameclear";
        Invoke("StopAnimator", 3.0f);
        // ゲーム停止
        GameStop();
    }

    public void StopAnimator()
    {
        animator.Play(stopAnime);
    }

    // ゲームオーバー
    public void GameOver()
    {
        animator.Play(deadAnime);
        // ゲームオーバーにする
        gameState = "gameover";
        // ゲーム停止
        GameStop();
        // ================
        // ゲームオーバー演出
        // ================
        // プレイヤーのアタリを消す
        GetComponent<CapsuleCollider2D>().enabled = false;
        // プレイヤーを上に少し跳ね上げる演出
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    // ゲーム停止
    void GameStop()
    {
        // Rigidbody2Dを取得
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        // 速度を0にして強制停止
        rbody.velocity = new Vector2(0, 0);
    }
}
