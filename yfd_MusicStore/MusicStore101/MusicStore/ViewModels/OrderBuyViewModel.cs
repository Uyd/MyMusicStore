using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.ViewModels
{
    public class OrderBuyViewModel
    {
        [Required(ErrorMessage = "收件人不能为空")]
        [Display(Name = "收件人")]
        public string AddressPerson { get; set; }

        [Required(ErrorMessage = "收货地址不能为空")]
        [Display(Name = "收货地址")]
        public string Address { get; set; }

        [Required(ErrorMessage = "手机号不能为空")]
        [Display(Name = "手机")]
        [StringLength(11, ErrorMessage = "{0}长度不能小于{2}大于{1}位", MinimumLength = 6)]
        public string MobilNumber { get; set; }

        public LoginUserSessionModel Login { get; set; }
        public ShoppingCartViewModel ShoppingCart { get; set; }
    }
}