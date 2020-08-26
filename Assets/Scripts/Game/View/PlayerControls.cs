using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    protected Joystick joystickMove;

    Vector3 player_Velocity;

    [Tooltip("A number betwenn 0 and 1. Move distance to activate")]
    [SerializeField]
    private float JoyStickMinSensitivity;

    private void OnEnable()
    {
        joystickMove.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        joystickMove.gameObject.SetActive(false);
    }

    public Vector3 InputPlayerMoveSpeed(Transform transform)
    {
        player_Velocity = new Vector3(0, 0, 0);

        player_Velocity = new Vector3(joystickMove.Horizontal, joystickMove.Vertical, 0);
        return player_Velocity;
    }
}
