using System;
using System.Collections.Generic;
using System.Linq;
using MapMagic;
using UnityEngine;

namespace Recte_Ravenfield
{
    public class RecteManager : MonoBehaviour
    {
        /*
         * TO-DO List
         *
         * 1. Add Keycodes
         * 2. Add Teleporting - Done
         * 3. Add Players Menu - Done
         * 4. Add Coloring Menu
         * 5. Add Different Health Bar Locations
         * 6. Fix Aimbot
         * 7. Have Healthbar Scale With Box
         * 
         * */
        public void Update()
        {
            try
            {
                if (
                    Input.GetKeyDown(
                        (KeyCode)
                            Enum.Parse(
                                typeof(KeyCode),
                                PlayerPrefs.GetString("MenuKeycode", "RightShift")
                            )
                    )
                )
                {
                    PlayerPrefsX.SetBool("Open", !PlayerPrefsX.GetBool("Open"));
                }
                bool flag = this.am == null || !this.am.isActiveAndEnabled;
                if (flag)
                {
                    this.am = ActorManager.instance;
                }
                else
                {
                    bool flag2 = this.player == null || !this.player.isActiveAndEnabled;
                    if (flag2)
                    {
                        this.player = this.am.player;
                    }
                    if (PlayerPrefsX.GetBool("Invincible"))
                    {
                        this.player.balance = float.MaxValue;
                        this.player.SetHealth(float.MaxValue);
                    }
                    if (this.player.activeWeapon != null)
                    {
                        if (PlayerPrefsX.GetBool("InfiniteAmmo"))
                        {
                            this.player.activeWeapon.ammo = int.MaxValue;
                        }
                        if (PlayerPrefsX.GetBool("NoRecoil"))
                        {
                            this.player.activeWeapon.configuration.kickback = (
                                this.player.activeWeapon.configuration.randomKick = 0f
                            );
                        }
                        if (PlayerPrefsX.GetBool("RapidFire"))
                        {
                            this.player.activeWeapon.configuration.cooldown = 0.005f;
                        }
                        if (PlayerPrefsX.GetBool("NoSpread"))
                        {
                            this.player.activeWeapon.configuration.followupMaxSpreadAim = (
                                this.player.activeWeapon.configuration.spread = (
                                    this.player.activeWeapon.configuration.followupMaxSpreadHip = (
                                        this.player.activeWeapon.configuration.followupSpreadGain =
                                            (
                                                this.player
                                                    .activeWeapon
                                                    .configuration
                                                    .followupSpreadStayTime = 0f
                                            )
                                    )
                                )
                            );
                        }

                        if (PlayerPrefsX.GetBool("Speed"))
                        {
                            this.player.speedMultiplier = PlayerPrefs.GetFloat("SpeedMultiply");
                        }
                    }
                }

                bool aimbot = PlayerPrefsX.GetBool("Aimbot123123");
                if (aimbot)
                {
                    bool flag3 =
                        this.target == null || !this.target.isActiveAndEnabled || this.target.dead;
                    if (flag3)
                    {
                        this.target = this.GetTarget();
                    }
                    else
                    {
                        Vector3 vector = this.player.WeaponMuzzlePosition();
                        RaycastHit raycastHit;
                        bool flag4 = Physics.Raycast(
                            vector,
                            (this.target.WeaponMuzzlePosition() - vector).normalized,
                            out raycastHit
                        );
                        if (flag4)
                        {
                            Collider[] array = (Collider[])
                                this.target.GetFieldValue("hitboxColliders");
                            bool flag5 =
                                raycastHit.collider != null && array.Contains(raycastHit.collider);
                            if (flag5)
                            {
                                FpsActorController.instance.fpCamera.transform.LookAt(
                                    this.target.Position()
                                        + Vector3.up * PlayerPrefs.GetFloat("AimOffset", .5f)
                                );
                            }
                            else
                            {
                                this.target = null;
                            }
                        }
                        else
                        {
                            this.target = null;
                        }
                    }
                }
            }
            catch (NullReferenceException) { }
        }

        private void Skeleton()
        {
            foreach (Actor a in ActorManager.instance.actors)
            {
                if (Camera.main.WorldToScreenPoint(a.transform.position).z > 0)
                {
                    Vector3 p1 = new Vector3();
                    Vector3 p2 = new Vector3();
                    Vector3 p3 = new Vector3();
                    Vector3 p4 = new Vector3();
                    Vector3 p5 = new Vector3();
                    Vector3 p6 = new Vector3();
                    Vector3 p7 = new Vector3();
                    Vector3 p8 = new Vector3();
                    Vector3 p9 = new Vector3();
                    if (a.team == 0 && a.aiControlled)
                    {
                        foreach (Transform child in a.transform)
                        {
                            Transform c = child.GetChild(0).transform;
                            foreach (Transform c1 in c.transform)
                            {
                                Transform Bone1 = c1.GetChild(0).transform;
                                foreach (Transform c3 in Bone1.transform) // Bone.001
                                {
                                    Transform c4 = c3.GetChild(0).transform; //0 - Bone.004     1 - Arm.L.001      2 - Arm.R.001
                                    foreach (Transform Bone4 in c4.transform)
                                    {
                                        p4 = Camera.main.WorldToScreenPoint(
                                            Bone4.transform.position
                                        );
                                    }
                                    Transform c5 = c3.GetChild(1).transform; //0 - Bone.004     1 - Arm.L.001      2 - Arm.R.001
                                    p5 = new Vector3(p4.x, p4.y - 50f, p4.z);
                                    foreach (Transform c6 in c5.transform)
                                    {
                                        //c6 - Arm.L.001
                                        Transform c7 = c6.GetChild(0).transform;
                                        foreach (Transform c8 in c7.transform)
                                        {
                                            p2 = Camera.main.WorldToScreenPoint(
                                                c8.transform.position
                                            );
                                        }
                                        p1 = Camera.main.WorldToScreenPoint(c6.transform.position);
                                    }
                                    p6 = Camera.main.WorldToScreenPoint(c5.transform.position);
                                    Transform RightArm = c3.GetChild(2).transform; //0 - Bone.004     1 - Arm.L.001      2 - Arm.R.001
                                    foreach (Transform c6 in RightArm.transform)
                                    {
                                        //c6 - Arm.L.001
                                        Transform c7 = c6.GetChild(0).transform;
                                        foreach (Transform c8 in c7.transform)
                                        {
                                            p7 = Camera.main.WorldToScreenPoint(
                                                c8.transform.position
                                            );
                                        }
                                        p8 = Camera.main.WorldToScreenPoint(c6.transform.position);
                                    }
                                    p9 = Camera.main.WorldToScreenPoint(c5.transform.position);
                                }
                                p3 = Camera.main.WorldToScreenPoint(Bone1.transform.position); // Setting Bone.001 Position
                                //C3 - Bone.002
                                //C5 - Bone.005
                                /*

                                Transform LeftLeg = c1.GetChild(1).transform;
                                foreach (Transform c3 in LeftLeg.transform) // Leg.L
                                {
                                    Transform c4 = c3.GetChild(0).transform;
                                    foreach (Transform c5 in c4.transform) // Leg.L.001
                                    {
                                        p2 = Camera.main.WorldToScreenPoint(c5.position); // Setting Leg.L.001 Position
                                    }
                                }
                                Transform RightLeg = c1.GetChild(2).transform;
                                foreach (Transform c3 in RightLeg.transform) // Leg.R
                                {
                                    Transform c4 = c3.GetChild(0).transform;
                                    foreach (Transform c5 in c4.transform) // Leg.R.001
                                    {
                                        p1 = Camera.main.WorldToScreenPoint(c5.position); // Setting Leg.R.001 Position
                                    }
                                }*/
                            }
                        }
                        Color color = Color.Lerp(
                            new Color(0f, 0f, 0f),
                            new Color(1f, 1f, 1f),
                            Mathf.PingPong(Time.time, 2f)
                        );
                        RecteUtils.DrawBoneLine(p3, p5, color);
                        RecteUtils.DrawBoneLine(p4, p5, color);
                        RecteUtils.DrawBoneLine(p1, p2, color);
                        RecteUtils.DrawBoneLine(p6, p1, color);
                        RecteUtils.DrawBoneLine(p7, p8, color);
                        RecteUtils.DrawBoneLine(p8, p9, color);
                    }
                }
            }
        }

        public static Texture2D redTexture;
        public static Texture DefaultTexture;
        public static Shader DefaultShader;

        private void Chams()
        {
            Color color = Color.Lerp(
                RecteUtils.GetColorFromString("482BE2"),
                RecteUtils.GetColorFromString("CB2BE2"),
                Mathf.PingPong(Time.time, 2f)
            );
            Color color2 = Color.Lerp(
                RecteUtils.GetColorFromString("C3E4FA"),
                RecteUtils.GetColorFromString("4BB7FA"),
                Mathf.PingPong(Time.time, 2f)
            );
            foreach (Actor a in ActorManager.instance.actors)
            {
                var rend = a.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rend)
                {
                    foreach (Material m in r.materials)
                    {
                        if (PlayerPrefsX.GetBool("Chams")) {
                            if (m.shader == DefaultShader)
                            {
                                if (PlayerPrefsX.GetBool("Chams"))
                                {
                                    Shader shader = Shader.Find("GUI/Text Shader");
                                    // Default shader on Unity
                                    m.shader = shader;
                                    m.color = color;
                                    m.renderQueue = 4000;
                                }
                            }
                        }
                        if (!PlayerPrefsX.GetBool("Chams"))
                        {
                            m.shader = DefaultShader;
                        }
                        
                    }
                }
            }
        }

        public void Tracers()
        {
            foreach (Actor a in ActorManager.instance.actors)
            {
                if (a.name != player.name)
                {
                    Color color2 = RecteUtils.GetColorFromString("8a2be2");
                    Vector3 vector = Camera.main.WorldToScreenPoint(a.transform.position);
                    if (vector.z > 0)
                    {
                        if (PlayerPrefs.GetInt("TracerLoc") == 0)
                        {
                            RecteUtils.DrawLine(
                                new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f),
                                new Vector2(vector.x, (float)Screen.height - vector.y - 64f),
                                3f,
                                color2
                            );
                        }
                        if (PlayerPrefs.GetInt("TracerLoc") == 1)
                        {
                            RecteUtils.DrawLine(
                                new Vector2((float)Screen.width / 2f, 0f),
                                new Vector2(vector.x, (float)Screen.height - vector.y - 64f),
                                3f,
                                color2
                            );
                        }
                        if (PlayerPrefs.GetInt("TracerLoc") == 2)
                        {
                            RecteUtils.DrawLine(
                                new Vector2((float)Screen.width / 2f, (float)Screen.height),
                                new Vector2(vector.x, (float)Screen.height - vector.y - 64f),
                                3f,
                                color2
                            );
                        }
                    }
                }
            }
        }

        public void OnGUI()
        {
            bool @bool = PlayerPrefsX.GetBool("Open");
            if (@bool)
            {
                this.rect1 = GUILayout.Window(
                    1,
                    this.rect1,
                    new GUI.WindowFunction(this.MenuWindow),
                    "Recte | kanati.gay",
                    new GUILayoutOption[0]
                );
            }

            try
            {
                if (PlayerPrefsX.GetBool("aimFOV"))
                {
                    RecteUtils.DrawCircle(
                        RecteUtils.CenterOfScreen(),
                        PlayerPrefs.GetFloat("fovSize", 10f),
                        Color.cyan,
                        5,
                        10
                    );
                }
                if (PlayerPrefsX.GetBool("Tracers"))
                {
                    this.Tracers();
                }
                if (PlayerPrefsX.GetBool("Chams"))
                {
                    this.Chams();
                }
                if (PlayerPrefsX.GetBool("Skeleton"))
                {
                    this.Skeleton();
                }
                if (PlayerPrefsX.GetBool("FPSCounter"))
                {
                    GUILayout.Label(
                        $"<b><color=#8a2be2>FPS: {((int)((float)((int)(1f / Time.unscaledDeltaTime))))}</color></b>"
                    );
                }
                if (PlayerPrefsX.GetBool("Crosshair"))
                {
                    if (PlayerPrefs.GetInt("CrosshairMode") == 1)
                    {
                        Vector2 cen = new Vector2(
                            (float)Screen.width / 2f,
                            (float)Screen.height / 2f
                        );
                        RecteUtils.DrawLine(
                            new Vector2(cen.x - 15f, cen.y - 15f),
                            new Vector2(cen.x + 15f, cen.y + 15f),
                            Color.cyan,
                            3f
                        );
                        RecteUtils.DrawLine(
                            new Vector2(cen.x - 15f, cen.y + 15f),
                            new Vector2(cen.x + 15f, cen.y - 15f),
                            Color.cyan,
                            3f
                        );
                    }
                    if (PlayerPrefs.GetInt("CrosshairMode") == 0)
                    {
                        Vector2 cen = new Vector2(
                            (float)Screen.width / 2f,
                            (float)Screen.height / 2f
                        );
                        RecteUtils.DrawCrosshair(cen, 15f, Color.cyan, 3f);
                    }
                }
                if (PlayerPrefsX.GetBool("CoordsDisplay"))
                {
                    bool flag2 = player == null;
                    if (flag2)
                    {
                        return;
                    }
                    string text = "X: ";
                    Vector3 position = player.rigidbody.transform.position;
                    string text2 = Math.Round(double.Parse(position.x.ToString()), 0).ToString();
                    string text3 = "\nY: ";
                    string text4 = Math.Round(double.Parse(position.y.ToString()), 0).ToString();
                    string text5 = "\nZ: ";
                    string text6 = Math.Round(double.Parse(position.z.ToString()), 0).ToString();
                    GUILayout.Label(
                        string.Concat(
                            new string[]
                            {
                                string.Format(
                                    "<b><color=#"
                                        + PlayerPrefs.GetString("CoordsHex", "8a2be2")
                                        + ">",
                                    Array.Empty<object>()
                                ),
                                text,
                                text2,
                                text3,
                                text4,
                                text5,
                                text6,
                                "</color></b>"
                            }
                        ),
                        new GUILayoutOption[0]
                    );
                }

                if (PlayerPrefsX.GetBool("Aimbot"))
                {
                    this.Aimbot();
                }
                if (PlayerPrefsX.GetBool("ESP"))
                {
                    foreach (Actor actor in ActorManager.instance.actors)
                    {
                        if (actor != player)
                        {
                            Vector3 vector = Camera.main.WorldToScreenPoint(
                                actor.transform.position
                            );
                            float dis = Mathf.Round(
                                Vector3.Distance(actor.Position(), player.Position())
                            );
                            if (!actor.dead)
                            {
                                if (vector.z > 0f)
                                {
                                    if (PlayerPrefsX.GetBool("BoxESP"))
                                    {
                                        /*
                                        RecteUtils.DrawLine(new Vector2(num5 + num7, num6 - num8 - 45f), new Vector2(num5, num6 - num8 - 45f), Color.black, num9);
                                        RecteUtils.DrawLine(new Vector2(num5 + num7 * actor.health / 100f, num6 - num8 - 45f), new Vector2(num5, num6 - num8 - 45f), RecteUtils.hpColor(actor.health), 3f);
                                        */
                                        Vector3 pivotPos = actor.transform.position; //Pivot point NOT at the origin, at the center
                                        Vector3 playerFootPos;
                                        playerFootPos.x = pivotPos.x;
                                        playerFootPos.z = pivotPos.z;
                                        playerFootPos.y = pivotPos.y - 2f;
                                        Vector3 playerHeadPos;
                                        playerHeadPos.x = pivotPos.x;
                                        playerHeadPos.z = pivotPos.z;
                                        playerHeadPos.y = pivotPos.y + 2f; //At the feet
                                        Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(
                                            playerFootPos
                                        ); //At the feet
                                        Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(
                                            playerHeadPos
                                        );
                                        float height = w2s_headpos.y - w2s_footpos.y;
                                        float widthOffset = 2f;
                                        float width = height / widthOffset;
                                        //RecteUtils.DrawLine(new Vector2(w2s_footpos.x - (width / 2), (float)Screen.height - w2s_footpos.y - height), )
                                        RecteUtils.DrawLine(
                                            new Vector2(
                                                w2s_footpos.x - (width / 2),
                                                (float)Screen.height - w2s_footpos.y - height - 15f * dis / 100f
                                            ),
                                            new Vector2(
                                                w2s_footpos.x - (width / 2) + width,
                                                (float)Screen.height - w2s_footpos.y - height - 15f * dis / 100f
                                            ),
                                            Color.black,
                                            4f
                                        );
                                        RecteUtils.DrawLine(
                                            new Vector2(
                                                w2s_footpos.x - (width / 2),
                                                (float)Screen.height - w2s_footpos.y - height - 15f * dis / 100f
                                            ),
                                            new Vector2(
                                                w2s_footpos.x
                                                    - (width / 2)
                                                    + width * actor.health / 100f,
                                                (float)Screen.height - w2s_footpos.y - height - 15f * dis / 100f
                                            ),
                                            RecteUtils.hpColor(actor.health),
                                            3f
                                        );
                                        RecteUtils.DrawBox(
                                            w2s_footpos.x - (width / 2),
                                            (float)Screen.height - w2s_footpos.y - height,
                                            width,
                                            height,
                                            Color.red,
                                            2f
                                        );
                                    }
                                    if (actor.team == 0 && PlayerPrefsX.GetBool("TeamESP"))
                                    {
                                        RecteUtils.DrawString(
                                            new Vector2(
                                                vector.x - 5f,
                                                (float)Screen.height - vector.y
                                            ),
                                            $"<b><color=#009666>{actor.name}\nHP: {Mathf.Round(actor.health)}/{actor.maxHealth}\n[{dis}m]</color></b>",
                                            true
                                        );
                                    }
                                    if (actor.team == 1 && PlayerPrefsX.GetBool("EnemyESP"))
                                    {
                                        RecteUtils.DrawString(
                                            new Vector2(
                                                vector.x - 5f,
                                                (float)Screen.height - vector.y
                                            ),
                                            $"<b><color=#937cf2>{actor.name}\nHP: {Mathf.Round(actor.health)}/{actor.maxHealth}\n[{dis}m]</color></b>",
                                            true
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException) { }
        }

        private bool IsInFieldOfView(Vector2 screenPosition)
        {
            if (PlayerPrefs.GetFloat("fovSize") <= 0f)
                return true;

            var distance = Vector2.Distance(RecteUtils.CenterOfScreen(), screenPosition);
            return distance <= PlayerPrefs.GetFloat("fovSize");
        }

        private Camera main = Camera.main;

        private GameObject closestObject;

        public void RenderMenu()
        {
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("FPSCounter")
                && GUILayout.Button(
                    "FPS Display: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("FPSCounter", true);
            }
            if (
                PlayerPrefsX.GetBool("FPSCounter")
                && GUILayout.Button(
                    "FPS Display: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("FPSCounter", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("Crosshair")
                && GUILayout.Button(
                    "Crosshair: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Crosshair", true);
            }
            if (
                PlayerPrefsX.GetBool("Crosshair")
                && GUILayout.Button(
                    "Crosshair: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Crosshair", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                PlayerPrefs.GetInt("CrosshairMode") == 0
                && GUILayout.Button(
                    "Crosshair Mode: Normal",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefs.SetInt("CrosshairMode", 1);
            }
            if (
                PlayerPrefs.GetInt("CrosshairMode") == 1
                && GUILayout.Button(
                    "Crosshair Mode: Diagonal",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefs.SetInt("CrosshairMode", 0);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("CoordsDisplay")
                && GUILayout.Button(
                    "Coordinates: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("CoordsDisplay", true);
            }
            if (
                PlayerPrefsX.GetBool("CoordsDisplay")
                && GUILayout.Button(
                    "Coordinates: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("CoordsDisplay", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("Chams")
                && GUILayout.Button(
                    "Chams: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Chams", true);
            }
            if (
                PlayerPrefsX.GetBool("Chams")
                && GUILayout.Button(
                    "Chams: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Chams", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("Tracers")
                && GUILayout.Button(
                    "Tracers: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Tracers", true);
            }
            if (
                PlayerPrefsX.GetBool("Tracers")
                && GUILayout.Button(
                    "Tracers: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Tracers", false);
            }
            GUILayout.EndHorizontal();
            if (PlayerPrefsX.GetBool("Tracers"))
            {
                if (
                    PlayerPrefs.GetInt("TracerLoc") == 0
                    && GUILayout.Button(
                        "Tracer Location: Center",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefs.SetInt("TracerLoc", 1);
                }
                if (
                    PlayerPrefs.GetInt("TracerLoc") == 1
                    && GUILayout.Button(
                        "Tracer Location: Top",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefs.SetInt("TracerLoc", 2);
                }
                if (
                    PlayerPrefs.GetInt("TracerLoc") == 2
                    && GUILayout.Button(
                        "Tracer Location: Bottom",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefs.SetInt("TracerLoc", 0);
                }
            }
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("Skeleton")
                && GUILayout.Button(
                    "Skeleton: <color=red>Disabled</color> (WIP)",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Skeleton", true);
            }
            if (
                PlayerPrefsX.GetBool("Skeleton")
                && GUILayout.Button(
                    "Skeleton: <color=lime>Enabled</color>(WIP)",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Skeleton", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("BoxESP")
                && GUILayout.Button(
                    "BoxESP: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("BoxESP", true);
            }
            if (
                PlayerPrefsX.GetBool("BoxESP")
                && GUILayout.Button(
                    "BoxESP: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("BoxESP", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("ESP")
                && GUILayout.Button(
                    "ESP: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("ESP", true);
            }
            if (
                PlayerPrefsX.GetBool("ESP")
                && GUILayout.Button(
                    "ESP: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("ESP", false);
            }
            GUILayout.EndHorizontal();
            if (PlayerPrefsX.GetBool("ESP"))
            {
                GUILayout.BeginHorizontal();
                if (
                    !PlayerPrefsX.GetBool("TeamESP")
                    && GUILayout.Button(
                        "Team ESP: <color=red>Disabled</color>",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefsX.SetBool("TeamESP", true);
                }
                if (
                    PlayerPrefsX.GetBool("TeamESP")
                    && GUILayout.Button(
                        "Team ESP: <color=lime>Enabled</color>",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefsX.SetBool("TeamESP", false);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (
                    !PlayerPrefsX.GetBool("EnemyESP")
                    && GUILayout.Button(
                        "Enemy ESP: <color=red>Disabled</color>",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefsX.SetBool("EnemyESP", true);
                }
                if (
                    PlayerPrefsX.GetBool("EnemyESP")
                    && GUILayout.Button(
                        "Enemy ESP: <color=lime>Enabled</color>",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefsX.SetBool("EnemyESP", false);
                }
                GUILayout.EndHorizontal();
            }
        }

        public void Aimbot()
        {
            Vector3 mp = new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f);
            if (Input.GetKey(KeyCode.Mouse1))
            {
                float closestdistance = Mathf.Infinity;

                foreach (Actor a in ActorManager.instance.actors)
                {
                    Vector3 v4 = Camera.main.WorldToScreenPoint(a.transform.position);
                    Vector2 v5 = new Vector2(v4.x, v4.y);
                    if (!a.dead)
                    {
                        if (a.team != 0)
                        {
                            if (closestObject == null)
                            {
                                if (IsInFieldOfView(v5))
                                {
                                    float distance = Vector3.Distance(
                                        RecteUtils.CenterOfScreen(),
                                        a.transform.position
                                    );
                                    if (distance < closestdistance) // or <= ,i will explain 2 cases.
                                    {
                                        closestdistance = distance;
                                        closestObject = a.gameObject;
                                    }
                                }
                            }
                        }
                    }
                    //when for loop ended, the closestObject is the one you need.
                }
                Vector3 v3 = Camera.main.WorldToScreenPoint(closestObject.transform.position);
                Vector2 v2 = new Vector2(v3.x, v3.y);
                if (IsInFieldOfView(v2))
                {
                    this.player.activeWeapon.transform.LookAt(
                        closestObject.transform.position
                            + Vector3.up * PlayerPrefs.GetFloat("AimOffset", .5f)
                    );
                }
                //FpsActorController.instance.fpCamera.transform.LookAt(closestObject.transform.position + Vector3.up * PlayerPrefs.GetFloat("AimOffset", .5f));
            }
        }

        private Actor target;
        private Rect rect1;

        private Actor GetTarget()
        {
            int num = ((this.player.team != 0) ? 0 : 1);
            List<Actor> list = new List<Actor>(ActorManager.AliveActorsOnTeam(num));
            list.RemoveAll((Actor actor) => actor.IsSeated() && actor.seat.vehicle.burning);
            IOrderedEnumerable<Actor> orderedEnumerable = list.OrderBy(
                (Actor x) => Vector3.Distance(x.Position(), this.player.Position())
            );
            Vector3 vector = this.player.WeaponMuzzlePosition();
            Vector3 vector2 = this.player.controller.FacingDirection();
            foreach (Actor actor2 in orderedEnumerable)
            {
                Collider[] array = (Collider[])actor2.GetFieldValue("hitboxColliders");
                RaycastHit raycastHit;
                bool flag = Physics.Raycast(
                    vector,
                    (actor2.WeaponMuzzlePosition() - vector).normalized,
                    out raycastHit
                );
                if (flag)
                {
                    bool flag2 = raycastHit.collider != null && array.Contains(raycastHit.collider);
                    if (flag2)
                    {
                        return actor2;
                    }
                }
            }
            return null;
        }

        public void Start()
        {
            this.rect1 = new Rect((float)((double)Screen.width / 2.64), 70f, 431f, 50f);
        }

        private ActorManager am;
        private Actor player;

        public void playerMenu()
        {
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("Invincible")
                && GUILayout.Button(
                    "Invincible: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Invincible", true);
                this.player.SetHealth(float.MaxValue);
            }
            if (
                PlayerPrefsX.GetBool("Invincible")
                && GUILayout.Button(
                    "Invincible: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Invincible", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("Speed")
                && GUILayout.Button(
                    "Speed Multiplier: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Speed", true);
            }
            if (
                PlayerPrefsX.GetBool("Speed")
                && GUILayout.Button(
                    "Speed Multiplier: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Speed", false);
            }
            GUILayout.EndHorizontal();

            if (PlayerPrefsX.GetBool("Speed"))
            {
                GUILayout.Label("Speed Multiplier: " + PlayerPrefs.GetFloat("SpeedMultiply"));
                PlayerPrefs.SetFloat(
                    "SpeedMultiply",
                    GUILayout.HorizontalSlider(
                        Mathf.Round(PlayerPrefs.GetFloat("SpeedMultiply")),
                        1f,
                        10f,
                        new GUILayoutOption[0]
                    )
                );
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("X: ");
            PlayerPrefs.SetString(
                "TeleportX",
                GUILayout.TextField(PlayerPrefs.GetString("TeleportX"), new GUILayoutOption[0])
            );
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Y: ");
            PlayerPrefs.SetString(
                "TeleportY",
                GUILayout.TextField(PlayerPrefs.GetString("TeleportY"), new GUILayoutOption[0])
            );
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Z: ");
            PlayerPrefs.SetString(
                "TeleportZ",
                GUILayout.TextField(PlayerPrefs.GetString("TeleportZ"), new GUILayoutOption[0])
            );
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Teleport To Coords", new GUILayoutOption[0]))
            {
                this.player.SetPositionAndRotation(
                    new Vector3(
                        float.Parse(PlayerPrefs.GetString("TeleportX")),
                        float.Parse(PlayerPrefs.GetString("TeleportY")),
                        float.Parse(PlayerPrefs.GetString("TeleportZ"))
                    ),
                    player.transform.rotation
                );
                //this.player.transform.position = new Vector3(float.Parse(PlayerPrefs.GetString("TeleportX")), float.Parse(PlayerPrefs.GetString("TeleportY")), float.Parse(PlayerPrefs.GetString("TeleportZ")));
            }
        }

        public void weaponMenu()
        {
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("InfiniteAmmo")
                && GUILayout.Button(
                    "Infinite Ammo: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("InfiniteAmmo", true);
            }
            if (
                PlayerPrefsX.GetBool("InfiniteAmmo")
                && GUILayout.Button(
                    "Infinite Ammo: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("InfiniteAmmo", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("NoRecoil")
                && GUILayout.Button(
                    "NoRecoil: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("NoRecoil", true);
            }
            if (
                PlayerPrefsX.GetBool("NoRecoil")
                && GUILayout.Button(
                    "NoRecoil: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("NoRecoil", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("NoSpread")
                && GUILayout.Button(
                    "NoSpread: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("NoSpread", true);
            }
            if (
                PlayerPrefsX.GetBool("NoSpread")
                && GUILayout.Button(
                    "NoSpread: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("NoSpread", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("RapidFire")
                && GUILayout.Button(
                    "Rapid Fire: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("RapidFire", true);
            }
            if (
                PlayerPrefsX.GetBool("RapidFire")
                && GUILayout.Button(
                    "Rapid Fire: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("RapidFire", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("Aimbot")
                && GUILayout.Button(
                    "Aimbot: <color=red>Disabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Aimbot", true);
            }
            if (
                PlayerPrefsX.GetBool("Aimbot")
                && GUILayout.Button(
                    "Aimbot: <color=lime>Enabled</color>",
                    new GUILayoutOption[] { GUILayout.Height(35f) }
                )
            )
            {
                PlayerPrefsX.SetBool("Aimbot", false);
            }
            GUILayout.EndHorizontal();
            if (PlayerPrefsX.GetBool("Aimbot"))
            {
                GUILayout.Label("Aim Offset: " + PlayerPrefs.GetFloat("AimOffset").ToString());
                PlayerPrefs.SetFloat(
                    "AimOffset",
                    GUILayout.HorizontalSlider(
                        float.Parse(
                            Math.Round((double)PlayerPrefs.GetFloat("AimOffset", 0.1f), 2)
                                .ToString()
                        ),
                        0f,
                        4f,
                        new GUILayoutOption[0]
                    )
                );
                GUILayout.BeginHorizontal();
                if (
                    !PlayerPrefsX.GetBool("aimFOV")
                    && GUILayout.Button(
                        "FOV Circle: <color=red>Disabled</color>",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefsX.SetBool("aimFOV", true);
                }
                if (
                    PlayerPrefsX.GetBool("aimFOV")
                    && GUILayout.Button(
                        "FOV Circle: <color=lime>Enabled</color>",
                        new GUILayoutOption[] { GUILayout.Height(35f) }
                    )
                )
                {
                    PlayerPrefsX.SetBool("aimFOV", false);
                }
                GUILayout.EndHorizontal();
                GUILayout.Label("FOV Size: " + PlayerPrefs.GetFloat("fovSize").ToString());
                PlayerPrefs.SetFloat(
                    "fovSize",
                    GUILayout.HorizontalSlider(
                        Mathf.Round(PlayerPrefs.GetFloat("fovSize", 10f)),
                        5f,
                        100f,
                        new GUILayoutOption[0]
                    )
                );
            }
        }

        public Vector2 scrollPos;

        public void playerList()
        {
            this.scrollPos = GUILayout.BeginScrollView(
                this.scrollPos,
                new GUILayoutOption[] { GUILayout.Height(500f) }
            );
            GUILayout.Label("Player Team");

            foreach (Actor a in ActorManager.instance.actors)
            {
                if (a != null)
                {
                    if (a.team == 0)
                    {
                        GUILayout.Button(
                            "<b><color=blue>" + a.name + "</color></b>",
                            new GUILayoutOption[] { GUILayout.Height(25f) }
                        );
                        GUILayout.BeginHorizontal();
                        if (
                            GUILayout.Button(
                                "<b><color=red>Kill</color></b>",
                                new GUILayoutOption[] { GUILayout.Height(25f) }
                            )
                        )
                        {
                            a.Kill(DamageInfo.Default);
                        }
                        if (
                            GUILayout.Button(
                                "Teleport",
                                new GUILayoutOption[] { GUILayout.Height(25f) }
                            )
                        )
                        {
                            this.player.SetPositionAndRotation(
                                a.transform.position + a.transform.up * 2f,
                                player.transform.rotation
                            );
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.Label("Enemy Team");
            foreach (Actor a in ActorManager.instance.actors)
            {
                if (a != null)
                {
                    if (a.team == 1)
                    {
                        GUILayout.Button(
                            "<b><color=red>" + a.name + "</color></b>",
                            new GUILayoutOption[] { GUILayout.Height(25f) }
                        );
                        GUILayout.BeginHorizontal();
                        if (
                            GUILayout.Button(
                                "<b><color=red>Kill</color></b>",
                                new GUILayoutOption[] { GUILayout.Height(25f) }
                            )
                        )
                        {
                            a.Kill(DamageInfo.Default);
                        }
                        if (
                            GUILayout.Button(
                                "Teleport",
                                new GUILayoutOption[] { GUILayout.Height(25f) }
                            )
                        )
                        {
                            this.player.SetPositionAndRotation(
                                a.transform.position + a.transform.up * 2f,
                                player.transform.rotation
                            );
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndScrollView();
        }

        public void MenuWindow(int wID)
        {
            bool flag = !PlayerPrefsX.GetBool("Open");
            if (!flag)
            {
                try
                {
                    GUI.color = Color.white;
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Render", new GUILayoutOption[] { GUILayout.Height(35f) }))
                    {
                        PlayerPrefs.SetInt("menuWindow", 0);
                    }
                    if (GUILayout.Button("Player", new GUILayoutOption[] { GUILayout.Height(35f) }))
                    {
                        PlayerPrefs.SetInt("menuWindow", 1);
                    }
                    if (GUILayout.Button("Weapon", new GUILayoutOption[] { GUILayout.Height(35f) }))
                    {
                        PlayerPrefs.SetInt("menuWindow", 2);
                    }
                    if (
                        GUILayout.Button(
                            "Keybinds",
                            new GUILayoutOption[] { GUILayout.Height(35f) }
                        )
                    )
                    {
                        PlayerPrefs.SetInt("menuWindow", 3);
                    }

                    if (
                        GUILayout.Button("Players", new GUILayoutOption[] { GUILayout.Height(35f) })
                    )
                    {
                        PlayerPrefs.SetInt("menuWindow", 4);
                    }
                    GUILayout.EndHorizontal();

                    if (PlayerPrefs.GetInt("menuWindow") == 0) // Render
                    {
                        this.RenderMenu();
                    }
                    if (PlayerPrefs.GetInt("menuWindow") == 1) // Player
                    {
                        this.playerMenu();
                    }
                    if (PlayerPrefs.GetInt("menuWindow") == 2) // Weapon
                    {
                        this.weaponMenu();
                    }

                    if (PlayerPrefs.GetInt("menuWindow") == 3) // Keybinds
                    { }
                    // Teleports
                    if (PlayerPrefs.GetInt("menuWindow") == 4)
                    {
                        this.playerList();
                    } // Players
                    if (GUILayout.Button("Unload Hacks"))
                    {
                        Loader.Unload();
                    }
                    GUI.DragWindow();
                }
                catch (NullReferenceException) { }
            }
        }
    }
}
