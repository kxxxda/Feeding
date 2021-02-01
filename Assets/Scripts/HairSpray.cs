using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairSpray : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Invoke("Disable", 0.25f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    public void StartSpray()
    {
        anim.SetTrigger("OnSpray");
    }
}
