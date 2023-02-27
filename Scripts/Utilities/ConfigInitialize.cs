using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigInitialize : MonoBehaviour
{
    public AppSettings.Config config;
    public Camera maincamera;
    public Material boardMat;
    public Material fontBGMat;
    public Material guideMat;

    public void Initialize()
    {
        boardMat.color = new Color(config.boardcolor[0] / 255f, config.boardcolor[1] / 255f, config.boardcolor[2] / 255f);

        maincamera.backgroundColor = new Color(config.backgroundcolor[0]/255f, config.backgroundcolor[1] / 255f, config.backgroundcolor[2] / 255f);

        float fontBGAlpha = fontBGMat.color.a;
        fontBGMat.color = new Color(config.fontbgcolor[0] / 255f, config.fontbgcolor[1] / 255f, config.fontbgcolor[2] / 255f, fontBGAlpha);

        guideMat.color = new Color(config.guidecolor[0] / 255f, config.guidecolor[1] / 255f, config.guidecolor[2] / 255f);
    }
}
