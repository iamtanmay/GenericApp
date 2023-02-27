using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Utilities
{
    /// <summary>
    /// Loads new scene with a Fade transition. 
    /// Must have a child with a Quad and FadeMaterial script to create FadeOut effect
    /// </summary>
    public class SceneTransition : MonoBehaviour
    {
        public string sceneToLoad;
        public Color fadeColor = Color.white;
        public float timeBeforeFading = 3f, fadeSpeed = 2f, countDown = 0f;
        public bool triggered = false, initialized = false;
        public FadeMaterial fadeOut;

        public void Activate()
        {
            if (!initialized)
            {
                gameObject.SetActive(true);
                this.enabled = true;
                triggered = true;
                initialized = true;
                countDown = timeBeforeFading;
            }
        }

        public void Deactivate()
        {
            this.enabled = false;
            triggered = false;
            initialized = false;
            gameObject.SetActive(false);
        }

        public void FixedUpdate()
        {
            if (triggered)
            {
                if (countDown <= 0f && !initialized)
                    Activate();

                if (countDown <= 0f)
                {
                    fadeOut.speed = fadeSpeed;
                    fadeOut.fadeColor = fadeColor;
                    fadeOut.startAlpha = 0f;
                    fadeOut.targetAlpha = 1f;

                    fadeOut.onfinish = new UnityEvent();
                    fadeOut.onfinish.AddListener(LoadScene);
                    fadeOut.Activate();
                    Deactivate();
                }

                countDown -= Time.deltaTime;
            }
        }

        public void LoadScene()
        {            
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}