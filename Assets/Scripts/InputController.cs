using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

    public class InputController : Controller
    {

        public enum ControlType { WASD, IJKL };
        public ControlType controls;

        [Serializable]
        private class PlayerControls
        {
            public List<keyCode> keyCodes;
        }

        private class keyCode
        {
            public ControlKeys controlkeys;

            public KeyCode key;
        }

        [SerializeField]
        List<PlayerControls> playerControls;




        // Use this for initialization
        void Start()
        {
            //Add myself to the list of players.
            GameManager.Instance.players.Add(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (controls == ControlType.WASD)
            {
                if (Input.GetKey(KeyCode.W))
                {  //Move forward
                    Pawn.mover.MoveForward();
                }
                if (Input.GetKey(KeyCode.S))
                {
                    //Move backward
                    Pawn.mover.Back();
                }
                if (Input.GetKey(KeyCode.A))
                //Move Left
                {
                    Pawn.mover.Left();
                }

                if (Input.GetKey(KeyCode.D))
                //Move Right
                {
                    Pawn.mover.Right();
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Pawn.shooter.Shoot();
                }
            }
            else if (controls == ControlType.IJKL)
            {
                if (Input.GetKey(KeyCode.I))
                {
                    Pawn.mover.MoveForward();
                }
                if (Input.GetKey(KeyCode.K))
                {
                    Pawn.mover.Back();
                }
                if (Input.GetKey(KeyCode.J))
                {
                    Pawn.mover.Left();
                }
                if (Input.GetKey(KeyCode.L))
                {
                    Pawn.mover.Right();
                }
                if (Input.GetKey(KeyCode.RightShift))
                {
                    Pawn.shooter.Shoot();
                }
            }
        }
        public void OnDestroy()
        {
            //Remove me from the list.
            //GameManager.Instance.players.Remove(this);
        }
    }




