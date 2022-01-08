using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dotcom.ViewModels
{
    public class NewProductModel
    {
        [Required(ErrorMessage = "Не указано название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано описание"),
            MinLength(50, ErrorMessage = "Длина сообщения меньше 50 символов"),
            DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Не указана цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Не указано количество")]
        public decimal Quantity { get; set; }
    }
}
