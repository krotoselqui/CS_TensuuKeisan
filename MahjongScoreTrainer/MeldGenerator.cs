using System;

namespace MahjongScoreTrainer
{
    static class MeldGenerator
    {
        public static void MakeRandomShuntsu(int jouken, ref int[] maisu, out string str_shuntsu, out int[] shuntsu_xs, int setClr = -1, int setNum = -1)
        {



            string strmenz = "";
            const int sizeOfMenz = 3;
            shuntsu_xs = new int[sizeOfMenz] { 0, 0, 0 };


            bool maisuSufficient = false;

            //string tbl = "";
            //for (int ptclr = 0; ptclr < 3; ptclr++)
            //{
            //    for (int ptnum = 1; ptnum <= 9; ptnum++)
            //    {

            //        tbl += maisu[(ptclr * 9 + ptnum)].ToString();

            //    }
            //    Debug.WriteLine(tbl);
            //    tbl = "";
            //}

            while (!maisuSufficient)
            {

                int clr = Program.r.Next(3); //色 0-2
                int num = Program.r.Next(7) + 1; //数 1-7

                if (setClr != -1) clr = setClr;
                if (setNum != -1) num = setNum;

                //条件設定　ここから====================================================

                if (jouken == 8) //断幺九
                {
                    if (num == 1 || num == 7) { continue; }
                }
                else if (jouken == 23) //混全帯幺九
                {
                    if (num != 1 && num != 7) { continue; }
                }
                else if (jouken == 33) //純全帯幺九
                {
                    if (num != 1 && num != 7) { continue; }
                }
                else if (jouken == 34) //混一色
                {

                }
                else if (jouken == 43) //緑一色
                {
                    num = 2;
                }


                //条件設定　ここまで====================================================

                for (int i = 0; i < shuntsu_xs.Length; i++)
                {
                    shuntsu_xs[i] = clr * 9 + num + i;
                    if (maisu[shuntsu_xs[i]] <= 0)
                    {
                        continue;
                    }
                }

                if ((maisu[shuntsu_xs[0]] >= 1) && (maisu[shuntsu_xs[1]] >= 1) && (maisu[shuntsu_xs[2]] >= 1))
                {
                    maisuSufficient = true;
                }

            }

            for (int i = 0; i < shuntsu_xs.Length; i++)
            {

                string s = "";// XS_PAI_STR[shuntsu_xs[i]];
                maisu[shuntsu_xs[i]] -= 1;
                strmenz += s;

            }

            str_shuntsu = strmenz;

        }

    }
}
