using UnityEngine;

namespace Assets.Scripts.Character.Behaviors
{
    public abstract class Behavior : MonoBehaviour
    {
        /// <summary> Boolean for storing info from the animator. </summary>
        private bool idle;
        /// <summary> Boolean for storing info from the animator. </summary>
        private bool move;
        /// <summary> Boolean for storing info from the animator. </summary>
        private bool attack;
        /// <summary> Boolean for storing info from the animator. </summary>
        private bool jump;
        /// <summary> Boolean for storing info from the animator. </summary>
        private bool control;
        /// <summary> Boolean for storing info from the animator. </summary>
        private bool allowInterrupt;
        /// <summary> Boolean for storing info from the animator. </summary>
        private bool takeDamage;
        /// <summary> Integer to indicate how many times a character has jumped.</summary>
        public int jumpNumber = 0;
        /// <summary> Integer to indicate how many times a character can jump.</summary>
        public int jumpMax = 0;

        /// <summary> Used by animator to set state bools. </summary>
        public void animIdle()
        {
            idle = true;
            move = false;
            attack = false;
            jumping = false;
            takeDamage = false;
            control = false;
        }

        /// <summary> Used by animator to set state bools. </summary>
        public void animMove()
        {
            idle = false;
            move = true;
            attack = false;
            jumping = false;
            takeDamage = false;
            control = false;
        }

        /// <summary> Used by animator to set state bools. </summary>
        public void animAttack()
        {
            idle = false;
            move = false;
            attack = true;
            jumping = false;
            takeDamage = false;
            control = false;
        }

        /// <summary> Used by animator to set state bools. </summary>
        public void animJump()
        {
            idle = false;
            move = false;
            attack = false;
            jump = true;
            takeDamage = false;
            control = false;
        }

        /// <summary> Used by animator to set state bools. </summary>
        public void animControl()
        {
            idle = false;
            move = false;
            attack = false;
            jump = false;
            takeDamage = false;
            control = true;
        }

        /// <summary> Used by animator to set state bools. </summary>
        public void animLand()
        {
            jumpNumber = 0;
            anim.SetBool("CanJump", true);
        }

        /// <summary> Used by animator to set state bools. </summary>
        public void animDone()
        {
            idle = false;
            move = false;
            attack = false;
            jumping = false;
            takeDamage = false;
            control = false;
        }

        /// <summary> Used by animator to set state bools. </summary>
        public void animTakeDamage()
        {
            idle = false;
            move = false;
            attack = false;
            jumping = false;
            takeDamage = true;
            control = false;
        }

        /// <summary> Reference to the Rigidbody for physics. </summary>
        [SerializeField]
        protected Rigidbody2D rgbdy;
        /// <summary> Reference to the Animator for setting/retrieving state. </summary>
        [SerializeField]
        protected Animator anim;
        /// <summary> Transform used for in air detection. </summary>
        [SerializeField]
        protected Transform foot;
        /// <summary> Reference to the ghosting handler. </summary>
        [SerializeField]
        private PlatformGhoster ghoster;
        /// <summary> Used to force InAir to be true while jumping. </summary>
        private bool jumping;
        /// <summary> Reference to the driver for mechs. </summary>
        internal Animator driver;
        /// <summary> Reference to the controller for switching. </summary>
        /// CHANGE TO PROPERTY
        [SerializeField]
        protected Controllers.Controller controller;
        private Vector3 size;

        void Start()
        {
            jumping = false;
            size = transform.localScale;
            Init();
        }

        void Update()
        {
            bool inAir;
            if (Mathf.Abs(rgbdy.velocity.y) < .001f)
                inAir = false;
            else
                inAir = true;

            // Force inAir to true until char is off ground
            if (inAir && jumping)
                jumping = false;
            if (!inAir && jumping)
                inAir = true;

            Move();
            if (!attack)
            {
                if (anim.GetBool("InputLeft"))
                    this.transform.localScale = new Vector3(-size.x, size.y, size.z);
                else if (anim.GetBool("InputRight"))
                    this.transform.localScale = new Vector3(size.x, size.y, size.z);
            }
            if (idle)
            {
                jumpNumber = 0;
                Idle();
            }
            else if (attack)
            {
                Attack();
                attack = false;
            }
            else if (jump)
            {
                Jump();
                jump = false;
                jumping = true;
                inAir = true;
                jumpNumber++;
                anim.SetBool("CanJump", jumpNumber < jumpMax);
            }
            else if(takeDamage)
            {
                anim.SetBool("TakeDamage", false);
                TakeDamage();
                takeDamage = false;
            }
            else if(control)
            {
                Control();
                control = false;
            }

            if (rgbdy.velocity.y > .01f)
                ghoster.raycastEnabled = true;
            else
                ghoster.raycastEnabled = false;

            anim.SetBool("InAir", inAir);
        }

        void FixedUpdate()
        {
            FixedRun();
        }

        internal void SetController(Controllers.Controller controller)
        {
            this.controller = controller;
            anim.SetBool("Controlled", controller != null);
            anim.SetBool("Control", false);
        }

        /// <summary> Called when the character is initing. </summary>
        protected abstract void Init();
        /// <summary> Called when the character is idle. </summary>
        protected abstract void Idle();
        /// <summary> Called when the character is moving. </summary>
        protected abstract void Move();
        /// <summary> Called when the character is attacking. </summary>
        protected abstract void Attack();
        /// <summary> Called when the character is jumping. </summary>
        protected abstract void Jump();
        /// <summary> Called when the character is hurt. </summary>
        protected abstract void TakeDamage();
        /// <summary> Called in FixedUpdate. </summary>
        protected abstract void FixedRun();
        /// <summary> Called in FixedUpdate. </summary>
        protected abstract void Control();
    }
}


