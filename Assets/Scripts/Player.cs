using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{

    public InputActionAsset gameInput;

    InputAction move;
    InputAction run;

    [SerializeField] Weapon weapon;

    bool inBattle = false;
    protected override void Start()
    {
        base.Start();

        gameInput.FindActionMap("Game").Enable();
        move = gameInput.FindActionMap("Game").FindAction("Move");
        run = gameInput.FindActionMap("Game").FindAction("Run");

        gameInput.FindActionMap("Game").FindAction("Attack").performed += Attack;

        AcquireWeapon(weapon);

        //Debug
        gameInput.FindActionMap("Debug").Enable();
        gameInput.FindActionMap("Debug").FindAction("ToggleBattleMode").performed += ToggleBattleMode;
        
    }

    // !!!Забывает предыдущее оружие!!!
    private void AcquireWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        
        Movement(move.ReadValue<Vector2>());
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

        bool isRunning = run.ReadValue<float>() > 0;
        animator.SetBool("isRunning", isRunning);

        if (isRunning) movement3D *= runModifier;

        //print(movement3D.magnitude);

        characterController.Move(movement3D);

        if (rotationAffectected)
            characterController.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement3D), speed * rotatingSpeed * Time.deltaTime);
        else
            characterController.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement3D), rotatingSpeed * Time.deltaTime);

    }

    void Attack(InputAction.CallbackContext context)
    {
        animator.SetBool("isAttacking", inBattle);
    }
    void StartAttack()
    {
        //print("Attack started");
        if (weapon == null)
        {
            Debug.LogError("Null weapon!!!");
            return;
        }
        weapon.StartAttack();
    }
    void EndAttack()
    {
        //print("Attack end");
        if (weapon == null)
        {
            Debug.LogError("Null weapon!!!");
            return;
        }
        weapon.EndAttack(); 
        animator.SetBool("isAttacking", false);
    }
    
    // Debug
    void ToggleBattleMode(InputAction.CallbackContext context)
    {
        SetBattleMode(!inBattle);
    }

    public void SetBattleMode(bool state)
    {
        inBattle = state;

        animator.SetBool("inBattle", inBattle);

        print("Battle mode is " + inBattle.ToString());
    }

    public override void GetDamage(float damage, GameObject attacker = null) // Костыль, т.к. сломана анимация получения урона и событие неуязвимости срабатывает дважды
    {
        base.GetDamage(damage, attacker);

        if (healthPoints > 0) 
            DamageCooldownStart();
        
    }
}

