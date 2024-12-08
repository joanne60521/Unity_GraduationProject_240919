using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGameControl : MonoBehaviour
{
    public InputActionReference leftSelectValueReference;
    public float leftSelectValue;
    public bool startGame = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftSelectValue = leftSelectValueReference.action.ReadValue<float>();
        
        // Start game
        if (leftSelectValue > 0.9)
        {
            startGame = true;

        }
    }
}
