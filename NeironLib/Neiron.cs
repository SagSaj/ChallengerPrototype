using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace NeironLib
{
    public class Neiron
    {
        public List<Double> Weights;
        public Double AnswerIn { get; set; }
        public Double Answer { get; set; }
        public int Layear { get; set; }
        public Double DeltaMiss { get; set; }
        public List<Double> DWeights;
        public Neiron(int _Layear)
        {
            Layear = _Layear;
            Weights = new List<Double>();
        }
        public static Double ActivationFunction(Double d)
        {
            Double out1 = Convert.ToDouble(2/ (1 + Math.Exp(-Convert.ToDouble(d)))-1);
            // out1 = d;
            return out1;
        }
        public static Double DActivationFunction(Double d)
        {
            Double ActivationD = ActivationFunction(d);
            Double out1 = (1 - ActivationD) *(1+ActivationD)/2;
            // out1 = 1;
            return out1;
        }
        public static Double InvertActivationFunction(Double d)
        {
            if (d >= 1)
                return -100;
            if (d <= -1)
                return 100;
            Double out1 = -Math.Log(2/(d+1)-1);
            // out1 = d;
            return out1;
        }
        public void Summing(ref List<Neiron>[] Sheet)
        {
            Answer = 0;
            for (int i = 0; i < Sheet[Layear - 1].Count; i++)
            {
                Answer += Sheet[Layear - 1][i].Answer * Weights[i];
            }
            AnswerIn = Answer;
            Answer = ActivationFunction(Answer);
        }
        public void SummingInput(Double[] InputInformation)
        {
            Answer = 0;
            for (int i = 0; i < InputInformation.Length; i++)
            {
                Answer += InputInformation[i] * Weights[i];
            }
            AnswerIn = Answer;
            Answer = ActivationFunction(Answer);
        }
        public void LearnNeironInput(Double Sigma, Double Alfa, ref List<Neiron>[] Sheet)
        {
            DWeights = new List<Double>();
            DeltaMiss = (Sigma) * DActivationFunction(AnswerIn);
            for (int i = 0; i < Weights.Count; i++)
            {
                DWeights.Add(Alfa * DeltaMiss * Sheet[Layear - 1][i].Answer);
              //  Weights[i] += Alfa * DeltaMiss * Sheet[Layear - 1][i].Answer;
            }

        }
        public void LearnNeiron(Double Sigma, Double Alfa, ref List<Neiron>[] Sheet, ref Double[] Input)
        {
            DWeights = new List<Double>();
            DeltaMiss = Sigma * DActivationFunction(AnswerIn);
            if (Layear != 0)
                for (int i = 0; i < Weights.Count; i++)
                {
                    DWeights.Add(Alfa * DeltaMiss * Sheet[Layear - 1][i].Answer);
                   // Weights[i] += Alfa * DeltaMiss * Sheet[Layear - 1][i].Answer;
                }
            else
                for (int i = 0; i < Weights.Count; i++)
                {
                    DWeights.Add(Alfa * DeltaMiss * Input[i]);
                   // Weights[i] += Alfa * DeltaMiss * Input[i];
                }
        }
        public void ConfirmWeights()
        {
            for (int i = 0; i < Weights.Count; i++)
            {
                Weights[i] += DWeights[i];
            }
            DWeights.Clear();
        }
    };
    public class NeironSheet
    {
       
        //TableNeiron
        public List<Neiron>[] Sheet;
        //Alfa
        public Double Alfa = 0.05;
        public Double[] Answers;
        public Double[] Input;
        public int InputWeight = 0;
        public void InitiateNeironSheet(int[] countNeiron, int _InputWaight)//First layer -> Input,Last->Output
        {
            InputWeight = _InputWaight;
            Sheet = new List<Neiron>[countNeiron.Length];
            for (int i = 0; i < countNeiron.Length; i++)
            {
                Sheet[i] = new List<Neiron>();
                for (int j = 0; j < countNeiron[i]; j++)
                {
                    Sheet[i].Add(new Neiron(i));
                }
            }
            Random r = new Random();
            foreach (Neiron n in Sheet[0])
            {
                for (int i = 0; i < _InputWaight; i++)
                    n.Weights.Add(Convert.ToDouble(r.NextDouble()) - Convert.ToDouble(0.5));
            }
            for (int i = 1; i < countNeiron.Length; i++)
            {
                foreach (Neiron n in Sheet[i])
                {
                    for (int j = 0; j < Sheet[i - 1].Count; j++)
                        n.Weights.Add(Convert.ToDouble(r.NextDouble()) - Convert.ToDouble(0.5));
                }
            }
            //r.NextDouble()) - Convert.ToDouble(0.5)
        }
        //public double[] GetAnswerFromBack(Double[] InputInformation)
        //{
            
        //    //LinearAlgebra.Matricies.DoubleMatrix Matr = new LinearAlgebra.Matricies.DoubleMatrix();

        //    //LinearAlgebra.Matricies.DoubleMatrix Matr2 = Matr.PseudoInverse;
        //    //double[] Arry = new double[] { 1, 2 };
        //    //Arry = Matr2 * Arry;
        //    //Matr.GetType();
        //    double[] Answers = new double[Sheet[Sheet.Length - 1].Count];
        //    List<int> excludeW = new List<int>();
        //    List<double> excludeWAnswer = new List<double>();
        //    int expence;
        //    for (int j = Sheet.Length - 1; j > -1; j--)
        //    {
        //        Answers = new double[Sheet[j].Count];
        //        if (Sheet[j].Count > Sheet[j][0].Weights.Count)
        //            return new double[] { };
        //        Random r = new Random();
        //       excludeW = new List<int>();
        //        excludeWAnswer = new List<double>();
        //        {
        //            for (int i = 0; i < Sheet[j][0].Weights.Count - Sheet[j].Count; i++)
        //            {
        //                int p = 0;
        //                p = r.Next(Sheet[j][0].Weights.Count);
        //                if (excludeW.IndexOf(p) != -1)
        //                    i--;
        //                else
        //                {
        //                    excludeW.Add(p);
        //                    excludeWAnswer.Add(0);
        //                }
        //            }
        //        }

        //        LinearAlgebra.Matricies.DoubleMatrix Matr = new LinearAlgebra.Matricies.DoubleMatrix(Sheet[j].Count, Sheet[j].Count);
        //        ///
                
        //        for (int SummingIndexInLayear = 0; SummingIndexInLayear < Sheet[j].Count; SummingIndexInLayear++)
        //        {
        //            expence = 0;
        //            if (j == Sheet.Length - 1)
        //                Answers[SummingIndexInLayear] = Neiron.InvertActivationFunction(InputInformation[SummingIndexInLayear]);
        //            else
        //                Answers[SummingIndexInLayear] = Neiron.InvertActivationFunction(Sheet[j][SummingIndexInLayear].Answer);
        //            for (int z = 0; z < Sheet[j][SummingIndexInLayear].Weights.Count; z++)
        //            {
        //                if (excludeW.IndexOf(z) != -1)
        //                {
        //                    expence++;
        //                    Answers[SummingIndexInLayear] -= excludeWAnswer[excludeW.IndexOf(z)] * Sheet[j][SummingIndexInLayear].Weights[z];
        //                }
        //                else
        //                    Matr[SummingIndexInLayear, z - expence] = Sheet[j][SummingIndexInLayear].Weights[z];
        //            }

        //        }
        //        Answers = Matr.PseudoInverse * Answers;
        //        expence=0;
        //        if(j!=0)
        //            for (int SummingIndexInLayear = 0; SummingIndexInLayear < Sheet[j-1].Count; SummingIndexInLayear++)
        //            {
                        
        //                if (excludeW.IndexOf(SummingIndexInLayear) != -1)
        //                {
        //                    expence++;
        //                    Sheet[j-1][SummingIndexInLayear].Answer = excludeWAnswer[excludeW.IndexOf(SummingIndexInLayear)];
        //                }
        //                else
        //                    Sheet[j-1][SummingIndexInLayear].Answer=Answers[SummingIndexInLayear-expence];
        //            }
        //    }
        //    double[] ret = new double[InputWeight];
        //    expence = 0;
        //    for (int SummingIndexInLayear = 0; SummingIndexInLayear < InputWeight; SummingIndexInLayear++)
        //    {

        //        if (excludeW.IndexOf(SummingIndexInLayear) != -1)
        //        {
        //            expence++;
        //            ret[SummingIndexInLayear] = Neiron.ActivationFunction(excludeWAnswer[excludeW.IndexOf(SummingIndexInLayear)]);
        //        }
        //        else
        //            ret[SummingIndexInLayear] = Neiron.ActivationFunction(Answers[SummingIndexInLayear-expence]);
        //    }
        //    return ret;
        //}
        public void GetAnswer(Double[] InputInformation)
        {
            Input = InputInformation;
            for (int index = 0; index < Sheet[0].Count; index++)
            {
                Sheet[0][index].SummingInput(InputInformation);
            }
            for (int j = 1; j < Sheet.Length; j++)
            {
                for (int SummingIndexInLayear = 0; SummingIndexInLayear < Sheet[j].Count; SummingIndexInLayear++)
                {
                    Sheet[j][SummingIndexInLayear].Summing(ref Sheet);
                }
            }
            Answers = new Double[Sheet[Sheet.Length - 1].Count];
            for (int index = 0; index < Sheet[Sheet.Length - 1].Count; index++)
            {
                Answers[index] = Sheet[Sheet.Length - 1][index].Answer;
            }
        }
        public static Double GetSigma(Double RealAn,Double An)
        {
            return (RealAn-An);
        }
        public void LearnOutNeironInput(Double[] RealAnswer)//Simple Learning
        {
            Double[] RealInput = new Double[Input.Length];
            for (int index = 0; index < RealAnswer.Length; index++)
                Sheet[Sheet.Length - 1][index].LearnNeironInput(GetSigma(RealAnswer[index],Sheet[Sheet.Length - 1][index].Answer), Alfa, ref Sheet);
            for (int j = Sheet.Length - 2; j > -1; j--)
            {
                for (int LearningIndexInLayear = 0; LearningIndexInLayear < Sheet[j].Count; LearningIndexInLayear++)
                {
                    Double LearningSigma = 0;
                    for (int i = 0; i < Sheet[j + 1].Count; i++)
                        LearningSigma += Sheet[j + 1][i].DeltaMiss * Sheet[j + 1][i].Weights[LearningIndexInLayear];
                    Sheet[j][LearningIndexInLayear].LearnNeiron(LearningSigma,Alfa,ref Sheet,ref Input);
                }
            }
            for (int j = Sheet.Length - 1; j > -1; j--)
            {
                for (int LearningIndexInLayear = 0; LearningIndexInLayear < Sheet[j].Count; LearningIndexInLayear++)
                {
                    Sheet[j][LearningIndexInLayear].ConfirmWeights();
                }
            }
        }
        public Double[] LearnOutNeironInputAmbi(Double[] Sigma)//Out Sigmas
        {
            Double[] RealInput = new Double[Input.Length];
            for (int index = 0; index < Sigma.Length; index++)
                Sheet[Sheet.Length - 1][index].LearnNeironInput(Sigma[index], Alfa, ref Sheet);
            for (int j = Sheet.Length - 2; j > -1; j--)
            {
                for (int LearningIndexInLayear = 0; LearningIndexInLayear < Sheet[j].Count; LearningIndexInLayear++)
                {
                    Double LearningSigma = 0;
                    for (int i = 0; i < Sheet[j + 1].Count; i++)
                        LearningSigma += Sheet[j + 1][i].DeltaMiss * Sheet[j + 1][i].Weights[LearningIndexInLayear];
                    Sheet[j][LearningIndexInLayear].LearnNeiron(LearningSigma, Alfa, ref Sheet, ref Input);
                }
            }
            for (int LearningIndexInLayear = 0; LearningIndexInLayear < Input.Length; LearningIndexInLayear++)
            {
                Double LearningSigma = 0;
                for (int i = 0; i < Sheet[0].Count; i++)
                    LearningSigma += Sheet[0][i].DeltaMiss * Sheet[0][i].Weights[LearningIndexInLayear];
                {
                    Double DeltaMiss = LearningSigma * Neiron.DActivationFunction(Input[LearningIndexInLayear]);

                    RealInput[LearningIndexInLayear] =  Alfa * DeltaMiss * Input[LearningIndexInLayear];
                }
            }
            return RealInput;
        }
        public void Learn(Double[] input, Double[] output)
        {
            GetAnswer(input);
            LearnOutNeironInput(output);
        }

        //////
        public void BinariSave(string FileName)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.OpenOrCreate)))
            {
                // записываем в файл значение каждого поля структуры
                writer.Write(InputWeight);
                writer.Write(Sheet.Length);
                foreach (List<Neiron> lsn in Sheet)
                {
                    writer.Write(lsn.Count);
                    for (int i = 0; i < lsn.Count; i++)
                    {
                        writer.Write(lsn[i].Weights.Count);
                        for (int j = 0; j < lsn[i].Weights.Count; j++)
                        {
                            writer.Write(lsn[i].Weights[j]);
                        }
                    }
                }
            }

        }
        public string print()
        {
            string s = "";
            int neironNum = 1;
            foreach (List<Neiron> lsn in Sheet)
            {
                for (int i = 0; i < lsn.Count; i++)
                {
                    s += Environment.NewLine;
                    s += neironNum.ToString() + "->";
                    neironNum++;
                    for (int j = 0; j < lsn[i].Weights.Count; j++)
                    {
                        s += " " + Math.Round(lsn[i].Weights[j], 6).ToString();
                    }
                }
            }
            return s;
        }
        public void BinaryLoad(string FileName)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open)))
            {
                // пока не достигнут конец файла
                // считываем каждое значение из файла
                int InpW = reader.ReadInt32();
                InputWeight = InpW;
                int Length = reader.ReadInt32();
                Sheet = new List<Neiron>[Length];
                for (int z = 0; z < Length; z++)
                {
                    Sheet[z] = new List<Neiron>();
                    int Length1 = reader.ReadInt32();
                    for (int i = 0; i < Length1; i++)
                    {
                        Neiron n = new Neiron(z);

                        Sheet[z].Add(n);
                        int Length2 = reader.ReadInt32();
                        n.Weights = new List<Double>();
                        for (int j = 0; j < Length2; j++)
                        {
                            Double w = reader.ReadDouble();
                            n.Weights.Add(w);
                        }
                    }
                }

            }
        }
    }
}
