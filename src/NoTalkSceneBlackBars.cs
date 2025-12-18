using BepInEx;
using KKAPI;
using KKAPI.MainGame;
using UnityEngine;
using UnityEngine.UI;

[BepInPlugin(
    "com.mado.koikatsu.noblackbars",
    "Koikatsu No TalkScene Black Bars",
    "1.2")]
[BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]
public class TalkSceneNoBlackBars : BaseUnityPlugin
{
    private bool wasInTalkScene = false;

    void Update()
    {
        bool isInTalkScene = FindObjectOfType<TalkScene>() != null;

        // Entrou na toxin
        if (isInTalkScene && !wasInTalkScene)
        {
            Logger.LogInfo("TalkScene entered");
            DisableImageBackgrounds();
        }

        // Saiu da toxin
        if (!isInTalkScene && wasInTalkScene)
        {
            Logger.LogInfo("TalkScene exited");
            RestoreImageBackgrounds();
        }

        wasInTalkScene = isInTalkScene;
    }

    void DisableImageBackgrounds()
    {
        var images = FindObjectsOfType<Image>();

        foreach (var img in images)
        {
            if (img.name == "Image_BackGround")
            {
                img.enabled = false;
            }
        }

        Logger.LogInfo("Image_BackGround disabled (TalkScene).");
    }

    void RestoreImageBackgrounds()
    {
        var images = FindObjectsOfType<Image>();

        foreach (var img in images)
        {
            if (img.name == "Image_BackGround" && !img.enabled)
            {
                img.enabled = true;
            }
        }

        Logger.LogInfo("Image_BackGround restored after TalkScene exit.");
    }
}
