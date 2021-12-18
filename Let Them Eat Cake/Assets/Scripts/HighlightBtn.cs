using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightBtn : MonoBehaviour
{
    public static HighlightBtn instance;

    [Header ("Left Leg")]
    public Sprite left_up;
    public Sprite left_up_highlight;
    public Sprite left_down;
    public Sprite left_down_highlight;
    public Sprite left_left;
    public Sprite left_left_highlight;
    public Sprite left_right;
    public Sprite left_right_highlight;
    public Sprite left_jump;
    public Sprite left_jump_highlight;

    public GameObject btn_left_up;
    public GameObject btn_left_down;
    public GameObject btn_left_left;
    public GameObject btn_left_right;
    public GameObject btn_left_jump;

    [Header ("Right Leg")]
    public Sprite right_up;
    public Sprite right_up_highlight;
    public Sprite right_down;
    public Sprite right_down_highlight;
    public Sprite right_left;
    public Sprite right_left_highlight;
    public Sprite right_right;
    public Sprite right_right_highlight;
    public Sprite right_jump;
    public Sprite right_jump_highlight;

    public GameObject btn_right_up;
    public GameObject btn_right_down;
    public GameObject btn_right_left;
    public GameObject btn_right_right;
    public GameObject btn_right_jump;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftLegMove(int index)
    {
        Sprite button = null;
        Sprite highlightBtn = null;
        GameObject targetBtn = null;

        switch (index)
        {
            case 0:
                button = left_up;
                highlightBtn = left_up_highlight;
                targetBtn = btn_left_up;
                break;
            case 1:
                button = left_down;
                highlightBtn = left_down_highlight;
                targetBtn = btn_left_down;
                break;
            case 2:
                button = left_left;
                highlightBtn = left_left_highlight;
                targetBtn = btn_left_left;
                break;
            case 3:
                button = left_right;
                highlightBtn = left_right_highlight;
                targetBtn = btn_left_right;
                break;
            case 4:
                button = left_jump;
                highlightBtn = left_jump_highlight;
                targetBtn = btn_left_jump;
                break;
        }

        if (button != null && highlightBtn != null && targetBtn != null)
        {
            StartCoroutine(HighlightBtnCoroutine(targetBtn, button, highlightBtn));
        }
    }

    public void RightLegMove(int index)
    {
        Sprite button = null;
        Sprite highlightBtn = null;
        GameObject targetBtn = null;

        switch (index)
        {
            case 0:
                button = right_up;
                highlightBtn = right_up_highlight;
                targetBtn = btn_right_up;
                break;
            case 1:
                button = right_down;
                highlightBtn = right_down_highlight;
                targetBtn = btn_right_down;
                break;
            case 2:
                button = right_left;
                highlightBtn = right_left_highlight;
                targetBtn = btn_right_left;
                break;
            case 3:
                button = right_right;
                highlightBtn = right_right_highlight;
                targetBtn = btn_right_right;
                break;
            case 4:
                button = right_jump;
                highlightBtn = right_jump_highlight;
                targetBtn = btn_right_jump;
                break;
        }

        if (button != null && highlightBtn != null && targetBtn != null)
        {
            StartCoroutine(HighlightBtnCoroutine(targetBtn, button, highlightBtn));
        }
    }

    IEnumerator HighlightBtnCoroutine(GameObject targetBtn, Sprite btn, Sprite highlightBtn)
    {
        targetBtn.GetComponent<Image>().sprite = highlightBtn;

        yield return new WaitForSeconds(0.2f);

        targetBtn.GetComponent<Image>().sprite = btn;
    }
}
