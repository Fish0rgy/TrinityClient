using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Module.World.World_Hacks.Just_H
{
    class JustHMisc
    {
        public static void EnableVROnlyBtn(bool state) => GameObject.Find("기믹/ele/ele trick").gameObject.active = state;
    }
}
