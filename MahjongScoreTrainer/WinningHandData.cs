using System;

namespace MahjongScoreTrainer
{
    class WinningHandData
    {


        public int[] tehai;
        public int[][] furotehai;
        public int[] furotype;
        public int agarixs;

        public int bakaze;
        public int jikaze;

        public bool istsumoagari;


        public WinningHandData(int[] tehai, int[][] furotehai, int[] furotype, int agarixs, int bakaze, int jikaze, bool istsumoagari)
        {


            this.tehai = tehai;
            this.furotehai = furotehai;
            this.furotype = furotype;
            this.agarixs = agarixs;

            this.bakaze = bakaze;
            this.jikaze = jikaze;

            this.istsumoagari = istsumoagari;


        }
    }
}
