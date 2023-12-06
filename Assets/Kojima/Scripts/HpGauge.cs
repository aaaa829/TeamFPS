using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    [SerializeField] private Player player; //プレイヤーの格納用

    int maxHp; //プレイヤーの体力最大値格納用
    int currentHp; //プレイヤーの現在の体力を格納

    [SerializeField] private Slider slider; //体力ゲージ表示用スライダー

    void Start()
    {
        slider.value = 1; //スライダーのゲージをフルに
        maxHp = player.GetMAXHP(); //プレイヤーの最大HPを取得
    }

    void Update()
    {
        currentHp = player.GetHp(); //プレイヤーの現在HPを取得
        slider.value = (float)currentHp / (float)maxHp; //ゲージにHPを表示
    }
}
