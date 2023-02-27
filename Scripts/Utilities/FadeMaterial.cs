using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class FadeMaterial : MonoBehaviour
    {
        public Color fadeColor;
        public float startAlpha, targetAlpha, currentAlpha, tolerance = 0.01f;
        public float speed = 2f;
        public Material mat;
        public bool triggered, initialized;
        public bool disableOnDeactivate = false;
        public UnityEvent onfinish;

        public void Activate()
        {
            gameObject.SetActive(true);
            this.enabled = true;
            triggered = true;
            initialized = true;
            currentAlpha = startAlpha;
            Color tcolor = fadeColor;
            tcolor.a = startAlpha;
            mat.color = tcolor;
        }

        public void Deactivate()
        {
            triggered = false;
            initialized = false;
            this.enabled = false;
            onfinish.Invoke();

            if (disableOnDeactivate)
                gameObject.SetActive(false);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (triggered)
            {
                if (!initialized)
                    Activate();

                currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, speed * Time.deltaTime);
                Color tcolor = mat.color;
                tcolor.a = currentAlpha;
                mat.color = tcolor;

                float difference = Mathf.Abs(targetAlpha - currentAlpha);

                if (difference < tolerance)
                    Deactivate();
            }
        }
    }
}
