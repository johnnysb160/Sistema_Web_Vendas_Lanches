using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Compras.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        public List<PedidoDetalhe> PedidoItens { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Sobrenome")]
        [Display(Name = "Sobrenome")]
        [StringLength(50)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o Endereço")]
        [Display(Name = "Endereço")]
        [StringLength(50)]
        public string Endereco1 { get; set; }

        [Required(ErrorMessage = "Informe o Complemento do Endereço")]
        [Display(Name = "Complemento do Endereço")]
        [StringLength(50)]
        public string Endereco2 { get; set; }

        [Required(ErrorMessage = "Informe o Cep")]
        [Display(Name = "CEP")]
        [StringLength(10, MinimumLength =8)]
        public string Cep { get; set; }

        [StringLength(10)]
        public string Estado { get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o Telefone")]
        [Display(Name = "Telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o Email")]
        [Display(Name = "Email")]
        [StringLength(80)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",
            ErrorMessage = "O email não possui um formato correto")]
        public string Email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PedidoTotal { get; set; }

        [Display(Name = "Data/Hora do Recebimento do Pedido")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        [Display(Name = "Data/Hora da Entrega do Pedido")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PedidoEntregue { get; set; }
    }
}
