using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 画像を持つGameObject
    public GameObject mainImage;
    // GAME START 画像
    public Sprite gameStartSpr;
    // GAME CLEAR 画像
    public Sprite gameClearSpr;
    // GAME OVER 画像
    public Sprite gameOverSpr;
    // パネル
    public GameObject panel;
    // RESTARTボタン
    public GameObject restartButton;
    // NEXTボタン
    public GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {
        // 画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        // パネルを非表示にする
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームクリア
        if (PlayerController.gameState == "gameclear")
        {
            // 画像を表示
            mainImage.SetActive(true);
            // パネルを表示
            panel.SetActive(true);
            // RESTARTボタンを無効化
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            // 画像を設定する
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";
        }
        // ゲームオーバー
        else if (PlayerController.gameState == "gameover")
        {
            // 画像を表示
            mainImage.SetActive(true);
            // パネルを表示
            panel.SetActive(true);
            // NEXTボタンを無効化
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            // 画像を設定する
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
        }
        // ゲーム中
        else if (PlayerController.gameState == "playing")
        {}
    }

    // 画像を非表示にする
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
