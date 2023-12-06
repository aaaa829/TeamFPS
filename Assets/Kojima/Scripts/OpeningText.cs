using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpeningText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpText;
    [SerializeField] private float textSpeed;
    string[] textList;
    public GameObject textPanel;
    public GameObject imagePanel;
    public GameObject buttonPanel;


    void Start()
    {
        textPanel.gameObject.SetActive(true);
        imagePanel.gameObject.SetActive(false);
        buttonPanel.gameObject.SetActive(false);
        tmpText = tmpText.gameObject.GetComponent<TextMeshProUGUI>();
        textList = new string[] { "0607PM/2 Studio", $"Iwatsuru", "Jinnouchi", "and Kojima", "Rock On Gifts" };
        StartCoroutine(ChangeText());
        StartCoroutine(SetImage());
        StartCoroutine(SetButton());
    }

    void Update()
    {

        if (!buttonPanel.gameObject.activeSelf)
            Debug.Log(buttonPanel.gameObject.activeSelf);
        {
            if (Input.GetMouseButtonDown(0))
            {
                buttonPanel.gameObject.SetActive(true);
                StopCoroutine(SetImage());
                textPanel.gameObject.SetActive(false);
                imagePanel.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator ChangeText()
    {
        for (int i = 0; i < textList.Length; i++)
        {
            tmpText.text = textList[i];
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator SetImage()
    {
        float waitTime = textList.Length * textSpeed;
        yield return new WaitForSeconds(waitTime);
        imagePanel.gameObject.SetActive(true);
    }

    IEnumerator SetButton()
    {
        float waitTime = textList.Length * textSpeed;
        yield return new WaitForSeconds(waitTime);
        buttonPanel.gameObject.SetActive(true);
    }
}
