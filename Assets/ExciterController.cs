using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExciterController : MonoBehaviour
{
    private Animator animator;
    private Text text;
    private Color initColor;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        text = gameObject.GetComponent<Text>();
        initColor = text.color;
    }

    public void ShowExciter(string newText)
    {
        Debug.Log("Excite " + newText);

        if (text != null)
            Debug.Log("textmesh OK");

        if (animator != null)
            Debug.Log("animator OK");

        text.text = newText;
        animator.SetTrigger("Excite");

        //Invoke("HideExciter", 2f);
    }

    public void HideExciter()
    {
        //text.color = initColor;
        //gameObject.SetActive(false);
    }
}
