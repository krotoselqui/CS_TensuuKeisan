using System;

namespace MahjongScoreTrainer
{
    class HandDecomposition
    {

        public int ag_type;
        public int ag_index;
        public int ag_nbm;
        public int furo_suu;
        public int[,] th_info;
        public int[] th_typecount;
        public bool menzenbreak;


        public HandDecomposition(int ag_type, int ag_index, int ag_nbm, int furo_suu, int[,] th_info, int[] th_typecount, bool menzenbreak)
        {

            this.ag_type = ag_type;
            this.ag_index = ag_index;
            this.ag_nbm = ag_nbm;
            this.furo_suu = furo_suu;
            this.th_info = th_info;
            this.th_typecount = th_typecount;
            this.menzenbreak = menzenbreak;
        }
    }
}
