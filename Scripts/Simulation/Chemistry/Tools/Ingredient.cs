using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace AppStarter
{
    public class Ingredient : Item
    {
        float _interval = 1f;
        float _time = 0f;
        public int disappearTimeAfterBurning = 200;
        public bool ignited = false, create = false;
        public float temperature = 298;
        public float excessEnergy = 0f;
        public float environmentTemperature = 298;

        public float IgnitionTemperature = 744;
        public float Mass = 0.01f;
        public float SurfaceArea = 0.004f;
        public float SpecificHeatCapacity = 1017;
        public float ThermalConductivity = 151;
        public Transform fX, bottom, top, stripRender, ash;
        public Animation anim;

        public float shrink_offset = 0.02f;
        public float create_anim_speed = 0.0001f, burn_anim_speed = 0.00004f;
        public float closeEnough = 0.000001f;
        public float burn_fx_speed = 0.02f;
        public float burn_fx_target = -0.5f;
        public float goalScale, goalPos;

        public bool _create = false;
        public int _selfDestruct = -1;

        public override int Slot { get { return -1; } }

        public void Burn()
        {
            ignited = true;
            goalScale = 0f;
            goalPos = stripRender.localPosition.z - 1 * shrink_offset;
            fX.gameObject.SetActive(true);
            ash.gameObject.SetActive(true);
            API.updateQuestEvent(typeID, instanceID, questID, true);
        }

        public void Create()
        {
            create = true;
            goalScale = 2 * shrink_offset;
            goalPos = stripRender.localPosition.z + 1 * shrink_offset;
        }

        public void TemperatureCalculation()
        {
            float energyChange = (environmentTemperature - temperature) * SurfaceArea * ThermalConductivity;
            excessEnergy = excessEnergy + energyChange;
            temperature = temperature + energyChange / (Mass * SpecificHeatCapacity);

            if (temperature > IgnitionTemperature && !ignited)
                Burn();
        }

        public void FxAnimate()
        {
            Vector3 newPos = fX.localPosition;
            newPos.z = Mathf.Lerp(newPos.z, burn_fx_target, burn_fx_speed);
            fX.localPosition = newPos;
        }

        public void ShrinkAnimate(float offset, float ispeed)
        {
            Vector3 newScale = stripRender.localScale, newPos = stripRender.localPosition;
            newScale.z = newScale.z + 2 * ispeed;
            newPos.z = newPos.z + ispeed;
            stripRender.localScale = newScale;
            stripRender.localPosition = newPos;
        }

        ///// <summary>
        ///// Generic VR Controller Interaction event with eventType for getting source 
        ///// </summary>
        ///// <param name="controller"></param>
        //public override void Interact(IMixedRealityController controller, string eventType)
        //{
        //    Release();
        //}

        //public override void Release()
        //{
        //    transform.parent = null;
        //    EnablePhysics();
        //}

        public void FixedUpdate()
        {
            if (_selfDestruct != -1)
            {
                _selfDestruct--;
                if (_selfDestruct == 0)
                    GameObject.Destroy(transform.gameObject);
            }

            _time += Time.deltaTime;
            while (_time >= _interval)
            {
                TemperatureCalculation();

                _time -= _interval;
            }

            if (_create)
            {
                _create = false;
                Create();
            }

            if (Mathf.Abs(stripRender.localScale.z - goalScale) < closeEnough)
            {
                if (ignited)
                {
                    fX.gameObject.SetActive(false);
                    stripRender.gameObject.SetActive(false);
                    triggerOnToolDrop.Invoke();
                    _selfDestruct = disappearTimeAfterBurning;
                }

                create = false;
                ignited = false;
            }

            if (create)
            {
                ShrinkAnimate(shrink_offset, create_anim_speed);
            }

            if (ignited)
            {
                FxAnimate();
                ShrinkAnimate(shrink_offset, burn_anim_speed);
            }
        }
    }
}