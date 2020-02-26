using System;
using UnityEngine;

namespace speedhack
{
    public class speedchanger : MonoBehaviour
    {
        public float targetSpeed;


        private void Start()
        {
            On.RafaForceRenderRate.LateUpdate += FrameratePolice;
            On.FamesLockerScript.Awake += NoRun1;
            On.TimeManagerObjtScr.Update += TimeManagerObjtScrOnUpdate;
            On.SteamManagerFailsafeScr.Update += DontTouchMyTimescaleBro;
        }

        private void DontTouchMyTimescaleBro(On.SteamManagerFailsafeScr.orig_Update orig, SteamManagerFailsafeScr self)
        {
            orig(self);
            Time.timeScale = targetSpeed;
        }

        private void TimeManagerObjtScrOnUpdate(On.TimeManagerObjtScr.orig_Update orig, TimeManagerObjtScr self)
        {
            orig(self);
            if (self.CurrState == TimeManagerObjtScr.Staters.Idle)
            {
                Time.timeScale = targetSpeed;
            }
        }

        private void NoRun1(On.FamesLockerScript.orig_Awake orig, FamesLockerScript self)
        {
            Application.targetFrameRate = (int) getFPS();
        }

        private void FrameratePolice(On.RafaForceRenderRate.orig_LateUpdate orig, RafaForceRenderRate self)
        {
            if ((float)QualitySettings.vSyncCount == 0f)
            {
                if (self.VsyncOn)
                {
                    self.VsyncOn = false;
                }
                if (Application.targetFrameRate != (int) getFPS())
                {
                    Application.targetFrameRate = (int) getFPS();
                    return;
                }
            }
            else
            {
                if (QualitySettings.vSyncCount != self.VsyncSixtyCount)
                {
                    QualitySettings.vSyncCount = self.VsyncSixtyCount;
                }
                if (!self.VsyncOn)
                {
                    self.VsyncOn = true;
                }
            }
        }

        private void Update()
        {
            if (Time.timeScale <= 0.0001f)
                return;

            if (Time.timeScale != targetSpeed)
            {
                Time.timeScale = targetSpeed;
            }

            if (Application.targetFrameRate == 60 && targetSpeed != 1.0f)
            {
                Application.targetFrameRate = (int) getFPS();
            }
        }

        private void LateUpdate()
        {
            if (Time.timeScale <= 0.0001f)
                return;

            if (Time.timeScale != targetSpeed)
            {
                Time.timeScale = targetSpeed;
            }
        }

        private float getFPS()
        {
            return 60.0f * targetSpeed;
        }
    }
}