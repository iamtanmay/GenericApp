using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSystem : MonoBehaviour
{
    //Outline shader
    public Outline outlineFX;

    //Highlight Shader
    public Transform highlightFX;

    //GameObjects containing particleFX
    public Transform objectFX;

    //Pulse fade speed
    public float pulseFade = 0.01f;
    //Single fade speed
    public float singleFade = 0.01f;
    //Limits of fading
    public float minFade = 0.01f, maxFade=1f;

    //List of playable Audio FX
    public AudioSource[] audioFX;

    public int mode = 0;
    public float currentFade;
    public Color currColor;
    public MeshRenderer render;

    public void Start()
    {
        currentFade = minFade;
    }

    /// <summary>
    /// 1) Outline 2) Highlight 3) Object
    /// </summary>
    /// <param name="iMode"></param>
    public void TriggerFX(int iMode, int index)
    {
        switch(iMode)
        {
            case 1: outlineFX.enabled = true; break;
            case 2: highlightFX.gameObject.SetActive(true); render = highlightFX.gameObject.GetComponent<MeshRenderer>(); break;
            case 3: objectFX.gameObject.SetActive(true); this.enabled = false; break;
            default: return;
        }
        mode = iMode;
    }

    public void PlayAudio(int index)
    {
        audioFX[index].Play();
    }

    public void DisableAllFX()
    {
        currentFade = 0f;
        currColor = new Color();

        if (mode == 1)
        {
            currColor = outlineFX.OutlineColor;
            currColor.a = 0f;
            outlineFX.OutlineColor = currColor;
            currColor = new Color();
            outlineFX.enabled = false;
        }

        if (mode == 2)
            render.enabled = false;

        if (mode == 3)
            objectFX.gameObject.SetActive(false);

        this.enabled = false;
    }

    //Enables Fading up or down
    public void ToggleFade()
    {
        if (pulseFade != 0)
            pulseFade = -1f * pulseFade;
        if (singleFade != 0)
        {
            singleFade = -1f * singleFade;

            if (mode == 1)
                outlineFX.enabled = true;
            if (mode == 2)
                render.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Pulse fade Mode
        if (pulseFade != 0)
        {
            currentFade = currentFade + pulseFade;

            if (currentFade > maxFade || currentFade < minFade)
                ToggleFade();

            if (mode == 1)
            {
                currColor = outlineFX.OutlineColor;
                currColor.a = currentFade;
                outlineFX.OutlineColor = currColor;
            }
            if (mode == 2)
            {
                currColor = render.material.GetColor("_OutlineColor");
                currColor.a = currentFade;
                render.material.SetColor("_OutlineColor", currColor);
            }
        }
        //Single fade Mode
        if (singleFade != 0)
        {
            currentFade = currentFade + singleFade;

            if (currentFade > maxFade || currentFade < minFade)
                DisableAllFX();

            if (mode == 1)
            {
                currColor = outlineFX.OutlineColor;
                currColor.a = currentFade;
                outlineFX.OutlineColor = currColor;
            }
            if (mode == 2)
            {
                currColor = render.material.GetColor("_OutlineColor");
                currColor.a = currentFade;
                render.material.SetColor("_OutlineColor", currColor);
            }
        }
    }
}
