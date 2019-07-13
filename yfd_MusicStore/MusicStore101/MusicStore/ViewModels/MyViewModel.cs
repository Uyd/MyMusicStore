using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MusicStoreEntity;

namespace MusicStore.ViewModels
{
    public class MyViewModel
    {
        //收货信息模块
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

        //个人信息模块
        [Display(Name = "头像")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Avada { get; set; }

        [Display(Name="姓名")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public bool Sex { get; set; }

        [Display(Name = "生日")]
        public string Birthday { get; set; }

        [Display(Name = "家庭住址")]
        public string HomeAddress { get; set; }
                
    }
}