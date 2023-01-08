using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    Animator animator;
    public int sceneIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
