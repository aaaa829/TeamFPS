using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendScore : MonoBehaviour
{
    public void SendScoreStart(User user)
    {
        StartCoroutine(SendingScore(user));
    }
    public IEnumerator SendingScore(User user)
    {

        string url = "http://localhost/FPS/save_score.py";
        WWWForm form = new WWWForm();
        form.AddField("user_name", user.GetUserName());
        form.AddField("score", PlayerPrefs.GetInt("score"));
        form.AddField("kill_cnt", PlayerPrefs.GetInt("tagcount"));
        form.AddField("kill_score", PlayerPrefs.GetInt("tagscore"));
        form.AddField("time_score", (int)PlayerPrefs.GetFloat("time") * 100);
        form.AddField("item_score", PlayerPrefs.GetInt("score") - PlayerPrefs.GetInt("tagscore"));
        form.AddField("item_cnt", 0);

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
    }
}
