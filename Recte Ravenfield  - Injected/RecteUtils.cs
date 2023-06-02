using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte_Ravenfield
{
    internal static class RecteUtils
    {
        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

        public static object GetFieldValue(this object instance, string fieldName)
        {
            FieldInfo field = instance
                .GetType()
                .GetField(
                    fieldName,
                    BindingFlags.Instance
                        | BindingFlags.Static
                        | BindingFlags.Public
                        | BindingFlags.NonPublic
                );
            return (field == null) ? null : field.GetValue(instance);
        }

        public static int HexToDec(string Hex)
        {
            return Convert.ToInt32(Hex, 16);
        }

        public static string CreateRandomString(int _length)
        {
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-=_{}|;':";
            char[] array = new char[_length];
            for (int i = 0; i < _length; i++)
            {
                array[i] = text[UnityEngine.Random.Range(0, text.Length)];
            }
            return new string(array);
        }

        public static void DrawBox(float x, float y, float w, float h, Color color, float thickness)
        {
            DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
            DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
            DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
            DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
        }

        public static Color hpColor(float hp)
        {
            Color c = new Color();
            int N = 4;
            float M = 1.5f;
            float healthCalc = (float)(((float)hp / (float)100)) * 100f;
            float N_root = (float)Mathf.Pow((healthCalc / 100f), (1f / M));
            float N_power = (float)Mathf.Pow((healthCalc / 100f), N);

            if (healthCalc < 50)
            {
                c = Color.Lerp(Color.red, Color.yellow, (float)N_root);
            }
            else if (healthCalc >= 50)
            {
                c = Color.Lerp(Color.yellow, Color.green, (float)N_power);
            }
            return c;
        }

        // Token: 0x06001BBD RID: 7101 RVA: 0x0002D219 File Offset: 0x0002B419
        public static float HexToFloatNormalized(string Hex)
        {
            return (float)RecteUtils.HexToDec(Hex) / 255f;
        }

        public static void DrawCircle(
            Vector2 center,
            float radius,
            Color color,
            float width,
            int segmentsPerQuarter
        )
        {
            GUI.color = color;
            int totalSegments = segmentsPerQuarter * 4;
            float step = 1f / totalSegments;
            var lastV = center + new Vector2(radius, 0);

            for (int i = 1; i <= totalSegments; ++i)
            {
                float t = i * step;
                var currentV =
                    center
                    + new Vector2(
                        radius * Mathf.Cos(2 * Mathf.PI * t),
                        radius * Mathf.Sin(2 * Mathf.PI * t)
                    );
                DrawLine(lastV, currentV, width, color);
                lastV = currentV;
            }
        }

        public static Vector2 CenterOfScreen()
        {
            return new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
        }

        public static void DrawLine(
            Vector2 lineStart,
            Vector2 lineEnd,
            float thickness,
            Color color
        )
        {
            GUI.color = color;

            var vector = lineEnd - lineStart;
            float pivot = /* 180/PI */
                Mathf.Rad2Deg * Mathf.Atan(vector.y / vector.x);
            if (vector.x < 0f)
                pivot += 180f;

            thickness = Mathf.Max(thickness, 1f);
            int yOffset = (int)Mathf.Ceil(thickness / 2);

            GUIUtility.RotateAroundPivot(pivot, lineStart);
            GUI.DrawTexture(
                new Rect(lineStart.x, lineStart.y - yOffset, vector.magnitude, thickness),
                Texture2D.whiteTexture
            );
            GUIUtility.RotateAroundPivot(-pivot, lineStart);
        }

        public static void DrawCrosshair(Vector2 position, float size, Color color, float thickness)
        {
            //Pasted From https://github.com/sailro/EscapeFromTarkov-Trainer/blob/master/UI/Render.cs
            GUI.color = color;
            var texture = Texture2D.whiteTexture;
            GUI.DrawTexture(
                new Rect(position.x - size, position.y, size * 2 + thickness, thickness),
                texture
            );
            GUI.DrawTexture(
                new Rect(position.x, position.y - size, thickness, size * 2 + thickness),
                texture
            );
        }

        // Token: 0x06001BBE RID: 7102 RVA: 0x001070E8 File Offset: 0x001052E8
        public static Color GetColorFromString(string HexCode)
        {
            float num = RecteUtils.HexToFloatNormalized(HexCode.Substring(0, 2));
            float num2 = RecteUtils.HexToFloatNormalized(HexCode.Substring(2, 2));
            float num3 = RecteUtils.HexToFloatNormalized(HexCode.Substring(4, 2));
            return new Color(num, num2, num3);
        }

        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
        {
            Matrix4x4 matrix = GUI.matrix;
            bool flag = !RecteUtils.lineTex;
            if (flag)
            {
                RecteUtils.lineTex = new Texture2D(1, 1);
            }
            Color color2 = GUI.color;
            GUI.color = color;
            float num = Vector3.Angle(pointB - pointA, Vector2.right);
            bool flag2 = pointA.y > pointB.y;
            if (flag2)
            {
                num = -num;
            }
            GUIUtility.ScaleAroundPivot(
                new Vector2((pointB - pointA).magnitude, width),
                new Vector2(pointA.x, pointA.y + 0.5f)
            );
            GUIUtility.RotateAroundPivot(num, pointA);
            GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), RecteUtils.lineTex);
            GUI.matrix = matrix;
            GUI.color = color2;
        }

        public static void DrawBoneLine(
            Vector3 w2s_objectStart,
            Vector3 w2s_objectFinish,
            Color color
        )
        {
            if (w2s_objectStart != null && w2s_objectFinish != null)
            {
                RecteUtils.DrawLine(
                    new Vector2(w2s_objectStart.x - 5f, (float)Screen.height - w2s_objectStart.y),
                    new Vector2(w2s_objectFinish.x - 5f, (float)Screen.height - w2s_objectFinish.y),
                    color,
                    3f
                );
            }
        }

        private static Dictionary<int, RecteUtils.RingArray> ringDict =
            new Dictionary<int, RecteUtils.RingArray>();

        // Token: 0x04000024 RID: 36
        private static GUIStyle __style = new GUIStyle();

        // Token: 0x04000025 RID: 37
        private static GUIStyle __outlineStyle = new GUIStyle();

        // Token: 0x04000026 RID: 38
        public static Camera cam = Camera.main;

        // Token: 0x04000027 RID: 39
        public static Texture2D lineTex;

        // Token: 0x04000028 RID: 40
        public static Texture2D boxTex;

        // Token: 0x04000029 RID: 41
        private static Texture2D aaLineTexs;

        // Token: 0x0400002A RID: 42
        private static Material blendMaterials;

        // Token: 0x0400002B RID: 43
        private static Texture2D lineTexs;

        // Token: 0x0400002C RID: 44
        private static Material blitMaterials;

        // Token: 0x0400002D RID: 45
        public static Material mat;

        // Token: 0x0400002E RID: 46
        public static Color color;

        // Token: 0x0400002F RID: 47
        public static float radius = 5f;

        // Token: 0x02000007 RID: 7
        private class RingArray
        {
            // Token: 0x17000003 RID: 3
            // (get) Token: 0x06000044 RID: 68 RVA: 0x00007A73 File Offset: 0x00005C73
            // (set) Token: 0x06000045 RID: 69 RVA: 0x00007A7B File Offset: 0x00005C7B
            public Vector2[] Positions { get; private set; }

            // Token: 0x06000046 RID: 70 RVA: 0x00007A84 File Offset: 0x00005C84
            public RingArray(int numSegments)
            {
                this.Positions = new Vector2[numSegments];
                float num = 360f / (float)numSegments;
                for (int i = 0; i < numSegments; i++)
                {
                    float num2 = 0.017453292f * num * (float)i;
                    this.Positions[i] = new Vector2(Mathf.Sin(num2), Mathf.Cos(num2));
                }
            }
        }

        public static void DrawString(Vector2 position, string label, bool centered = true)
        {
            var content = new GUIContent(label);
            var size = StringStyle.CalcSize(content);

            var upperLeft = centered ? position - size / 2f : position;
            GUI.Label(new Rect(upperLeft, size), content);
        }

        public static void DrawString(
            Vector2 pos,
            string text,
            Color color,
            bool center = true,
            int size = 12,
            FontStyle fontStyle = FontStyle.Bold,
            int depth = 1
        )
        {
            RecteUtils.__style.fontSize = size;
            RecteUtils.__style.richText = true;
            RecteUtils.__style.normal.textColor = color;
            RecteUtils.__style.fontStyle = fontStyle;
            RecteUtils.__outlineStyle.fontSize = size;
            RecteUtils.__outlineStyle.richText = true;
            RecteUtils.__outlineStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
            RecteUtils.__outlineStyle.fontStyle = fontStyle;
            GUIContent guicontent = new GUIContent(text);
            GUIContent guicontent2 = new GUIContent(text);
            if (center)
            {
                pos.x -= RecteUtils.__style.CalcSize(guicontent).x / 2f;
            }
            switch (depth)
            {
                case 0:
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), guicontent, RecteUtils.__style);
                    break;
                case 1:
                    GUI.Label(
                        new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f),
                        guicontent2,
                        RecteUtils.__outlineStyle
                    );
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), guicontent, RecteUtils.__style);
                    break;
                case 2:
                    GUI.Label(
                        new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f),
                        guicontent2,
                        RecteUtils.__outlineStyle
                    );
                    GUI.Label(
                        new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f),
                        guicontent2,
                        RecteUtils.__outlineStyle
                    );
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), guicontent, RecteUtils.__style);
                    break;
                case 3:
                    GUI.Label(
                        new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f),
                        guicontent2,
                        RecteUtils.__outlineStyle
                    );
                    GUI.Label(
                        new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f),
                        guicontent2,
                        RecteUtils.__outlineStyle
                    );
                    GUI.Label(
                        new Rect(pos.x, pos.y - 1f, 300f, 25f),
                        guicontent2,
                        RecteUtils.__outlineStyle
                    );
                    GUI.Label(
                        new Rect(pos.x, pos.y + 1f, 300f, 25f),
                        guicontent2,
                        RecteUtils.__outlineStyle
                    );
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), guicontent, RecteUtils.__style);
                    break;
            }
        }
    }
}
