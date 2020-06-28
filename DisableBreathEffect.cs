using MelonLoader;
using System;
using Harmony;
using UnityEngine;

namespace DisableBreathEffect {

	internal class Mod : MelonMod {
		public override void OnApplicationStart() {
			Debug.Log($"[{InfoAttribute.Name}] Version {InfoAttribute.Version} loaded!");
		}
	}

	[HarmonyPatch(typeof(Breath), "Start", new Type[0])]
	internal static class DisableBreathEffectPatch {

		private static void Postfix(Breath __instance) {
			__instance.m_ColdBreathTempThreshold = -float.MaxValue;
			__instance.m_VeryColdBreathTempThreshold = -float.MaxValue;
			__instance.m_FreezingBreathTempThreshold = -float.MaxValue;
			__instance.StopBreathEffectImmediate();
		}
	}
}
