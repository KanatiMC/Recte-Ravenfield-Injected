using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Recte_Ravenfield
{
    // Token: 0x02000002 RID: 2
    internal class PlayerPrefsX
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public static bool SetBool(string name, bool value)
        {
            try
            {
                PlayerPrefs.SetInt(name, (!value) ? 0 : 1);
            }
            catch
            {
                return false;
            }
            return true;
        }

        // Token: 0x06000002 RID: 2 RVA: 0x0000208C File Offset: 0x0000028C
        public static bool GetBool(string name)
        {
            return PlayerPrefs.GetInt(name) == 1;
        }

        // Token: 0x06000003 RID: 3 RVA: 0x000020A8 File Offset: 0x000002A8
        public static bool GetBool(string name, bool defaultValue)
        {
            return 1 == PlayerPrefs.GetInt(name, (!defaultValue) ? 0 : 1);
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
        public static Color GetColor(string key, Color defaultValue)
        {
            bool flag = PlayerPrefs.HasKey(key);
            Color color;
            if (flag)
            {
                color = PlayerPrefsX.GetColor(key);
            }
            else
            {
                color = defaultValue;
            }
            return color;
        }

        // Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
        public static Color GetColor(string key)
        {
            float[] floatArray = PlayerPrefsX.GetFloatArray(key);
            bool flag = floatArray.Length < 4;
            Color color;
            if (flag)
            {
                color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                color = new Color(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
            }
            return color;
        }

        // Token: 0x06000006 RID: 6 RVA: 0x00002148 File Offset: 0x00000348
        public static bool SetColor(string key, Color color)
        {
            return PlayerPrefsX.SetFloatArray(
                key,
                new float[] { color.r, color.g, color.b, color.a }
            );
        }

        // Token: 0x06000007 RID: 7 RVA: 0x0000218C File Offset: 0x0000038C
        public static float[] GetFloatArray(string key)
        {
            List<float> list = new List<float>();
            List<float> list2 = list;
            PlayerPrefsX.ArrayType arrayType = PlayerPrefsX.ArrayType.Float;
            int num = 1;
            bool flag = PlayerPrefsX.f__mgcache7 == null;
            if (flag)
            {
                PlayerPrefsX.f__mgcache7 = new Action<List<float>, byte[]>(
                    PlayerPrefsX.ConvertToFloat
                );
            }
            PlayerPrefsX.GetValue<List<float>>(
                key,
                list2,
                arrayType,
                num,
                PlayerPrefsX.f__mgcache7
            );
            return list.ToArray();
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000021E4 File Offset: 0x000003E4
        private static void GetValue<T>(
            string key,
            T list,
            PlayerPrefsX.ArrayType arrayType,
            int vectorNumber,
            Action<T, byte[]> convert
        )
            where T : IList
        {
            bool flag = PlayerPrefs.HasKey(key);
            if (flag)
            {
                byte[] array = Convert.FromBase64String(PlayerPrefs.GetString(key));
                bool flag2 = (array.Length - 1) % (vectorNumber * 4) != 0;
                if (flag2)
                {
                    Debug.LogError("Corrupt preference file for " + key);
                }
                else
                {
                    bool flag3 = (PlayerPrefsX.ArrayType)array[0] != arrayType;
                    if (flag3)
                    {
                        Debug.LogError(key + " is not a " + arrayType.ToString() + " array");
                    }
                    else
                    {
                        PlayerPrefsX.Initialize();
                        int num = (array.Length - 1) / (vectorNumber * 4);
                        for (int i = 0; i < num; i++)
                        {
                            convert(list, array);
                        }
                    }
                }
            }
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000022A4 File Offset: 0x000004A4
        public static float[] GetFloatArray(string key, float defaultValue, int defaultSize)
        {
            bool flag = PlayerPrefs.HasKey(key);
            float[] array;
            if (flag)
            {
                array = PlayerPrefsX.GetFloatArray(key);
            }
            else
            {
                float[] array2 = new float[defaultSize];
                for (int i = 0; i < defaultSize; i++)
                {
                    array2[i] = defaultValue;
                }
                array = array2;
            }
            return array;
        }

        // Token: 0x0600000A RID: 10 RVA: 0x000022EC File Offset: 0x000004EC
        public static bool SetFloatArray(string key, float[] floatArray)
        {
            PlayerPrefsX.ArrayType arrayType = PlayerPrefsX.ArrayType.Float;
            int num = 1;
            bool flag = PlayerPrefsX.f__mgcache1 == null;
            if (flag)
            {
                PlayerPrefsX.f__mgcache1 = new Action<float[], byte[], int>(
                    PlayerPrefsX.ConvertFromFloat
                );
            }
            return PlayerPrefsX.SetValue<float[]>(
                key,
                floatArray,
                arrayType,
                num,
                PlayerPrefsX.f__mgcache1
            );
        }

        // Token: 0x0600000B RID: 11 RVA: 0x0000232F File Offset: 0x0000052F
        private static void ConvertToFloat(List<float> list, byte[] bytes)
        {
            list.Add(PlayerPrefsX.ConvertBytesToFloat(bytes));
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002340 File Offset: 0x00000540
        private static float ConvertBytesToFloat(byte[] bytes)
        {
            PlayerPrefsX.ConvertFrom4Bytes(bytes);
            return BitConverter.ToSingle(PlayerPrefsX.byteBlock, 0);
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002364 File Offset: 0x00000564
        private static bool SetValue<T>(
            string key,
            T array,
            PlayerPrefsX.ArrayType arrayType,
            int vectorNumber,
            Action<T, byte[], int> convert
        )
            where T : IList
        {
            byte[] array2 = new byte[4 * array.Count * vectorNumber + 1];
            array2[0] = Convert.ToByte(arrayType);
            PlayerPrefsX.Initialize();
            for (int i = 0; i < array.Count; i++)
            {
                convert(array, array2, i);
            }
            return PlayerPrefsX.SaveBytes(key, array2);
        }

        // Token: 0x0600000E RID: 14 RVA: 0x000023D4 File Offset: 0x000005D4
        private static void Initialize()
        {
            bool isLittleEndian = BitConverter.IsLittleEndian;
            if (isLittleEndian)
            {
                PlayerPrefsX.endianDiff1 = 0;
                PlayerPrefsX.endianDiff2 = 0;
            }
            else
            {
                PlayerPrefsX.endianDiff1 = 3;
                PlayerPrefsX.endianDiff2 = 1;
            }
            bool flag = PlayerPrefsX.byteBlock == null;
            if (flag)
            {
                PlayerPrefsX.byteBlock = new byte[4];
            }
            PlayerPrefsX.idx = 1;
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002428 File Offset: 0x00000628
        private static bool SaveBytes(string key, byte[] bytes)
        {
            try
            {
                PlayerPrefs.SetString(key, Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
            return true;
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00002464 File Offset: 0x00000664
        private static void ConvertTo4Bytes(byte[] bytes)
        {
            bytes[PlayerPrefsX.idx] = PlayerPrefsX.byteBlock[PlayerPrefsX.endianDiff1];
            bytes[PlayerPrefsX.idx + 1] = PlayerPrefsX.byteBlock[1 + PlayerPrefsX.endianDiff2];
            bytes[PlayerPrefsX.idx + 2] = PlayerPrefsX.byteBlock[2 - PlayerPrefsX.endianDiff2];
            bytes[PlayerPrefsX.idx + 3] = PlayerPrefsX.byteBlock[3 - PlayerPrefsX.endianDiff1];
            PlayerPrefsX.idx += 4;
        }

        // Token: 0x06000011 RID: 17 RVA: 0x000024D4 File Offset: 0x000006D4
        private static void ConvertFrom4Bytes(byte[] bytes)
        {
            PlayerPrefsX.byteBlock[PlayerPrefsX.endianDiff1] = bytes[PlayerPrefsX.idx];
            PlayerPrefsX.byteBlock[1 + PlayerPrefsX.endianDiff2] = bytes[PlayerPrefsX.idx + 1];
            PlayerPrefsX.byteBlock[2 - PlayerPrefsX.endianDiff2] = bytes[PlayerPrefsX.idx + 2];
            PlayerPrefsX.byteBlock[3 - PlayerPrefsX.endianDiff1] = bytes[PlayerPrefsX.idx + 3];
            PlayerPrefsX.idx += 4;
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002542 File Offset: 0x00000742
        private static void ConvertFromFloat(float[] array, byte[] bytes, int i)
        {
            PlayerPrefsX.ConvertFloatToBytes(array[i], bytes);
        }

        // Token: 0x06000013 RID: 19 RVA: 0x0000254F File Offset: 0x0000074F
        private static void ConvertFloatToBytes(float f, byte[] bytes)
        {
            PlayerPrefsX.byteBlock = BitConverter.GetBytes(f);
            PlayerPrefsX.ConvertTo4Bytes(bytes);
        }

        // Token: 0x04000001 RID: 1
        private static int endianDiff1;

        // Token: 0x04000002 RID: 2
        private static int endianDiff2;

        // Token: 0x04000003 RID: 3
        private static int idx;

        // Token: 0x04000004 RID: 4
        private static byte[] byteBlock;

        // Token: 0x04000005 RID: 5
        [CompilerGenerated]
        private static Action<int[], byte[], int> f__mgcache0;

        // Token: 0x04000006 RID: 6
        [CompilerGenerated]
        private static Action<float[], byte[], int> f__mgcache1;

        // Token: 0x04000007 RID: 7
        [CompilerGenerated]
        private static Action<Vector2[], byte[], int> f__mgcache2;

        // Token: 0x04000008 RID: 8
        [CompilerGenerated]
        private static Action<Vector3[], byte[], int> f__mgcache3;

        // Token: 0x04000009 RID: 9
        [CompilerGenerated]
        private static Action<Quaternion[], byte[], int> f__mgcache4;

        // Token: 0x0400000A RID: 10
        [CompilerGenerated]
        private static Action<Color[], byte[], int> f__mgcache5;

        // Token: 0x0400000B RID: 11
        [CompilerGenerated]
        private static Action<List<int>, byte[]> f__mgcache6;

        // Token: 0x0400000C RID: 12
        [CompilerGenerated]
        private static Action<List<float>, byte[]> f__mgcache7;

        // Token: 0x0400000D RID: 13
        [CompilerGenerated]
        private static Action<List<Vector2>, byte[]> f__mgcache8;

        // Token: 0x0400000E RID: 14
        [CompilerGenerated]
        private static Action<List<Vector3>, byte[]> f__mgcache9;

        // Token: 0x0400000F RID: 15
        [CompilerGenerated]
        private static Action<List<Quaternion>, byte[]> f__mgcacheA;

        // Token: 0x04000010 RID: 16
        [CompilerGenerated]
        private static Action<List<Color>, byte[]> f__mgcacheB;

        // Token: 0x02000006 RID: 6
        private enum ArrayType
        {
            // Token: 0x04000031 RID: 49
            Float,

            // Token: 0x04000032 RID: 50
            Int32,

            // Token: 0x04000033 RID: 51
            Bool,

            // Token: 0x04000034 RID: 52
            String,

            // Token: 0x04000035 RID: 53
            Vector2,

            // Token: 0x04000036 RID: 54
            Vector3,

            // Token: 0x04000037 RID: 55
            Quaternion,

            // Token: 0x04000038 RID: 56
            Color
        }
    }
}
