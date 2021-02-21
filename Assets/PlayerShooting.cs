using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<ProjectileType> projectileTypes;

    // the player's currently-held bullets
    private Queue<string> projectiles;
    
    private Vector2 aimDirection = Vector2.one;
    // Start is called before the first frame update
    void OnTryShoot(InputValue input)
    {
        if (projectiles.Count == 0)
        {
            // for sound/ui
            SendMessage("ShootFail");
        }
        else
        {
            string projectile = projectiles.Dequeue();
            // for sound
            SendMessage("ShootSuccess", projectile);

            // this is O(n), but there's like 6 bullet types so
            // i think it's fine--Dictionaries are a pain to
            // serialize
            foreach (ProjectileType projectileType in projectileTypes)
            {
                if (projectileType.name == projectile)
                {
                    // TODO move this forward a bit
                    Vector3 projectilePos = gameObject.transform.position;
                    GameObject newProj = Instantiate(
                        projectileType.template, projectilePos, Quaternion.identity
                    );
                    //newProj.GetComponent<Rigidbody2D>().position = projectilePos;
                    Projectile script = newProj.GetComponent<Projectile>();
                    newProj.layer = LayerMask.NameToLayer("ProjectileFromAlly");
                    script.direction = aimDirection;
                }
            }
            
            
            
        }
    }

    void OnAim(InputValue input)
    {
        Vector2 mouseScreenPos = input.Get<Vector2>();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector2 direction = mousePos - (Vector2) gameObject.transform.position;
        direction = direction.normalized;

        if (direction != Vector2.zero)
        {
            aimDirection = direction;
        }

    }
    
    void Start()
    {
        projectiles = new Queue<string>();
        projectiles.Enqueue("test");
    }

}
