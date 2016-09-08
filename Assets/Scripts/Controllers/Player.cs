using UnityEngine;
using Assets.Scripts.Util;

namespace Assets.Scripts.Controllers
{
    class Player : Controllers.Controller
    {
        void Start()
        {

        }

        void Update()
        {
            // Temp, remove later
            if (Input.GetKeyUp(KeyCode.Escape))
                Application.Quit();

            Attack(CustomInput.BoolFreshPress(CustomInput.UserInput.Attack));
            Up(CustomInput.BoolHeld(CustomInput.UserInput.Up));//Checks if up is being held.
            Down(CustomInput.BoolHeld(CustomInput.UserInput.Down));//Checks if down is being held.
            Jump(CustomInput.BoolFreshPress(CustomInput.UserInput.Jump));
            anim.SetBool("Control", CustomInput.BoolFreshPress(CustomInput.UserInput.Control));

            if (CustomInput.BoolHeld(CustomInput.UserInput.Left))
            {
                Left(true);
                Right(false);
                Move(true);
                MoveSpeed(CustomInput.Raw(CustomInput.UserInput.Left));
            }
            else if (CustomInput.BoolHeld(CustomInput.UserInput.Right))
            {
                Left(false);
                Right(true);
                Move(true);
                MoveSpeed(CustomInput.Raw(CustomInput.UserInput.Right));
            }
            else
            {
                Left(false);
                Right(false);
                Move(false);
                MoveSpeed(0f);
            }
        }
    }
}