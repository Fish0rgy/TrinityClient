using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.Utilities;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRCSDK2;
 
namespace Trinity.Module.World
{
    internal class FloorDrop : BaseModule
    {
        public FloorDrop() : base("Floor Drop", "Removes floor from underneath", Main.Instance.WorldButton, SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath), false, false) { }

        public override void OnEnable()
        { 
            LogHandler.Log(LogHandler.Colors.Green, "Floor Dropper Activivated!", false, false);
        }
    } 
	 
}
