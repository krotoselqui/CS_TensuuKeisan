using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ConsoleApplication9
{

    //役が持つ性質
    //●副露が可能かどうか
    //●部分役か否か
    //●標準形である必要があるか否か
    //●待ちが限定されるか

    class YAKU
    {
        public int number;
        public string name;
        public bool isAdopted, enableFuro, isBubun, needNormalForm, needSPForm, isRestrictedMachi;

        public YAKU(int num, string name, bool isAdopted, bool enableFuro, bool isBubun, bool needNormalForm, bool needSPForm, bool isRestrictedMachi)
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

    class Program
    {
        public static System.Random r = new System.Random(Environment.TickCount);

        public readonly static string[] YAKU_STR = 
        {	//// 一飜
	        "門前清自摸和","立直","一発","槍槓","嶺上開花",
	        "海底摸月","河底撈魚","平和","断幺九","一盃口",
	        "自風 東","自風 南","自風 西","自風 北",
	        "場風 東","場風 南","場風 西","場風 北",
	        "役牌 白","役牌 發","役牌 中",
	        //// 二飜
	        "両立直","七対子","混全帯幺九","一気通貫","三色同順",
	        "三色同刻","三槓子","対々和","三暗刻","小三元","混老頭",
	        //// 三飜
	        "二盃口","純全帯幺九","混一色",
	        //// 六飜
	        "清一色",
	        //// 満貫
	        "人和",
	        //// 役満
	        "天和","地和","大三元","四暗刻","四暗刻単騎","字一色",
	        "緑一色","清老頭","九蓮宝燈","純正九蓮宝燈","国士無双",
	        "国士無双１３面","大四喜","小四喜","四槓子",
	        //// 懸賞役
	        "ドラ","裏ドラ","赤ドラ"
        };

        public const int PAISTR_MODE = 1;
        public static int PaiDisp_Mode = -1;

        public static string[][] XS_PAI_STR = 
        {new string[]{ "9",
            "q","w","e","r","t","y","u","i","o",
            "z","x","c","v","b","n","m",",",".",
            "a","s","d","f","g","h","j","k","l",
            "1","2","3","4","5","6","7"
        },
        new string[]{ "■",
            "一","二","三","四","伍","六","七","八","九",
            "①","②","③","④","⑤","⑥","⑦","⑧","⑨",
            "１","２","３","４","５","６","７","８","９",
            "東","南","西","北","白","發","中"
        }};

        public static string[][] XS_PAI_STR_yoko = 
        {new string[] { ")",
            "Q","W","E","R","T","Y","U","I","O",
            "Z","X","C","V","B","N","M","<",">",
            "A","S","D","F","G","H","J","K","L",
            "!","\"","#","$","%","&","'"
        },
        new string[] { "□",
            "(一)","(二)","(三)","(四)","(伍)","(六)","(七)","(八)","(九)",
            "(①)","(②)","(③)","(④)","(⑤)","(⑥)","(⑦)","(⑧)","(⑨)",
            "(１)","(２)","(３)","(４)","(５)","(６)","(７)","(８)","(９)",
            "(東)","(南)","(西)","(北)","(白)","(發)","(中)"
        }};


        public static string[] FUROTYPE_STR = { 
             "雀頭", //0
             "暗刻", //1
             "明刻", //2
             "暗槓", //3
             "明槓", //4
             "暗順", //5
             "明順"};//6



        static void SortTehai(ref int[] tehai)
        {
            int[] backup = new int[tehai.Length];
            bool machiMigrated = false;
            for (int i = 0; i < tehai.Length; i++)
            {
                backup[i] = tehai[i];
            }

            int count = 0;

            for (int i = 1; i < 35; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < tehai.Length; k++)
                    {

                        if (backup[k] == i)
                        {
                            backup[k] = 0;
                            //if ((machi == k) && (!machiMigrated))
                            //{
                            //    machi = count;
                            //    machiMigrated = true;
                            //}
                            tehai[count] = i;
                            count++;
                        }

                    }
                }
            }

        }

        static int XBtoXS(int xb)
        {
            int xs = -1;
            if ((xb >= 136) || (xb < 0)) return 0;
            xs = (int)(xb / 4) + 1;
            return xs;
        }



        static void Tedukuri_Test()
        {

            int[] paiyama = new int[136];
            for (int i = 0; i < 136; i++)
            {
                int x = r.Next(i);
                paiyama[i] = paiyama[x];
                paiyama[x] = i;
            }

            string str = "";
            for (int i = 0; i < 136; i++)
            {
                if (i % 17 == 0 && i != 0)
                {
                    Console.WriteLine(str);
                    Console.WriteLine();
                    str = "";
                }

                //str += paiyama[i].ToString("000");
                //str += ",";

                int tmp_xs = XBtoXS(paiyama[i]);
                str += XS_PAI_STR[1][tmp_xs];

            }

            Console.WriteLine("");

            int[] haipai = new int[14];

            for (int i = 0; i < haipai.Length; i++)
            {
                haipai[i] = paiyama[i];
            }

            string enteredstring = " ";
            while (enteredstring == " ")
            {
                Console.Write("Enterで確認");
                try
                { enteredstring = Console.ReadLine(); }
                catch
                { }
            }






        }

        static void Main(string[] args)
        {

            //Tedukuri_Test();

            //for (int i = 0; i < YAKU_STR.Length; i++)
            //{
            //    string s = i.ToString("00") + " : ";
            //    s += YAKU_STR[i];
            //    Debug.Print(s);

            //}



            for (int i = 0; i < 15; i++)
            {
                Debug.Print("役の数 = " + i.ToString());
                Debug.Print(((int)((i + 1) / 2)).ToString());


            }


            int mondai_suu = -1;
            int seed = -2;



            Console.WriteLine("");
            Console.WriteLine("                        　　ﾜｧｲ                   ");
            Console.WriteLine("・゜・*:.｡..｡.:*・゜(n'∀')ηﾟ・*:.｡. .｡.:*・゜・*");
            Console.WriteLine("                        　　                      ");
            Console.WriteLine("・゜・*:.｡  よい子のための点数計算  ｡ .｡.:*・゜・*");
            //Console.WriteLine("                   [ 試用版 ]   　　              ");
            Console.WriteLine("                        　　                      ");
            Console.WriteLine("・゜・*:.｡..｡.:*・*:.｡. .｡.:*ﾟ・*:.｡. .｡.:*・゜・*");
            Console.WriteLine("");

            #region ダイアログ

            //mondai_suu = 1;
            if (mondai_suu >= 1)
            {
                Console.WriteLine("問題数を入力 : " + mondai_suu.ToString());
            }
            while (mondai_suu < 1)
            {
                Console.Write("問題数を入力 : ");
                try
                { mondai_suu = int.Parse(Console.ReadLine()); }
                catch { }
            }

            while (seed < -1)
            {
                Console.Write("シード値を入力（ ランダムなら: -1 ）: ");
                try
                { seed = int.Parse(Console.ReadLine()); }
                catch { }
            }

            while (PaiDisp_Mode != 0 && PaiDisp_Mode != 1)
            {
                Console.Write("出力形式を入力（ 専用フォント: 0 通常の牌記号: 1 ）: ");
                try
                { PaiDisp_Mode = int.Parse(Console.ReadLine()); }
                catch { }
            }

            #endregion

            if (seed != -1)
            {
                r = new System.Random(seed);
            }

            System.IO.StreamWriter sw_q;
            string datenow_str = DateTime.Now.ToString();
            string stCurrentDir = System.IO.Directory.GetCurrentDirectory();
            string filename_q = "question.txt";
            sw_q = new System.IO.StreamWriter(filename_q, false, System.Text.Encoding.GetEncoding("shift_jis"));

            System.IO.StreamWriter sw_a;
            string filename_a = "answer.txt";
            sw_a = new System.IO.StreamWriter(filename_a, false, System.Text.Encoding.GetEncoding("shift_jis"));

            System.IO.StreamWriter sw_y;
            string filename_y = "answer_yaku.txt";
            sw_y = new System.IO.StreamWriter(filename_y, false, System.Text.Encoding.GetEncoding("shift_jis"));



            var yaku = new YAKU[YAKU_STR.Length];
            int[] yaku_List = new int[34] {  7,  8,  9, 14, 18, 19, 20, 22, 
                                            23, 24, 25, 26, 27, 28, 29, 30, 
                                            31, 32, 33, 34, 35, 39, 40, 41, 
                                            42, 43, 44, 45, 46, 47, 48, 49, 
                                            50, 51 };

            //普段
            int[] yaku_AppearFreq = new int[34]{ 800,1000, 500, 500, 500, 500, 500, 200, 
                                                 200, 200, 200,  80, 200, 200, 200, 200, 
                                                 200, 100, 100, 100, 100,  20,  20,   1, 
                                                   2,   1,   1,   3,   1,   3,   1,   1, 
                                                   3,   0 };

            string[] mirudake = new string[34] { "平和",       "断幺九",      "一盃口",       "場風 東",    "役牌 白",     "役牌 發",   "役牌 中",       "七対子",
                                                "混全帯幺九",  "一気通貫",    "三色同順",     "三色同刻",   "三槓子",      "対々和",    "三暗刻",        "小三元",
                                                "混老頭",      "二盃口",      "純全帯幺九",   "混一色",     "清一色",      "大三元",    "四暗刻",        "四暗刻単騎",
                                                "字一色",      "緑一色",      "清老頭",       "九蓮宝燈",   "純正九蓮宝燈","国士無双",  "国士無双１３面","大四喜",
                                                "小四喜",      "四槓子"};

            ////まんべんなく調べる時用
            //int[] yaku_AppearFreq = new int[34]{ 1, 1, 1, 1, 1, 1, 1, 1, 
            //                                     1, 1, 1, 1, 1, 1, 1, 1, 
            //                                     1, 1, 1, 1, 1, 1, 1, 1, 
            //                                     1, 1, 1, 1, 1, 1, 1, 1, 
            //                                     1, 1 };

            int[] yaku_ResultCount = new int[34];
            for (int y = 0; y < yaku_ResultCount.Length; y++)
            {
                yaku_ResultCount[y] = 0;
            }

            int[] yaku_startFreq = new int[34];
            int[] yaku_endFreq = new int[34];

            int Freq_pos = 0;
            int Freq_MAX = 0;

            for (int i = 0; i < yaku_AppearFreq.Length; i++)
            {
                //Debug.Print(yaku_AppearFreq[i].ToString());

                string tmp_freq_view = i.ToString("00") + " : ";

                yaku_startFreq[i] = Freq_pos;
                tmp_freq_view += yaku_startFreq[i].ToString("0000");

                tmp_freq_view += " to ";

                Freq_pos += yaku_AppearFreq[i];

                yaku_endFreq[i] = Freq_pos - 1;
                tmp_freq_view += yaku_endFreq[i].ToString("0000");

                //Console.WriteLine(tmp_freq_view);
                //Debug.Print(tmp_freq_view);
            }

            Freq_MAX = Freq_pos;

            Console.WriteLine("");
            Console.WriteLine("                 |Start             |Fin");
            Console.Write("Processing...    ");
            int max_bar = 20;
            int cur_bar = 0;

            int cs_left = Console.CursorLeft;
            int cs_top = Console.CursorTop;

            for (int i = 0; i < mondai_suu; i++)
            {

                Console.SetCursorPosition(cs_left, cs_top);

                while ((i * 100 / mondai_suu) >= (cur_bar * 100 / max_bar))
                {
                    Console.Write("|");
                    cur_bar++;
                }

                cs_left = Console.CursorLeft;
                cs_top = Console.CursorTop;
                Console.SetCursorPosition(cs_left, cs_top);
                Console.Write(" " + (i + 1).ToString() + " / " + mondai_suu.ToString());

                //Console.SetCursorPosition(0, cs_top + 1);
                //string paddin = "";
                //for (int ff = 0; ff < i % 100; ff++)
                //{
                //    paddin += "";
                //}
                Console.Write("   +｡ﾟφ(ゝω・｀ )+｡ﾟ ｶｷｶｷ");



                string tmp_rndFreq = "rndFreq = ";

                int rndFreq = r.Next(Freq_MAX);
                tmp_rndFreq += rndFreq.ToString("000");

                int rndYaku = -1;

                for (int y = 0; y < yaku_AppearFreq.Length; y++)
                {

                    if (yaku_startFreq[y] <= rndFreq && rndFreq <= yaku_endFreq[y])
                    {
                        rndYaku = yaku_List[y];
                        break;
                    }

                }


                //Console.WriteLine(i.ToString("0000000") + " yaku : " + rndYaku);
                //Debug.Print(i.ToString("0000000") + " yaku : " + rndYaku);



                //Debug.Print("◇ 問題 " + (i + 1).ToString("") + " ◇");
                //Debug.Print("");
                //Console.WriteLine("◇ 問題 " + (i + 1).ToString("") + " ◇");
                //Console.WriteLine();


                //【役rndYakuの条件を満たした手牌を作成】

                int[] tmp_tehai;
                int[][] tmp_furotehai;
                int[] tmp_furotype;
                int tmp_machixs;

                //【任意の役の手牌を生成】
                //rndYaku = 47;

                string tmpstr = makeYaku(rndYaku, out tmp_tehai, out  tmp_furotehai, out tmp_furotype, out tmp_machixs);

                #region 手牌を手動で作るとき
                //tmpstr = "補正中!";
                //tmp_tehai = new int[] { 1,1,3,3,5,5,6,6,7,7,8,8,9,9 };
                //tmp_machixs = 9;
                //tmp_furotehai = new int[4][];
                //tmp_furotehai[0] = new int[2] { 0, 0 };
                //tmp_furotehai[1] = new int[2] { 0, 0 };
                //tmp_furotehai[2] = new int[2] { 0, 0 };
                //tmp_furotehai[3] = new int[2] { 0, 0 };
                //tmp_furotype = new int[4];
                #endregion

                //【手牌を表示】
                string tx_q = "";
                if (PaiDisp_Mode == 1) tx_q += "[" + (i + 1).ToString() + "]";
                sw_q.WriteLine(tx_q + tmpstr);
                //Console.WriteLine(tmpstr);
                Debug.Print(tmpstr);

                //Console.WriteLine();
                //Debug.Print("");

                //【詳細の手牌を表示】

                #region 手牌詳細表示

                string th_str = "";

                for (int t = 0; t < tmp_tehai.Length; t++)
                {
                    if (tmp_tehai[t] == 0) break;
                    th_str += XS_PAI_STR[PaiDisp_Mode][tmp_tehai[t]];
                    th_str += "";
                }

                //Debug.Print("手牌 = " + th_str + "");
                //Console.WriteLine("手牌 = " + th_str + "");

                //Debug.Print("待ち = " + XS_PAI_STR[PaiDisp_Mode][tmp_machixs] + "");
                //Console.WriteLine("待ち = " + XS_PAI_STR[PaiDisp_Mode][tmp_machixs] + "");

                string fr_str = "";
                string frtype_str = "";

                for (int f = 0; f < tmp_furotehai.Length; f++)
                {
                    //if (tmp_furotype[f] == 0) break;

                    for (int m = 0; m < tmp_furotehai[f].Length; m++)
                    {
                        if (tmp_furotehai[f][m] == 0) break;
                        fr_str += XS_PAI_STR[PaiDisp_Mode][tmp_furotehai[f][m]];
                        fr_str += "";
                    }

                    frtype_str = FUROTYPE_STR[tmp_furotype[f]];
                    if (tmp_furotype[f] == 0) frtype_str = "----";

                    //Debug.Print("副露[" + f.ToString() + "] = 【" + frtype_str + "】" + fr_str + "");
                    //Console.WriteLine("副露[" + f.ToString() + "] = 【" + frtype_str + "】" + fr_str + "");

                    fr_str = "";

                }

                #endregion

                //【役の判定】

                const int int_TON = 0;
                const int int_NAN = 1;
                const int int_XIA = 2;
                const int int_PEI = 3;

                const bool bool_TSUMO = true;
                const bool bool_RON = false;


                AGARI_DATA[] ad = new AGARI_DATA[4];
                string[] ac_str = new string[4]{
                 "東/東/ツモ","東/東/ロン",
                "東/西/ツモ","東/西/ロン"};

                int[] agaricond_ba = new int[4] { int_TON, int_TON, int_TON, int_TON };
                int[] agaricond_ie = new int[4] { int_TON, int_TON, int_XIA, int_XIA };
                bool[] agaricond_istm = new bool[4] { bool_TSUMO, bool_RON, bool_TSUMO, bool_RON };

                for (int j = 0; j < 4; j++)
                {
                    ad[j] = new AGARI_DATA(tmp_tehai, tmp_furotehai, tmp_furotype, tmp_machixs, agaricond_ba[j], agaricond_ie[j], agaricond_istm[j]);
                }

                YAKU_HANTEI yh = new YAKU_HANTEI();

                string kaitou_str = "[" + (i + 1).ToString() + "] ";
                string yaku_str = "";
                sw_y.WriteLine("[" + (i + 1).ToString() + "] ");


                //アガリ状態だけループして調査
                for (int a_cond = 0; a_cond < 4; a_cond++)
                {
                    string yx = "" + ac_str[a_cond] + " ";
                    bool[] list;
                    int Fan, Fu;
                    SCORE_DATA sd;

                    int x = yh.Yaku_Keishiki(out list, out sd, ad[a_cond]);

                    //Debug.Print("");
                    //Debug.Print("●" + ac_str[a_cond]);
                    //Debug.Print("");

                    #region 飜・符の文字列操作

                    string result_str = "                     ";
                    if (sd.ykm)
                    {
                        if (sd.fan != 1)
                        {
                            result_str += sd.fan.ToString() + "倍役満";
                            yx += sd.fan.ToString() + "倍役満 ";
                        }
                        else
                        {
                            result_str += "役満";
                            yx += "役満 ";
                        }
                    }
                    else
                    {
                        result_str += sd.fu.ToString() + "符 " + sd.fan.ToString() + "飜";
                        yx += sd.fu.ToString() + "符" + sd.fan.ToString() + "飜 ";
                    }

                    result_str += "   ";

                    if (ad[a_cond].istsumoagari)
                    {
                        if (ad[a_cond].jikaze == 0)
                        {
                            result_str += sd.other_pay.ToString() + " ∀ ";
                            kaitou_str += sd.other_pay.ToString() + "∀";
                        }
                        else
                        {
                            result_str += sd.other_pay.ToString() + " - " + sd.dealer_pay.ToString();
                            kaitou_str += sd.other_pay.ToString() + "-" + sd.dealer_pay.ToString();
                        }
                    }
                    else
                    {
                        result_str += sd.scoresum.ToString();
                        kaitou_str += sd.scoresum.ToString();
                    }

                    #endregion

                    string yl_str = "";
                    yh.Yakulist_view(list, out yl_str);

                    for (int y = 0; y < yaku_List.Length; y++)
                    {
                        if (list[yaku_List[y]])
                        {
                            yaku_ResultCount[y]++;
                        }
                    }

                    sw_y.WriteLine(yx + yl_str);
                    //Debug.Print(result_str);
                    if (a_cond != 3)
                    {
                        kaitou_str += " / ";
                    }

                }



                sw_a.WriteLine(kaitou_str);
                //Console.WriteLine(kaitou_str);
                //Console.WriteLine();
                //Debug.Print("------------------------------------------------------------------------------");

                //Console.WriteLine(tmp_rndFreq + " " + tmpstr);
                //Debug.Print(tmp_rndFreq + " " + tmpstr);



            }

            sw_q.Close();
            sw_a.Close();
            sw_y.Close();

            Console.WriteLine();
            Console.WriteLine(" Finished.");

            Console.WriteLine();
            for (int y = 0; y < yaku_List.Length; y++)
            {
                string yakuname = YAKU_STR[yaku_List[y]];
                Console.WriteLine(yakuname + " : " + yaku_ResultCount[y].ToString() + " 回");
            }

            Console.WriteLine("何かキーを押すと終了します...");
            Console.ReadKey();


        }


        static string makeYaku(int yakuNum, out int[] out_tehai, out int[][] out_furotehai, out int[] out_furotype, out int out_machi_xs)
        {
            string tehaistr = "";
            bool typeSP = false;
            bool isChitoi = false;
            bool isKokushi = false;

            if (yakuNum == 22) typeSP = true;
            if (yakuNum == 45) typeSP = true;
            if (yakuNum == 46) typeSP = true;
            if (yakuNum == 47) typeSP = true;
            if (yakuNum == 48) typeSP = true;

            int[] maisu = new int[35];
            for (int i = 0; i < maisu.Length; i++)
            {
                maisu[i] = 4;
            }

            int[] tehai = new int[20];
            int[][] furotehai = new int[4][];
            furotehai[0] = new int[4];
            furotehai[1] = new int[4];
            furotehai[2] = new int[4];
            furotehai[3] = new int[4];

            int[] furotype = new int[4];

            int count = 0;

            int[][] partsXS = new int[5][];
            partsXS[0] = new int[4];
            partsXS[1] = new int[4];
            partsXS[2] = new int[4];
            partsXS[3] = new int[4];
            partsXS[4] = new int[4];

            int[] partsType = new int[5];

            const int MENZ_ATAMA = 0;
            const int MENZ_ANKO = 1;
            const int MENZ_MINKO = 2;
            const int MENZ_ANKAN = 3;
            const int MENZ_MINKAN = 4;
            const int MENZ_SHUNTSU = 5;
            const int MENZ_FSHUNTSU = 6;

            string strparts;
            int[] xsparts;

            int machi_xs = -1;





            if (yakuNum == 7) //平和
            {

                for (int i = 0; i < 4; i++)
                {
                    makeRandomShuntsu(-1, ref maisu, out strparts, out xsparts);
                    partsXS[i] = xsparts;
                    partsType[i] = 5;
                }
                makeRandomAtama(7, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                if (tileNum(partsXS[0][0]) == 7)
                {
                    machi_xs = partsXS[0][2];
                }
                else
                {
                    machi_xs = partsXS[0][0];
                }

            }
            else if (yakuNum == 8) //断幺九
            {

                for (int i = 0; i < 4; i++)
                {
                    int shurui = r.Next(9); //弄るとフーロ率が変化(他は順子に。)
                    int furo = r.Next(2);

                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(8, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + furo;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(8, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + furo;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(8, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + furo;
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(8, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }


                }

                makeRandomAtama(8, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 9) //一盃口
            {

                //はじめの2順子

                makeRandomShuntsu(9, ref maisu, out strparts, out xsparts);
                partsXS[0] = xsparts;
                partsType[0] = 5;

                int settledClr = tileClr(xsparts[0]);
                int settledNum = tileNum(xsparts[0]);

                makeRandomShuntsu(9, ref maisu, out strparts, out xsparts, settledClr, settledNum);
                partsXS[1] = xsparts;
                partsType[1] = 5;


                for (int i = 2; i < 4; i++)
                {
                    int shurui = r.Next(9); //弄るとフーロ率が変化(他は順子に。)

                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(9, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(9, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(9, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(9, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }


                }

                makeRandomAtama(8, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 14 || yakuNum == 18 || yakuNum == 19 || yakuNum == 20) //役牌
            {
                int yakuxs_num = yakuNum - 13; //東-1 白-5 發-6 中-7

                int type = r.Next(2);
                int furo = r.Next(2);

                if (type == 0)
                {
                    makeRandomKantsu(14, ref maisu, out strparts, out xsparts, 3, yakuxs_num);
                    partsType[0] = 3 + furo;
                }
                else
                {
                    makeRandomKotsu(14, ref maisu, out strparts, out xsparts, 3, yakuxs_num);
                    partsType[0] = 1 + furo;
                }
                partsXS[0] = xsparts;

                for (int i = 1; i < 4; i++)
                {
                    type = r.Next(9); //弄るとフーロ率が変化(他は順子に。)
                    furo = r.Next(2);

                    if (type == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(14, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + furo;
                    }
                    else if (type == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(14, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + furo;

                    }
                    else if (type == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(14, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + furo;
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(14, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }


                }

                makeRandomAtama(14, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 23) //混全帯幺九
            {

                for (int i = 0; i < 4; i++)
                {
                    int shurui = r.Next(9); //弄るとフーロ率が変化(他は順子に。)
                    int furo = r.Next(2);

                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(23, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + furo;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(23, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + furo;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(23, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + furo;
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(23, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }


                }

                makeRandomAtama(23, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 24) //一気通貫
            {

                //はじめの3順子
                int ittsuclr = r.Next(3);


                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, ittsuclr, 1);
                partsXS[0] = xsparts;
                partsType[0] = 5 + r.Next(2);

                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, ittsuclr, 4);
                partsXS[1] = xsparts;
                partsType[1] = 5 + r.Next(2);

                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, ittsuclr, 7);
                partsXS[2] = xsparts;
                partsType[2] = 5 + r.Next(2);


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(9); //弄るとフーロ率が変化(他は順子に。)
                    int furo = r.Next(4);
                    if (furo != 0)
                    {
                        furo = 0;
                    }
                    else
                    {
                        furo = 1;
                    }

                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + furo;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + furo;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + furo;
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + furo;
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 25) //三色同順
            {

                //はじめの3順子
                int sansyokunum = r.Next(7) + 1;
                int furo_bairitsu = 10; //でかいほど面前

                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, 0, sansyokunum);
                partsXS[0] = xsparts;
                partsType[0] = 5 + retOne(furo_bairitsu);

                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, 1, sansyokunum);
                partsXS[1] = xsparts;
                partsType[1] = 5 + retOne(furo_bairitsu);

                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, 2, sansyokunum);
                partsXS[2] = xsparts;
                partsType[2] = 5 + retOne(furo_bairitsu);


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(9); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 26) //三色同刻
            {

                //はじめの3刻子
                int sansyokunum = r.Next(7) + 1;
                int kan_bairitsu = 10;
                int furo_bairitsu = 10; //でかいほど鳴かない

                for (int i = 0; i < 3; i++)
                {
                    int kan = retOne(kan_bairitsu);

                    if (kan == 0)
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, i, sansyokunum);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, i, sansyokunum);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }

                }


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 27) //三槓子
            {

                //はじめの3槓子
                int sansyokunum = r.Next(7) + 1;
                int furo_bairitsu = 2; //でかいほど鳴かない

                for (int i = 0; i < 3; i++)
                {
                    makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                    partsXS[i] = xsparts;
                    partsType[i] = 3 + retOne(furo_bairitsu);
                }


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 28) //対々和
            {

                int furo_bairitsu = 2; //でかいほど鳴かない

                for (int i = 0; i < 4; i++)
                {

                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 29) //三暗刻
            {

                //はじめの3刻子
                int sansyokunum = r.Next(7) + 1;
                int furo_bairitsu = 2; //でかいほど鳴かない

                for (int i = 0; i < 3; i++)
                {

                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3;
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1;
                    }
                }


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 2;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 4;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                machi_xs = -1;

                for (int i = 0; i < 5; i++)
                {
                    int m = 4 - i;
                    int tmptype = partsType[m];
                    if ((tmptype == 0))
                    {
                        machi_xs = partsXS[m][1];
                    }
                }


            }
            else if (yakuNum == 30) //小三元
            {

                //はじめの2刻子
                int toitsuNum = r.Next(3) + 5;
                int[] kotsuNum = new int[2] { 0, 0 };
                int furo_bairitsu = 2; //でかいほど鳴かない
                if (toitsuNum == 5)
                {
                    kotsuNum[0] = 6;
                    kotsuNum[1] = 7;
                }
                else if (toitsuNum == 6)
                {
                    kotsuNum[0] = 5;
                    kotsuNum[1] = 7;
                }
                else if (toitsuNum == 7)
                {
                    kotsuNum[0] = 5;
                    kotsuNum[1] = 6;
                }
                else
                {
                    toitsuNum = 5;
                    kotsuNum[0] = 6;
                    kotsuNum[1] = 7;
                }


                for (int i = 0; i < 2; i++)
                {

                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, 3, kotsuNum[i]);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 3, kotsuNum[i]);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }

                }


                for (int i = 2; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)
                    int tmpclr = r.Next(3);

                    if (shurui == 0) //暗刻・明刻
                    {
                        //三元牌が入るとまずいので避ける

                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, tmpclr);
                        partsXS[i] = xsparts;
                        partsType[i] = 2;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        //三元牌が入るとまずいので避ける

                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, tmpclr);
                        partsXS[i] = xsparts;
                        partsType[i] = 4;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts, 3, toitsuNum);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                machi_xs = -1;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 31) //混老頭
            {

                int furo_bairitsu = 2; //でかいほど鳴かない

                for (int i = 0; i < 4; i++)
                {

                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 32) //二盃口
            {

                //はじめの4順子

                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[0] = xsparts;
                partsType[0] = 5;

                int settledClr = tileClr(xsparts[0]);
                int settledNum = tileNum(xsparts[0]);

                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, settledClr, settledNum);
                partsXS[1] = xsparts;
                partsType[1] = 5;


                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[2] = xsparts;
                partsType[2] = 5;

                settledClr = tileClr(xsparts[0]);
                settledNum = tileNum(xsparts[0]);

                makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, settledClr, settledNum);
                partsXS[3] = xsparts;
                partsType[3] = 5;

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 33) //純全帯幺九
            {

                for (int i = 0; i < 4; i++)
                {
                    int shurui = r.Next(12); //弄るとフーロ率が変化(他は順子に。)
                    int furo_bairitsu = 3;

                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 34) //混一色
            {

                int some_clr = r.Next(3);
                int kanoccurCount = 0;


                for (int i = 0; i < 4; i++)
                {
                    int shurui = r.Next(4); //弄るとフーロ率が変化(他は順子に。)
                    int tmp_clr = some_clr;
                    int tupai = r.Next(2);
                    if (tupai == 1) tmp_clr = 3;
                    int furo_bairitsu = 2;

                    if (tmp_clr != 3 && shurui == 1 && kanoccurCount >= 2) shurui = 5;
                    if (tmp_clr != 3 && shurui == 1 && i == 3) shurui = 5;

                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, tmp_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, tmp_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                        kanoccurCount++;
                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }


                }

                makeRandomAtama(8, ref maisu, out strparts, out xsparts, some_clr);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 35) //清一色
            {

                int some_clr = r.Next(3);
                int kanoccurCount = 0;


                for (int i = 0; i < 4; i++)
                {
                    int shurui = r.Next(4); //弄るとフーロ率が変化(他は順子に。)
                    int furo_bairitsu = 2;
                    if (shurui == 1 && kanoccurCount >= 2) shurui = 5;
                    if (shurui == 1 && i == 3) shurui = 5;

                    //Debug.Print("i = " + i.ToString() + " / shurui = " + shurui.ToString());

                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                        kanoccurCount++;
                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }

                    //Debug.Print("i = " + i.ToString() + " -> type" + partsType[i].ToString() + " >> " + xsparts[0].ToString());



                }

                makeRandomAtama(8, ref maisu, out strparts, out xsparts, some_clr);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 39) //大三元
            {

                //はじめの3刻子
                int kan_bairitsu = 10;
                int furo_bairitsu = 2; //でかいほど鳴かない

                for (int i = 0; i < 3; i++)
                {
                    int kan = retOne(kan_bairitsu);

                    if (kan == 0)
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 3, i + 5);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, 3, i + 5);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }

                }


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗刻・明刻
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 40) //四暗刻
            {

                //はじめの4刻子
                int kan_bairitsu = 10;

                for (int i = 0; i < 4; i++)
                {
                    int kanoccur = retOne(10);
                    if (kanoccur == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3;
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1;
                    }

                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                machi_xs = -1;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 41) //四暗刻単騎
            {

                //はじめの4刻子
                int kan_bairitsu = 10;

                for (int i = 0; i < 4; i++)
                {
                    int kanoccur = retOne(10);
                    if (kanoccur == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3;
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1;
                    }

                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                machi_xs = partsXS[4][1];

            }
            else if (yakuNum == 42) //字一色
            {

                //はじめの4刻子
                int kan_bairitsu = 10;
                int furo_bairitsu = 2;

                for (int i = 0; i < 4; i++)
                {
                    int kanoccur = retOne(10);


                    if (kanoccur == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, 3);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 3);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }

                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts, 3);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }

            }
            else if (yakuNum == 43) //緑一色
            {

                int hatsu_bairitsu = 2;
                int hatsu_kan_bairitsu = 10;
                int shuntsu_bairtisu = 2;

                //はじめの2面子
                int furo_bairitsu = 2;


                int kanoccur = retOne(hatsu_kan_bairitsu);

                if (kanoccur == 1) //暗槓・暗槓
                {
                    makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, 3, 6);
                    partsXS[0] = xsparts;
                    partsType[0] = 3 + retOne(furo_bairitsu);
                }
                else  //刻子
                {
                    makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 3, 6);
                    partsXS[0] = xsparts;
                    partsType[0] = 1 + retOne(furo_bairitsu);
                }

                int shuntsuoccur = retOne(shuntsu_bairtisu);

                if (shuntsuoccur == 1)
                {
                    makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, 2, 2);
                    partsXS[1] = xsparts;
                    partsType[1] = 5 + retOne(furo_bairitsu);
                }
                else
                {
                    makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 2);
                    partsXS[1] = xsparts;
                    partsType[1] = 1 + retOne(furo_bairitsu);
                }


                for (int i = 2; i < 4; i++)
                {
                    kanoccur = retOne(20);

                    if (kanoccur == 1) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, 2);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 2);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }

                }


                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts, 2);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }

            }
            else if (yakuNum == 44) //清老頭
            {

                int furo_bairitsu = 2; //でかいほど鳴かない

                for (int i = 0; i < 4; i++)
                {

                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 49) //大四喜
            {

                //はじめの4刻子
                int kan_bairitsu = 10;
                int furo_bairitsu = 2; //でかいほど鳴かない

                for (int i = 0; i < 4; i++)
                {
                    int kan = retOne(kan_bairitsu);

                    if (kan == 0)
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 3, i + 1);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }
                    else
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, 3, i + 1);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }

                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 50) //小四喜
            {

                //はじめの3刻子
                int toitsuNum = r.Next(4) + 1;
                int[] kotsuNum = new int[3] { 0, 0, 0 };
                int furo_bairitsu = 2; //でかいほど鳴かない

                if (toitsuNum == 1)
                {
                    kotsuNum[0] = 2;
                    kotsuNum[1] = 3;
                    kotsuNum[2] = 4;
                }
                else if (toitsuNum == 2)
                {
                    kotsuNum[0] = 1;
                    kotsuNum[1] = 3;
                    kotsuNum[2] = 4;
                }
                else if (toitsuNum == 3)
                {
                    kotsuNum[0] = 1;
                    kotsuNum[1] = 2;
                    kotsuNum[2] = 4;
                }
                else if (toitsuNum == 4)
                {
                    kotsuNum[0] = 1;
                    kotsuNum[1] = 2;
                    kotsuNum[2] = 3;
                }
                else
                {
                    toitsuNum = 1;
                    kotsuNum[0] = 2;
                    kotsuNum[1] = 3;
                    kotsuNum[2] = 4;
                }


                for (int i = 0; i < 3; i++)
                {

                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗槓・暗槓
                    {
                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, 3, kotsuNum[i]);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + retOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 3, kotsuNum[i]);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + retOne(furo_bairitsu);
                    }

                }


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)
                    int tmpclr = r.Next(3);

                    if (shurui == 0) //暗刻・明刻
                    {
                        //風牌が入るとまずいので避ける

                        makeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, tmpclr);
                        partsXS[i] = xsparts;
                        partsType[i] = 2;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        //風牌が入るとまずいので避ける

                        makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, tmpclr);
                        partsXS[i] = xsparts;
                        partsType[i] = 4;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        makeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + retOne(furo_bairitsu);
                    }


                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts, 3, toitsuNum);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                machi_xs = -1;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else if (yakuNum == 51) //四槓子
            {

                //はじめの3槓子
                int sansyokunum = r.Next(7) + 1;
                int furo_bairitsu = 2; //でかいほど鳴かない

                for (int i = 0; i < 4; i++)
                {
                    makeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                    partsXS[i] = xsparts;
                    partsType[i] = 3 + retOne(furo_bairitsu);
                }

                makeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[4] = xsparts;
                partsType[4] = 0;

                for (int i = 0; i < 5; i++)
                {
                    int tmptype = partsType[i];
                    if (machi_xs == -1 && (tmptype == 0 || tmptype == 1 || tmptype == 5))
                    {
                        machi_xs = partsXS[i][1];
                    }
                }


            }
            else
            {

            }


            if (!typeSP)
            { //七対子などの特殊形

                for (int i = 0; i < 5; i++)
                {
                    for (int k = 0; k < partsXS[i].Length; k++)
                    {
                        if (partsType[i] == 0 || partsType[i] == 1 || partsType[i] == 5)
                        {

                            tehai[count] = partsXS[i][k];
                            count++;

                        }
                        else
                        {


                        }

                    }
                }


            }
            else if (yakuNum == 22) //七対子
            {
                int pos = 0;
                int machitoitsu = r.Next(7);

                //すべての牌を強制的に二枚減らす
                for (int i = 0; i < maisu.Length; i++)
                {
                    maisu[i] -= 2;
                }

                for (int i = 0; i < 7; i++)
                {

                    makeRandomAtama(22, ref maisu, out strparts, out xsparts);
                    tehai[pos] = xsparts[0];
                    tehai[pos + 1] = xsparts[1];

                    if (machitoitsu == i) machi_xs = tehai[pos];
                    pos += 2;

                }
            }
            else if (yakuNum == 45 || yakuNum == 46) //チュウレン
            {
                int churenclr = r.Next(3);
                int pos = 0;

                for (int i = 0; i < 3; i++)
                {
                    tehai[pos] = churenclr * 9 + 1;
                    pos++;
                }

                for (int i = 2; i <= 8; i++)
                {
                    tehai[pos] = churenclr * 9 + i;
                    pos++;
                }

                for (int i = 0; i < 3; i++)
                {
                    tehai[pos] = churenclr * 9 + 9;
                    pos++;
                }

                int add_num = r.Next(9) + 1;
                int add_xs = churenclr * 9 + add_num;
                tehai[pos] = add_xs;

                machi_xs = add_xs;

                if (yakuNum == 45)
                {

                    while (add_xs == machi_xs)
                    {
                        machi_xs = (churenclr * 9) + r.Next(9) + 1;
                    }

                }

            }
            else if (yakuNum == 47 || yakuNum == 48)  //国士
            {

                int[] yaoChuArray = new int[13] { 1, 9, 10, 18, 19, 27, 28, 29, 30, 31, 32, 33, 34 };
                for (int i = 0; i < 13; i++)
                {

                    tehai[i] = yaoChuArray[i];

                }

                int add_pai = yaoChuArray[r.Next(13)];
                tehai[13] = add_pai;

                machi_xs = add_pai;

                if (yakuNum == 47)
                {
                    while (add_pai == machi_xs)
                    {
                        machi_xs = yaoChuArray[r.Next(13)];
                    }
                }

            }



            //Debug.Print(machi_xs.ToString());
            SortTehai(ref tehai);

            //Debug.Print(machi_xs.ToString());

            //ソート後
            bool machiSkipped = false;
            for (int j = 0; j < tehai.Length; j++)
            {
                if (tehai[j] == machi_xs && !machiSkipped)
                {
                    machiSkipped = true;
                    continue;
                }
                if (tehai[j] == 0) break;
                tehaistr += XS_PAI_STR[PaiDisp_Mode][tehai[j]];
            }

            if (machi_xs != -1)
            {

                tehaistr += " " + XS_PAI_STR[PaiDisp_Mode][machi_xs];

            }


            int furocount = 0;

            for (int i = 0; i < 5; i++)
            {
                int basyo = r.Next(3); //チー以外の副露者

                if (partsType[i] == 2) //明刻
                {

                    tehaistr += " ";
                    for (int m = 0; m < 3; m++)
                    {

                        furotehai[furocount][m] = partsXS[i][m];

                        if (m == basyo)
                        {
                            tehaistr += XS_PAI_STR_yoko[PaiDisp_Mode][partsXS[i][m]];
                        }
                        else
                        {
                            tehaistr += XS_PAI_STR[PaiDisp_Mode][partsXS[i][m]];
                        }
                    }
                    tehaistr += "";

                    furotype[furocount] = 2;
                    furocount++;

                }
                else if (partsType[i] == 3) //暗槓
                {

                    tehaistr += " ";
                    for (int m = 0; m < 4; m++)
                    {

                        furotehai[furocount][m] = partsXS[i][m];

                        if (m == 0 || m == 3)
                        {
                            tehaistr += XS_PAI_STR[PaiDisp_Mode][0];
                        }
                        else
                        {
                            tehaistr += XS_PAI_STR[PaiDisp_Mode][partsXS[i][m]];
                        }
                    }
                    tehaistr += "";

                    furotype[furocount] = 3;
                    furocount++;

                }
                else if (partsType[i] == 4) //明槓
                {

                    tehaistr += " ";
                    for (int m = 0; m < 4; m++)
                    {

                        furotehai[furocount][m] = partsXS[i][m];

                        if (m == 0 && basyo == 0)
                        {
                            tehaistr += XS_PAI_STR_yoko[PaiDisp_Mode][partsXS[i][m]];
                        }
                        else if (m == 1 && basyo == 1)
                        {
                            tehaistr += XS_PAI_STR_yoko[PaiDisp_Mode][partsXS[i][m]];
                        }
                        else if (m == 3 && basyo == 2)
                        {
                            tehaistr += XS_PAI_STR_yoko[PaiDisp_Mode][partsXS[i][m]];
                        }
                        else
                        {
                            tehaistr += XS_PAI_STR[PaiDisp_Mode][partsXS[i][m]];
                        }
                    }
                    tehaistr += "";

                    furotype[furocount] = 4;
                    furocount++;

                }
                else if (partsType[i] == 6) //副露順子
                {

                    int[] shuntsu_sorted = new int[3] { -1, -1, -1 };
                    switch (basyo)
                    {
                        case 0:
                            shuntsu_sorted[0] = partsXS[i][0];
                            shuntsu_sorted[1] = partsXS[i][1];
                            shuntsu_sorted[2] = partsXS[i][2];
                            break;
                        case 1:
                            shuntsu_sorted[0] = partsXS[i][1];
                            shuntsu_sorted[1] = partsXS[i][0];
                            shuntsu_sorted[2] = partsXS[i][2];
                            break;
                        case 2:
                            shuntsu_sorted[0] = partsXS[i][2];
                            shuntsu_sorted[1] = partsXS[i][0];
                            shuntsu_sorted[2] = partsXS[i][1];
                            break;
                        default:
                            shuntsu_sorted[0] = partsXS[i][0];
                            shuntsu_sorted[1] = partsXS[i][1];
                            shuntsu_sorted[2] = partsXS[i][2];
                            break;
                    }

                    tehaistr += " ";
                    for (int m = 0; m < 3; m++)
                    {

                        furotehai[furocount][m] = partsXS[i][m];

                        if (m == 0)
                        {
                            tehaistr += XS_PAI_STR_yoko[PaiDisp_Mode][shuntsu_sorted[m]];
                        }
                        else
                        {
                            tehaistr += XS_PAI_STR[PaiDisp_Mode][shuntsu_sorted[m]];
                        }
                    }
                    tehaistr += "";

                    furotype[furocount] = 6;
                    furocount++;

                }
                else
                {

                }




            }

            out_furotehai = furotehai;
            out_tehai = tehai;
            out_machi_xs = machi_xs;
            out_furotype = furotype;

            return tehaistr;
        }

        #region MenzCreate

        //====================================================================================================================================================================
        //====================================================================================================================================================================
        //==================================================================　　　　ランダム面子部品生成　　　　==============================================================
        //====================================================================================================================================================================
        //====================================================================================================================================================================

        static void makeRandomShuntsu(int jouken, ref int[] maisu, out string str_shuntsu, out int[] shuntsu_xs, int setClr = -1, int setNum = -1)
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

                int clr = r.Next(3); //色 0-2
                int num = r.Next(7) + 1; //数 1-7

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

        static void makeRandomKotsu(int jouken, ref int[] maisu, out string str_kotsu, out int[] kotsu_xs, int setClr = -1, int setNum = -1)
        {

            string str = "";
            const int size = 3;
            kotsu_xs = new int[size] { 0, 0, 0 };

            bool maisuSufficient = false;

            while (!maisuSufficient)
            {

                int clr = r.Next(4); //色 0-3
                int num = r.Next(9) + 1; //数 1-9

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
                    if (num >= 8) num = r.Next(7) + 1;

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

        static void makeRandomKantsu(int jouken, ref int[] maisu, out string str_kan, out int[] kan_xs, int setClr = -1, int setNum = -1)
        {
            string strkan = "";
            const int size = 4;
            kan_xs = new int[size] { 0, 0, 0, 0 };

            bool maisuSufficient = false;

            while (!maisuSufficient)
            {

                int clr = r.Next(4); //色 0-3
                int num = r.Next(9) + 1; //数 1-9

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
                    if (num >= 8) num = r.Next(7) + 1;

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

        static void makeRandomAtama(int jouken, ref int[] maisu, out string str_atama, out int[] atama_xs, int setClr = -1, int setNum = -1)
        {

            string strhead = "";
            const int size = 2;
            atama_xs = new int[size] { 0, 0 };

            bool maisuSufficient = false;

            while (!maisuSufficient)
            {

                int clr = r.Next(4); //色 0-3
                int num = r.Next(9) + 1; //数 1-9

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
                    if (num >= 8) num = r.Next(7) + 1;

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





        static int tileClr(int xs)
        {
            int i = -1;
            if ((xs > 34) || (xs <= 0)) return -1;
            i = (int)((xs - 1) / 9);
            return i;
        }

        static int tileNum(int xs)
        {
            int i = -1;
            if ((xs > 34) || (xs <= 0)) return -1;
            i = ((xs - 1) % 9) + 1;
            return i;
        }

        static int retOne(int d)
        {
            if (d < 0) return 0;

            int rnd = r.Next(d);
            if (rnd != 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }


        }

        #endregion


    }
}
