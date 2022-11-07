using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ListView
{
    internal class PlayingPlayerCell : MonoBehaviour
    {
        public Text lbl_score;
        public Text lbl_content;
        public Image image_head;

        void Start()
        {
            //GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value);
        }

        public void init(string name, long score, string url)
        {
            //SetContent(name);
            SetScore(score);
            StartCoroutine(SetImage(url));
            
        }

        public void SetContent(string content)
        {
            lbl_content.gameObject.SetActive(true);
            lbl_content.text = content;
        }

        public void SetScore(long score)
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
}
