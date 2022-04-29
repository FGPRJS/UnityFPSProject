using System.Collections;
using System.Collections.Generic;
using Contents.Controller.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class HPBarFollower : MonoBehaviour
{
    private Player player;
    private Vector2 moveValue;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
