using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemyMelee : Entity
{
    [SerializeField] float radarRadius = 2f;
    [SerializeField] float damage = 1f;
    protected override void Start()
    {
        base.Start();
    }
    void FixedUpdate()
    {
        Movement(TargetDirection(GetTarget()));
    }
    protected override void Movement(Vector2 direction)
    {
        if (!canMove) return;

        if (direction == Vector2.zero)
        {
            animator.SetFloat("VectorForward", -0.01f); // Костыль для обмана аниматора
            return;
        }

        Vector3 movement3D = new Vector3(direction.x * speed, 0, direction.y * speed);
        animator.SetFloat("VectorForward", movement3D.magnitude - 0.01f); // Костыль для обмана аниматора

        //animator.SetBool("isRunning", isRunning);

        //if (isRunning) movement3D *= runModifier;

        //print(movement3D.magnitude);

        characterController.Move(movement3D);

        if (rotationAffectected)
            characterController.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement3D), speed * rotatingSpeed * Time.deltaTime);
        else
            characterController.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement3D), rotatingSpeed * Time.deltaTime);

    }
    Vector2 TargetDirection(Entity target)
    {
        if (target == null) return Vector2.zero;

        Vector3 direction = target.transform.position - transform.position;

        return new Vector2(direction.x, direction.z);
    }
    Entity GetTarget()
    {
        Collider[] intruders = Physics.OverlapSphere(transform.position, radarRadius, 1 << 3);

        //print("Intruders " + intruders.Length);

        Collider target = intruders.FirstOrDefault(intruder => intruder.CompareTag("Player")) ?? null;

        if (target != null)
            return target.GetComponent<Entity>();

        return null;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(healthPoints <=0 ) return;
        print("Hit on " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Player"))
            collision.GetComponent<Player>().GetDamage(damage);
    }
}
