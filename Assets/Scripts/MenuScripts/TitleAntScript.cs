using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TitleAntScript : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Animator animator;
    
    void Update()
    {
        if (eventSystem.currentSelectedGameObject == this.gameObject)
        {
            animator.SetBool("isSelected", true);
        }

        if (eventSystem.currentSelectedGameObject.name == "Quit")
        {
            animator.SetBool("quitSelected", true);
        }

        if (eventSystem.currentSelectedGameObject != this.gameObject)
        {
            animator.SetBool("isSelected", false);
        }

        if (eventSystem.currentSelectedGameObject.name != "Quit")
        {
            animator.SetBool("quitSelected", false);
        }
    }
}
