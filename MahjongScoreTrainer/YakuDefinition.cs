using System;

namespace MahjongScoreTrainer
{
    //役が持つ性質
    //●副露が可能かどうか
    //●部分役か否か
    //●標準形である必要があるか否か
    //●待ちが限定されるか

    class YakuDefinition
    {
        public int number;
        public string name;
        public bool isAdopted, enableFuro, isBubun, needNormalForm, needSPForm, isRestrictedMachi;

        public YakuDefinition(int num, string name, bool isAdopted, bool enableFuro, bool isBubun, bool needNormalForm, bool needSPForm, bool isRestrictedMachi)
        {

            this.number = num;
            this.name = name;
            this.isAdopted = isAdopted;
            this.enableFuro = enableFuro;
            this.isBubun = isBubun;
            this.needNormalForm = needNormalForm;
            this.needSPForm = needSPForm;
            this.isRestrictedMachi = isRestrictedMachi;

        }

    }
}
