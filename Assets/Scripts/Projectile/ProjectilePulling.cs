using UnityEngine;
using System.Collections.Generic;

public class ProjectilePulling : MonoBehaviour
{
    [SerializeField]
    List<ProjectileBehaviour> projectileList = new List<ProjectileBehaviour>();

    public enum ProjectileType
    {
        Player,
        Enemy        
    }

    void Start()
    {
        projectileList = new List<ProjectileBehaviour>(GetComponentsInChildren<ProjectileBehaviour>(true));
    }

    public void launchProjectile(Vector3 target, Transform origin, ProjectileType projectileType)
    {
        foreach (var projectile in projectileList)
        {
            if (!projectile.gameObject.activeInHierarchy && projectile.projectileType == projectileType)
            {
                projectile.Setprojectile(target, origin);
                print("Projectile lanzado !!!");
                break;
            }
        }
    }

}
