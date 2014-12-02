using System;
using System.Collections.Generic;
using System.Linq;

namespace ALPACA
{
    public class ListManager
    {
        public List<string> addressList { get; set; }

        public List<string> MergeList(List<string> inputList)
        {
            return MergeList(addressList, inputList);
        }
        public List<string> MergeList(List<string> inputList1, List<string> inputList2)
        {
            inputList1.AddRange(inputList2);
            inputList1 = inputList1.Distinct().ToList();
            inputList1.Sort();
            return inputList1;
        }
        public List<string> RemoveList(List<string> inputList)
        {
            return RemoveList(addressList, inputList);
        }
        public List<string> RemoveList(List<string> finalList, List<string> listToRemove)
        {
            finalList = finalList.Except(listToRemove).ToList();
            finalList.Sort();
            return finalList;
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
