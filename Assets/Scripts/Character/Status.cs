using UnityEngine;

namespace Assets.Scripts.Character
{
    class Status : MonoBehaviour
    {
        public Animator anim;
        public SpriteRenderer sprite;
        public int totalHealth;
        public int currentHealth;
        /// <summary> How long the enemy is invunerable after being hit. </summary>
        [SerializeField]
        protected float invulerabilityTime;

        /// <summary> When greater than 0, this enemy is invunerable and doesn't take damage. </summary>
        protected float invulerability;
        /// <summary> When true this enemy's sprite is being rendered. </summary>
        protected bool render;
        /// <summary> True if the enemy has been hit by something damaging. </summary>
        protected bool hit;

        protected int damage;

        /// <summary> The amount of damage the enemy deals to the player when colliding with it. </summary>
        [SerializeField]
        protected int collideDamage = 1;

        protected bool Invincible
        {
            get { return invulerability > 0; }
        }

        /// <summary>
        /// The amount of damage the enemy deals to the player when colliding with it.
        /// </summary>
        public int CollideDamage
        {
            get { return collideDamage; }
        }

        void Start()
        {
            hit = false;
            render = true;
            invulerability = 0f;
            currentHealth = totalHealth;
        }

        void Update()
        {
            if (hit)
            {
                if (invulerability <= 0)
                {
                    currentHealth -= damage;
                    invulerability = invulerabilityTime;
                }
                hit = false;
                damage = 0;
                anim.SetBool("TakeDamage", true);
            }
            if (invulerability > 0)
            {
                render = !render;
                sprite.enabled = render;
                invulerability -= Time.deltaTime;
            }
            else if (!render)
            {
                render = true;
                sprite.enabled = true;
            }
            if (currentHealth <= 0 || transform.position.y < -6)
            {
                sprite.enabled = true;
                //Die();
            }
        }

        public void NotifyCollision(Collision2D col, int d)
        {
            if (Invincible)
                return;
            damage = d;
            hit = true;
        }
    }
}
