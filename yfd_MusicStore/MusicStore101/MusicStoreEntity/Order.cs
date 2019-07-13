using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreEntity.UserAndRole;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreEntity
{
   public class Order
    {
        [ScaffoldColumn(false)]
        public Guid ID { get; set; }

        [ScaffoldColumn(false)]
        public DateTime OrderDateTime { get; set; }//订单的时间

        [ScaffoldColumn(false)]
        public virtual Person Person { get; set; }//所属用户

        [Required(ErrorMessage = "收件人不能为空")]
        [Display(Name = "收件人")]
        public string AddressPerson { get; set; }//收件人

        [Required(ErrorMessage = "收货地址不能为空")]
        [Display(Name = "收货地址")]
        public string Address { get; set; }//收件人地址

        [Required(ErrorMessage = "手机号不能为空")]
        [Display(Name = "手机")]
        [StringLength(11, ErrorMessage = "{0}长度不能小于{2}大于{1}位", MinimumLength = 6)]
        public string MobilNumber { get; set; }//收件人的手机

        [ScaffoldColumn(false)]
        public decimal TotalPrice { get; set; }//总价

        //支付宝和微信都定义有自己特有的支付字段
        [ScaffoldColumn(false)]
        public string TradeNo { get;set; }//支付流水号

        [ScaffoldColumn(false)]
        public bool PaySuccess { get; set; }//是否支付成功

        [ScaffoldColumn(false)]
        public virtual  EnumOrderStatus EnumOrderStatus { get; set; }//订单状态
        //签名字段 验证是否是自己网站创建的订单

        //购买专辑明细,ef中的包含关系
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            ID = Guid.NewGuid();
            OrderDateTime=DateTime.Now;
            TradeNo = OrderDateTime.ToString("yyyyMMddhhmmssffff");
        }
    }
}
