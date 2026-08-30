using UnityEngine;

public class AttacksVFXcontroller : MonoBehaviour, IAttacks
{

    [SerializeField]
    ParticleSystem meleeVFX, rangueVFX, areaVFX;

    public void SetMeleeAttack()
    {
        if(meleeVFX.isPlaying)
            meleeVFX.Stop();
        meleeVFX.Play();
    }
   

    public void SetRangettack()
    {
        if (rangueVFX.isPlaying)
            rangueVFX.Stop();
        rangueVFX.Play();
    }

    public void SetAreaAttack()
    {
        if (areaVFX.isPlaying)
            areaVFX.Stop();
        areaVFX.Play();
    }
}
