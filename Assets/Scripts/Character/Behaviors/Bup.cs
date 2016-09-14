using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character.Behaviors
{
    class Bup : Behaviors.Behavior
    {
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private Vector3 jumpForce;
        [SerializeField]
        private GameObject attack;
        [SerializeField]
        private Transform barrel;
        private bool jumpNow;
        private bool moveNow;

        private List<Mech> mechs;

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.layer == LayerMask.NameToLayer("Mech"))
            {
                Mech m = col.GetComponent<Mech>();
                if (!mechs.Contains(m))
                    mechs.Add(m);
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Mech"))
            {
                Mech m = col.GetComponent<Mech>();
                if (mechs.Contains(m))
                    mechs.Remove(m);
            }
        }

        protected override void FixedRun()
        {
            if (jumpNow)
            {
                transform.Translate(0, .3f, 0);
                rgbdy.velocity = new Vector3(rgbdy.velocity.x, 0);
                rgbdy.AddForce(jumpForce, ForceMode2D.Impulse);
                jumpNow = false;
            }
            if(moveNow)
            {
                rgbdy.velocity = new Vector3(anim.GetFloat("MoveSpeed") * moveSpeed, rgbdy.velocity.y);
                moveNow = false;
            }
        }

        protected override void Init()
        {
            jumpNow = false;
            moveNow = false;
            mechs = new List<Mech>();
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
            Attacks.Attack a = Instantiate(attack).GetComponent<Attacks.Attack>();
            a.gameObject.transform.position = barrel.position;
            a.direction = Util.Enum.Direction.None;
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
            if (mechs.Count > 0)
            {
                mechs.Sort((x, y) => (Vector2.Distance(this.transform.position, x.transform.position).CompareTo(Vector2.Distance(this.transform.position, y.transform.position))));
                Mech m = mechs[0];
                controller.SetAnimator(m.GetComponent<Animator>());
                m.driver = anim;
                m.SetController(controller);
                SetController(null);
                mechs.Clear();
            }       
        }
    }
}
