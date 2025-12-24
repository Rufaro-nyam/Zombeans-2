//    ExampleScript - DirectAccess


using UnityEngine;
using Leguar.LowHealth;
using UnityEngine.UI;

namespace Leguar.LowHealth.Example {

	public class ExampleScript_DirectAccess : MonoBehaviour {

		// This script (LowHealthDirectAccess) is attached to camera in the scene
		public LowHealthDirectAccess shaderAccessScript;

		private float wakingUp1;
		public float wakingUp2;
		public float takingDamage;
		public float beingDizzy;

		public RawImage blood_img;

		void Start() {
			wakingUp1 = 0f;
			wakingUp2 = 0f;
			takingDamage = 0f;
			beingDizzy = 0f;
		}

		public void WakingUp1Clicked() {
			wakingUp1 = 1f;
		}

		public void WakingUp2Clicked() {
			wakingUp2 = 1f;
		}

		public void TakeDamageClicked() {
			takingDamage = 1f;
		}

		public void GetDizzyClicked() {
			beingDizzy = 1f;
		}

		void Update() {

			takingDamage = Mathf.Clamp(takingDamage, 0f, 1f);
			
		   if (wakingUp1>0f) {
				wakingUp1 -= Time.deltaTime*0.1f;
				shaderAccessScript.SetVisionLossEffect(wakingUp1);
			}

			if (wakingUp2>0f) {
				wakingUp2 -= Time.deltaTime*0.05f;
				float sin = Mathf.Sin(Mathf.PI*0.5f+(1f-wakingUp2)*Mathf.PI*3f);
				float dl = (sin+1f)*0.5f*wakingUp2;
				shaderAccessScript.SetDetailLossEffect(dl);
			}

			if (takingDamage>0f) {
				takingDamage -= Time.deltaTime*0.2f;
				shaderAccessScript.SetColorLossEffect(takingDamage, 1f);
				float newalpha = Mathf.Clamp01(takingDamage);
				Color current_alpha = blood_img.color;
				current_alpha.a = newalpha;
				blood_img.color = current_alpha;
//				shaderAccessScript.SetVisionLossEffect(takingDamage*0.5f); // Uncomment to add darkening effect to taking damage, but this will then clash with "Waking up 1" effect
			}

			if (beingDizzy>0f) {
				beingDizzy -= Time.deltaTime*0.05f;
				float sin = Mathf.Sin((beingDizzy)*Mathf.PI*10f);
				float dv = smoothCurve(1f-beingDizzy)*0.8f + sin*0.2f*beingDizzy;
				shaderAccessScript.SetDoubleVisionEffect(dv);


            }
            AudioLowPassFilter[] filters = FindObjectsOfType<AudioLowPassFilter>();
            foreach (AudioLowPassFilter l in filters)
            {
				if(l.cutoffFrequency < 22000) 
				{
                    l.cutoffFrequency += Time.deltaTime * (0.05f * 22000);
                }

            }

        }

		private float smoothCurve(float time) {
			if (time>=1f) {
				return 0f;
			}
			float t;
			if (time<0.1f) {
				t = time*5f;
			} else {
				t = 0.5f+(time-0.1f)/0.9f*0.5f;
			}
			float sin = Mathf.Sin(Mathf.PI*t);
			return sin;
		}

	}

}
