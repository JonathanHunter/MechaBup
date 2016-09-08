using UnityEngine;

namespace Assets.Scripts.Controllers.AI
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
