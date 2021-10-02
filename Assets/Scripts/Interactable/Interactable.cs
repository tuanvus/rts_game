using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Interactables
{
    public class Interactable : MonoBehaviour
    {
        public bool isInteracting = false;
        public GameObject highlight = null;
        public virtual void Awake()
        {
            highlight.SetActive(false);
        }
        public virtual void OnInteractEnter()
        {
            ShowHighlight();
            isInteracting = true;
        }
        public virtual void OnInteractExit()
        {

            isInteracting = false;
        }
        public virtual void ShowHighlight()
        {
            HideHighlight();
            highlight.SetActive(true);
        }
        public virtual void HideHighlight()
        {
            highlight.SetActive(false);
        }

    }
}