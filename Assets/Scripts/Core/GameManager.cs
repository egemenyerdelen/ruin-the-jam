using Helpers;
using UnityEngine;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        private void Start()
        {
            Cursor.visible = false;
        }
    }
}