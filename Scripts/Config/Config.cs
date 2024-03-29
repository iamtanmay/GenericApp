﻿/*******************************************************
 * Copyright (C) 2017 Doron Weiss  - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of unity license.
 * 
 * See https://abnormalcreativity.wixsite.com/home for more info
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AppSettings {
	[System.Serializable]
	public class Config : ConfigBase {

        [Header("--Simple primitive example--")]
        public bool show = true;
        public int height = 0;

        [Header("--Lists and arrays--")]
        public int[] guidecolor = { 130, 152, 213 };
        public int[] backgroundcolor = { 130, 152, 214 };
        public int[] boardcolor = { 0, 0, 0 };
        public int[] fontbgcolor = { 128, 128, 128 };
        public List<float> listExample;

        [Header("--Enum and Class--")]
        public EnumExample enumExample;
        public MySpecialClassExample classExample;

        #region Enums and classes for serialization

        public enum EnumExample {
            Enum1,Enum2
        }


        [System.Serializable]
        public class MySpecialClassExample
        {
            public string txt = "abcd";
        }
        #endregion

        private new void Awake() {
			base.Awake ();
            SetupSingelton();
        }
/// <summary>
/// 
/// </summary>
        #region  Singelton
        public static Config _instance;
        public static Config Instance { get { return _instance; } }
        private void SetupSingelton()
        {
            if (_instance != null)
            {
                Debug.LogError("Error in settings. Multiple singeltons exists: " + _instance.name + " and now " + this.name);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion
    }
}