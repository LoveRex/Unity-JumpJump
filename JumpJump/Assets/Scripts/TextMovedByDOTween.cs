using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

public class TextMovedByDOTween : MonoBehaviour
{

    private Text text;

    // Use this for initialization

    void Start()
    {

        text = this.transform.GetComponent<Text>();

    }

    public void SetText(string content)
    {
        text = this.transform.GetComponent<Text>();
        text.text = content;
    }
    // Update is called once per frame

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TextMoved(text);

        //}

    }

    public void TextMoved()
    {
        var graphic = this.transform.GetComponent<Text>(); 
        //获得Text的rectTransform，和颜色，并设置颜色微透明

        RectTransform rect = graphic.rectTransform;

        Color color = graphic.color;

        graphic.color = new Color(color.r, color.g, color.b, 0);

        //设置一个DOTween队列

        Sequence textMoveSequence = DOTween.Sequence();

        //设置Text移动和透明度的变化值

        Tweener textMove01 = rect.DOMoveY(rect.position.y + 50, 0.5f);

        Tweener textMove02 = rect.DOMoveY(rect.position.y + 100, 0.5f);
        Tweener textMove03 = rect.DOMoveY(rect.position.y, 0.5f);


        Tweener textColor01 = graphic.DOColor(new Color(color.r, color.g, color.b, 1), 0.5f);

        Tweener textColor02 = graphic.DOColor(new Color(color.r, color.g, color.b, 0), 0.5f);


        //Append 追加一个队列，Join 添加一个队列

        //中间间隔一秒

        //Append 再追加一个队列，再Join 添加一个队列

        textMoveSequence.Append(textMove01);

        textMoveSequence.Join(textColor01);

        textMoveSequence.AppendInterval(0.6f);

        textMoveSequence.Append(textMove02);

        textMoveSequence.Join(textColor02);
        textMoveSequence.Append(textMove03);

        textMoveSequence.Join(textColor02);

        textMoveSequence.OnComplete(() => {
            Destroy(this.gameObject);
        });

    }

}
