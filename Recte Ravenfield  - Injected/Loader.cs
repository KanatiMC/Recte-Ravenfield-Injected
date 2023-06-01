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
        public static void Load()
        {
            
            Loader.LoadGO = new GameObject("Recte" + RecteUtils.CreateRandomString(15));
            Loader.LoadGO.AddComponent<RecteManager>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.LoadGO);
        }
        public static void Unload()
        {
            GameObject.Destroy(LoadGO);
        }
        // Token: 0x04000010 RID: 16
        private static GameObject LoadGO;
    }
}
