
using System;
using UnityEngine;

namespace Game.World
{
    public class NoiseMap
    {
        public static float PerlinNoiseScale(int x, int y, int s_seed ,float scale)
        {
            return Mathf.PerlinNoise((x + s_seed) * scale, (y + s_seed) * scale);
        }
    }
}