using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class LoadImage : MonoBehaviour
    {
        private string url = "";
        public string Url { get { return url; } }
        public string SetUrl { set { url = value; } }

        public void setMaterial(string url)
        {
            StartCoroutine(DownMaterial(url));
        }

        IEnumerator DownSprite(string url)
        {
            UnityWebRequest wr = new UnityWebRequest(url);
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            wr.downloadHandler = texDl;
            yield return wr.SendWebRequest();
            int width = 300;
            int high = 300;
            if (wr.result != UnityWebRequest.Result.ConnectionError)
            {
                Texture2D tex = new Texture2D(width, high);
                tex = texDl.texture;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                transform.GetComponent<Image>().sprite = sprite;
            }
        }

        IEnumerator DownMaterial(string url)
        {
            UnityWebRequest wr = new UnityWebRequest(url);
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            wr.downloadHandler = texDl;
            yield return wr.SendWebRequest();
            if (wr.result != UnityWebRequest.Result.ConnectionError)
            {
                Texture2D tex = null;
                tex = texDl.texture;
                this.transform.GetComponent<Renderer>().material.mainTexture = tex;
            }
        }
    }
}
