using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceResetAnimator : MonoBehaviour
{
    public void ResetAnimators()
    {
        var animators = GetComponentsInChildren<Animator>(true);
        foreach (var animator in animators)
        {
            try
            {
                for (int i = 0; i < animator.parameterCount; i++)
                {
                    var parameter = animator.GetParameter(i);
                    Debug.Log("TRIGG " + parameter.name + " on " + animator.gameObject.name);
                    if (parameter.type == AnimatorControllerParameterType.Trigger)
                    {
                        Debug.Log("TRIGG " + parameter.name + " on " + animator.gameObject.name);
                        animator.ResetTrigger(parameter.name);
                        if (parameter.name == "ResetSeq")
                        {
                            Debug.Log("RESET ! on " + animator.gameObject.name);
                            animator.SetTrigger("ResetSeq");
                        }
                    }
                }
            }
            catch
            {
                Debug.Log("trycatch animator reset" + animator.gameObject.name);
            }
        }
    }
}
