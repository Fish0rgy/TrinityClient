using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Utilities
{
    public static class WU
    {
        public static GameObject[] GetAllGameObjects() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

    }
}
 