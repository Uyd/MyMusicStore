using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreEntity.UserAndRole;

namespace MusicStoreEntity
{
    /// <summary>
    /// 用户下单详细信息
    /// </summary>
    public class PersonOrder
    {
        public Guid ID { get; set; }
        public virtual Person Person { get; set; }
        public string AddressPerson { get; set; }
        public string OrderAddress { get; set; }
        public string OrderPhone { get; set; }

        public PersonOrder()
        {
            ID=Guid.NewGuid();
        }
    }
}
