using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.ViewModels
{
    /// <summary>
    /// 修改密码模型
    /// </summary>
    public class ChangePwdViewModel
    {
        [Required(ErrorMessage = "密码不能为空")]
        [Display(Name = "旧密码")]
        [DataType(DataType.Password)]
       public string OriginalPwd { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [Display(Name = "新密码")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "{0}长度不能小于{2}大于{1}位", MinimumLength = 6)]
        public string NewPassWord { get; set; }

        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("NewPassWord",ErrorMessage = "密码输入不一致")]
        public string ConfirmNewPassWord { get; set; }
    }
}