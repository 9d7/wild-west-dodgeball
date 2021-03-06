using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    private Animator anim;
    private CanvasGroup cg;

    public void StartDrawAnim()
    {
        cg.alpha = 1;
        anim.SetTrigger("DoDraw");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
