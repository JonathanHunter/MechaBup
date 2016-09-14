using UnityEngine;
using Assets.Scripts.Util;

namespace Assets.Scripts.Attacks
{
    public class Attack : MonoBehaviour
    {
        public Enum.Direction direction;
        
        [SerializeField]
        private float speed;
        [SerializeField]
        private float lifeTime;
        private float currentLifeTime;
        //[SerializeField]
        //private Util.SoundPlayer sound;

        private bool hit;

        void Start()
        {
            currentLifeTime = 0;
            hit = false;
            //sound.PlaySong(0);
        }

        void Update()
        {
            switch (direction)
            {
                case Enum.Direction.Up:
                    transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
                    break;
                case Enum.Direction.Down:
                    Vector3 directionDown = transform.up;
                    directionDown.y = -directionDown.y;
                    transform.Translate(directionDown * speed * Time.deltaTime, Space.World);
                    break;
                case Enum.Direction.Left:
                    Vector3 directionLeft = transform.right;
                    directionLeft.x = -directionLeft.x;
                    transform.Translate(directionLeft * speed * Time.deltaTime, Space.World);
                    break;
                case Enum.Direction.Right:
                    transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
                    break;
            }
            if ((currentLifeTime += Time.deltaTime) > lifeTime || hit)
                Die();
        }

        private void Die()
        {
            Destroy(this.gameObject);
        }

        public void NotifyCollision(Collision2D col)
        {
            hit = true;
        }
    }
}
