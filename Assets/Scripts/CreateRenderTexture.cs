using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRenderTexture : MonoBehaviour
{
    // Start is called before the first frame update

    

    void Start()
    {
       RenderTexture textureI = Instantiate(GameObject.Find("PortalTex - i").GetComponent<RenderTexture>());
       RenderTexture textureO = Instantiate(GameObject.Find("PortalTex - o").GetComponent<RenderTexture>());
       Material materialO = Instantiate(GameObject.Find("Portal - o").GetComponent<Material>());
       Material materialI = Instantiate(GameObject.Find("Portal - i").GetComponent<Material>());
        materialO.mainTexture = textureO;
        materialI.mainTexture = textureI;
        Renderer[] renderers = new Renderer[3];
        renderers= this.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Debug.Log(renderer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
