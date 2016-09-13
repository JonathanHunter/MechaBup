using UnityEngine;

namespace Assets.Scripts.Character.Controllers
{
    public class Controller : MonoBehaviour
    {
        /// <summary> The animator to send signals to. </summary>
        [SerializeField]
        protected Animator anim;

        public void SetAnimator(Animator anim)
        {
            MoveSpeed(0);
            Up(false);
            Down(false);
            Left(false);
            Right(false);
            this.anim = anim;
        }

        /// <summary> Tells the animator to attack. </summary>
        /// <param name="b"> What to set the anim bool to. </param>
        protected void Attack(bool b)
        {
            anim.SetBool("Attack", b);
        }

        /// <summary> Tells the animator to press up. </summary>
        /// <param name="b"> What to set the anim bool to. </param>
        protected void Up(bool b)
        {
            anim.SetBool("InputUp", b);
        }

        /// <summary> Tells the animator to press down. </summary>
        /// <param name="b"> What to set the anim bool to. </param>
        protected void Down(bool b)
        {
            anim.SetBool("InputDown", b);
        }

        /// <summary> Tells the animator to press left. </summary>
        /// <param name="b"> What to set the anim bool to. </param>
        protected void Left(bool b)
        {
            anim.SetBool("InputLeft", b);
        }

        /// <summary> Tells the animator to press right. </summary>
        /// <param name="b"> What to set the anim bool to. </param>
        protected void Right(bool b)
        {
            anim.SetBool("InputRight", b);
        }

        /// <summary> Tells the animator to move. </summary>
        /// <param name="b"> What to set the anim bool to. </param>
        protected void Move(bool b)
        {
            anim.SetBool("IsMoving", b);
        }

        /// <summary> Tells the animator the MoveSpeed. </summary>
        /// <param name="f"> What to set the anim float to. </param>
        protected void MoveSpeed(float f)
        {
            anim.SetFloat("MoveSpeed", f);
        }

        /// <summary> Tells the animator to jump. </summary>
        /// <param name="b"> What to set the anim bool to. </param>
        protected void Jump(bool b)
        {
            anim.SetBool("Jump", b);
        }
    }
}
