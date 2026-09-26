using System;
using UnityEngine;

namespace GOOMPS
{
	internal class GOOMPSCollision : MonoBehaviour
	{
		public static void ChangePlayerVisibility(VRRig _rig, bool _visible)
		{
			if (_rig == GorillaTagger.Instance.offlineVRRig)
				return;

			if (Cfg.audio.Value && !_visible) // Only play sound when they disappear
			{
				GorillaTagger instance = GorillaTagger.Instance;
				if (instance != null)
				{
					VRRig offlineVRRig = instance.offlineVRRig;
					if (offlineVRRig != null)
						offlineVRRig.tagSound.PlayOneShot(GorillaTagger.Instance.offlineVRRig.clipToPlay[5]);
				}
			}

			_rig.muted = Cfg.mute.Value && !_visible;
			
			Renderer[] renderers = _rig.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers)
			{
				r.forceRenderingOff = !_visible;
				r.enabled = _visible;
			}
		}

		private void OnTriggerEnter(Collider coll)
		{
			if (coll.name.Contains("Body"))
				ChangePlayerVisibility(coll.GetComponentInParent<VRRig>(), false);
		}

		private void OnTriggerExit(Collider coll)
		{
			if (coll.name.Contains("Body"))
				ChangePlayerVisibility(coll.GetComponentInParent<VRRig>(), true);
		}
	}
}
