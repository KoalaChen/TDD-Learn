using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomShare
{
    /// <summary>
    /// 訂單服務
    /// </summary>
    public class OrderService
    {

        /// <summary>
        /// 建立訂單服務的執行個體
        /// </summary>
        public OrderService()
        {

        }
        /// <summary>
        /// 加總
        /// </summary>
        /// <param name="getValueFunc">取得欄位值，可指定需要加總的欄位</param>
        /// <param name="sources">計算來源</param>
        /// <param name="groupCount">幾筆資料為一組</param>
        /// <exception cref="ArgumentNullException">getValueFunc為null</exception>
        /// <exception cref="ArgumentNullException">sources為null</exception>
        /// <exception cref="ArgumentException">groupCount小於1</exception>
        /// <exception cref="ArgumentException">sources陣列中有一個null值</exception>
        /// <returns>計算結果</returns>

        public IEnumerable<int> Sum(
            Func<Order, int> getValueFunc, 
            IEnumerable<Order> sources, 
            int groupCount)
        {
            if (getValueFunc == null)
                throw new ArgumentNullException(nameof(getValueFunc));
            if (sources == null)
                throw new ArgumentNullException(nameof(sources));
            if (groupCount < 1)
                throw new ArgumentException($"{nameof(groupCount)}的值{groupCount}必須大於等於1");
            if(sources.Any(a=>a == null))
                throw new ArgumentException($"{nameof(sources)}陣列中有null值");

            var resultList = new List<int>();
            IEnumerable<Order> partialArray = null;
            var groupCountTimes = 0;

            do
            {
                //分筆取出計算
                partialArray = sources.Skip(groupCountTimes * groupCount).Take(groupCount);
                //計次
                groupCountTimes++;
                //若有
                if(partialArray.Any())
                {
                    var partialResult = partialArray.Sum(getValueFunc);
                    resultList.Add(partialResult);
                }
            }
            while (partialArray.Any());

            return resultList;
        }
    }
}
