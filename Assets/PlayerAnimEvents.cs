using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Palyer palyer;

    void Start()
    {
        palyer = GetComponentInParent<Palyer>();
    }

    /**
     * 攻击结束
     */
    private void AnimationTrigger()
    {
        palyer.AttackOver();
    }
}
