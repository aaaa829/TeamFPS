using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    string name; //プレイヤー名
    int hp; //プレイヤーの体力
    const int MAXHP = 100; //プレイヤーの体力最大値
    int score; //スコア格納用
    int playerTagScore; //プレイヤータグスコア格納用
    int playerTagCount; //プレイヤータグ収集数カウント用
    [SerializeField] private float speed; //プレイヤーの動く速度
    float initSpeed; //初期速度の保存用

    [SerializeField] private float jumpPower; //プレイヤーのジャンプ力
    float initJumpPower; //初期ジャンプ力の保存用
    [SerializeField] private float sensivytivy; //水平の感度（マウスHorizontal感度）
    [SerializeField] private float speedRotation; //プレイヤーの回転速度
    [SerializeField] private GameObject gun; //銃のオブジェクト取得用
    [SerializeField] private Text bulletsText;


    Vector3 playerPosition; //プレイヤーの位置取得用
    Vector3 playerRotation = Vector3.zero; //プレイヤーの向き取得(初期位置を0，0，0に設定)
    Vector3 playerScale; //プレイヤーのサイズ取得用


    float positionX; //プレイヤーのX位置
    float positionZ; //プレイヤーのz位置
    float rotationX; //プレイヤーのx回転
    float scaleY; //プレイヤーのyサイズ
    float offsetZ; //プレイヤーと視点カメラの差

    Rigidbody rb; //RigidBody Component 取得用
    Shot shot; //銃の残弾処理用
    Bullet bulletAtk; //バレットの攻撃力取得用

    int healHp; //Hp回復用
    public GameObject playerCamera; //視点カメラの取得用

    Vector3 cameraPosition; //視点カメラの位置取得用
    Quaternion cameraRotation;

    bool isDead; //生死判定
    bool isJump; //ジャンプの判定
    bool isCrouch; //しゃがみ判定
    bool isForcus; //フォーカス判定

    bool cursorLock; //カーソル固定判定

    Animator animator; //アニメーター

    Player(string name, int score, int playerTagScore, int playerTagCount, float timer)
    {
        this.name = name;
        this.score = score;
        this.playerTagScore = playerTagScore;
        this.playerTagCount = playerTagCount;
    }

    //プレイヤー名取得用
    public String GetName()
    {
        return this.name;
    }

    //プレイヤー名代入用
    public void SetName(string name)
    {
        this.name = name;
    }

    //HP取得用
    public int GetHp()
    {
        return this.hp;
    }

    public int GetMAXHP()
    {
        return Player.MAXHP;
    }

    //スコア取得用
    public int GetScore()
    {
        return this.score;
    }

    public int GetPlayerTagCount()
    {
        return this.playerTagCount;
    }

    public int GetPlayerTagScore()
    {
        return this.playerTagScore;
    }
    void Start()
    {
        animator = GetComponent<Animator>(); //アニメーターコンポーネントの取得
        rb = GetComponent<Rigidbody>(); //リジッドボディコンポーネントの取得

        // GameObject userObject = GameObject.Find("UserDataObject");
        // SetName(userObject.GetComponent<User>().GetUserName()); //プレイヤー名の取得...Player1仮置き
        hp = MAXHP; //HP最大値を代入
        score = 0; //スコアを代入
        initSpeed = speed; //スピードの初期値を保存
        initJumpPower = jumpPower; //ジャンプ力の初期値を保存

        playerPosition = transform.localPosition; //プレイヤーの初期位置を保存
        playerScale = transform.localScale; //プレイヤーの初期サイズを保存
        scaleY = playerScale.y; //プレイヤーのY縦初期サイズを保存

        cameraPosition = playerCamera.transform.localPosition; //視点カメラの初期位置を保存
        offsetZ = transform.position.z - playerCamera.transform.position.z; //プレイヤーとカメラの差を保存

        isDead = false; //生死判定をFalseに
        isJump = false; //ジャンプ判定をFalseに
        isCrouch = false; //しゃがみ判定をFalseに
        isForcus = false; //フォーカス判定をFalseに

        shot = gun.GetComponent<Shot>();

        animator.SetFloat("MotionSpeed", 1.0f); //アニメーターのwalk基本値を設定
    }

    void Update()
    {
        positionX = Input.GetAxis("Horizontal"); //入力の水平値を取得
        positionZ = Input.GetAxis("Vertical"); //入力の垂直値を取得
        rotationX = playerCamera.transform.rotation.eulerAngles.y;
        playerRotation = new Vector3(0.0f, rotationX, 0.0f); //プレイヤーの横回転値を取得
        transform.rotation = Quaternion.Euler(playerRotation); //プレイヤーの横回転を実行

        //ジャンプ
        if (Input.GetKeyDown("space") && !isJump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            animator.SetBool("Jump", true);
        }

        //しゃがみ
        // if (Input.GetKeyDown(KeyCode.LeftControl))
        // {
        //     Crouching();
        // }

        //フォーカス
        // if (Input.GetMouseButtonDown(1))
        // {
        //     ForcusAim();
        // }

        //銃の残弾数処理
        bulletsText.text = shot.remainingbullets.ToString();

        //カーソルロックを実行
        UpdateCursorLock();

        //生死判定
        IsDead();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed * positionZ, ForceMode.VelocityChange); //プレイヤーの前後移動
        rb.AddForce(transform.right * speed * positionX, ForceMode.VelocityChange); //プレイヤーの横移動
        animator.SetFloat("Speed", speed); //移動アニメーションのスピードを取得

    }

    //GameOver判定
    public bool IsDead()
    {
        if (hp <= 0 || this.transform.position.y <= -5)
        {
            isDead = true;
        }

        return isDead;
    }

    //カーソルの非表示、中央固定
    public void UpdateCursorLock()
    {
        //ESCキーでカーソルロックを解除
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        //左クリックでカーソルロック
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //コライダー接触処理
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "OBJ")
        {
            isJump = false;
            animator.SetBool("Jump", false);
            animator.SetBool("Grounded", true);
        }

        //スコア獲得用処理
        if (other.gameObject.tag == "Score")
        {
            Item item = other.gameObject.GetComponent<Item>();
            score += item.score;
        }

        if (other.gameObject.tag == "EnemyScore")
        {
            EnemyItem enemyItem = other.gameObject.GetComponent<EnemyItem>();
            score += enemyItem.score;
        }

        //プレイヤータグ獲得用処理
        if (other.gameObject.tag == "PlayerTag")
        {
            Item item = other.gameObject.GetComponent<Item>();
            score += item.score;
            playerTagScore += item.score;
            playerTagCount++;
        }

        //リカバリーアイテム獲得処理
        if (other.gameObject.tag == "Heal")
        {
            Heal heal = other.gameObject.GetComponent<Heal>();
            int recovery = hp + heal.HealHP;
            if (recovery > 100)
            {
                hp = MAXHP;
            }
            else
            {
                hp += heal.HealHP;
            }
        }
    }

    //ダメージ処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            bulletAtk = other.gameObject.GetComponent<Bullet>();
            hp -= bulletAtk.attack;
        }
    }

    //しゃがみ
    private void Crouching()
    {
        if (!isCrouch)
        {
            playerScale.y = scaleY / 2.0f;
            transform.localScale = playerScale;
            speed = initSpeed * 0.7f;
            float cameraPositionY = cameraPosition.y / 1.5f;
            float cameraPositionZ = this.transform.position.z + 0.5f;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, cameraPositionY, cameraPositionZ);
            isCrouch = true;
        }
        else
        {
            playerScale.y = scaleY;
            transform.localScale = playerScale;
            speed = initSpeed;
            float cameraPositionY = cameraPosition.y;
            float cameraPositionZ = this.transform.position.z - offsetZ;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, cameraPositionY, cameraPositionZ);
            isCrouch = false;
            animator.SetFloat("Speed", speed);
        }

    }

    //フォーカス
    private void ForcusAim()
    {
        if (!isForcus)
        {
            jumpPower = initJumpPower * 0.5f;
            speed = initSpeed * 0.8f;
            sensivytivy *= 0.6f;
            float cameraPositionZ = transform.position.z + offsetZ + 5.0f;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, cameraPositionZ);
            isForcus = true;
        }
        else
        {
            jumpPower = initJumpPower;
            speed = initSpeed;
            sensivytivy *= 0.8f;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, transform.position.z + offsetZ);
            isForcus = false;
        }
    }

    //アニメーションエラー対応用
    private void OnFootstep()
    {

    }

    //アニメーションエラー対応用
    private void OnLand()
    {

    }

}
