using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte_Ravenfield
{
    public class Loader
    {
        // Token: 0x0600000A RID: 10 RVA: 0x0000298E File Offset: 0x00000B8E
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<RecteManager>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Unload()
        {
            GameObject.Destroy(Load);
        }

        // Token: 0x04000010 RID: 16
        private static GameObject Load;
    }
}
