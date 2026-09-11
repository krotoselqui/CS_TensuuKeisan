using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace MahjongScoreTrainer
{

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
            ShowTitle();
            GenerationOptions options = ReadOptions(Console.In, Console.Out);
            Random random = options.Seed == -1 ? r : new Random(options.Seed);
            int[] counts;
            using (var questions = new System.IO.StreamWriter("question.txt", false, Encoding.GetEncoding("shift_jis")))
            using (var answers = new System.IO.StreamWriter("answer.txt", false, Encoding.GetEncoding("shift_jis")))
            using (var details = new System.IO.StreamWriter("answer_yaku.txt", false, Encoding.GetEncoding("shift_jis")))
            {
                var progress = new ConsoleProgress();
                counts = GenerateProblems(options, random, questions, answers, details, progress.Report);
            }
            ShowSummary(counts);
            Console.ReadKey();
        }

        internal static GenerationOptions ReadOptions(System.IO.TextReader input, System.IO.TextWriter output)
        {
            int problemCount = ReadInteger(input, output, "問題数を入力 : ", value => value >= 1);
            int seed = ReadInteger(input, output, "シード値を入力（ ランダムなら: -1 ）: ", value => value >= -1);
            int displayMode = ReadInteger(input, output, "出力形式を入力（ 専用フォント: 0 通常の牌記号: 1 ）: ", value => value == 0 || value == 1);
            return new GenerationOptions(problemCount, seed, displayMode);
        }

        private static int ReadInteger(System.IO.TextReader input, System.IO.TextWriter output, string prompt, Func<int, bool> isValid)
        {
            while (true)
            {
                output.Write(prompt);
                int value;
                // EOF still retries, matching the existing interactive input contract.
                if (int.TryParse(input.ReadLine(), out value) && isValid(value)) return value;
            }
        }

        private sealed class WinningCondition
        {
            internal readonly int RoundWind;
            internal readonly int SeatWind;
            internal readonly bool IsTsumo;
            internal readonly string Label;

            internal WinningCondition(int roundWind, int seatWind, bool isTsumo, string label)
            {
                RoundWind = roundWind;
                SeatWind = seatWind;
                IsTsumo = isTsumo;
                Label = label;
            }
        }

        private static readonly WinningCondition[] WinningConditions =
        {
            new WinningCondition(0, 0, true, "東/東/ツモ"),
            new WinningCondition(0, 0, false, "東/東/ロン"),
            new WinningCondition(0, 2, true, "東/西/ツモ"),
            new WinningCondition(0, 2, false, "東/西/ロン")
        };

        private sealed class EvaluatedAnswer
        {
            internal readonly WinningCondition Condition;
            internal readonly ScoreResult Score;
            internal readonly bool[] Yaku;
            internal readonly string YakuText;

            internal EvaluatedAnswer(WinningCondition condition, ScoreResult score, bool[] yaku, string yakuText)
            {
                Condition = condition;
                Score = score;
                Yaku = yaku;
                YakuText = yakuText;
            }
        }

        private static EvaluatedAnswer[] EvaluateAnswers(int[] tiles, int[][] melds, int[] meldTypes, int winningTile)
        {
            var evaluator = new HandEvaluator();
            var answers = new EvaluatedAnswer[WinningConditions.Length];
            for (int i = 0; i < WinningConditions.Length; i++)
            {
                WinningCondition condition = WinningConditions[i];
                var hand = new WinningHandData(tiles, melds, meldTypes, winningTile,
                    condition.RoundWind, condition.SeatWind, condition.IsTsumo);
                bool[] yaku;
                ScoreResult score;
                evaluator.Yaku_Keishiki(out yaku, out score, hand);
                string yakuText;
                evaluator.Yakulist_view(yaku, out yakuText);
                answers[i] = new EvaluatedAnswer(condition, score, yaku, yakuText);
            }
            return answers;
        }

        private static string FormatPayment(EvaluatedAnswer answer)
        {
            if (!answer.Condition.IsTsumo) return answer.Score.scoresum.ToString();
            if (answer.Condition.SeatWind == 0) return answer.Score.other_pay.ToString() + "∀";
            return answer.Score.other_pay.ToString() + "-" + answer.Score.dealer_pay.ToString();
        }

        private static string FormatDetails(EvaluatedAnswer answer)
        {
            ScoreResult score = answer.Score;
            string value = score.ykm
                ? (score.fan == 1 ? "役満 " : score.fan.ToString() + "倍役満 ")
                : score.fu.ToString() + "符" + score.fan.ToString() + "飜 ";
            return answer.Condition.Label + " " + value + answer.YakuText;
        }

        private static void WriteAnswers(int number, EvaluatedAnswer[] results,
            System.IO.TextWriter answers, System.IO.TextWriter details, int[] counts)
        {
            string prefix = "[" + number.ToString() + "] ";
            details.WriteLine(prefix);
            var payments = new string[results.Length];
            for (int i = 0; i < results.Length; i++)
            {
                EvaluatedAnswer result = results[i];
                payments[i] = FormatPayment(result);
                details.WriteLine(FormatDetails(result));
                for (int y = 0; y < YakuSelector.Entries.Length; y++)
                {
                    if (result.Yaku[YakuSelector.Entries[y].Id]) counts[y]++;
                }
            }
            answers.WriteLine(prefix + string.Join(" / ", payments));
        }

        internal static int[] GenerateProblems(GenerationOptions options, Random random,
            System.IO.TextWriter questions, System.IO.TextWriter answers, System.IO.TextWriter details,
            Action<int, int> reportProgress = null)
        {
            if (options == null) throw new ArgumentNullException("options");
            if (random == null) throw new ArgumentNullException("random");
            if (questions == null) throw new ArgumentNullException("questions");
            if (answers == null) throw new ArgumentNullException("answers");
            if (details == null) throw new ArgumentNullException("details");

            // Temporary bridge to the unchanged legacy hand generator. Sequential use only.
            Random previousRandom = r;
            int previousMode = PaiDisp_Mode;
            r = random;
            PaiDisp_Mode = options.DisplayMode;
            try
            {
                var counts = new int[YakuSelector.Entries.Length];
                for (int i = 0; i < options.ProblemCount; i++)
                {
                    if (reportProgress != null) reportProgress(i, options.ProblemCount);
                    int yakuId = YakuSelector.Select(random);
                    int[] tiles;
                    int[][] melds;
                    int[] meldTypes;
                    int winningTile;
                    string question = makeYaku(yakuId, out tiles, out melds, out meldTypes, out winningTile);
                    string prefix = options.DisplayMode == 1 ? "[" + (i + 1).ToString() + "]" : "";
                    questions.WriteLine(prefix + question);
                    Debug.Print(question);
                    EvaluatedAnswer[] results = EvaluateAnswers(tiles, melds, meldTypes, winningTile);
                    WriteAnswers(i + 1, results, answers, details, counts);
                }
                return counts;
            }
            finally
            {
                r = previousRandom;
                PaiDisp_Mode = previousMode;
            }
        }


        static void ShowTitle()
        {
            Console.WriteLine("");
            Console.WriteLine("                        　　ﾜｧｲ                   ");
            Console.WriteLine("・゜・*:.｡..｡.:*・゜(n'∀')ηﾟ・*:.｡. .｡.:*・゜・*");
            Console.WriteLine("                        　　                      ");
            Console.WriteLine("・゜・*:.｡  よい子のための点数計算  ｡ .｡.:*・゜・*");
            //Console.WriteLine("                   [ 試用版 ]   　　              ");
            Console.WriteLine("                        　　                      ");
            Console.WriteLine("・゜・*:.｡..｡.:*・*:.｡. .｡.:*ﾟ・*:.｡. .｡.:*・゜・*");
            Console.WriteLine("");
        }

        static void ShowSummary(int[] yakuResultCount)
        {
            Console.WriteLine();
            Console.WriteLine(" Finished.");

            Console.WriteLine();
            for (int y = 0; y < YakuSelector.Entries.Length; y++)
            {
                string yakuname = YAKU_STR[YakuSelector.Entries[y].Id];
                Console.WriteLine(yakuname + " : " + yakuResultCount[y].ToString() + " 回");
            }

            Console.WriteLine("何かキーを押すと終了します...");
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
                YakuMeldGenerator.GeneratePinfu(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 8) //断幺九
            {
                YakuMeldGenerator.GenerateTanyao(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 9) //一盃口
            {
                YakuMeldGenerator.GenerateIipeikou(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 14 || yakuNum == 18 || yakuNum == 19 || yakuNum == 20) //役牌
            {
                YakuMeldGenerator.GenerateYakuhai(yakuNum, ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 23) //混全帯幺九
            {
                YakuMeldGenerator.GenerateHonchantaiyaochuu(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 24) //一気通貫
            {
                YakuMeldGenerator.GenerateIttsu(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 25) //三色同順
            {
                YakuMeldGenerator.GenerateSanshokuDoujun(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 26) //三色同刻
            {

                //はじめの3刻子
                int sansyokunum = r.Next(7) + 1;
                int kan_bairitsu = 10;
                int furo_bairitsu = 10; //でかいほど鳴かない

                for (int i = 0; i < 3; i++)
                {
                    int kan = RandomSelection.RetOne(kan_bairitsu);

                    if (kan == 0)
                    {
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, i, sansyokunum);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else
                    {
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, i, sansyokunum);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);
                    }

                }


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗刻・明刻
                    {
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }


                }

                MeldGenerator.MakeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
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
                    MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                    partsXS[i] = xsparts;
                    partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);
                }


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗刻・明刻
                    {
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }


                }

                MeldGenerator.MakeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
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
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }


                }

                MeldGenerator.MakeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
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
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3;
                    }
                    else  //刻子
                    {
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1;
                    }
                }


                for (int i = 3; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)


                    if (shurui == 0) //暗刻・明刻
                    {
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 2;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 4;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }


                }

                MeldGenerator.MakeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
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
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, 3, kotsuNum[i]);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, 3, kotsuNum[i]);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }

                }


                for (int i = 2; i < 4; i++)
                {
                    int shurui = r.Next(20); //弄るとフーロ率が変化(他は順子に。)
                    int tmpclr = r.Next(3);

                    if (shurui == 0) //暗刻・明刻
                    {
                        //三元牌が入るとまずいので避ける

                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, tmpclr);
                        partsXS[i] = xsparts;
                        partsType[i] = 2;
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        //三元牌が入るとまずいので避ける

                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, tmpclr);
                        partsXS[i] = xsparts;
                        partsType[i] = 4;

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }


                }

                MeldGenerator.MakeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts, 3, toitsuNum);
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
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //刻子
                    {
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }


                }

                MeldGenerator.MakeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
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

                MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[0] = xsparts;
                partsType[0] = 5;

                int settledClr = TileUtilities.GetColor(xsparts[0]);
                int settledNum = TileUtilities.GetNumber(xsparts[0]);

                MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, settledClr, settledNum);
                partsXS[1] = xsparts;
                partsType[1] = 5;


                MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                partsXS[2] = xsparts;
                partsType[2] = 5;

                settledClr = TileUtilities.GetColor(xsparts[0]);
                settledNum = TileUtilities.GetNumber(xsparts[0]);

                MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, settledClr, settledNum);
                partsXS[3] = xsparts;
                partsType[3] = 5;

                MeldGenerator.MakeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
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
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);

                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }


                }

                MeldGenerator.MakeRandomAtama(yakuNum, ref maisu, out strparts, out xsparts);
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
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, tmp_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, tmp_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);
                        kanoccurCount++;
                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }


                }

                MeldGenerator.MakeRandomAtama(8, ref maisu, out strparts, out xsparts, some_clr);
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
                        MeldGenerator.MakeRandomKotsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 1 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else if (shurui == 1) //暗槓・暗槓
                    {
                        MeldGenerator.MakeRandomKantsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 3 + RandomSelection.RetOne(furo_bairitsu);
                        kanoccurCount++;
                    }
                    else if (shurui == 2) //順子・副露順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 5 + RandomSelection.RetOne(furo_bairitsu);
                    }
                    else  //順子
                    {
                        MeldGenerator.MakeRandomShuntsu(yakuNum, ref maisu, out strparts, out xsparts, some_clr);
                        partsXS[i] = xsparts;
                        partsType[i] = 5;
                    }

                    //Debug.Print("i = " + i.ToString() + " -> type" + partsType[i].ToString() + " >> " + xsparts[0].ToString());



                }

                MeldGenerator.MakeRandomAtama(8, ref maisu, out strparts, out xsparts, some_clr);
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
                YakuMeldGenerator.GenerateDaisangen(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 40) //四暗刻
            {
                YakuMeldGenerator.GenerateSuuankou(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 41) //四暗刻単騎
            {
                YakuMeldGenerator.GenerateSuuankouTanki(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 42) //字一色
            {
                YakuMeldGenerator.GenerateTsuiisou(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 43) //緑一色
            {
                YakuMeldGenerator.GenerateRyuuiisou(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 44) //清老頭
            {
                YakuMeldGenerator.GenerateChinroutou(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 49) //大四喜
            {
                YakuMeldGenerator.GenerateDaisuushi(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 50) //小四喜
            {
                YakuMeldGenerator.GenerateShousuushi(ref maisu, partsXS, partsType, ref machi_xs);
            }
                        else if (yakuNum == 51) //四槓子
            {
                YakuMeldGenerator.GenerateSuukantsu(ref maisu, partsXS, partsType, ref machi_xs);
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
            else
            {
                SpecialHandGenerator.Generate(yakuNum, ref maisu, ref tehai, ref machi_xs);
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







        #endregion


    }
}
