using System.Collections.Generic;

namespace SupplyChain.Models
{
    public class ChartData
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
        public List<ChartDataset> Datasets { get; set; }
    }
}
