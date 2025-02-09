using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using System.IO;

namespace PortableSaves
{
    [HarmonyPatch(typeof(SaveSlots))]
    internal static class PortableSavePatches
    {
        [HarmonyPatch("GetBackupPath")]
        [HarmonyPostfix]
        public static void GetBackupPathPatch(int slot, int backupIndex, ref string __result)
        {
            __result = GetPath(slot, backupIndex);
            Debug.Log("Overrode path with " + __result);
        }

        [HarmonyPatch("GetCurrentSavePath")]
        [HarmonyPostfix]
        public static void GetCurrentSavePathPatch(int ___currentSlot, ref string __result)
        {
            __result = GetPath(___currentSlot, -1);
            Debug.Log("Overrode path with " + __result);
        }

        [HarmonyPatch("GetSlotSavePath")]
        [HarmonyPostfix]
        public static void GetSlotSavePathPatch(int slot, ref string __result)
        {
            __result = GetPath(slot, -1);
            Debug.Log("Overrode path with " + __result);
        }
        private static string GetPath(int slot, int backupIndex)
        {
            string basePath = Application.dataPath + "/Saves";
            if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);
            string backup = backupIndex != -1 ? "_backup" + backupIndex : "";
            string path = basePath + "/slot" + slot.ToString() + backup + ".save";
            return path;
        }
    }
}
