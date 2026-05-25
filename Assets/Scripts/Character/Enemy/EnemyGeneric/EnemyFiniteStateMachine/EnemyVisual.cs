using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    public Animator Anim { get; private set; }

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void UpdateMovementBlendTree(float moveX, float moveY)
    {
        if (Anim != null)
        {
            Anim.SetFloat("moveX", moveX);
            Anim.SetFloat("moveY", moveY);
        }
    }
    public void PlayAnimation(string animBoolName, bool value)
    {
        if (Anim != null)
        {
            Anim.SetBool(animBoolName, value);
        }
    }
}
