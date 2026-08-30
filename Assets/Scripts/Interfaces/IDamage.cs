
using UnityEngine.EventSystems;
public interface IDamage : IEventSystemHandler
{
    void ReciveDamage(float damage);
}
