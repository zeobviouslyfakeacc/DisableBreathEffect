using System;
using System.Reflection;
using Harmony;

[HarmonyPatch(typeof(Breath), "PlayBreathEffect", new Type[0])]
internal static class DisableBreathEffect {

	private static readonly MethodInfo shouldSuppressBreathEffect = AccessTools.Method(typeof(Breath), "ShouldSuppressBreathEffect");

	private static bool Prefix(Breath __instance) {
		bool suppress = (bool) shouldSuppressBreathEffect.Invoke(__instance, new object[0]);
		if (suppress)
			return false;

		switch (GameManager.GetFatigueComponent().GetHeavyBreathingState()) {
			case HeavyBreathingState.Light:
				GameAudioManager.PlaySound(AK.EVENTS.PLAY_VOBREATHELOWINTENSITYNOLOOP, GameManager.GetPlayerObject());
				break;
			case HeavyBreathingState.Medium:
				GameAudioManager.PlaySound(AK.EVENTS.PLAY_VOBREATHMEDIUMINTENSITYNOLOOP, GameManager.GetPlayerObject());
				break;
			case HeavyBreathingState.Heavy:
				GameAudioManager.PlaySound(AK.EVENTS.PLAY_VOBREATHHIGHINTENSITYNOLOOP, GameManager.GetPlayerObject());
				break;
		}

		return false;
	}
}
