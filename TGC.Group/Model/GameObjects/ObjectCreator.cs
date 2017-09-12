﻿using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Core.SceneLoader;

namespace TGC.Group.Model.GameObjects{
    public class ObjectCreator{
        public static Random Rand { get; } = new Random(666);

        public Vector3 getRandomPositionVector(int deltaY, Mapa mapa){
            int rand1 = Rand.Next(0, 64 - 1);
            int rand2 = Rand.Next(0, 64 - 1);

            int x = mapa.heightmap.GetLength(0) * (rand1 / mapa.MapaLength);
            int z = mapa.heightmap.GetLength(1) * (rand2 / mapa.MapaLength);
            int y = mapa.heightmap[rand1, rand2];

            return new Vector3(rand1 * mapa.scaleXZ, (y - deltaY) * mapa.scaleY, rand2 * mapa.scaleXZ);
        }

        public static Vector3 getRandomScaleVector(){
            float scale = (float)Rand.NextDouble();
            if (scale < 0.4f) scale = 0.4f;
            return new Vector3(scale, scale, scale);
        }

        public List<TgcMesh> createObjects(int cantidad, String dir, int deltaY, Mapa mapa){
            TgcMesh tree = mapa.Loader.loadSceneFromFile(mapa.MediaDir + dir).Meshes[0];
            List<TgcMesh> lista = new List<TgcMesh>();
            TgcMesh instance;

            for (int i = 1; i <= cantidad; i++){
                instance = tree.createMeshInstance(tree.Name + "_" + i);
                instance.Scale = getRandomScaleVector();
                instance.Position = getRandomPositionVector(deltaY, mapa);
                instance.Transform = Matrix.Scaling(instance.Scale) * Matrix.Translation(instance.Position);
                instance.updateBoundingBox();
                instance.AlphaBlendEnable = true;
                lista.Add(instance);
            }
            return lista;
        }
    }
}
