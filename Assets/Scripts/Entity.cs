using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement"),Min(0f),SerializeField] protected float speed = 0.1f;
    [SerializeField,Min(0f)] protected float rotatingSpeed = 20f;
    [Tooltip("Is rotation affected by speed"), SerializeField] protected bool rotationAffectected = false;
    [SerializeField, Min(1.0f)] protected float runModifier = 1.5f;
    protected CharacterController characterController;
    protected Animator animator;
    [Header("Health"), SerializeField, Min(1f)] protected float healthPoints = 3f;
    //[SerializeField, Min(0f)] float damageCooldown = 0.5f; На будущее, если понадобится хард кодный кулдаун
    [SerializeField]protected bool canBeDamaged = true;

    [SerializeField] protected bool canMove = true;

    protected virtual void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void Movement(Vector2 direction)
    {
        if (!canMove) return;
    }
    public virtual void GetDamage(float damage, GameObject attacker = null)
    {
        if(!canBeDamaged) return;

        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            animator.SetBool("onDeath", true);
            canMove = false;
        }
        else
        {
            animator.SetBool("onDamage", true);
            //if (attacker != null) ;
        }
    }
    protected void DamageCooldownStart()
    {
        canBeDamaged = false;
    }
    protected void DamageCooldownEnd()
    {
        canBeDamaged = true;
        animator.SetBool("onDamage", false);
    }
    protected void DIE()
    {
        Destroy(this);
    }
    public float GetHealth()
    {
        return healthPoints;
    }
}
