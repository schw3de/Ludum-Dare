using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace schw3de.ld.utils
{
    public static class Extensions
    {
        public static void SetMaterial(this MeshRenderer meshRenderer, Material material)
        {
            if(meshRenderer.material == material)
            {
                return;
            }

            meshRenderer.material = material;
        }
    }
}
