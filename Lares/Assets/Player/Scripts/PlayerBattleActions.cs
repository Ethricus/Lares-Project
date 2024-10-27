using UnityEngine;
using UnityEngine.InputSystem;

namespace Lares.BattleSystem
{
    enum PlayerAttacks
    { 
    
    }

    public class PlayerBattleActions : MonoBehaviour
    {
        PlayerInput _input;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _input = GetComponent<PlayerInput>();
            _input.currentActionMap.FindAction("MovePlayer");
        }

        
    }
}
