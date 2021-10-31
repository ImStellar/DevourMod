﻿using MelonLoader;
using Opsive.UltimateCharacterController.Character;
using Photon.Bolt;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevourMod
{
    public class DevourMain : MelonMod
    {
        public static Camera Cam;
        public static int logcounter = 0;
        public static bool flying = false;
        public static bool lobbyfly = false;
        public static int hiii = 1;
        public static bool WorldCrasher = false;
        public static bool something = false;
        private GameObject player;
        public static int num1 = 1;
        public static float Delay2 = 0f;
        public static bool spinbot = false;
        public static bool pingloopend = false;
        public static bool pingloopstart = false;
        public static bool rankspoof = false;
        public static float Delay1 = 0f;
        public static bool gamelagger = false;
        public static bool isinworld = false;
        public static bool freefall = false;
        public static bool unlockach = false;
        public static float flySpeed = 5;
        public static float testspeed = 1;
        private static GameObject LocalPlayerCached;
        public static GameObject LocalPlayer => LocalPlayerCached ??= FindObject<PlayerCharacterBehaviour>().gameObject;
        public static List<SurvivalDemonBehaviour> Demons = new List<SurvivalDemonBehaviour>();
        public static List<GoatBehaviour> Goats = new List<GoatBehaviour>();
        public static List<SurvivalEggInteractable> Eggs = new List<SurvivalEggInteractable>();
        public static List<SurvivalInteractable> Items = new List<SurvivalInteractable>();
        public static List<KeyBehaviour> Keys = new List<KeyBehaviour>();
        public static List<CollectableInteractable> Collectibles = new List<CollectableInteractable>();
        public static SurvivalAzazelBehaviour Azazel;
        public static GameObject Ritual;
        public static Vector3 cachedposition;


        private void GUI()
        {
            DevourForm form = new DevourForm();
            form.Show();
        }

        public override void OnApplicationStart()
        {
            GUI();
            MelonLogger.Msg("Cheat Loaded Succesfully!");
        }

        public override void OnLevelWasLoaded(int level)
        {
            MelonLogger.Msg($"Loaded Level: {level}");
            if (level != 0)
                isinworld = true;
            if (level == 0)
                isinworld = false;
            LocalPlayerCached = null;
        }

        internal static T FindObject<T>() where T : Component
        {
            var TType = UnhollowerRuntimeLib.Il2CppType.Of<T>();
            var foundObjects = UnityEngine.Object.FindObjectsOfType(TType);
            if (foundObjects.Count != 0 && !isinworld)
            {
                foundObjects = null;
            }
            if (foundObjects is null || foundObjects.Count == 0 && isinworld)
                foundObjects = Resources.FindObjectsOfTypeAll(TType);
            if (foundObjects != null && foundObjects.Count != 0 && isinworld && foundObjects.Select(foundObject => foundObject.TryCast<T>()).FirstOrDefault().gameObject.name.Contains("(Clone)") && foundObjects.Select(foundObject => foundObject.TryCast<T>()).FirstOrDefault().gameObject.GetComponentInChildren<UltimateCharacterLocomotionHandler>() != null)
            {
                MelonLogger.Msg($"setting your localplayer value to: {foundObjects.Select(foundObject => foundObject.TryCast<T>()).FirstOrDefault().gameObject.name}");
                return foundObjects.Select(foundObject => foundObject.TryCast<T>()).FirstOrDefault();
            }
            return default;
        }

        public override void OnUpdate()
        {
            try
            {
                //Delay1 += Time.deltaTime;
                //if (Delay1 > 1f)
                //{
                //    foreach (GoatBehaviour goat in Resources.FindObjectsOfTypeAll<GoatBehaviour>())
                //    {
                //        Goats.Add(goat);
                //    }
                //    MelonLogger.Msg(Goats.Count);
                //    Delay1 = 0f;
                //}
                if (rankspoof)
                {
                    foreach (NolanRankController rank in Resources.FindObjectsOfTypeAll<NolanRankController>())
                    {
                        int __result;
                        if (num1 == 1)
                        {
                            pingloopend = false;
                            pingloopstart = true;
                        }
                        if (num1 == 100)
                        {
                            pingloopstart = false;
                            pingloopend = true;
                        }
                        if (pingloopstart == true)
                        {
                            num1++;
                        }
                        if (pingloopend == true)
                        {
                            num1--;
                        }
                        __result = num1;
                        rank.SetRank(__result);
                    }
                }
                if (LocalPlayer == null && flying)
                {
                    LocalPlayer.GetComponent<UltimateCharacterLocomotion>().UseGravity = true;
                    foreach (Collider collide in Resources.FindObjectsOfTypeAll<Collider>())
                    {
                        if (!collide.gameObject.transform.IsChildOf(LocalPlayer.gameObject.transform))
                        {
                            collide.enabled = true;
                        }
                    }
                    flying = false;
                }
                if (Input.GetKeyDown(KeyCode.T))
                {
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    if (Physics.Raycast(ray, out RaycastHit raycastHit)) LocalPlayer.GetComponent<UltimateCharacterLocomotion>().SetPosition(raycastHit.point);
                    MelonLogger.Msg($"[Teleport] Position: {LocalPlayer.gameObject.transform.position.ToString()}");
                }
                if (freefall)
                {
                    try
                    {
                        var k = UnityEngine.Object.FindObjectOfType<KeyBehaviour>();
                        k.gameObject.SetActive(false);
                        BoltNetwork.Instantiate(k.entity.PrefabId, new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity), new Quaternion(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity));
                    }
                    catch { }
                }
                if (spinbot)
                {
                    Quaternion quater = Quaternion.Euler(LocalPlayer.GetComponent<UltimateCharacterLocomotion>().transform.eulerAngles.x, LocalPlayer.GetComponent<UltimateCharacterLocomotion>().transform.eulerAngles.y + 10, LocalPlayer.GetComponent<UltimateCharacterLocomotion>().transform.eulerAngles.z);
                    LocalPlayer.GetComponent<UltimateCharacterLocomotion>().SetRotation(quater);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    foreach (Opsive.UltimateCharacterController.Traits.Respawner respaw in Resources.FindObjectsOfTypeAll<Opsive.UltimateCharacterController.Traits.Respawner>())
                    {
                        respaw.Respawn();
                    }
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    if (LocalPlayer != null)
                    {
                        if (flying)
                        {
                            MelonLogger.Msg("Fly Disabled");
                            LocalPlayer.GetComponent<UltimateCharacterLocomotion>().UseGravity = true;
                            LocalPlayer.GetComponent<UltimateCharacterLocomotion>().TimeScale = 1f;
                            foreach (Collider collide in Resources.FindObjectsOfTypeAll<Collider>())
                            {
                                if (!collide.gameObject.transform.IsChildOf(LocalPlayer.gameObject.transform))
                                {
                                    collide.enabled = true;
                                }
                            }
                            flying = false;
                        }
                        else
                        {
                            MelonLogger.Msg("Fly Enabled");
                            LocalPlayer.GetComponent<UltimateCharacterLocomotion>().UseGravity = false;
                            LocalPlayer.GetComponent<UltimateCharacterLocomotion>().TimeScale = flySpeed;
                            foreach (Collider collide in Resources.FindObjectsOfTypeAll<Collider>())
                            {
                                if (!collide.gameObject.transform.IsChildOf(LocalPlayer.gameObject.transform))
                                {
                                    collide.enabled = false;
                                }
                            }
                            player = LocalPlayer.gameObject;
                            flying = true;
                        }
                    }
                }
                if (flying)
                {
                    float number = Input.GetKey(KeyCode.LeftShift) ? (flySpeed * 2f) : flySpeed;
                    if (Input.mouseScrollDelta.y != 0)
                    {
                        flySpeed += (int)Input.mouseScrollDelta.y;

                        if (flySpeed <= 0)
                            flySpeed = 1;
                    }
                    if (Input.GetKey(KeyCode.E))
                    {
                        LocalPlayer.GetComponent<UltimateCharacterLocomotion>().SetPosition(player.transform.position += Vector3.up * number * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyCode.Q))
                    {
                        LocalPlayer.GetComponent<UltimateCharacterLocomotion>().SetPosition(player.transform.position -= Vector3.up * number * Time.deltaTime);
                    }
                }
            }
            catch { }
        }

        
    }
}