using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeironLib
{
    class ConvolutionalNeironSheet
    {
        List<NeironSheet>[] ConvolutionSheet;
        public void InitiateNeironSheet(int[] countNeiron, int _InputWaight)//First layer -> Input,Last->Output
        {
            ConvolutionSheet = new List<NeironSheet>[10];
            //InputWeight = _InputWaight;
            //Sheet = new List<Neiron>[countNeiron.Length];
            //for (int i = 0; i < countNeiron.Length; i++)
            //{
            //    Sheet[i] = new List<Neiron>();
            //    for (int j = 0; j < countNeiron[i]; j++)
            //    {
            //        Sheet[i].Add(new Neiron(i));
            //    }
            //}
            //Random r = new Random();
            //foreach (Neiron n in Sheet[0])
            //{
            //    for (int i = 0; i < _InputWaight; i++)
            //        n.Weights.Add(Convert.ToDouble(r.NextDouble()) - Convert.ToDouble(0.5));
            //}
            //for (int i = 1; i < countNeiron.Length; i++)
            //{
            //    foreach (Neiron n in Sheet[i])
            //    {
            //        for (int j = 0; j < Sheet[i - 1].Count; j++)
            //            n.Weights.Add(Convert.ToDouble(r.NextDouble()) - Convert.ToDouble(0.5));
            //    }
            //}
        }
    }
}
