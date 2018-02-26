using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlowWebApi.Dtos
{
    public class FlowCreation
    {
        [Display(Name ="产品名称")]
        [Required(ErrorMessage ="{0}是必填的")]
        [StringLength(10,MinimumLength =2,ErrorMessage ="{0}的长度必须不小于{2}，大于等于{1}")]
        public string Name { get; set; }

        [Display(Name ="价格")]
        [Required(ErrorMessage ="{0}价格必填")]
        [Range(0,100,ErrorMessage ="{0}的值必须要在{1}和{2}之间")]
        public decimal Price { get; set; }

        [Display(Name = "描述")]
        [MaxLength(100, ErrorMessage = "{0}的长度不能大于{1}")]
        public string des { set; get; }
    }
}
