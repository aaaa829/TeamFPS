using System.Collections;
using System;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Login : MonoBehaviour
{

    [SerializeField] private InputField _nameInput;
    [SerializeField] private InputField _passInput;
    [SerializeField] private GameObject _userObject;
    UserTotalData userdata;
    UserRecordData records;
    List<User> scores;
    User loginUser;
    User unionUser;
    bool isLoginCheck = false;
    bool isScoreCheck = false;
    bool isUnionCheck = false;

    void Update()
    {
        if (isLoginCheck && isScoreCheck)
        {
            UnionUser();
        }
        if (isLoginCheck && isScoreCheck && isUnionCheck)
        {
            SceneManager.LoadScene("Lobby");

        }
    }

    public void SendLoginUser()
    {
        StartCoroutine(Get_user_records());
        StartCoroutine(Login_User());
    }


    public IEnumerator Login_User()
    {
        string url = "http://localhost/fps/login_user.py";

        WWWForm form = new WWWForm();
        form.AddField("user_name", _nameInput.text);
        form.AddField("user_pass", _passInput.text);

        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + uwr.error);
                    break;
                default:
                    string responseText = uwr.downloadHandler.text;
                    userdata = JsonUtility.FromJson<UserTotalData>(responseText);
                    loginUser = _userObject.GetComponent<User>();
                    loginUser.SetUserName(userdata.result[0].user_name);
                    loginUser.SetTotalTime((float)(userdata.result[0].total_time_score / 100));
                    loginUser.SetPlayerTagCount(userdata.result[0].total_kill_cnt);

                    isLoginCheck = true;
                    break;
            }
        }

    }

    public IEnumerator Get_user_records()
    {
        string url = "http://localhost/fps/get_user_record.py";

        WWWForm form = new WWWForm();
        form.AddField("user_name", _nameInput.text);
        form.AddField("user_pass", _passInput.text);

        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + uwr.error);
                    break;
                default:
                    string responseText = uwr.downloadHandler.text;
                    records = JsonUtility.FromJson<UserRecordData>(responseText);
                    scores = new List<User>();
                    for (int i = 0; i <= 2; i++)
                    {
                        User scoreUser = new User();
                        scoreUser.SetUserName($"User:{i}");
                        scoreUser.SetScore(records.result[i].score);
                        scoreUser.SetSuvivalTime((float)(records.result[i].time_score / 100));
                        scoreUser.SetPlayerTagCount(records.result[i].kill_cnt);
                        scores.Add(scoreUser);
                        // Debug.Log(scores[0].GetScore());
                    }
                    foreach (var item in scores)
                    {
                        Debug.Log($"{item.GetUserName()}{item.GetScore()}");
                    }
                    isScoreCheck = true;
                    break;
            }
        }
    }

    public void UnionUser()
    {
        unionUser = _userObject.GetComponent<User>();
        unionUser.SetUserName(loginUser.GetUserName());
        unionUser.SetTotalScore(loginUser.GetTotalScore());
        unionUser.SetTotalTime(loginUser.GetTotalTime());
        unionUser.SetPlayerTagCount(loginUser.GetPlayerTagCount());
        unionUser.SetScores(scores);
        foreach (var item in unionUser.GetScores())
        {
            // Debug.Log(item.GetScore());
        }
        // Debug.Log($"union: {unionUser.GetScores()[2].GetScore()}");
        DontDestroyOnLoad(unionUser);
        isUnionCheck = true;
    }

    class UserTotalData
    {
        public Userdata[] result;
    }

    [Serializable]
    class Userdata
    {
        public string user_name; //ユーザー名（userName）
        public int total_score; //総スコア
        // public int[] scores;
        public int total_time_score; //総生存時間（totalTime）
        public int total_kill_cnt; //総キル数（playerTagCount）
    }

    class UserRecordData
    {
        public Userrecord[] result;
    }

    [Serializable]

    class Userrecord
    {
        public string user_name;
        public int score;
        public int kill_cnt;
        public int time_score;

    }
}