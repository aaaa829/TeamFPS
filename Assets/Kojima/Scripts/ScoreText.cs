using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private Player player; //プレイヤー取得用

    int score; //スコア格納用
    Text scoreText; //スコアテキスト格納用

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        score = player.GetScore(); //Playerからスコアを取得
        scoreText.text = score + ""; //スコアテキストに代入
    }
}
