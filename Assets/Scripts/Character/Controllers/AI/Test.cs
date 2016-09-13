using UnityEngine;

namespace Assets.Scripts.Character.Controllers.AI
{
    class Test : Controller
    {
        void Update()
        {
            // constantly attack
            Attack(true);
        }
    }
}
