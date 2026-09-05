
using UnityEngine.EventSystems;
public interface IDamage : IEventSystemHandler
{
    void ReceiveDamage(float damage);
}
