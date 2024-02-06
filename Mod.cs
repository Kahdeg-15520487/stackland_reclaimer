using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace Reclaimer
{
    public class ReclaimerMod : Mod
    {
        public static ModLogger L;
        public override void Ready()
        {
            L = Logger;
            Logger.Log("Ready!");
        }
    }
}