using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUserName : MonoBehaviour
{
    User user;

    [SerializeField] private Text userNameText;

    void Start()
    {
        user = GameObject.Find("UserDataObject").GetComponent<User>();
        userNameText.text = user.GetUserName();
    }
}
