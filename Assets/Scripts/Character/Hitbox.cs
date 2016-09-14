using UnityEngine;
using Assets.Scripts.Util;

namespace Assets.Scripts.Character
{
    class Hitbox : MonoBehaviour
    {
        /// <summary> The owner of this hitbox. </summary>
        [SerializeField]
        [Tooltip("Reference to the owner of this hitbox.")]
        private Enum.HitboxType type;
        public Enum.HitboxType Type { get { return type; } }

        /// <summary> The owner of this hitbox. </summary>
        [SerializeField]
        [Tooltip("Reference to the owner of this hitbox.")]
        private GameObject owner;
        public GameObject Owner { get { return owner; } set { if (owner == null) { owner = value; } } }

        /// <summary> How much damage this collider does. </summary>
        [SerializeField]
        [Tooltip("How much damage this collider does.")]
        private int damage;
        public int Damage { get { return damage; } }

        void OnCollisionEnter2D(Collision2D c)
        {
            if (c.gameObject.layer != LayerMask.NameToLayer("Feet") && c.gameObject.layer != LayerMask.NameToLayer("Platform"))
            {
                Hitbox hitbox = c.gameObject.GetComponent<Hitbox>();
                if (hitbox == null)
                    Debug.LogError("unknown collision between " + gameObject.name + " and " + c.gameObject.name);
                else
                {
                    switch (hitbox.Type)
                    {
                        case Enum.HitboxType.Attack: AttackCollision(c, hitbox); break;
                        case Enum.HitboxType.Character: CharacterCollision(c, hitbox); break;
                        case Enum.HitboxType.Mech: MechCollision(c, hitbox); break;
                        case Enum.HitboxType.Enemy: EnemyCollision(c, hitbox); break;
                    }
                }
            }
        }

        /// <summary> Handle what happens when this hitbox collides with an attack one. </summary>
        /// <param name="c"> The original Collision2D object. </param>
        /// <param name="h"> The other hitbox. </param>
        private void AttackCollision(Collision2D c, Hitbox h)
        {
            switch (type)
            {
                case Enum.HitboxType.Attack: owner.GetComponent<Attacks.Attack>().NotifyCollision(c); break;
                case Enum.HitboxType.Character: owner.GetComponent<Status>().NotifyCollision(c, h.damage); break;
                case Enum.HitboxType.Mech: owner.GetComponent<Status>().NotifyCollision(c, h.damage); break;
                case Enum.HitboxType.Enemy: owner.GetComponent<Status>().NotifyCollision(c, h.damage); break;
            }
        }

        /// <summary> Handle what happens when this hitbox collides with a character one. </summary>
        /// <param name="c"> The original Collision2D object. </param>
        /// <param name="h"> The other hitbox. </param>
        private void CharacterCollision(Collision2D c, Hitbox h)
        {
            switch (type)
            {
                case Enum.HitboxType.Attack: owner.GetComponent<Attacks.Attack>().NotifyCollision(c); break;
                case Enum.HitboxType.Character: break;
                case Enum.HitboxType.Mech: break;
                case Enum.HitboxType.Enemy: break;
            }
        }

        /// <summary> Handle what happens when this hitbox collides with a mech one. </summary>
        /// <param name="c"> The original Collision2D object. </param>
        /// <param name="h"> The other hitbox. </param>
        private void MechCollision(Collision2D c, Hitbox h)
        {
            switch (type)
            {
                case Enum.HitboxType.Attack: owner.GetComponent<Attacks.Attack>().NotifyCollision(c); break;
                case Enum.HitboxType.Character: break;
                case Enum.HitboxType.Mech: break;
                case Enum.HitboxType.Enemy: break;
            }
        }

        /// <summary> Handle what happens when this hitbox collides with a enemy one. </summary>
        /// <param name="c"> The original Collision2D object. </param>
        /// <param name="h"> The other hitbox. </param>
        private void EnemyCollision(Collision2D c, Hitbox h)
        {
            switch (type)
            {
                case Enum.HitboxType.Attack: owner.GetComponent<Attacks.Attack>().NotifyCollision(c); break;
                case Enum.HitboxType.Character: owner.GetComponent<Status>().NotifyCollision(c, h.damage); break;
                case Enum.HitboxType.Mech: break;
                case Enum.HitboxType.Enemy: break;
            }
        }

    }
}
