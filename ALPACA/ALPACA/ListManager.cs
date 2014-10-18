using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPACA
{
    class ListManager
    {
        private List<String> addressList;

        public void mergeList(List<String> inputList)
        {
            addressList.AddRange(inputList);
            addressList.Sort();
        }
        public void mergeList(List<String> inputList1, List<String> inputList2)
        {
            inputList1.AddRange(inputList2);
            inputList1.Sort();
            exportList(inputList1); 
        }
        public void removeList(List<String> inputList)
        {
            addressList = addressList.Except(inputList).ToList();
            addressList.Sort();
        }
        public void exportList()
        {
            exportList(addressList); 
        }
        public void exportList(List<String> outList)
        {
            //TODO export code
        }
    }
}
