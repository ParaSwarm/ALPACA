using System;
using System.Collections.Generic;
using System.Linq;

namespace ALPACA
{
    public class ListManager
    {
        private List<string> addressList;

        public void MergeList(List<string> inputList)
        {
            addressList.AddRange(inputList);
            addressList.Sort();
        }
        public void MergeList(List<string> inputList1, List<string> inputList2)
        {
            inputList1.AddRange(inputList2);
            inputList1.Sort();
            ExportList(inputList1); 
        }
        public void RemoveList(List<string> inputList)
        {
            addressList = addressList.Except(inputList).ToList();
            addressList.Sort();
        }
        public void ExportList()
        {
            ExportList(addressList); 
        }
        public void ExportList(List<string> outList)
        {
            //TODO export code
        }
    }
}
