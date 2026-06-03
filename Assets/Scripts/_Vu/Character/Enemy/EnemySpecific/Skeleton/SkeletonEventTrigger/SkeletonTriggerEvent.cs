using UnityEngine;

public class SkeletonTriggerEvent : MonoBehaviour
{
    private Skeleton skeleton;

    private void Awake()
    {
        
        skeleton = GetComponentInParent<Skeleton>();
    }

    
    public void TriggerDamageEvent()
    {
        if (skeleton != null)
        {
            skeleton.TriggerDamageEvent(); 
        }
    }
}
