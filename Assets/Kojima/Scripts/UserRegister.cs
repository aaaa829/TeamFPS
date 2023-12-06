using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserRegister : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private InputField passInput;

    [SerializeField] private GameObject userObject;

    string name;
    string pass;
    string date;
    List<User> scores = new List<User>();

    public void SendEntryUser()
    {
        StartCoroutine(Entry_User());
    }
    public void Inputdata()
    {
        name = nameInput.text;
        pass = passInput.text;
        date = System.DateTime.Now.ToString();
    }

    public IEnumerator Entry_User()
    {
        string url = "http://localhost/fps/regist_user.py";

        WWWForm form = new WWWForm();
        form.AddField("user_name", name);
        form.AddField("user_pass", pass);
        form.AddField("created_date", date);

        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + uwr.error);
                    break;
            }
        }

        SetUser();
    }

    void SetUser()
    {
        for (int i = 0; i <= 2; i++)
        {
            User userdata = new User();
            userdata.SetScore(00000);
            userdata.SetTotalTime(0.0f);
            userdata.SetPlayerTagCount(0);
            scores.Add(userdata);
        }

        User loginUser = userObject.GetComponent<User>();
        loginUser.SetUserName(name);
        loginUser.SetScores(scores);
        loginUser.SetTotalTime(0.0f);
        loginUser.SetPlayerTagCount(0);

        DontDestroyOnLoad(loginUser);
        SceneManager.LoadScene("Lobby");
    }
}
