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


        public static void MakeRandomKotsu(int jouken, ref int[] maisu, out string str_kotsu, out int[] kotsu_xs, int setClr = -1, int setNum = -1)
        {

            string str = "";
            const int size = 3;
            kotsu_xs = new int[size] { 0, 0, 0 };

            bool maisuSufficient = false;

            while (!maisuSufficient)
            {

                int clr = Program.r.Next(4); //色 0-3
                int num = Program.r.Next(9) + 1; //数 1-9

                int clr_back = clr;
                int num_back = num;

                if (setClr != -1) clr = setClr;
                if (setNum != -1) num = setNum;

                if ((clr == 3) && (num >= 8)) continue;

                //条件設定　ここから====================================================

                if (jouken == 8) //断幺九
                {

                    if (clr == 3)
                    {
                        continue;
                    }
                    else if (num == 1 || num == 9)
                    {
                        continue;
                    }

                }
                else if (jouken == 23) //混全帯幺九
                {
                    if (clr != 3)
                    {
                        if (num != 1 && num != 9)
                        {
                            continue;
                        }
                    }
                }
                else if (jouken == 31) //混老頭
                {
                    if (clr != 3)
                    {
                        if (num != 1 && num != 9)
                        {
                            continue;
                        }
                    }
                }
                else if (jouken == 33) //純全帯幺九
                {
                    if (clr == 3) continue;
                    if (num != 1 && num != 9)
                    {
                        continue;
                    }
                }
                else if (jouken == 34) //混一色
                {

                    if (clr_back == 3) clr = 3;
                    if (num >= 8) num = Program.r.Next(7) + 1;

                }
                else if (jouken == 43) //緑一色
                {

                    if (num != 2 && num != 3 && num != 4 && num != 6 && num != 8) continue;

                }
                else if (jouken == 44) //清老頭
                {
                    if (clr == 3) continue;
                    if (num != 1 && num != 9)
                    {
                        continue;
                    }
                }

                //条件設定　ここまで====================================================

                kotsu_xs[0] = clr * 9 + num;
                kotsu_xs[1] = clr * 9 + num; //必要
                kotsu_xs[2] = clr * 9 + num;

                if ((maisu[kotsu_xs[0]] >= 3))
                {
                    maisuSufficient = true;
                }

            }

            for (int i = 0; i < kotsu_xs.Length; i++)
            {

                string s = "";// XS_PAI_STR[kotsu_xs[i]];
                maisu[kotsu_xs[0]] -= 1;
                str += s;

            }

            str_kotsu = str;

        }


        public static void MakeRandomKantsu(int jouken, ref int[] maisu, out string str_kan, out int[] kan_xs, int setClr = -1, int setNum = -1)
        {
            string strkan = "";
            const int size = 4;
            kan_xs = new int[size] { 0, 0, 0, 0 };

            bool maisuSufficient = false;

            while (!maisuSufficient)
            {

                int clr = Program.r.Next(4); //色 0-3
                int num = Program.r.Next(9) + 1; //数 1-9

                int clr_back = clr;
                int num_back = num;

                if (setClr != -1) clr = setClr;
                if (setNum != -1) num = setNum;

                if ((clr == 3) && (num >= 8)) continue;

                //条件設定　ここから====================================================

                if (jouken == 8) //断幺九
                {

                    if (clr == 3)
                    {
                        continue;
                    }
                    else if (num == 1 || num == 9)
                    {
                        continue;
                    }

                }
                else if (jouken == 23) //混全帯幺九
                {
                    if (clr != 3)
                    {
                        if (num != 1 && num != 9)
                        {
                            continue;
                        }
                    }
                }
                else if (jouken == 31) //混老頭
                {
                    if (clr != 3)
                    {
                        if (num != 1 && num != 9)
                        {
                            continue;
                        }
                    }
                }
                else if (jouken == 33) //純全帯幺九
                {
                    if (clr == 3) continue;
                    if (num != 1 && num != 9)
                    {
                        continue;
                    }
                }
                else if (jouken == 34) //混一色
                {

                    if (clr_back == 3) clr = 3;
                    if (num >= 8) num = Program.r.Next(7) + 1;

                }
                else if (jouken == 43) //緑一色
                {

                    if (num != 2 && num != 3 && num != 4 && num != 6 && num != 8) continue;

                }
                else if (jouken == 44) //清老頭
                {
                    if (clr == 3) continue;
                    if (num != 1 && num != 9)
                    {
                        continue;
                    }
                }


                //条件設定　ここまで====================================================

                kan_xs[0] = clr * 9 + num;
                kan_xs[1] = clr * 9 + num; //必要
                kan_xs[2] = clr * 9 + num; //必要
                kan_xs[3] = clr * 9 + num; //必要

                if ((maisu[kan_xs[0]] == 4))
                {
                    maisuSufficient = true;
                }

            }

            for (int i = 0; i < kan_xs.Length; i++)
            {

                string s = "";// XS_PAI_STR[kan_xs[i]];
                maisu[kan_xs[0]] -= 1;
                strkan += s;

            }

            str_kan = strkan;
        }


        public static void MakeRandomAtama(int jouken, ref int[] maisu, out string str_atama, out int[] atama_xs, int setClr = -1, int setNum = -1)
        {

            string strhead = "";
            const int size = 2;
            atama_xs = new int[size] { 0, 0 };

            bool maisuSufficient = false;

            while (!maisuSufficient)
            {

                int clr = Program.r.Next(4); //色 0-3
                int num = Program.r.Next(9) + 1; //数 1-9

                int clr_back = clr;
                int num_back = num;

                if (setClr != -1) clr = setClr;
                if (setNum != -1) num = setNum;

                if ((clr == 3) && (num >= 8)) continue;

                //条件設定　ここから====================================================

                if (jouken == 7) //平和
                {

                    if (clr == 3)
                    {

                        if ((num == 1) || (num == 3) || (num == 5) || (num == 6) || (num == 7)) continue;

                    }

                }
                else if (jouken == 8) //断幺九
                {

                    if (clr == 3)
                    {
                        continue;
                    }
                    else if (num == 1 || num == 9)
                    {
                        continue;
                    }

                }
                else if (jouken == 23) //混全帯幺九
                {
                    if (clr != 3)
                    {
                        if (num != 1 && num != 9)
                        {
                            continue;
                        }
                    }
                }
                else if (jouken == 31) //混老頭
                {
                    if (clr != 3)
                    {
                        if (num != 1 && num != 9)
                        {
                            continue;
                        }
                    }
                }
                else if (jouken == 33) //純全帯幺九
                {
                    if (clr == 3) continue;
                    if (num != 1 && num != 9)
                    {
                        continue;
                    }
                }
                else if (jouken == 34) //混一色
                {

                    if (clr_back == 3) clr = 3;
                    if (num >= 8) num = Program.r.Next(7) + 1;

                }
                else if (jouken == 43) //緑一色
                {

                    if (num != 2 && num != 3 && num != 4 && num != 6 && num != 8) continue;

                }
                else if (jouken == 44) //清老頭
                {
                    if (clr == 3) continue;
                    if (num != 1 && num != 9)
                    {
                        continue;
                    }
                }


                //条件設定　ここまで====================================================

                atama_xs[0] = clr * 9 + num;
                atama_xs[1] = clr * 9 + num; //必要

                if ((maisu[atama_xs[0]] >= 2))
                {
                    maisuSufficient = true;
                }

            }

            for (int i = 0; i < atama_xs.Length; i++)
            {

                string s = "";//XS_PAI_STR[atama_xs[i]];
                maisu[atama_xs[0]] -= 1;
                strhead += s;

            }

            str_atama = strhead;

        }
    }
}