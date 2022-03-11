using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRenderTexture : MonoBehaviour
{
    // Start is called before the first frame update

 
    public RenderTexture template;

    void Start()
    {
        var texI = new RenderTexture(template);
        var texO = new RenderTexture(template);
        Debug.Log("Portal Pair Children: " + this.transform.childCount);
        Debug.Log("Portal i Children: " + this.transform.GetChild(0).transform.childCount);
        Debug.Log("Portal o Children: " + this.transform.GetChild(1).transform.childCount);

        this.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = texO;
        this.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_EmissionMap", texO);

        this.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material.mainTexture = texO;
        this.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material.SetTexture("_EmissionMap", texO);
        this.transform.GetChild(0).transform.GetChild(2).GetComponent<Camera>().targetTexture = texI;

        this.transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = texI;
        this.transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_EmissionMap", texI);

        this.transform.GetChild(1).transform.GetChild(1).GetComponent<Renderer>().material.mainTexture = texI;
        this.transform.GetChild(1).transform.GetChild(1).GetComponent<Renderer>().material.SetTexture("_EmissionMap", texI);
        this.transform.GetChild(1).transform.GetChild(2).GetComponent<Camera>().targetTexture = texO;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
