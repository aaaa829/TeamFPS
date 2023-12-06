using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    User user;
    Player player;
    public GameObject playerObject;
    public GameObject mainCanvas;
    public GameObject gameEndCanvas;

    public GameObject gameOverTextPanel;

    public GameObject gameClearTextPanel;

    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;

    public GameObject itemPrefabs;

    public float timer;

    public Text countdownText;

    int minute;
    int seconds;

    public Color endColor;


    SendScore sendscore;

    void Start()
    {
        mainCanvas.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        gameEndCanvas.SetActive(false); //ゲームーオーバー用キャンバスをオフに
        gameOverTextPanel.SetActive(false);
        gameClearTextPanel.SetActive(false);
        player = playerObject.gameObject.GetComponent<Player>();
        user = GameObject.Find("UserDataObject").GetComponent<User>();
        nameText.text = user.GetUserName();
        endColor = Color.red;
        sendscore = GetComponent<SendScore>();
        PlayerPrefs.SetFloat("initTime", timer);
    }

    void Update()
    {
        CountdownTimer();
        ChangeTimerColor();

        if (player.IsDead())
        {
            EndGame();
            gameOverTextPanel.SetActive(true);
            Destroy(playerObject);
            DropPlayerTag();
            return;
        }

        if (timer <= 0 && !player.IsDead())
        {
            EndGame();
            gameClearTextPanel.SetActive(true);
        }

    }

    public void EndGame()
    {
        PlayerPrefs.SetFloat("time", timer);
        PlayerPrefs.SetInt("score", player.GetScore());
        PlayerPrefs.SetInt("tagcount", player.GetPlayerTagCount());
        PlayerPrefs.SetInt("tagscore", player.GetPlayerTagScore());
        Cursor.lockState = CursorLockMode.None;
        enabled = false;
        gameEndCanvas.SetActive(true);
        sendscore.SendScoreStart(user);
        Invoke("MoveResult", 3.0f);
    }


    //プレイヤータグドロップ
    void DropPlayerTag()
    {
        Transform itemDropTransform = player.transform;
        //プレイヤーのアイテムドロップ処理
        Drop(itemDropTransform);

    }

    void Drop(Transform currentTransform)
    {
        var parent = currentTransform;
        float rangeXZ = Random.Range(1.5f, 2f);
        float randX = Random.Range(-rangeXZ, rangeXZ);
        float randZ = Random.Range(-rangeXZ, rangeXZ);
        GameObject item = Instantiate(itemPrefabs, transform.position + new Vector3(0, 2f, 0),
            Quaternion.Euler(-90, 0, 0), parent);
        Rigidbody rd = item.GetComponent<Rigidbody>();
        rd.AddForce(new Vector3(randX, 7f, randZ), ForceMode.Impulse);

    }

    public void MoveResult()
    {
        SceneManager.LoadScene("Result");
    }

    public void CountdownTimer()
    {
        if (timer <= 0.0f)
        {
            return;
        }
        timer -= Time.deltaTime;
        minute = (int)timer / 60;
        seconds = (int)timer % 60;

        countdownText.text = string.Format("{0}:{1}", minute, seconds);
    }

    public void ChangeTimerColor()
    {
        if (timer <= 30)
        {
            countdownText.color = endColor;
        }
    }

}
