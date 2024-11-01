using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [Category("Entities"), SerializeField] List<Entity> enemies = new List<Entity>(); // Можно заспавнить, а можно и занести со сцены

    [SerializeField] Player player;

    bool battleMode = false;

    [Category("Level controls")] public InputActionAsset gameInput;

    public delegate void GameOver();
    public event GameOver gameOverEvent;
    void Start()
    {
        gameInput.FindActionMap("GameUI").FindAction("Restart").performed += RestartLevel;

        foreach (Entity e in enemies)
            e.onDeathEvent += KillEntity;

        player.onDeathEvent += PlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void KillEntity(Entity entity)
    {
        entity.onDeathEvent -= KillEntity;

        if(enemies.Contains(entity)) enemies.Remove(entity);

        if (enemies.Count > 0 && !battleMode)
        {
            MusicPlayer.GetInstance().ChangeMusic(MusicPlayer.musicID.OverworldBattle);

            battleMode = true;

            player.SetBattleMode(battleMode);
        }
        else if (enemies.Count == 0 && battleMode)
        {
            MusicPlayer.GetInstance().ChangeMusic(MusicPlayer.musicID.OverworldPeaceful);

            battleMode = false;

            player.SetBattleMode(battleMode);
        }
    }
    void PlayerDeath(Entity entity)
    {
        MusicPlayer.GetInstance().ChangeMusic(MusicPlayer.musicID.GameOver);

        gameOverEvent.Invoke();
        gameInput.FindActionMap("GameUI").Enable();

    }
    void RestartLevel(InputAction.CallbackContext context)
    {
        gameInput.FindActionMap("GameUI").Disable();
        SceneManager.LoadScene(0);
    }
}
