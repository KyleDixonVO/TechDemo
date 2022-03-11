using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowControls : MonoBehaviour
{
    private TMP_Text ControlPrompt;
    private TMP_Text ControlMenu;
    private bool togglePrompt;
    private bool hideTooltip;
    // Start is called before the first frame update
    void Start()
    {
        hideTooltip = false;
        togglePrompt = false;
        ControlPrompt = GameObject.Find("ControlPrompt").GetComponent<TMP_Text>();
        ControlMenu = GameObject.Find("Controls").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hideTooltip = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            togglePrompt = !togglePrompt;
        }

        if (togglePrompt == true)
        {
            ControlMenu.enabled = true;
        }
        else
        {
            ControlMenu.enabled = false;
        }


        if (hideTooltip == false)
        {
            ControlPrompt.enabled = true;
        }
        else
        {
            ControlPrompt.enabled = false;
        }
    }
}
