using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Text lbl_score;
    public Text lbl_name;
    public Image image_head;

    void Start()
    {
        //GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value);
    }

    public void init(string name, int score, string url)
    {
        SetName(name);
        SetScore(score);
        StartCoroutine(SetImage(url));
    }

    public void SetName(string name)
    {
        lbl_name.text = name;
    }

    public void SetScore(int score)
    {
        lbl_score.text = Convert.ToString(score) + "分";
    }

    public IEnumerator SetImage(string url)
    {
        UnityWebRequest wr = new UnityWebRequest(url);
        DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
        wr.downloadHandler = texDl;
        yield return wr.SendWebRequest();
        int width = (int)image_head.GetComponent<RectTransform>().rect.width;
        int high = (int)image_head.GetComponent<RectTransform>().rect.height;
        if (wr.result != UnityWebRequest.Result.ConnectionError)
        {
            Texture2D tex = new Texture2D(width, high);
            tex = texDl.texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            image_head.transform.GetComponent<Image>().sprite = sprite;
        }
    }
}
