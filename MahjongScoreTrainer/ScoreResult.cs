using System;

namespace MahjongScoreTrainer
{
    class ScoreResult
    {

        public const bool KIRIAGE = false;

        public int fan;
        public int fu;
        public bool ykm;
        public int level = -1;
        public int scoresum = 0;
        public int other_pay = 0;
        public int dealer_pay = 0;
        public bool oya;

        private int[] level_fromFan = new int[] { 0, 0, 0, 0, 0, 1, 2, 2, 3, 3, 3, 4, 4, 5 };

        private int[] sc_fromLevel = new int[]{ 0,2000,3000,4000,6000,8000,
                                                  16000,24000,32000,40000,48000,
                                                  56000,64000};

        private string[] str_level = new string[] { "和了", "満貫", "跳満", "倍満", "三倍満", "役満",
                                                          "二倍役満" ,"三倍役満" ,"四倍役満" ,"五倍役満" ,"六倍役満" ,
                                                          "七倍役満", "八倍役満" };

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fan">飜数。役満の場合は何倍か</param>
        /// <param name="fu">符</param>
        /// <param name="ykm">役満か否か</param>
        /// <param name="oya">親かどうか</param>
        public ScoreResult(int fan, int fu, bool ykm, bool oya)
        {
            //Debug.Print("飜数 = " + fan.ToString() + " 符数 = " + fu.ToString() + " で初期化されました");

            this.fan = fan;
            this.fu = fu;
            this.ykm = ykm;
            this.oya = oya;

            if (this.fan < 0) this.fan = 0;

            if (!ykm)
            {
                if (this.fan >= 13)
                {
                    this.level = 5;
                }
                else
                {
                    this.level = level_fromFan[fan];
                }
            }
            else //n倍役満
            {
                this.level = fan + 4;
            }

            calcScore_fromFanFu();

        }

        public void calcScore_fromFanFu()
        {
            int tmp_sc = 0;

            if (this.fan < 5 && !ykm)
            { //満貫の確認

                tmp_sc = (int)Math.Pow(2, 5) * this.fu * (int)Math.Pow(2, this.fan - 1);
                if (tmp_sc >= 8000) this.level = 1;

                if (KIRIAGE && this.fan == 4 && this.fu == 30)
                {
                    this.level = 1;
                }
            }

            if (this.level > 0) //満貫以上
            {
                if (this.oya)
                {
                    this.scoresum = sc_fromLevel[this.level] * 6;
                    this.other_pay = sc_fromLevel[this.level] * 2;
                    this.dealer_pay = 0;
                }
                else
                {
                    this.scoresum = sc_fromLevel[this.level] * 4;
                    this.other_pay = sc_fromLevel[this.level];
                    this.dealer_pay = sc_fromLevel[this.level] * 2;
                }

            }
            else
            {
                if (this.oya)
                {
                    this.scoresum = (int)(tmp_sc * 1.5);
                    this.other_pay = (int)(this.scoresum / 3);
                    this.dealer_pay = 0;

                    //10の位で切り上げ
                    this.scoresum = ((int)((this.scoresum + 90) / 100)) * 100;
                    this.other_pay = ((int)((this.other_pay + 90) / 100)) * 100;
                }
                else
                {
                    this.scoresum = tmp_sc;
                    this.other_pay = (int)(this.scoresum / 4);
                    this.dealer_pay = (int)(this.scoresum / 2);

                    //10の位で切り上げ
                    this.scoresum = ((int)((this.scoresum + 90) / 100)) * 100;
                    this.other_pay = ((int)((this.other_pay + 90) / 100)) * 100;
                    this.dealer_pay = ((int)((this.dealer_pay + 90) / 100)) * 100;
                }

            }
        }


    }
}
