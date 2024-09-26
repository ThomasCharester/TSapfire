using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    BoxCollider damageCollider;
    // Start is called before the first frame update
    [SerializeField] float damage = 1f;
    void Start()
    {
        damageCollider = GetComponent<BoxCollider>();
        damageCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartAttack()
    {
        //print("Sweeep");
        damageCollider.enabled = true;
    }
    public void EndAttack()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Entity"))
            collision.GetComponent<Entity>().GetDamage(damage);
    }
}
