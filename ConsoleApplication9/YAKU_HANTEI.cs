using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApplication9
{
    class AGARI_DATA
    {


        public int[] tehai;
        public int[][] furotehai;
        public int[] furotype;
        public int agarixs;

        public int bakaze;
        public int jikaze;

        public bool istsumoagari;


        public AGARI_DATA(int[] tehai, int[][] furotehai, int[] furotype, int agarixs, int bakaze, int jikaze, bool istsumoagari)
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

    class SCORE_DATA
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
        public SCORE_DATA(int fan, int fu, bool ykm, bool oya)
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

    class MENZ_DATA
    {

        public int ag_type;
        public int ag_index;
        public int ag_nbm;
        public int furo_suu;
        public int[,] th_info;
        public int[] th_typecount;
        public bool menzenbreak;


        public MENZ_DATA(int ag_type, int ag_index, int ag_nbm, int furo_suu, int[,] th_info, int[] th_typecount, bool menzenbreak)
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

    class YAKU_HANTEI
    {

        public static int PAISTR_MODE = 1;

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

        public static string[] FUROTYPE_STR = { 
             "雀頭", //0
             "暗刻", //1
             "明刻", //2
             "暗槓", //3
             "明槓", //4
             "暗順", //5
             "明順"};//6

        const int TYPE_ATAMA = 0;
        const int TYPE_ANKO = 1;
        const int TYPE_MINKO = 2;
        const int TYPE_ANKAN = 3;
        const int TYPE_MINKAN = 4;
        const int TYPE_SHUNTSU = 5;
        const int TYPE_FSHUNTSU = 6;

        //【役リスト・Yaku_Keishiki】

        //● 判定可能であるもの
        //○ 判定不可能であるもの

        //●00 門前清自摸和 ○05 海底摸月     ●10 自風 東       ●15 場風 南
        //○01 立直         ○06 河底撈魚     ●11 自風 南       ●16 場風 西
        //○02 一発         ●07 平和         ●12 自風 西       ●17 場風 北
        //○03 槍槓         ●08 断幺九       ●13 自風 北       ●18 役牌 白
        //○04 嶺上開花     ●09 一盃口       ●14 場風 東       ●19 役牌 發

        //●20 役牌 中      ●25 三色同順     ●30 小三元        ●35 清一色
        //○21 両立直       ●26 三色同刻     ●31 混老頭        ○36 人和
        //●22 七対子       ●27 三槓子       ●32 二盃口        ○37 天和
        //●23 混全帯幺九   ●28 対々和       ●33 純全帯幺九    ○38 地和
        //●24 一気通貫     ●29 三暗刻       ●34 混一色        ●39 大三元

        //●40 四暗刻       ●45 九蓮宝燈         ●50 小四喜
        //●41 四暗刻単騎   ●46 純正九蓮宝燈     ●51 四槓子
        //●42 字一色       ●47 国士無双         ○52 ドラ
        //●43 緑一色       ●48 国士無双１３面   ○53 裏ドラ
        //●44 清老頭       ●49 大四喜           ○54 赤ドラ


        //【もらうもの】

        //◎ 場風 　　　　　　　(※デフォルト:東場)
        //◎ 自風 　　　　　　　(※デフォルト:西家)
        //◎ ツモかロンか　　　 (※デフォルト:ツモ)
        //◎ 手牌（晒していないもの）
        //◎ 副露した手牌（ジャグ配列）
        //◎ 副露したメンツのタイプ
        //◎ 和了り牌

        //【はきだすもの】

        //◎ 役リスト
        //◎ 面子分けされた手牌（ジャグ配列）？　七対子とか国士はどうするのか
        HANYOU hy = new HANYOU();

        public int Yaku_Keishiki(out bool[] out_AccYakuList, out SCORE_DATA out_sd, AGARI_DATA ad)
        {
            int Fan = 0;
            int Fu = 0;

            int MaxFan = 0;
            int MaxFu = 0;
            int MaxScore = 0;
            bool Maxykm = false;

            bool isOya = (ad.jikaze == 0);


            //【形式役を拾い出しリストを吐き出す】

            //【役リストを作成】
            bool[] YakuList = new bool[54];
            for (int i = 0; i < YakuList.Length; i++)
            {
                YakuList[i] = false;
            }

            //【情報整理用スペース】
            //面前かそうでないかの確認
            int furo_suu = 0;
            bool menzen_break = false;
            for (int m = 0; m < ad.furotype.Length; m++)
            {
                int tmp_ft = ad.furotype[m];
                if (tmp_ft == 2 || tmp_ft == 4 || tmp_ft == 6)
                {
                    menzen_break = true;
                }
                if (tmp_ft == 2 || tmp_ft == 3 || tmp_ft == 4 || tmp_ft == 6)
                {
                    furo_suu++;
                }
            }

            //【晒していない手牌をテーブル化】
            int[,] pai_table = new int[4, 11];
            int tehai_maisu = 0;

            for (int i = 0; i < ad.tehai.Length; i++)
            {
                if (ad.tehai[i] == 0) break;
                int tmpclr = hy.tileClr(ad.tehai[i]);
                int tmpnum = hy.tileNum(ad.tehai[i]);

                pai_table[tmpclr, tmpnum]++;
                tehai_maisu++;
            }


            //【標準形    和了】ここから===========================================================================
            #region 標準形

            //アガリ牌はどの役割なのか
            int agarixs_type = -1;

            #region 抽出準備（ループ外）

            //【テーブルからあり得る和了形を拾い出す】
            //種類に含まれるのは 刻子・順子・雀頭のみ。
            int[] extract_order = new int[5];
            for (int i = 0; i < extract_order.Length; i++)
            {
                extract_order[i] = -1;
            }

            int dimension;
            if (tehai_maisu > 13)
            {
                dimension = 5;
            }
            else if (tehai_maisu > 10)
            {
                dimension = 4;
            }
            else if (tehai_maisu > 7)
            {
                dimension = 3;
            }
            else if (tehai_maisu > 4)
            {
                dimension = 2;
            }
            else
            {
                dimension = 1;
            }

            //要素数

            //14枚(副露なし) ← 5個
            //11枚(副露１回) ← 4個
            // 8枚(副露２回) ← 3個
            // 5枚(副露３回) ← 2個
            // 2枚(副露４回）← 1個 (雀頭だけになる)

            //抽出リストを作成する
            int pattern_max = ((int)Math.Pow(2, dimension - 1)) * dimension;

            #endregion

            for (int ext_pattern = 0; ext_pattern < pattern_max; ext_pattern++)
            {

                #region 抽出準備（ループ内）

                //【刻子＋順子のパターンと頭がどこに来るかのパターンを把握】
                int ks_ptrn = ext_pattern % ((int)Math.Pow(2, dimension - 1));
                int ks_ptrn_back = ks_ptrn;
                int atama_ptrn = (int)((ext_pattern) / ((int)Math.Pow(2, dimension - 1)));

                //※暫定的に0が刻子、1が順子、2が雀頭を示す。

                //【パターン数から抜き出す順番を決定する】
                for (int i = 0; i < dimension; i++)
                {
                    int tw = dimension - 2 - i;
                    int x = (int)Math.Pow(2, tw);
                    if (ks_ptrn >= x)
                    {
                        extract_order[i] = 1;
                    }
                    else
                    {
                        extract_order[i] = 0;
                    }
                    ks_ptrn -= extract_order[i] * x;
                }

                //【雀頭を配置】
                for (int i = dimension - 1; i > atama_ptrn; i--)
                {
                    extract_order[i] = extract_order[i - 1];
                }
                extract_order[atama_ptrn] = 2;


                //【５要素に満たない場合は-1で埋める】
                //飛ばすと全部アタマになるので注意
                for (int i = dimension; i < 5; i++)
                {
                    extract_order[i] = -1;
                }


                //【出力された順番をメンツ表記に変更】
                for (int i = 0; i < extract_order.Length; i++)
                {
                    int k = extract_order[i];
                    switch (k)
                    {
                        case 0:
                            extract_order[i] = TYPE_ANKO;
                            break;
                        case 1:
                            extract_order[i] = TYPE_SHUNTSU;
                            break;
                        case 2:
                            extract_order[i] = TYPE_ATAMA;
                            break;
                        default:
                            extract_order[i] = -1;
                            break;
                    }

                }

                //【テスト出力】
                string strrr = "ext_pattern = " + ext_pattern.ToString("00") + " extract_order -> "; //OK
                for (int i = 0; i < 5; i++)
                {
                    if (extract_order[i] == -1) continue;
                    strrr += FUROTYPE_STR[extract_order[i]] + " ";
                }
                //Debug.Print(strrr);

                //【抽出リストに従い詳細を抽出する】
                int[,] tmp_info;
                int[] tmp_typecnt;

                Yaku_K_MenzExtract(out tmp_info, out tmp_typecnt, pai_table, extract_order);

                //【要素合計数を確認】
                int typecount_sum = 0;
                for (int i = 0; i < tmp_typecnt.Length; i++)
                {
                    typecount_sum += tmp_typecnt[i];
                }

                if (typecount_sum != dimension) continue;


                //Debug.Print(strrr);

                //【アガリ牌の位置候補を列挙して記録】

                //アガリ牌のあるメンツ種
                const int ATAMA_DETAIL_TYPE = 0;
                //アガリ牌のメンツ種のうち何番目のメンツか
                const int ATAMA_DETAIL_INDEX = 1;
                //アガリ牌のメンツの何番目の牌か
                const int ATAMA_DETAIL_NANBANME = 2;

                int agari_pos_kouho;
                int[,] a_detail;

                Yaku_K_ApFind(out agari_pos_kouho, out a_detail, tmp_info, tmp_typecnt, ad.agarixs);

                //【取得メンツを表示】
                for (int type = 0; type < 7; type++)
                {
                    string sm = FUROTYPE_STR[type] + " ";
                    for (int idx = 0; idx < 3; idx++)
                    {
                        if (tmp_info[type, idx] != -1)
                        {
                            sm += XS_PAI_STR[PAISTR_MODE][tmp_info[type, idx]];
                        }
                    }
                    sm += " ";
                }

                #endregion

                //【アガリ牌位置候補の数だけループして調査】
                for (int a_kouho = 0; a_kouho < agari_pos_kouho; a_kouho++)
                {
                    int ag_type = a_detail[a_kouho, ATAMA_DETAIL_TYPE];
                    int ag_index = a_detail[a_kouho, ATAMA_DETAIL_INDEX];
                    int ag_nbm = a_detail[a_kouho, ATAMA_DETAIL_NANBANME];
                    //アガリ牌の位置は ag_kind と ag_index と ag_nbm
                    //手牌のメンツ情報は tmp_info と tmp_typecnt
                    //副露のメンツ情報は ad.furotehai と ad.futotype

                    #region 和了形表示

                    //【確定和了形を表示】
                    string agari_str = "和了手牌 = ";

                    //和了形態
                    if (ad.istsumoagari)
                    {
                        agari_str += "ツモ | ";
                    }
                    else
                    {
                        agari_str += "ロン | ";
                    }

                    //手の内
                    for (int idx = 0; idx < tmp_typecnt[TYPE_ATAMA]; idx++)
                    {
                        string paistr = XS_PAI_STR[PAISTR_MODE][tmp_info[TYPE_ATAMA, idx]];

                        if (ag_type == TYPE_ATAMA)
                        {
                            agari_str += "[" + paistr + "]" + paistr;
                        }
                        else
                        {
                            agari_str += paistr + paistr;
                        }
                        agari_str += " ";
                    }

                    for (int idx = 0; idx < tmp_typecnt[TYPE_ANKO]; idx++)
                    {
                        string paistr = XS_PAI_STR[PAISTR_MODE][tmp_info[TYPE_ANKO, idx]];

                        if (ag_type == TYPE_ANKO && ag_index == idx)
                        {
                            agari_str += "[" + paistr + "]" + paistr + paistr;
                        }
                        else
                        {
                            agari_str += paistr + paistr + paistr;
                        }
                        agari_str += " ";
                    }

                    for (int idx = 0; idx < tmp_typecnt[TYPE_SHUNTSU]; idx++)
                    {
                        string[] paistr = {XS_PAI_STR[PAISTR_MODE][tmp_info[TYPE_SHUNTSU, idx]],
                                           XS_PAI_STR[PAISTR_MODE][tmp_info[TYPE_SHUNTSU, idx]+1],
                                           XS_PAI_STR[PAISTR_MODE][tmp_info[TYPE_SHUNTSU, idx]+2],};

                        for (int n = 0; n < 3; n++)
                        {
                            bool thispaiMachi = (ag_type == TYPE_SHUNTSU && ag_index == idx);
                            thispaiMachi = thispaiMachi && (ag_nbm == n);

                            if (thispaiMachi) agari_str += "[";
                            agari_str += paistr[n];
                            if (thispaiMachi) agari_str += "]";

                        }
                        agari_str += " ";
                    }

                    //副露
                    if (dimension != 5)
                    {

                        agari_str += "    (";

                        for (int f = 0; f < ad.furotehai.Length; f++)
                        {

                            if (ad.furotehai[f][0] != 0) agari_str += " ";

                            for (int m = 0; m < ad.furotehai[f].Length; m++)
                            {
                                if (ad.furotehai[f][m] == 0) break;
                                agari_str += XS_PAI_STR[PAISTR_MODE][ad.furotehai[f][m]];
                            }

                        }

                        agari_str += " )";

                    }

                    //Debug.Print(agari_str);

                    #endregion

                    //【符計算用の情報を整理】
                    int bakaze_pai = ad.bakaze + 28;
                    int jikaze_pai = ad.jikaze + 28;

                    int cur_Fu = 0;
                    string fu_str = "";

                    #region 符計算

                    //副底
                    cur_Fu = 20;
                    fu_str += " 副底 20 ";

                    //ツモ符
                    if (ad.istsumoagari)
                    {
                        cur_Fu += 2;
                        fu_str += " + ツモ 2 ";
                    }

                    //面前ロン符
                    if (!menzen_break && !ad.istsumoagari)
                    {
                        cur_Fu += 10;
                        fu_str += " + 面前ロン 10 ";
                    }


                    //メンツ符の情報
                    int[] MENZ_FU = new int[5] { 2, 4, 2, 16, 8 }; //半分のほう

                    //雀頭
                    int atama_xs = tmp_info[TYPE_ATAMA, 0];



                    if (atama_xs == jikaze_pai)
                    {
                        cur_Fu += 2;
                        fu_str += " + 自風雀頭 2 ";
                    }
                    if (atama_xs == bakaze_pai)
                    {
                        cur_Fu += 2;
                        fu_str += " + 場風雀頭 2 ";
                    }
                    if (hy.tileClr(atama_xs) == 3 && hy.tileNum(atama_xs) >= 5)
                    {
                        cur_Fu += 2;
                        fu_str += " + 三元雀頭 2 ";
                    }



                    //暗刻
                    for (int type = TYPE_ANKO; type <= TYPE_MINKAN; type++)
                    {

                        for (int idx = 0; idx < tmp_typecnt[type]; idx++)
                        {
                            int tmp_xs = tmp_info[type, idx];
                            bool yaoc_xs = hy.isYaochuXS(tmp_xs);
                            bool agari_menz = (ag_type == type && ag_index == idx);

                            //ロンアガリ該当メンツ(暗槓は待ちとならないので暗刻の場合しかない)
                            if (!ad.istsumoagari && agari_menz && (type == TYPE_ANKO))
                            {
                                if (yaoc_xs)
                                {
                                    cur_Fu += MENZ_FU[TYPE_MINKO] * 2;
                                    fu_str += " + 暗刻崩れ " + (MENZ_FU[TYPE_MINKO] * 2).ToString() + " ";
                                }
                                else
                                {
                                    cur_Fu += MENZ_FU[TYPE_MINKO];
                                    fu_str += " + 暗刻崩れ " + (MENZ_FU[TYPE_MINKO]).ToString() + " ";
                                }
                            }
                            else if (type == TYPE_ANKO) //通常の暗刻(暗刻しか通らないはずだけど)
                            {
                                if (yaoc_xs)
                                {
                                    cur_Fu += MENZ_FU[type] * 2;
                                    fu_str += " + 暗刻 " + (MENZ_FU[type] * 2).ToString() + " ";
                                }
                                else
                                {
                                    cur_Fu += MENZ_FU[type];
                                    fu_str += " + 暗刻 " + (MENZ_FU[type]).ToString() + " ";
                                }
                            }
                        }

                    }



                    //明刻・暗槓・明槓
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) continue;
                        bool yaoc_xs = hy.isYaochuXS(tmp_xs);
                        int type = ad.furotype[f];

                        if (type != TYPE_MINKO && type != TYPE_ANKAN && type != TYPE_MINKAN) continue;
                        if (yaoc_xs)
                        {
                            cur_Fu += MENZ_FU[type] * 2;
                            fu_str += " + " + FUROTYPE_STR[type] + " " + (MENZ_FU[type] * 2).ToString() + " ";
                        }
                        else
                        {
                            cur_Fu += MENZ_FU[type];
                            fu_str += " + " + FUROTYPE_STR[type] + " " + (MENZ_FU[type]).ToString() + " ";
                        }

                    }


                    //待形符
                    if (ag_type == TYPE_SHUNTSU && ag_nbm == 1) //嵌張
                    {
                        cur_Fu += 2;
                        fu_str += " + 嵌張 2 ";
                    }
                    else if (ag_type == TYPE_SHUNTSU && hy.tileNum(tmp_info[ag_type, ag_index]) == 7 && ag_nbm == 0) //辺張
                    {
                        cur_Fu += 2;
                        fu_str += " + 辺張 2 ";
                    }
                    else if (ag_type == TYPE_SHUNTSU && hy.tileNum(tmp_info[ag_type, ag_index]) == 1 && ag_nbm == 2)//辺張
                    {
                        cur_Fu += 2;
                        fu_str += " + 辺張 2 ";
                    }
                    else if (ag_type == TYPE_ATAMA)  //雀頭待ち
                    {
                        cur_Fu += 2;
                        fu_str += " + 単騎 2 ";
                    }

                    //ここまで来てピンフならロン30ツモ22になっているはず
                    //ピンヅモを20符に
                    if (!menzen_break)
                    {
                        if (ad.istsumoagari && cur_Fu == 22)
                        {
                            cur_Fu = 20;
                            fu_str += " ※ ピンヅモにより 20 ";
                        }
                    }
                    else //逆にピンフ形ロンは30符にする
                    {
                        if (cur_Fu == 20)
                        {
                            cur_Fu = 30;
                            fu_str += " ※ ピン形ロンより 30 ";
                        }
                    }

                    fu_str += " = " + cur_Fu.ToString();

                    cur_Fu = ((int)((cur_Fu + 9) / 10)) * 10;

                    //Debug.Print(fu_str);
                    //Debug.Print("TmpFu = " + cur_Fu.ToString());

                    #endregion


                    //【テンポラリ役リストを作成】
                    bool[] cur_YakuList = new bool[54];
                    for (int i = 0; i < cur_YakuList.Length; i++)
                    {
                        cur_YakuList[i] = false;
                    }

                    MENZ_DATA md = new MENZ_DATA(ag_type, ag_index, ag_nbm, furo_suu, tmp_info, tmp_typecnt, menzen_break);

                    for (int i = 0; i < cur_YakuList.Length; i++)
                    {
                        cur_YakuList[i] = Yaku_K_Judge(i, ad, md);
                    }

                    //【この和了形での飜数を求める】

                    #region 飜計算


                    int cur_Fan = 0;
                    Yaku_DoubleCut(ref cur_YakuList);
                    //Yakulist_view(cur_YakuList);

                    bool ykm;
                    cur_Fan = Yaku_K_CalcFan(cur_YakuList, md.menzenbreak, out ykm);

                    //情報整理
                    string rpointstr = "打点 : ";
                    if (ykm)
                    {
                        if (cur_Fan != 1)
                        {
                            rpointstr += cur_Fan.ToString() + "倍役満 ";
                        }
                        else
                        {
                            rpointstr += "役満 ";
                        }
                    }
                    else
                    {
                        rpointstr += cur_Fu.ToString() + " 符 " + cur_Fan.ToString() + " 飜 ";
                    }

                    #endregion

                    #region 点数見せるとき

                    rpointstr += "  ";

                    SCORE_DATA sc = new SCORE_DATA(cur_Fan, cur_Fu, ykm, isOya);

                    if (ad.istsumoagari)
                    {
                        if (ad.jikaze == 0)
                        {
                            rpointstr += sc.other_pay.ToString() + " ∀ ";
                        }
                        else
                        {
                            rpointstr += sc.other_pay.ToString() + " - " + sc.dealer_pay.ToString();
                        }
                    }
                    else
                    {
                        rpointstr += sc.scoresum.ToString();
                    }

                    //Debug.Print(rpointstr);

                    #endregion


                    //【最大和了点なら更新】
                    if (MaxScore < sc.scoresum)
                    {
                        MaxScore = sc.scoresum;
                        MaxFan = cur_Fan;
                        MaxFu = cur_Fu;
                        Maxykm = ykm;
                        for (int i = 0; i < YakuList.Length; i++)
                        {
                            YakuList[i] = cur_YakuList[i];
                        }
                    }

                }

            }

            #endregion
            //【標準形    和了】ここまで===========================================================================

            //【七対子    和了】ここから===========================================================================
            #region 七対子
            if (furo_suu == 0)
            {
                //他情報
                int cur_c_Fu = 25;
                int cur_c_Fan = 0;

                //【テンポラリ役リストを作成】
                bool[] cur_c_YakuList = new bool[54];
                for (int i = 0; i < cur_c_YakuList.Length; i++)
                {
                    cur_c_YakuList[i] = false;
                }


                //対子情報
                int[] toitsu_info = new int[7];
                int toitsu_cnt = 0;

                //テーブルを確認
                for (int clr = 0; clr <= 3; clr++)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (pai_table[clr, num] == 2)
                        {
                            toitsu_info[toitsu_cnt] = ReturnXS(clr, num);
                            toitsu_cnt++;
                        }
                    }
                }

                //７対子の場合
                if (toitsu_cnt == 7)
                {
                    for (int i = 0; i < cur_c_YakuList.Length; i++)
                    {
                        cur_c_YakuList[i] = Yaku_K_Chitoi_Judge(i, toitsu_info, ad);
                    }
                    cur_c_YakuList[22] = true;

                    Yaku_DoubleCut(ref cur_c_YakuList);

                    bool ykm;
                    cur_c_Fan = Yaku_K_CalcFan(cur_c_YakuList, false, out ykm);
                    //Debug.Print("cur_c_Fan = " + cur_c_Fan.ToString());
                    //Debug.Print("cur_c_Fu = " + cur_c_Fu.ToString());

                    SCORE_DATA sc_c = new SCORE_DATA(cur_c_Fan, cur_c_Fu, ykm, isOya);

                    #region 点みせるとき

                    string rpointstr = "";

                    if (ad.istsumoagari)
                    {
                        if (ad.jikaze == 0)
                        {
                            rpointstr += sc_c.other_pay.ToString() + " ∀ ";
                        }
                        else
                        {
                            rpointstr += sc_c.other_pay.ToString() + " - " + sc_c.dealer_pay.ToString();
                        }
                    }
                    else
                    {
                        rpointstr += sc_c.scoresum.ToString();
                    }

                    Debug.Print(rpointstr);

                    #endregion

                    //【最大和了点なら更新】
                    if (MaxScore < sc_c.scoresum)
                    {
                        MaxScore = sc_c.scoresum;
                        MaxFan = cur_c_Fan;
                        MaxFu = cur_c_Fu;
                        Maxykm = ykm;
                        for (int i = 0; i < YakuList.Length; i++)
                        {
                            YakuList[i] = cur_c_YakuList[i];
                        }
                    }

                }



            }
            #endregion
            //【七対子    和了】ここまで===========================================================================

            //【国士無双  和了】ここから===========================================================================
            #region 国士無双
            if (furo_suu == 0)
            {

                //他情報
                int cur_k_Fu = 30; //?
                int cur_k_Fan = 0; //必要なさそうだが。
                int[] yaochu_xs = new int[13] { 1, 9, 10, 18, 19, 27, 28, 29, 30, 31, 32, 33, 34 };
                int[] yaochu_maisu = new int[13];
                bool isjunsei = false;
                bool head_exist = false;

                //【ヤオ九牌の枚数を確認】
                for (int i = 0; i < yaochu_xs.Length; i++)
                {
                    int tmp_clr = hy.tileClr(yaochu_xs[i]);
                    int tmp_num = hy.tileNum(yaochu_xs[i]);
                    int tmp_maisu = pai_table[tmp_clr, tmp_num];
                    if (tmp_maisu == 0)
                    {
                        goto kokushi_none;
                    }
                    else if (tmp_maisu == 2)
                    {
                        head_exist = true;
                        if (ad.agarixs == yaochu_xs[i]) isjunsei = true;
                    }
                }

                if (!head_exist) goto kokushi_none;

                //【国士無双が認定される】
                //【テンポラリ役リストを作成】
                bool[] cur_k_YakuList = new bool[54];
                for (int i = 0; i < cur_k_YakuList.Length; i++)
                {
                    cur_k_YakuList[i] = false;
                }

                if (isjunsei)
                {
                    cur_k_YakuList[48] = true;
                }
                else
                {
                    cur_k_YakuList[47] = true;
                }

                bool ykm;
                cur_k_Fan = Yaku_K_CalcFan(cur_k_YakuList, false, out ykm);

                SCORE_DATA sc_k = new SCORE_DATA(cur_k_Fan, cur_k_Fu, ykm, isOya);

                #region 点みせるとき

                string rpointstr = "";

                if (ad.istsumoagari)
                {
                    if (ad.jikaze == 0)
                    {
                        rpointstr += sc_k.other_pay.ToString() + " ∀ ";
                    }
                    else
                    {
                        rpointstr += sc_k.other_pay.ToString() + " - " + sc_k.dealer_pay.ToString();
                    }
                }
                else
                {
                    rpointstr += sc_k.scoresum.ToString();
                }

                Debug.Print(rpointstr);

                #endregion

                //【最大和了点なら更新】
                if (MaxScore < sc_k.scoresum)
                {
                    MaxScore = sc_k.scoresum;
                    MaxFan = cur_k_Fan;
                    MaxFu = cur_k_Fu;
                    Maxykm = ykm;
                    for (int i = 0; i < YakuList.Length; i++)
                    {
                        YakuList[i] = cur_k_YakuList[i];
                    }
                }

            }

        kokushi_none:

            #endregion
            //【国士無双  和了】ここまで===========================================================================


            out_sd = new SCORE_DATA(MaxFan, MaxFu, Maxykm, isOya);
            out_AccYakuList = YakuList;

            return 0;


        }

        static int ReturnXS(int clr, int num)
        {
            return clr * 9 + num;
        }

        /// <summary>
        /// 抽出リスト(ext_order)に従い、刻子・順子・雀頭を決められた順番で抽出し詳細を返す
        /// </summary>
        /// <param name="out_info">(戻り値用)返す詳細情報</param>
        /// <param name="out_typecnt">(戻り値用)雀頭・刻子・順子がいくつあったか</param>
        /// <param name="in_pai_table">テーブル</param>
        /// <param name="ext_order">抽出リスト</param>
        public void Yaku_K_MenzExtract(out int[,] out_info, out int[] out_typecnt, int[,] in_pai_table, int[] ext_order)
        {
            //【抽出リスト(ext_order)に従い、刻子・順子・雀頭を決められた順番で抽出し詳細を返す】
            const int MAX_DIM = 5;
            int[,] info = new int[7, 5];
            int[] typecnt = new int[7];
            int[,] pai_table = new int[4, 11];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    pai_table[i, j] = in_pai_table[i, j];
                }

            }

            for (int type = 0; type < 7; type++)
            {
                typecnt[type] = 0;
                for (int i = 0; i < 5; i++)
                {
                    info[type, i] = -1;
                }
            }

            for (int i = 0; i < ext_order.Length; i++)
            {
                switch (ext_order[i])
                {
                    case -1:
                        //Debug.Print("ext order [-1] has detected");
                        break;
                    case TYPE_ATAMA:
                        for (int clr = 0; clr <= 3; clr++)
                        {
                            for (int num = 1; num <= 9; num++)
                            {
                                if (clr == 3 && num >= 8) break;
                                if (pai_table[clr, num] >= 2)
                                {
                                    pai_table[clr, num] -= 2;
                                    info[TYPE_ATAMA, typecnt[TYPE_ATAMA]] = ReturnXS(clr, num);
                                    typecnt[TYPE_ATAMA]++;
                                    goto Next_order;
                                }
                            }
                        }
                        break;
                    case TYPE_ANKO:
                        for (int clr = 0; clr <= 3; clr++)
                        {
                            for (int num = 1; num <= 9; num++)
                            {
                                if (clr == 3 && num >= 8) break;
                                if (pai_table[clr, num] >= 3)
                                {
                                    pai_table[clr, num] -= 3;
                                    info[TYPE_ANKO, typecnt[TYPE_ANKO]] = ReturnXS(clr, num);
                                    typecnt[TYPE_ANKO]++;
                                    goto Next_order;
                                }
                            }
                        }
                        break;
                    case TYPE_SHUNTSU:
                        for (int clr = 0; clr <= 2; clr++)
                        {
                            for (int num = 1; num <= 7; num++)
                            {
                                if (pai_table[clr, num] >= 1
                                    && pai_table[clr, num + 1] >= 1
                                    && pai_table[clr, num + 2] >= 1)
                                {
                                    pai_table[clr, num] -= 1;
                                    pai_table[clr, num + 1] -= 1;
                                    pai_table[clr, num + 2] -= 1;
                                    info[TYPE_SHUNTSU, typecnt[TYPE_SHUNTSU]] = ReturnXS(clr, num);
                                    typecnt[TYPE_SHUNTSU]++;
                                    goto Next_order;
                                }
                            }
                        }
                        break;
                    default:
                        Debug.Print("ext order [" + ext_order[i].ToString() + "] has detected");
                        break;

                }

            Next_order:

                int here_s_nothing_to_do;

            }

            out_info = info;
            out_typecnt = typecnt;

        }

        public void Yaku_K_ApFind(out int out_kouho_cnt, out int[,] out_detail, int[,] info, int[] typecnt, int agari_xs)
        {

            //detailの中身は[雀頭候補番号,詳細情報].

            //アガリ牌のあるメンツ種
            const int ATAMA_DETAIL_TYPE = 0;

            //アガリ牌のメンツ種のうち何番目のメンツか
            const int ATAMA_DETAIL_INDEX = 1;

            //アガリ牌のメンツの何番目の牌か
            const int ATAMA_DETAIL_NANBANME = 2;


            int[,] detail = new int[4, 3];
            int kouho_cnt = 0;
            int xs = agari_xs;

            for (int type = 0; type < 7; type++)
            {
                if (type != TYPE_ATAMA && type != TYPE_ANKO && type != TYPE_SHUNTSU) continue;
                for (int idx = 0; idx < typecnt[type]; idx++)
                {
                    switch (type)
                    {

                        case TYPE_ATAMA:
                            if (info[type, idx] == xs)
                            {
                                detail[kouho_cnt, ATAMA_DETAIL_TYPE] = TYPE_ATAMA;
                                detail[kouho_cnt, ATAMA_DETAIL_INDEX] = idx;
                                detail[kouho_cnt, ATAMA_DETAIL_NANBANME] = 0;
                                kouho_cnt++;
                            }
                            break;
                        case TYPE_ANKO:
                            if (info[type, idx] == xs)
                            {
                                detail[kouho_cnt, ATAMA_DETAIL_TYPE] = TYPE_ANKO;
                                detail[kouho_cnt, ATAMA_DETAIL_INDEX] = idx;
                                detail[kouho_cnt, ATAMA_DETAIL_NANBANME] = 0;
                                kouho_cnt++;
                            }
                            break;
                        case TYPE_SHUNTSU:
                            int cur_xs = info[type, idx];
                            int cur_num = hy.tileNum(cur_xs);
                            int xs_num = hy.tileNum(xs);

                            if (hy.tileClr(cur_xs) == hy.tileClr(xs) &&
                                cur_num <= xs_num &&
                                cur_num + 2 >= xs_num)
                            {
                                detail[kouho_cnt, ATAMA_DETAIL_TYPE] = TYPE_SHUNTSU;
                                detail[kouho_cnt, ATAMA_DETAIL_INDEX] = idx;
                                detail[kouho_cnt, ATAMA_DETAIL_NANBANME] = xs_num - cur_num;
                                kouho_cnt++;
                            }

                            break;

                    }
                }

            }

            out_kouho_cnt = kouho_cnt;
            out_detail = detail;


        }

        public bool Yaku_K_Judge(int yakuNum, AGARI_DATA ad, MENZ_DATA md)
        {

            if (yakuNum >= 10 && yakuNum <= 20) //役牌全般
            {
                #region 役牌
                bool yakuhai_found = false;
                int aim_yakuhai = -1;

                if (yakuNum >= 10 && yakuNum <= 13)//自風
                {
                    if ((yakuNum - 10) != ad.jikaze) return false;
                    aim_yakuhai = 28 + ad.jikaze;
                }
                else if (yakuNum >= 14 && yakuNum <= 17) //場風
                {
                    if ((yakuNum - 14) != ad.bakaze) return false;
                    aim_yakuhai = 28 + ad.bakaze;
                }
                else if (yakuNum >= 18 && yakuNum <= 20) //三元牌
                {
                    aim_yakuhai = yakuNum + 14;
                }

                //手牌の中について
                for (int type = TYPE_ANKO; type <= TYPE_MINKAN; type++)
                {
                    for (int idx = 0; idx < md.th_typecount[type]; idx++)
                    {
                        int tmp_xs = md.th_info[type, idx];
                        if (tmp_xs == aim_yakuhai) yakuhai_found = true;
                    }
                }

                //副露した牌について
                for (int f = 0; f < ad.furotehai.Length; f++)
                {
                    int tmp_xs = ad.furotehai[f][0];
                    if (tmp_xs == 0) break;

                    if (ad.furotype[f] != TYPE_FSHUNTSU) //対子系
                    {
                        if (tmp_xs == aim_yakuhai) yakuhai_found = true;
                    }
                }

                if (!yakuhai_found) return false;


                #region disp
                //switch (yakuNum)
                //{
                //    case 10:
                //        Debug.Print("自風 東");
                //        break;
                //    case 11:
                //        Debug.Print("自風 南");
                //        break;
                //    case 12:
                //        Debug.Print("自風 西");
                //        break;
                //    case 13:
                //        Debug.Print("自風 北");
                //        break;
                //    case 14:
                //        Debug.Print("場風 東");
                //        break;
                //    case 15:
                //        Debug.Print("場風 南");
                //        break;
                //    case 16:
                //        Debug.Print("場風 西");
                //        break;
                //    case 17:
                //        Debug.Print("場風 北");
                //        break;
                //    case 18:
                //        Debug.Print("役牌 白");
                //        break;
                //    case 19:
                //        Debug.Print("役牌 發");
                //        break;
                //    case 20:
                //        Debug.Print("役牌 中");
                //        break;
                //}
                #endregion
                return true;
                #endregion
            }

            if (yakuNum == 34 || yakuNum == 35) //染め
            {
                #region 混一色・清一色

                int some_clr = 3;
                bool some_tupai_contain = false;

                //手牌の中について
                for (int type = TYPE_ATAMA; type <= TYPE_SHUNTSU; type++)
                {
                    for (int idx = 0; idx < md.th_typecount[type]; idx++)
                    {
                        int tmp_xs = md.th_info[type, idx];
                        int tmp_clr = hy.tileClr(tmp_xs);
                        if (tmp_clr == 3) some_tupai_contain = true;

                        if (tmp_clr != some_clr && tmp_clr != 3 && some_clr != 3)
                        {
                            return false;
                        }
                        else if (tmp_clr != 3)
                        {
                            some_clr = tmp_clr;
                        }
                    }
                }

                //副露した牌について
                for (int f = 0; f < ad.furotehai.Length; f++)
                {
                    int tmp_xs = ad.furotehai[f][0];
                    if (tmp_xs == 0) break;

                    int tmp_clr = hy.tileClr(tmp_xs);
                    if (tmp_clr == 3) some_tupai_contain = true;

                    if (tmp_clr != some_clr && tmp_clr != 3 && some_clr != 3)
                    {
                        return false;
                    }
                    else if (tmp_clr != 3)
                    {
                        some_clr = tmp_clr;
                    }
                }

                if (some_clr == 3) return false; //字一色

                if (!some_tupai_contain && yakuNum == 34) //ホンイツなのに字牌なし
                {
                    return false;
                }
                else if (some_tupai_contain && yakuNum == 35)//チンイツなのに字牌あり
                {
                    return false;
                }

                if (yakuNum == 34)
                {
                    //Debug.Print("混一色");
                }
                else if (yakuNum == 35)
                {
                    //Debug.Print("清一色");
                }
                return true;
                #endregion
            }

            if (yakuNum == 45 || yakuNum == 46) //九蓮宝燈・純正九蓮宝燈
            {
                #region 九蓮宝燈・純正九蓮宝燈

                #region 解説

                //次のような9パターンの牌姿でしか九蓮宝燈は成立しない。

                //多い牌 雀頭  暗刻  暗順
                //  1     9   1     1 4 7
                //  2     2   1 9   3 6
                //  3     1   9     1 3 6
                //  4     9   1     2 4 7
                //  5     5   1 9   2 6
                //  6     1   9     1 4 6
                //  7     9   1     2 5 7
                //  8     8   1 9   2 5
                //  9     1   9     1 4 7

                #endregion

                bool[] churen_KotsuAcc = new bool[10] { false,false,false,false,
                                                                  false,false,false,
                                                                  false,false,false};
                bool[] churen_ShuntsuAcc = new bool[8] { false,false,false,false,
                                                                    false,false,false,
                                                                    false}; //--------
                //前チェック
                if (md.furo_suu != 0) return false;
                if (md.th_typecount[TYPE_SHUNTSU] == 0 ||
                    md.th_typecount[TYPE_SHUNTSU] >= 4) return false;
                if (md.th_typecount[TYPE_ANKO] == 0 ||
                    md.th_typecount[TYPE_ANKO] >= 3) return false;

                //雀頭検査
                int head_xs = md.th_info[TYPE_ATAMA, 0];
                int head_num = hy.tileNum(head_xs);
                int churen_clr = hy.tileClr(head_xs);
                if (churen_clr == 3) return false;
                if (head_num != 9 && head_num != 1 &&
                    head_num != 2 && head_num != 5 && head_num != 8) return false;

                //手中検査
                for (int type = TYPE_ANKO; type <= TYPE_SHUNTSU; type++)
                {
                    for (int idx = 0; idx < md.th_typecount[type]; idx++)
                    {
                        int tmp_xs = md.th_info[type, idx];
                        int tmp_num = hy.tileNum(tmp_xs);

                        if (type != TYPE_SHUNTSU) //刻子
                        {
                            if (hy.tileClr(tmp_xs) != churen_clr) return false;
                            churen_KotsuAcc[tmp_num] = true;

                        }
                        else //順子
                        {
                            if (hy.tileClr(tmp_xs) != churen_clr) return false;
                            churen_ShuntsuAcc[tmp_num] = true;
                        }
                    }
                }

                //結果の吟味
                if (head_num == 2 && (!churen_KotsuAcc[1] || !churen_KotsuAcc[9] ||
                                      !churen_ShuntsuAcc[3] || !churen_ShuntsuAcc[6])) return false;
                if (head_num == 5 && (!churen_KotsuAcc[1] || !churen_KotsuAcc[9] ||
                                      !churen_ShuntsuAcc[2] || !churen_ShuntsuAcc[6])) return false;
                if (head_num == 8 && (!churen_KotsuAcc[1] || !churen_KotsuAcc[9] ||
                                      !churen_ShuntsuAcc[2] || !churen_ShuntsuAcc[5])) return false;

                if (head_num == 1 && !churen_KotsuAcc[9]) return false;
                if (head_num == 9 && !churen_KotsuAcc[1]) return false;

                if ((head_num == 1 && (!churen_ShuntsuAcc[1] || !churen_ShuntsuAcc[3] || !churen_ShuntsuAcc[6])) &&
                    (head_num == 1 && (!churen_ShuntsuAcc[1] || !churen_ShuntsuAcc[4] || !churen_ShuntsuAcc[6])) &&
                    (head_num == 1 && (!churen_ShuntsuAcc[1] || !churen_ShuntsuAcc[4] || !churen_ShuntsuAcc[7]))) return false;

                if ((head_num == 9 && (!churen_ShuntsuAcc[1] || !churen_ShuntsuAcc[4] || !churen_ShuntsuAcc[7])) &&
                    (head_num == 9 && (!churen_ShuntsuAcc[2] || !churen_ShuntsuAcc[4] || !churen_ShuntsuAcc[7])) &&
                    (head_num == 9 && (!churen_ShuntsuAcc[2] || !churen_ShuntsuAcc[5] || !churen_ShuntsuAcc[7]))) return false;

                //純正の吟味
                bool isJunsei = false;
                int agari_num = hy.tileNum(ad.agarixs);

                if (head_num == 2 || head_num == 5 || head_num == 8)
                {
                    if (agari_num == head_num) isJunsei = true;
                }
                else if (head_num == 1)
                {
                    if (agari_num == 3 && churen_ShuntsuAcc[3]) isJunsei = true;
                    if (agari_num == 9 && churen_ShuntsuAcc[7]) isJunsei = true;
                    if (agari_num == 6 && !churen_ShuntsuAcc[3] && !churen_ShuntsuAcc[7]) isJunsei = true;
                }
                else if (head_num == 9)
                {
                    if (agari_num == 1 && churen_ShuntsuAcc[1]) isJunsei = true;
                    if (agari_num == 7 && churen_ShuntsuAcc[5]) isJunsei = true;
                    if (agari_num == 4 && !churen_ShuntsuAcc[1] && !churen_ShuntsuAcc[5]) isJunsei = true;
                }

                if (isJunsei && yakuNum == 45)
                {
                    return false;
                }
                else if (!isJunsei && yakuNum == 46)
                {
                    return false;
                }

                if (yakuNum == 45)
                {
                    //Debug.Print("九蓮宝燈");
                }
                else
                {
                    //Debug.Print("純正九蓮宝燈");
                }
                return true;
                #endregion
            }

            switch (yakuNum)
            {
                case 0: //門前清自摸和
                    #region 門前清自摸和
                    if (md.menzenbreak) return false;
                    if (!ad.istsumoagari) return false;

                    //Debug.Print("門前清自摸和");
                    return true;
                    break;
                    #endregion
                case 7: //平和
                    #region 平和
                    //前チェック
                    if (md.furo_suu != 0) return false;
                    if (md.th_typecount[TYPE_SHUNTSU] != 4) return false;
                    if (md.ag_type == TYPE_ATAMA) return false;

                    //雀頭判定
                    int atama_xs = md.th_info[TYPE_ATAMA, 0];
                    if (hy.tileClr(atama_xs) == 3 && hy.tileNum(atama_xs) >= 5)
                    {
                        return false;
                    }
                    else if (atama_xs == (ad.bakaze + 28) || atama_xs == (ad.jikaze + 28))
                    {
                        return false;
                    }

                    //待ち判定
                    if (md.ag_nbm == 1)
                    {
                        //カンチャン
                        return false;
                    }
                    else if (md.ag_nbm == 0 && hy.tileNum(md.th_info[TYPE_SHUNTSU, md.ag_index]) == 7)
                    {
                        //ペンチャン 789 の7
                        return false;
                    }
                    else if (md.ag_nbm == 2 && hy.tileNum(md.th_info[TYPE_SHUNTSU, md.ag_index]) == 1)
                    {
                        //ペンチャン 123 の3
                        return false;
                    }

                    //Debug.Print("平和");
                    return true;
                    break;
                    #endregion
                case 8: //断幺九
                    #region 断幺九
                    //手牌の中について
                    for (int type = TYPE_ATAMA; type <= TYPE_SHUNTSU; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];

                            if (type != TYPE_SHUNTSU) //対子系
                            {
                                if (hy.isYaochuXS(tmp_xs)) return false;
                            }
                            else //副露順子
                            {
                                if (hy.tileNum(tmp_xs) == 1 || hy.tileNum(tmp_xs) == 7) return false;
                            }

                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU) //対子系
                        {
                            if (hy.isYaochuXS(tmp_xs)) return false;
                        }
                        else //副露順子
                        {
                            if (hy.tileNum(tmp_xs) == 1 || hy.tileNum(tmp_xs) == 7) return false;
                        }
                    }

                    //Debug.Print("断幺九");
                    return true;
                    break;
                    #endregion
                case 9: //一盃口
                    #region 一盃口
                    //前チェック
                    if (md.menzenbreak) return false;
                    if (md.th_typecount[TYPE_SHUNTSU] < 2) return false;

                    //順子検査
                    //一盃口の対象順子は隣り.
                    int onaji_shuntsu_cnt = 0;
                    bool prev_doubled = false;

                    for (int idx = 1; idx < md.th_typecount[TYPE_SHUNTSU]; idx++)
                    {
                        if (prev_doubled)
                        {
                            prev_doubled = false;
                            continue;
                        }

                        if (md.th_info[TYPE_SHUNTSU, idx] == md.th_info[TYPE_SHUNTSU, idx - 1])
                        {
                            onaji_shuntsu_cnt++;
                            prev_doubled = true;
                        }
                    }

                    if (onaji_shuntsu_cnt == 0) return false;  //被り順子なし
                    if (onaji_shuntsu_cnt == 2) return false;　//二盃口のパターン

                    //Debug.Print("一盃口");
                    return true;
                    break;
                    #endregion
                case 22: //七対子
                    break;
                case 23:  //混全帯幺九
                    #region 混全帯幺九
                    bool chanta_tupai_contain = false;
                    int shuntsu_count = 0;

                    //手牌の中について
                    for (int type = TYPE_ATAMA; type <= TYPE_SHUNTSU; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];

                            if (type != TYPE_SHUNTSU) //対子系
                            {
                                if (!hy.isYaochuXS(tmp_xs)) return false;
                                if (hy.tileClr(tmp_xs) == 3) chanta_tupai_contain = true;
                            }
                            else //順子
                            {
                                if (hy.tileNum(tmp_xs) != 1 && hy.tileNum(tmp_xs) != 7) return false;
                                shuntsu_count++;
                            }

                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU) //対子系
                        {
                            if (!hy.isYaochuXS(tmp_xs)) return false;
                            if (hy.tileClr(tmp_xs) == 3) chanta_tupai_contain = true;
                        }
                        else //副露順子
                        {
                            if (hy.tileNum(tmp_xs) != 1 && hy.tileNum(tmp_xs) != 7) return false;
                            shuntsu_count++;
                        }
                    }

                    if (!chanta_tupai_contain) return false; //純チャン
                    if (shuntsu_count == 0) return false; //ホンロー

                    //Debug.Print("混全帯幺九");
                    return true;
                    break;
                    #endregion
                case 24: //一気通貫
                    #region 一気通貫
                    int[,] ittsu_elm = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; //色,番号

                    //手牌の中について
                    for (int idx = 0; idx < md.th_typecount[TYPE_SHUNTSU]; idx++)
                    {
                        int tmp_xs = md.th_info[TYPE_SHUNTSU, idx];
                        int tmp_num = hy.tileNum(tmp_xs);
                        if (tmp_num != 1 && tmp_num != 4 && tmp_num != 7) continue;
                        int numtoIndex = (int)((tmp_num - 1) / 3);
                        ittsu_elm[hy.tileClr(tmp_xs), numtoIndex] = 1;
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;
                        int tmp_num = hy.tileNum(tmp_xs);

                        if (ad.furotype[f] == TYPE_FSHUNTSU)
                        {
                            if (tmp_num != 1 && tmp_num != 4 && tmp_num != 7) continue;
                            int numtoIndex = (int)((tmp_num - 1) / 3);
                            ittsu_elm[hy.tileClr(tmp_xs), numtoIndex] = 1;
                        }
                    }

                    if ((ittsu_elm[0, 0] == 0 || ittsu_elm[0, 1] == 0 || ittsu_elm[0, 2] == 0) &&
                        (ittsu_elm[1, 0] == 0 || ittsu_elm[1, 1] == 0 || ittsu_elm[1, 2] == 0) &&
                        (ittsu_elm[2, 0] == 0 || ittsu_elm[2, 1] == 0 || ittsu_elm[2, 2] == 0)) return false;

                    //Debug.Print("一気通貫");
                    return true;
                    break;
                    #endregion
                case 25: //三色同順
                    #region 三色同順
                    int[,] sanshoku_elm = new int[3, 10] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                           { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
                                                           { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
                    //色、数字 数字をそのまま使うので0には注意

                    //手牌の中について
                    for (int idx = 0; idx < md.th_typecount[TYPE_SHUNTSU]; idx++)
                    {
                        int tmp_xs = md.th_info[TYPE_SHUNTSU, idx];
                        sanshoku_elm[hy.tileClr(tmp_xs), hy.tileNum(tmp_xs)] = 1;
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] == TYPE_FSHUNTSU)
                        {
                            sanshoku_elm[hy.tileClr(tmp_xs), hy.tileNum(tmp_xs)] = 1;
                        }
                    }

                    if ((sanshoku_elm[0, 1] == 0 || sanshoku_elm[1, 1] == 0 || sanshoku_elm[2, 1] == 0) &&
                        (sanshoku_elm[0, 2] == 0 || sanshoku_elm[1, 2] == 0 || sanshoku_elm[2, 2] == 0) &&
                        (sanshoku_elm[0, 3] == 0 || sanshoku_elm[1, 3] == 0 || sanshoku_elm[2, 3] == 0) &&
                        (sanshoku_elm[0, 4] == 0 || sanshoku_elm[1, 4] == 0 || sanshoku_elm[2, 4] == 0) &&
                        (sanshoku_elm[0, 5] == 0 || sanshoku_elm[1, 5] == 0 || sanshoku_elm[2, 5] == 0) &&
                        (sanshoku_elm[0, 6] == 0 || sanshoku_elm[1, 6] == 0 || sanshoku_elm[2, 6] == 0) &&
                        (sanshoku_elm[0, 7] == 0 || sanshoku_elm[1, 7] == 0 || sanshoku_elm[2, 7] == 0)) return false;

                    //Debug.Print("三色同順");
                    return true;
                    break;
                    #endregion
                case 26: //三色同刻
                    #region 三色同刻
                    int[,] doukou_elm = new int[3, 10] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
                                                         { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
                    //色、数字 数字をそのまま使うので0には注意

                    //手牌の中について
                    for (int type = TYPE_ANKO; type <= TYPE_MINKAN; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {

                            int tmp_xs = md.th_info[type, idx];
                            if (hy.tileClr(tmp_xs) == 3) continue;

                            doukou_elm[hy.tileClr(tmp_xs), hy.tileNum(tmp_xs)] = 1;

                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;
                        if (hy.tileClr(tmp_xs) == 3) continue;

                        if (ad.furotype[f] != TYPE_FSHUNTSU)
                        {
                            doukou_elm[hy.tileClr(tmp_xs), hy.tileNum(tmp_xs)] = 1;
                        }
                    }

                    if ((doukou_elm[0, 1] == 0 || doukou_elm[1, 1] == 0 || doukou_elm[2, 1] == 0) &&
                        (doukou_elm[0, 2] == 0 || doukou_elm[1, 2] == 0 || doukou_elm[2, 2] == 0) &&
                        (doukou_elm[0, 3] == 0 || doukou_elm[1, 3] == 0 || doukou_elm[2, 3] == 0) &&
                        (doukou_elm[0, 4] == 0 || doukou_elm[1, 4] == 0 || doukou_elm[2, 4] == 0) &&
                        (doukou_elm[0, 5] == 0 || doukou_elm[1, 5] == 0 || doukou_elm[2, 5] == 0) &&
                        (doukou_elm[0, 6] == 0 || doukou_elm[1, 6] == 0 || doukou_elm[2, 6] == 0) &&
                        (doukou_elm[0, 7] == 0 || doukou_elm[1, 7] == 0 || doukou_elm[2, 7] == 0) &&
                        (doukou_elm[0, 8] == 0 || doukou_elm[1, 8] == 0 || doukou_elm[2, 8] == 0) &&
                        (doukou_elm[0, 9] == 0 || doukou_elm[1, 9] == 0 || doukou_elm[2, 9] == 0)) return false;

                    //Debug.Print("三色同刻");
                    return true;
                    break;
                    #endregion
                case 27: //三槓子
                    #region 三槓子
                    int sankantsu_kantsucount = 0;
                    if (md.furo_suu == 0) return false;

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] == TYPE_MINKAN || ad.furotype[f] == TYPE_ANKAN)
                        {
                            sankantsu_kantsucount++;
                        }
                    }

                    if (sankantsu_kantsucount != 3) return false;

                    //Debug.Print("三槓子");
                    return true;
                    break;
                    #endregion
                case 28: //対々和
                    #region 対々和
                    //前チェック
                    if (md.th_typecount[TYPE_SHUNTSU] != 0) return false;

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] == TYPE_FSHUNTSU)
                        {
                            return false;
                        }
                    }

                    //Debug.Print("対々和");
                    return true;
                    break;
                    #endregion
                case 29: //三暗刻
                    #region 三暗刻
                    int anko_suu = 0;

                    anko_suu = md.th_typecount[TYPE_ANKO];
                    if (!ad.istsumoagari && md.ag_type == TYPE_ANKO) anko_suu -= 1;

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] == TYPE_ANKAN)
                        {
                            anko_suu++;
                        }
                    }

                    if (anko_suu != 3) return false;

                    //Debug.Print("三暗刻");
                    return true;
                    break;
                    #endregion
                case 30: //小三元
                    #region 小三元
                    int[] shousan_elm = new int[3] { 0, 0, 0 };

                    //雀頭検査
                    int shousan_head_xs = md.th_info[TYPE_ATAMA, 0];
                    if (shousan_head_xs != 32 && shousan_head_xs != 33 && shousan_head_xs != 34) return false;
                    shousan_elm[shousan_head_xs - 32] = 1;

                    //手牌の中について
                    for (int type = TYPE_ANKO; type <= TYPE_MINKAN; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];
                            if (tmp_xs < 32) continue;
                            shousan_elm[tmp_xs - 32] = 1;
                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU)
                        {
                            if (tmp_xs < 32) continue;
                            shousan_elm[tmp_xs - 32] = 1;
                        }
                    }

                    if ((shousan_elm[0] == 0 || shousan_elm[1] == 0 || shousan_elm[2] == 0)) return false;

                    //Debug.Print("小三元");
                    return true;
                    break;
                    #endregion
                case 31: //混老頭
                    #region 混老頭
                    //前チェック
                    if (md.th_typecount[TYPE_SHUNTSU] != 0) return false;

                    bool honro_tupai_contain = false;

                    //手牌の中について
                    for (int type = TYPE_ATAMA; type <= TYPE_ANKO; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];

                            if (!hy.isYaochuXS(tmp_xs)) return false;
                            if (hy.tileClr(tmp_xs) == 3) honro_tupai_contain = true;
                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU) //対子系
                        {
                            if (!hy.isYaochuXS(tmp_xs)) return false;
                            if (hy.tileClr(tmp_xs) == 3) honro_tupai_contain = true;
                        }
                        else //副露順子
                        {
                            return false;
                        }
                    }

                    if (!honro_tupai_contain) return false; //チンロートー

                    //Debug.Print("混老頭");
                    return true;
                    break;
                    #endregion
                case 32: //二盃口
                    #region 二盃口
                    //前チェック
                    if (md.menzenbreak) return false;
                    if (md.th_typecount[TYPE_SHUNTSU] < 4) return false;

                    //順子検査
                    //対象順子はそれぞれ隣り.
                    onaji_shuntsu_cnt = 0;
                    prev_doubled = false;

                    for (int idx = 1; idx < md.th_typecount[TYPE_SHUNTSU]; idx++)
                    {
                        if (prev_doubled)
                        {
                            prev_doubled = false;
                            continue;
                        }

                        if (md.th_info[TYPE_SHUNTSU, idx] == md.th_info[TYPE_SHUNTSU, idx - 1])
                        {
                            onaji_shuntsu_cnt++;
                            prev_doubled = true;
                        }
                    }

                    if (onaji_shuntsu_cnt != 2) return false;

                    //Debug.Print("二盃口");
                    return true;
                    break;
                    #endregion
                case 33: //純全帯幺九
                    #region 純全帯幺九
                    shuntsu_count = 0;

                    //手牌の中について
                    for (int type = TYPE_ATAMA; type <= TYPE_SHUNTSU; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];

                            if (type != TYPE_SHUNTSU) //対子系
                            {
                                if (!hy.isYaochuXS(tmp_xs)) return false;
                                if (hy.tileClr(tmp_xs) == 3) return false;
                            }
                            else //順子
                            {
                                if (hy.tileNum(tmp_xs) != 1 && hy.tileNum(tmp_xs) != 7) return false;
                                shuntsu_count++;
                            }

                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU) //対子系
                        {
                            if (!hy.isYaochuXS(tmp_xs)) return false;
                            if (hy.tileClr(tmp_xs) == 3) return false;
                        }
                        else //副露順子
                        {
                            if (hy.tileNum(tmp_xs) != 1 && hy.tileNum(tmp_xs) != 7) return false;
                            shuntsu_count++;
                        }
                    }

                    if (shuntsu_count == 0) return false; //チンロートー

                    //Debug.Print("純全帯幺九");
                    return true;
                    break;
                    #endregion
                case 39: //大三元
                    #region 大三元
                    int[] daisan_elm = new int[3] { 0, 0, 0 };

                    //雀頭検査
                    int daisan_head_xs = md.th_info[TYPE_ATAMA, 0];
                    if (daisan_head_xs == 32 || daisan_head_xs == 33 || daisan_head_xs == 34) return false;

                    //手牌の中について
                    for (int type = TYPE_ANKO; type <= TYPE_MINKAN; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];
                            if (tmp_xs < 32) continue;
                            daisan_elm[tmp_xs - 32] = 1;
                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU)
                        {
                            if (tmp_xs < 32) continue;
                            daisan_elm[tmp_xs - 32] = 1;
                        }
                    }

                    if ((daisan_elm[0] == 0 || daisan_elm[1] == 0 || daisan_elm[2] == 0)) return false;

                    //Debug.Print("大三元");
                    return true;
                    break;
                    #endregion
                case 40: //四暗刻
                    #region 四暗刻
                    anko_suu = 0;

                    anko_suu = md.th_typecount[TYPE_ANKO];
                    if (!ad.istsumoagari && md.ag_type == TYPE_ANKO) anko_suu -= 1;
                    if (md.ag_type == TYPE_ATAMA) return false;

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] == TYPE_ANKAN)
                        {
                            anko_suu++;
                        }
                    }

                    if (anko_suu != 4) return false;

                    //Debug.Print("四暗刻");
                    return true;
                    break;
                    #endregion
                case 41: //四暗刻単騎
                    #region 四暗刻単騎
                    anko_suu = 0;

                    anko_suu = md.th_typecount[TYPE_ANKO];
                    if (!ad.istsumoagari && md.ag_type == TYPE_ANKO) return false;
                    if (md.ag_type != TYPE_ATAMA) return false;

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] == TYPE_ANKAN)
                        {
                            anko_suu++;
                        }
                    }

                    if (anko_suu != 4) return false;

                    //Debug.Print("四暗刻単騎");
                    return true;
                    break;
                    #endregion
                case 42: //字一色
                    #region 字一色
                    //前チェック
                    if (md.th_typecount[TYPE_SHUNTSU] != 0) return false;

                    //手牌の中について
                    for (int type = TYPE_ATAMA; type <= TYPE_ANKO; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];

                            if (hy.tileClr(tmp_xs) != 3) return false;
                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU) //対子系
                        {
                            if (hy.tileClr(tmp_xs) != 3) return false;
                        }
                        else //副露順子
                        {
                            return false;
                        }
                    }

                    //Debug.Print("字一色");
                    return true;
                    break;
                    #endregion
                case 43: //緑一色
                    #region 緑一色

                    //發なし緑一色を許可する
                    bool Admit_HATSUNashi = true;

                    //　　　2s,3s,4s,6s,8s,發
                    //   → 20,21,22,24,26,33

                    bool ryu_hatsu_contain = false;

                    //手牌の中について
                    for (int type = TYPE_ATAMA; type <= TYPE_SHUNTSU; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];

                            if (type == TYPE_SHUNTSU)
                            {
                                if (tmp_xs != 20) return false;
                            }
                            else
                            {
                                if (tmp_xs != 20 && tmp_xs != 21 && tmp_xs != 22 &&
                                    tmp_xs != 24 && tmp_xs != 26 && tmp_xs != 33) return false;
                                if (tmp_xs == 33) ryu_hatsu_contain = true;
                            }
                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] == TYPE_FSHUNTSU) //順子
                        {
                            if (tmp_xs != 20) return false;
                        }
                        else //刻子
                        {
                            if (tmp_xs != 20 && tmp_xs != 21 && tmp_xs != 22 &&
                                tmp_xs != 24 && tmp_xs != 26 && tmp_xs != 33) return false;
                            if (tmp_xs == 33) ryu_hatsu_contain = true;
                        }
                    }

                    if (!Admit_HATSUNashi && ryu_hatsu_contain) return false; //發なしを認めない場合

                    //Debug.Print("緑一色");
                    return true;
                    break;
                    #endregion
                case 44: //清老頭
                    #region 清老頭
                    //前チェック
                    if (md.th_typecount[TYPE_SHUNTSU] != 0) return false;

                    //手牌の中について
                    for (int type = TYPE_ATAMA; type <= TYPE_ANKO; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];
                            if (!hy.isYaochuXS(tmp_xs)) return false;
                            if (hy.tileClr(tmp_xs) == 3) return false;
                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU) //対子系
                        {
                            if (!hy.isYaochuXS(tmp_xs)) return false;
                            if (hy.tileClr(tmp_xs) == 3) return false;
                        }
                        else //副露順子
                        {
                            return false;
                        }
                    }

                    //Debug.Print("清老頭");
                    return true;
                    break;
                    #endregion
                case 49: //大四喜
                    #region 大四喜
                    int[] daisushi_elm = new int[4] { 0, 0, 0, 0 };

                    //雀頭検査
                    int daisushi_head_xs = md.th_info[TYPE_ATAMA, 0];
                    if (daisushi_head_xs == 28 || daisushi_head_xs == 29 ||
                        daisushi_head_xs == 30 || daisushi_head_xs == 31) return false;

                    //手牌の中について
                    for (int type = TYPE_ANKO; type <= TYPE_MINKAN; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];
                            if (tmp_xs < 28 || tmp_xs >= 32) continue;
                            daisushi_elm[tmp_xs - 28] = 1;
                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU)
                        {
                            if (tmp_xs < 28 || tmp_xs >= 32) continue;
                            daisushi_elm[tmp_xs - 28] = 1;
                        }
                    }

                    if (daisushi_elm[0] == 0 || daisushi_elm[1] == 0 ||
                        daisushi_elm[2] == 0 || daisushi_elm[3] == 0) return false;

                    //Debug.Print("大四喜");
                    return true;
                    break;
                    #endregion
                case 50: //小四喜
                    #region 小四喜
                    int[] shousushi_elm = new int[4] { 0, 0, 0, 0 };

                    //雀頭検査
                    int shousushi_head_xs = md.th_info[TYPE_ATAMA, 0];
                    if (shousushi_head_xs != 28 && shousushi_head_xs != 29 &&
                        shousushi_head_xs != 30 && shousushi_head_xs != 31) return false;

                    shousushi_elm[shousushi_head_xs - 28] = 1;

                    //手牌の中について
                    for (int type = TYPE_ANKO; type <= TYPE_MINKAN; type++)
                    {
                        for (int idx = 0; idx < md.th_typecount[type]; idx++)
                        {
                            int tmp_xs = md.th_info[type, idx];
                            if (tmp_xs < 28 || tmp_xs >= 32) continue;
                            shousushi_elm[tmp_xs - 28] = 1;
                        }
                    }

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] != TYPE_FSHUNTSU)
                        {
                            if (tmp_xs < 28 || tmp_xs >= 32) continue;
                            shousushi_elm[tmp_xs - 28] = 1;
                        }
                    }

                    if ((shousushi_elm[0] == 0 || shousushi_elm[1] == 0 ||
                         shousushi_elm[2] == 0 || shousushi_elm[3] == 0)) return false;

                    //Debug.Print("小四喜");
                    return true;
                    break;
                    #endregion
                case 51: //四槓子
                    #region 四槓子
                    int sukantsu_kantsucount = 0;
                    if (md.furo_suu == 0) return false;

                    //副露した牌について
                    for (int f = 0; f < ad.furotehai.Length; f++)
                    {
                        int tmp_xs = ad.furotehai[f][0];
                        if (tmp_xs == 0) break;

                        if (ad.furotype[f] == TYPE_MINKAN || ad.furotype[f] == TYPE_ANKAN)
                        {
                            sukantsu_kantsucount++;
                        }
                    }

                    if (sukantsu_kantsucount != 4) return false;

                    //Debug.Print("四槓子");
                    return true;
                    break;
                    #endregion
            }

            return false;

        }

        public bool Yaku_K_Chitoi_Judge(int yakuNum, int[] info, AGARI_DATA ad)
        {
            //このメソッドを使用するのは、七対子が成立していることが前提となる。
            if (yakuNum == 22) return true;

            switch (yakuNum)
            {
                case 0: //門前清自摸和
                    #region 門前清自摸和
                    if (!ad.istsumoagari) return false;

                    //Debug.Print("門前清自摸和");
                    return true;
                    break;
                    #endregion
                case 8: //断幺九
                    #region 断幺九
                    //手牌の中について
                    for (int i = 0; i < info.Length; i++)
                    {
                        int tmp_xs = info[i];
                        if (hy.isYaochuXS(tmp_xs)) return false;
                    }
                    //Debug.Print("断幺九");
                    return true;
                    break;
                    #endregion
                case 31: //混老頭
                    #region 混老頭
                    for (int i = 0; i < info.Length; i++)
                    {
                        int tmp_xs = info[i];
                        if (!hy.isYaochuXS(tmp_xs)) return false;
                    }
                    //Debug.Print("混老頭");
                    return true;
                    break;
                    #endregion
                case 34: //混一色
                    #region 混一色
                    int honitsu_clr = 3;
                    bool tupai_contain = false;
                    for (int i = 0; i < info.Length; i++)
                    {
                        int tmp_xs = info[i];
                        int tmp_clr = hy.tileClr(tmp_xs);
                        if (tmp_clr == 3) tupai_contain = true;
                        if (honitsu_clr != tmp_clr && tmp_clr != 3 && honitsu_clr != 3)
                        {
                            return false;
                        }
                        else if (tmp_clr != 3 && honitsu_clr == 3)
                        {
                            honitsu_clr = tmp_clr;
                        }
                    }
                    if (!tupai_contain) return false;
                    if (honitsu_clr == 3) return false; //字一色
                    //Debug.Print("混一色");
                    return true;
                    break;
                    #endregion
                case 35: //清一色
                    #region 清一色
                    int chin_clr = -1;
                    for (int i = 0; i < info.Length; i++)
                    {
                        int tmp_xs = info[i];
                        int tmp_clr = hy.tileClr(tmp_xs);
                        if (tmp_clr == 3) return false;
                        if (chin_clr != tmp_clr && chin_clr != -1)
                        {
                            return false;
                        }
                        else if (chin_clr == -1)
                        {
                            chin_clr = tmp_clr;
                        }
                    }
                    //Debug.Print("清一色");
                    return true;
                    break;
                    #endregion
                case 42: //字一色
                    #region 字一色
                    for (int i = 0; i < info.Length; i++)
                    {
                        int tmp_xs = info[i];
                        int tmp_clr = hy.tileClr(tmp_xs);
                        if (tmp_clr != 3) return false;
                    }
                    //Debug.Print("字一色");
                    return true;
                    break;
                    #endregion

            }

            return false;
        }

        public bool Yaku_DoubleCut(ref bool[] ylist)
        {
            //役の重複削除

            bool ykm = false; //役満フラグ

            if (ylist.Length < 54)
            {
                Debug.Print("役リストの長さが不足しています.");
            }

            //役満チェック
            for (int i = 36; i < ylist.Length; i++)
            {
                if (ylist[i])
                {
                    ykm = true;
                    for (int y = 0; y < 36; y++)
                    {
                        ylist[y] = false;
                    }
                }
            }

            return ykm;
        }

        public void Yakulist_view(bool[] ylist, out string out_yakustr)
        {
            //役リストを表示

            string s = "";
            int ycount = 0;

            for (int y = 0; y < ylist.Length; y++)
            {
                if (ylist[y])
                {
                    if (ycount != 0) s += " / ";
                    s += YAKU_STR[y];
                    ycount++;
                }

            }

            out_yakustr = s;
            Debug.Print(s);

        }

        public int Yaku_K_CalcFan(bool[] ylist, bool Menzenbreak, out bool out_ykm)
        {
            bool ykm = false;
            int Fan = 0;

            //【１飜】
            //●00 門前清自摸和 ○05 海底摸月     ●10 自風 東       ●15 場風 南
            //○01 立直         ○06 河底撈魚     ●11 自風 南       ●16 場風 西
            //○02 一発         ●07 平和         ●12 自風 西       ●17 場風 北
            //○03 槍槓         ●08 断幺九       ●13 自風 北       ●18 役牌 白
            //○04 嶺上開花     ●09 一盃口       ●14 場風 東       ●19 役牌 發
            //                                                       ●20 役牌 中   

            //【２飜】(23 24 25)
            //○21 両立直       ●26 三色同刻     ●31 混老頭 
            //●22 七対子       ●27 三槓子    
            //●23 混全帯幺九   ●28 対々和    
            //●24 一気通貫     ●29 三暗刻   
            //●25 三色同順     ●30 小三元   

            //【３飜】(33 34)  【６飜】(35)
            //●32 二盃口       ●35 清一色
            //●33 純全帯幺九                 
            //●34 混一色  

            //【役満】
            //○36 人和        ●41 四暗刻単騎   ●46 純正九蓮宝燈     ●51 四槓子
            //○37 天和        ●42 字一色       ●47 国士無双      
            //○38 地和        ●43 緑一色       ●48 国士無双１３面 
            //●39 大三元      ●44 清老頭       ●49 大四喜  
            //●40 四暗刻      ●45 九蓮宝燈     ●50 小四喜         

            int[] fansuu = new int[55]{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,3,3,3,6,
                13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,13,1,1,1};

            for (int i = 0; i < 36; i++)
            {
                if (ylist[i] && Menzenbreak && (i == 23 || i == 24 || i == 25 || i == 33 || i == 34 || i == 35))
                {
                    Fan += fansuu[i] - 1;
                }
                else if (ylist[i])
                {
                    Fan += fansuu[i];
                }
            }

            for (int i = 36; i < ylist.Length; i++)
            {
                if (ylist[i])
                {
                    ykm = true;
                }

            }

            if (!ykm)
            {
                out_ykm = ykm;
                return Fan;
            }

            //=============================役満========================

            Fan = 0;

            for (int i = 36; i < ylist.Length; i++)
            {
                if (ylist[i])
                {
                    Fan++;
                }

            }

            out_ykm = ykm;
            return Fan;
        }

    }
}
