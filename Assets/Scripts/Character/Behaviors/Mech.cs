using System;
using UnityEngine;

namespace Assets.Scripts.Character.Behaviors
{
    class Mech : Behavior
    {
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private Vector3 jumpForce;
        private bool jumpNow;
        private bool moveNow;

        protected override void FixedRun()
        {
            if (jumpNow)
            {
                transform.Translate(0, .3f, 0);
                rgbdy.velocity = new Vector3(rgbdy.velocity.x, 0);
                rgbdy.AddForce(jumpForce, ForceMode2D.Impulse);
                jumpNow = false;
            }
            if (moveNow)
            {
                rgbdy.velocity = new Vector3(anim.GetFloat("MoveSpeed") * moveSpeed, rgbdy.velocity.y);
                moveNow = false;
            }
        }

        protected override void Init()
        {
            jumpNow = false;
            moveNow = false;
        }

        protected override void Idle()
        {
        }

        protected override void Move()
        {
            moveNow = true;
        }

        protected override void Attack()
        {
            //turn on collider or something
        }

        protected override void Jump()
        {
            jumpNow = true;
        }
        protected override void TakeDamage()
        {
        }

        protected override void Control()
        {
            controller.SetAnimator(driver);
            driver.gameObject.GetComponent<Behavior>().SetController(controller);
            SetController(null);
            driver = null;
        }
    }
}
