using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBoolHelper : MonoBehaviour
{
    public Animator animator;
    public void SetBoolTrue(string boolname)
    {
        animator.SetBool(boolname, true);
    }

    public void SetBoolFalse(string boolname)
    {
        animator.SetBool(boolname, false);
    }

    // Start is called before the first frame update
    void Start()
    {
      if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
}
